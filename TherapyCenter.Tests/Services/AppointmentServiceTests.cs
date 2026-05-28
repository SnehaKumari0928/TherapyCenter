using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TherapyCenter2.DTOs.Appointment;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Implementations;

namespace TherapyCenter.Tests.Services
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IAppointmentRepository>
            _appointmentRepoMock;

        private readonly Mock<ISlotRepository>
            _slotRepoMock;

        private readonly Mock<IPatientRepository>
            _patientRepoMock;

        private readonly Mock<IDoctorRepository>
            _doctorRepoMock;

        private readonly Mock<ITherapyRepository>
            _therapyRepoMock;

        private readonly Mock<IHttpContextAccessor>
            _httpContextAccessorMock;

        private readonly AppointmentService
            _service;

        public AppointmentServiceTests()
        {
            _appointmentRepoMock =
                new Mock<IAppointmentRepository>();

            _slotRepoMock =
                new Mock<ISlotRepository>();

            _patientRepoMock =
                new Mock<IPatientRepository>();

            _doctorRepoMock =
                new Mock<IDoctorRepository>();

            _therapyRepoMock =
                new Mock<ITherapyRepository>();

            _httpContextAccessorMock =
                new Mock<IHttpContextAccessor>();

            // Mock Logged-In User
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    "1")
            };

            var identity =
                new ClaimsIdentity(claims);

            var principal =
                new ClaimsPrincipal(identity);

            var context =
                new DefaultHttpContext();

            context.User = principal;

            _httpContextAccessorMock
                .Setup(x => x.HttpContext)
                .Returns(context);

            _service =
                new AppointmentService(
                    _appointmentRepoMock.Object,
                    _slotRepoMock.Object,
                    _patientRepoMock.Object,
                    _httpContextAccessorMock.Object,
                    _therapyRepoMock.Object,
                    _doctorRepoMock.Object
                );
        }

        [Fact]
        public async Task
            Create_Should_Create_Appointment_When_Slot_Available()
        {
            var dto =
                new AppointmentCreateDto
                {
                    SlotId = 1,
                    DoctorId = 1,
                    TherapyId = 1,
                    ReceptionistId = 1
                };

            var slot = new Slot
            {
                SlotId = 1,
                Date =
                    DateOnly.FromDateTime(
                        DateTime.Today),
                StartTime =
                    TimeOnly.FromTimeSpan(
                        TimeSpan.FromHours(10)),
                EndTime =
                    TimeOnly.FromTimeSpan(
                        TimeSpan.FromHours(11)),
                IsBooked = false
            };

            var patient =
                new Patient
                {
                    PatientId = 1,
                    UserId = 1
                };

            var created =
                new Appointment
                {
                    AppointmentId = 1,
                    PatientId = 1,
                    DoctorId = 1,
                    TherapyId = 1,
                    AppointmentDate =
                        slot.Date,
                    StartTime =
                        slot.StartTime,
                    EndTime =
                        slot.EndTime,
                    Status = "Scheduled"
                };

            _slotRepoMock
                .Setup(x =>
                    x.GetByIdAsync(dto.SlotId))
                .ReturnsAsync(slot);

            _patientRepoMock
                .Setup(x =>
                    x.GetByUserIdAsync(1))
                .ReturnsAsync(patient);

            _appointmentRepoMock
                .Setup(x =>
                    x.AddAsync(
                        It.IsAny<Appointment>()))
                .ReturnsAsync(created);

            var result =
                await _service
                .CreateAsync(dto);

            result.Should().NotBeNull();

            result.Status.Should()
                .Be("Scheduled");

            _slotRepoMock.Verify(
                x => x.UpdateAsync(
                    It.Is<Slot>(
                        s => s.IsBooked)),
                Times.Once);
        }

        [Fact]
        public async Task
            Create_Should_Throw_When_Slot_Not_Found()
        {
            _slotRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    (Slot?)null);

            var dto =
                new AppointmentCreateDto
                {
                    SlotId = 1
                };

            Func<Task> act =
                async () =>
                    await _service
                    .CreateAsync(dto);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Slot not found");
        }

        [Fact]
        public async Task
            Create_Should_Throw_When_Slot_Already_Booked()
        {
            var slot = new Slot
            {
                SlotId = 1,
                IsBooked = true
            };

            _slotRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(slot);

            var dto =
                new AppointmentCreateDto
                {
                    SlotId = 1
                };

            Func<Task> act =
                async () =>
                    await _service
                    .CreateAsync(dto);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Slot already booked");
        }

        [Fact]
        public async Task
            GetAll_Should_Return_List()
        {
            var list =
                new List<Appointment>
                {
                    new Appointment
                    {
                        AppointmentId = 1
                    },
                    new Appointment
                    {
                        AppointmentId = 2
                    }
                };

            _appointmentRepoMock
                .Setup(x =>
                    x.GetAllAsync())
                .ReturnsAsync(list);

            var result =
                await _service
                .GetAllAsync();

            result.Should()
                .HaveCount(2);
        }

        [Fact]
        public async Task
            GetById_Should_Return_When_Exists()
        {
            var appointment =
                new Appointment
                {
                    AppointmentId = 1
                };

            _appointmentRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    appointment);

            var result =
                await _service
                .GetByIdAsync(1);

            result.AppointmentId
                .Should().Be(1);
        }

        [Fact]
        public async Task
            GetById_Should_Throw_When_Not_Found()
        {
            _appointmentRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    (Appointment?)null);

            Func<Task> act =
                async () =>
                    await _service
                    .GetByIdAsync(1);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Appointment not found");
        }

        [Fact]
        public async Task
            Complete_Should_Set_Status_Completed()
        {
            var appointment =
                new Appointment
                {
                    AppointmentId = 1
                };

            _appointmentRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    appointment);

            await _service
                .CompleteAsync(1);

            appointment.Status
                .Should()
                .Be("Completed");
        }

        [Fact]
        public async Task
            Cancel_Should_Set_Status_Cancelled()
        {
            var appointment =
                new Appointment
                {
                    AppointmentId = 1
                };

            _appointmentRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    appointment);

            await _service
                .CancelAsync(1);

            appointment.Status
                .Should()
                .Be("Cancelled");
        }
    }
}