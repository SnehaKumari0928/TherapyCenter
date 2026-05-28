using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TherapyCenter2.DTOs.Slot;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Implementations;

namespace TherapyCenter.Tests.Services
{
    public class SlotServiceTests
    {
        private readonly Mock<ISlotRepository>
            _slotRepoMock;

        private readonly Mock<IDoctorRepository>
            _doctorRepoMock;

        private readonly Mock<IAppointmentRepository>
            _appointmentRepoMock;

        private readonly Mock<IHttpContextAccessor>
            _httpContextAccessorMock;

        private readonly SlotService
            _service;

        public SlotServiceTests()
        {
            _slotRepoMock =
                new Mock<ISlotRepository>();

            _doctorRepoMock =
                new Mock<IDoctorRepository>();

            _appointmentRepoMock =
                new Mock<IAppointmentRepository>();

            _httpContextAccessorMock =
                new Mock<IHttpContextAccessor>();

            SetupLoggedInDoctor();

            _service =
                new SlotService(
                    _slotRepoMock.Object,
                    _doctorRepoMock.Object,
                    _appointmentRepoMock.Object,
                    _httpContextAccessorMock.Object);
        }

        private void SetupLoggedInDoctor()
        {
            var claims =
                new List<Claim>
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        "1")
                };

            var identity =
                new ClaimsIdentity(
                    claims,
                    "TestAuth");

            var user =
                new ClaimsPrincipal(
                    identity);

            var context =
                new DefaultHttpContext
                {
                    User = user
                };

            _httpContextAccessorMock
                .Setup(x => x.HttpContext)
                .Returns(context);

            _doctorRepoMock
                .Setup(x =>
                    x.GetByUserIdAsync(1))
                .ReturnsAsync(
                    new Doctor
                    {
                        DoctorId = 1,
                        UserId = 1,
                        AvailableDays =
                            "Mon-Fri",
                        StartTime =
                            new TimeOnly(9, 0),
                        EndTime =
                            new TimeOnly(17, 0)
                    });
        }

        [Fact]
        public async Task
            CreateSlot_Should_Create_When_Valid()
        {
            var dto =
                new CreateSlotDto
                {
                    Date =
                        DateOnly
                        .FromDateTime(
                            DateTime.Today),

                    StartTime =
                        new TimeOnly(
                            10, 0),

                    EndTime =
                        new TimeOnly(
                            11, 0)
                };

            _slotRepoMock
                .Setup(x =>
                    x.GetByDoctorAndDateAsync(
                        1,
                        dto.Date))
                .ReturnsAsync(
                    new List<Slot>());

            _slotRepoMock
                .Setup(x =>
                    x.AddAsync(
                        It.IsAny<Slot>()))
                .ReturnsAsync(
                    (Slot s) => s);

            var result =
                await _service
                .CreateSlotAsync(dto);

            result.Should()
                .NotBeNull();

            result.DoctorId
                .Should().Be(1);
        }

        [Fact]
        public async Task
            CreateSlot_Should_Throw_When_Time_Invalid()
        {
            var dto =
                new CreateSlotDto
                {
                    StartTime =
                        new TimeOnly(
                            11, 0),

                    EndTime =
                        new TimeOnly(
                            10, 0)
                };

            Func<Task> act =
                async () =>
                    await _service
                    .CreateSlotAsync(
                        dto);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Invalid time");
        }

        [Fact]
        public async Task
            CreateSlot_Should_Throw_When_Overlap()
        {
            var dto =
                new CreateSlotDto
                {
                    Date =
                        DateOnly
                        .FromDateTime(
                            DateTime.Today),

                    StartTime =
                        new TimeOnly(
                            10, 30),

                    EndTime =
                        new TimeOnly(
                            11, 30)
                };

            var existing =
                new List<Slot>
                {
                    new Slot
                    {
                        StartTime =
                            new TimeOnly(
                                10, 0),

                        EndTime =
                            new TimeOnly(
                                11, 0)
                    }
                };

            _slotRepoMock
                .Setup(x =>
                    x.GetByDoctorAndDateAsync(
                        1,
                        dto.Date))
                .ReturnsAsync(
                    existing);

            Func<Task> act =
                async () =>
                    await _service
                    .CreateSlotAsync(
                        dto);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Slots are overlapped");
        }

        [Fact]
        public async Task
            GetAllSlots_Should_Return_List()
        {
            _slotRepoMock
                .Setup(x =>
                    x.GetAllAsync())
                .ReturnsAsync(
                    new List<Slot>
                    {
                        new Slot(),
                        new Slot()
                    });

            var result =
                await _service
                .GetAllSlotsAsync();

            result.Should()
                .HaveCount(2);
        }

        [Fact]
        public async Task
            GetSlotById_Should_Return_When_Exists()
        {
            _slotRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    new Slot
                    {
                        SlotId = 1
                    });

            var result =
                await _service
                .GetSlotByIdAsync(1);

            result.SlotId
                .Should().Be(1);
        }

        [Fact]
        public async Task
            GetSlotById_Should_Throw_When_Not_Found()
        {
            _slotRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    (Slot?)null);

            Func<Task> act =
                async () =>
                    await _service
                    .GetSlotByIdAsync(
                        1);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Slot not found");
        }

        [Fact]
        public async Task
            DeleteSlot_Should_Delete_When_Exists()
        {
            var slot =
                new Slot
                {
                    SlotId = 1
                };

            _slotRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(slot);

            _slotRepoMock
                .Setup(x =>
                    x.DeleteAsync(slot))
                .Returns(
                    Task.CompletedTask);

            await _service
                .DeleteSlotAsync(1);

            _slotRepoMock
                .Verify(
                    x => x.DeleteAsync(slot),
                    Times.Once);
        }

        [Fact]
        public async Task
            DeleteSlot_Should_Throw_When_Not_Found()
        {
            _slotRepoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    (Slot?)null);

            Func<Task> act =
                async () =>
                    await _service
                    .DeleteSlotAsync(1);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Slot not found");
        }
    }
}