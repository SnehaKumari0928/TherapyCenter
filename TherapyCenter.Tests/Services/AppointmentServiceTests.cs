using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Implementations;
using TherapyCenter2.Models;
using TherapyCenter2.DTOs.Appointment;
using System.Text;

namespace TherapyCenter.Tests.Services
{
    public class AppointmentServiceTests
    {

        private readonly Mock<IAppointmentRepository> _appointmentRepoMock;
        private readonly Mock<ISlotRepository> _slotRepoMock;
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            _appointmentRepoMock = new Mock<IAppointmentRepository>();
            _slotRepoMock = new Mock<ISlotRepository>();

            _service = new AppointmentService(
                _appointmentRepoMock.Object,
                _slotRepoMock.Object
            );
        }


        [Fact]
        public async Task Create_Should_Create_Appointment_When_Slot_Available()
        {
            var dto = new AppointmentCreateDto
            {
                SlotId = 1,
                PatientId = 1,
                DoctorId = 1,
                TherapyId = 1,
                ReceptionistId = 1
            };

            var slot = new Slot
            {
                SlotId = 1,
                Date = DateOnly.FromDateTime(DateTime.Today),
                StartTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10)), 
                EndTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(11)),   
                IsBooked = false
            };

            var created = new Appointment
            {
                AppointmentId = 1,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                TherapyId = dto.TherapyId,
                AppointmentDate = slot.Date,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime,
                Status = "Scheduled"
            };

            _slotRepoMock.Setup(x => x.GetByIdAsync(dto.SlotId))
                .ReturnsAsync(slot);

            _appointmentRepoMock.Setup(x => x.AddAsync(It.IsAny<Appointment>()))
                .ReturnsAsync(created);

            var result = await _service.CreateAsync(dto);

            result.Should().NotBeNull();
            result.Status.Should().Be("Scheduled");

            _slotRepoMock.Verify(x => x.UpdateAsync(It.Is<Slot>(s => s.IsBooked)), Times.Once);
        }

        [Fact]
        public async Task Create_Should_Throw_When_Slot_Not_Found()
        {
            _slotRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Slot?)null);

            var dto = new AppointmentCreateDto { SlotId = 1 };

            Func<Task> act = async () => await _service.CreateAsync(dto);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Slot not found");
        }

        [Fact]
        public async Task Create_Should_Throw_When_Slot_Already_Booked()
        {
            var slot = new Slot { SlotId = 1, IsBooked = true };

            _slotRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(slot);

            var dto = new AppointmentCreateDto { SlotId = 1 };

            Func<Task> act = async () => await _service.CreateAsync(dto);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Slot already booked");
        }


        [Fact]
        public async Task GetAll_Should_Return_List()
        {
            var list = new List<Appointment>
            {
                new Appointment { AppointmentId = 1 },
                new Appointment { AppointmentId = 2 }
            };

            _appointmentRepoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(list);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetById_Should_Return_When_Exists()
        {
            var appointment = new Appointment { AppointmentId = 1 };

            _appointmentRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(appointment);

            var result = await _service.GetByIdAsync(1);

            result.AppointmentId.Should().Be(1);
        }

        [Fact]
        public async Task GetById_Should_Throw_When_Not_Found()
        {
            _appointmentRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Appointment?)null);

            Func<Task> act = async () => await _service.GetByIdAsync(1);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Appointment not found");
        }


        [Fact]
        public async Task Update_Should_Update_Notes()
        {
            var appointment = new Appointment { AppointmentId = 1 };

            var dto = new AppointmentUpdateDto
            {
                Notes = "Updated notes"
            };

            _appointmentRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(appointment);

            _appointmentRepoMock.Setup(x => x.UpdateAsync(It.IsAny<Appointment>()))
                .Returns(Task.CompletedTask);

            var result = await _service.UpdateAsync(1, dto);

            result.Notes.Should().Be("Updated notes");
        }

        [Fact]
        public async Task Update_Should_Throw_When_Not_Found()
        {
            _appointmentRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Appointment?)null);

            var dto = new AppointmentUpdateDto();

            Func<Task> act = async () => await _service.UpdateAsync(1, dto);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Appointment not found");
        }


        [Fact]
        public async Task Delete_Should_Delete_When_Exists()
        {
            var appointment = new Appointment { AppointmentId = 1 };

            _appointmentRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(appointment);

            _appointmentRepoMock.Setup(x => x.DeleteAsync(appointment))
                .Returns(Task.CompletedTask);

            await _service.DeleteAsync(1);

            _appointmentRepoMock.Verify(x => x.DeleteAsync(appointment), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Throw_When_Not_Found()
        {
            _appointmentRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Appointment?)null);

            Func<Task> act = async () => await _service.DeleteAsync(1);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Appointment not found");
        }


        [Fact]
        public async Task Complete_Should_Set_Status_Completed()
        {
            var appointment = new Appointment { AppointmentId = 1 };

            _appointmentRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(appointment);

            _appointmentRepoMock.Setup(x => x.UpdateAsync(appointment))
                .Returns(Task.CompletedTask);

            await _service.CompleteAsync(1);

            appointment.Status.Should().Be("Completed");
        }

        [Fact]
        public async Task Cancel_Should_Set_Status_Cancelled()
        {
            var appointment = new Appointment { AppointmentId = 1 };

            _appointmentRepoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(appointment);

            _appointmentRepoMock.Setup(x => x.UpdateAsync(appointment))
                .Returns(Task.CompletedTask);

            await _service.CancelAsync(1);

            appointment.Status.Should().Be("Cancelled");
        }
    }
}
