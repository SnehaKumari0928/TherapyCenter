using System.Security.Claims;
using TherapyCenter2.DTOs.Slot;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Services.Implementations
{
    public class SlotService : ISlotService
    {
        private readonly ISlotRepository _slotRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SlotService(
            ISlotRepository slotRepository,
            IDoctorRepository doctorRepository,
            IAppointmentRepository appointmentRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _slotRepository = slotRepository;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SlotResponseDto> CreateSlotAsync(CreateSlotDto dto)
        {
            var doctor = await GetLoggedInDoctorAsync();

            if (dto.StartTime >= dto.EndTime)
                throw new Exception("Invalid time");

            var existingSlots =
                await _slotRepository.GetByDoctorAndDateAsync(
                    doctor.DoctorId,
                    dto.Date);

            foreach (var s in existingSlots)
            {
                bool overlap =
                    dto.StartTime < s.EndTime &&
                    dto.EndTime > s.StartTime;

                if (overlap)
                    throw new Exception(
                        "Slots are overlapped");
            }

            var slot = new Slot
            {
                DoctorId = doctor.DoctorId,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsBooked = false
            };

            var created =
                await _slotRepository
                .AddAsync(slot);

            return MapSlotResponse(created);
        }
        public async Task CreateBulkSlotsAsync(CreateBulkSlotDto dto, int doctorId)
        {
            if (dto.DurationMinutes <= 0)
                throw new ArgumentException("Duration must be greater than 0");

            if (dto.StartTime >= dto.EndTime)
                throw new ArgumentException("StartTime must be less than EndTime");

            // 1️⃣ Fetch all existing slots in ONE DB call
            var existingSlots = await _slotRepository
                .GetByDoctorAndDateAsync(doctorId, dto.Date);

            // Convert to fast lookup set (for O(1) checks)
            var existingSet = existingSlots
                .Select(s => (s.StartTime, s.EndTime))
                .ToHashSet();

            var newSlots =
                new List<Slot>();

            var current =
                dto.StartTime;

            while (current < dto.EndTime)
            {
                var next =
                    current.AddMinutes(
                        dto.DurationMinutes);

                if (next > dto.EndTime)
                    break;

                if (!existingSet.Contains(
                    (current, next)))
                {
                    newSlots.Add(
                        new Slot
                        {
                            DoctorId =
                                doctor.DoctorId,
                            Date = dto.Date,
                            StartTime =
                                current,
                            EndTime = next,
                            IsBooked =
                                false
                        });
                }

                current = next;
            }

            if (newSlots.Any())
            {
                await _slotRepository.BulkInsertAsync(newSlots);
            }
        }

        public async Task<List<SlotResponseDto>>
            GetAllSlotsAsync()
        {
            var slots =
                await _slotRepository
                .GetAllAsync();

            return slots
                .Select(MapSlotResponse)
                .ToList();
        }

        public async Task<List<SlotResponseDto>>
            GetSlotsByDoctorAsync(
                DateOnly date)
        {
            var doctor =
                await GetLoggedInDoctorAsync();

            var slots =
                await _slotRepository
                .GetByDoctorAndDateAsync(
                    doctor.DoctorId,
                    date);

            return slots
                .Select(MapSlotResponse)
                .ToList();
        }

        public async Task<SlotResponseDto>
            GetSlotByIdAsync(int id)
        {
            var slot =
                await _slotRepository
                .GetByIdAsync(id);

            if (slot == null)
                throw new Exception(
                    "Slot not found");

            return MapSlotResponse(
                slot);
        }

        public async Task<SlotResponseDto>
            UpdateSlotAsync(
                int id,
                UpdateSlotDto dto)
        {
            var slot =
                await _slotRepository
                .GetByIdAsync(id);

            if (slot == null)
                throw new Exception(
                    "Slot not found");

            if (slot.IsBooked)
            {
                throw new Exception(
                    "Cannot update booked slot");
            }

            if (dto.StartTime >= dto.EndTime)
                throw new Exception(
                    "Invalid time");

            var existingSlots =
                await _slotRepository
                .GetByDoctorAndDateAsync(
                    slot.DoctorId,
                    dto.Date);

            foreach (var s in existingSlots)
            {
                if (s.SlotId == id)
                    continue;

                bool overlap =
                    dto.StartTime <
                        s.EndTime &&
                    dto.EndTime >
                        s.StartTime;

                if (overlap)
                {
                    throw new Exception(
                        "Slots are overlapped");
                }
            }

            slot.Date =
                dto.Date;
            slot.StartTime =
                dto.StartTime;
            slot.EndTime =
                dto.EndTime;

            await _slotRepository
                .UpdateAsync(slot);

            return MapSlotResponse(
                slot);
        }

        public async Task DeleteSlotAsync(
            int id)
        {
            var slot =
                await _slotRepository
                .GetByIdAsync(id);

            if (slot == null)
                throw new Exception(
                    "Slot not found");

            await _slotRepository
                .DeleteAsync(slot);
        }

        public async Task<List<DoctorSlotDto>>
            GetGeneratedSlotsByDoctorAsync(
                DateOnly date)
        {
            var doctor =
                await GetLoggedInDoctorAsync();

            if (doctor.AvailableDays ==
                    null ||
                !IsDoctorAvailableOnDate(
                    doctor.AvailableDays,
                    date))
            {
                return new List<
                    DoctorSlotDto>();
            }

            var slots =
                new List<
                    DoctorSlotDto>();

            var start =
                doctor.StartTime!.Value;

            var end =
                doctor.EndTime!.Value;

            while (start < end)
            {
                var next =
                    start.AddMinutes(30);

                slots.Add(
                    new DoctorSlotDto
                    {
                        StartTime =
                            start,
                        EndTime = next,
                        Status =
                            "Available"
                    });

                start = next;
            }

            var appointments =
                await _appointmentRepository
                .GetByDoctorAndDateAsync(
                    doctor.DoctorId,
                    date);

            foreach (var slot in slots)
            {
                bool isBooked =
                    appointments.Any(a =>
                        a.StartTime ==
                            slot.StartTime &&
                        a.EndTime ==
                            slot.EndTime);

                if (isBooked)
                {
                    slot.Status =
                        "Booked";
                }
            }

            return slots;
        }

        private bool IsDoctorAvailableOnDate(string availableDays, DateOnly date)
        {
            var day =
                date.DayOfWeek;

            if (availableDays.Contains("-"))
            {
                var parts =
                    availableDays
                    .Split('-');

                var start =
                    ParseDay(
                        parts[0]);

                var end =
                    ParseDay(
                        parts[1]);

                return day >= start && day <= end;
            }

            return ParseDay(
                availableDays)
                == day;
        }

        private DayOfWeek ParseDay(
            string day)
        {
            return day
                .Trim()
                .ToLower() switch
            {
                "mon" =>
                    DayOfWeek.Monday,

                "tue" =>
                    DayOfWeek.Tuesday,

                "wed" =>
                    DayOfWeek.Wednesday,

                "thu" =>
                    DayOfWeek.Thursday,

                "fri" =>
                    DayOfWeek.Friday,

                "sat" =>
                    DayOfWeek.Saturday,

                "sun" =>
                    DayOfWeek.Sunday,

                _ => throw new Exception(
                    "Invalid day")
            };
        }

        private static
            SlotResponseDto
            MapSlotResponse(
                Slot slot)
        {
            return new SlotResponseDto
            {
                SlotId =
                    slot.SlotId,
                DoctorId =
                    slot.DoctorId,
                Date =
                    slot.Date,
                StartTime =
                    slot.StartTime,
                EndTime =
                    slot.EndTime,
                IsBooked =
                    slot.IsBooked
            };
        }
    }
}