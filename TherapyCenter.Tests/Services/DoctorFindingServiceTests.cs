using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TherapyCenter2.DTOs.DoctorFinding;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Implementations;

namespace TherapyCenter.Tests.Services
{
    public class DoctorFindingServiceTests
    {
        private readonly Mock<IDoctorFindingRepository>
            _repoMock;

        private readonly Mock<IPatientRepository>
            _patientRepoMock;

        private readonly Mock<IHttpContextAccessor>
            _httpContextAccessorMock;

        private readonly DoctorFindingService
            _service;

        public DoctorFindingServiceTests()
        {
            _repoMock =
                new Mock<IDoctorFindingRepository>();

            _patientRepoMock =
                new Mock<IPatientRepository>();

            _httpContextAccessorMock =
                new Mock<IHttpContextAccessor>();

            // Mock logged-in user
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
                new DoctorFindingService(
                    _repoMock.Object,
                    _patientRepoMock.Object,
                    _httpContextAccessorMock.Object
                );
        }

        [Fact]
        public async Task
            Create_Should_Create_Finding()
        {
            var dto =
                new CreateDoctorFindingDto
                {
                    AppointmentId = 1,
                    Observations = "Obs",
                    Recommendations = "Rec"
                };

            var created =
                new DoctorFinding
                {
                    FindingId = 1,
                    AppointmentId =
                        dto.AppointmentId,
                    Observations =
                        dto.Observations,
                    Recommendations =
                        dto.Recommendations,
                    CreatedAt =
                        DateTime.UtcNow
                };

            _repoMock
                .Setup(x =>
                    x.AddAsync(
                        It.IsAny<DoctorFinding>()))
                .ReturnsAsync(created);

            var result =
                await _service
                .CreateAsync(dto);

            result.Should()
                .NotBeNull();

            result.AppointmentId
                .Should()
                .Be(dto.AppointmentId);
        }

        [Fact]
        public async Task
            GetAll_Should_Return_List()
        {
            _repoMock
                .Setup(x =>
                    x.GetAllAsync())
                .ReturnsAsync(
                    new List<DoctorFinding>
                    {
                        new(),
                        new()
                    });

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
            _repoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    new DoctorFinding
                    {
                        FindingId = 1
                    });

            var result =
                await _service
                .GetByIdAsync(1);

            result.FindingId
                .Should().Be(1);
        }

        [Fact]
        public async Task
            GetById_Should_Throw_When_Not_Found()
        {
            _repoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    (DoctorFinding?)null);

            Func<Task> act =
                async () =>
                    await _service
                    .GetByIdAsync(1);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Finding not found");
        }

        [Fact]
        public async Task
            GetByAppointment_Should_Return_Finding()
        {
            var finding =
                new DoctorFinding
                {
                    FindingId = 1,
                    AppointmentId = 1
                };

            _repoMock
                .Setup(x =>
                    x.GetByAppointmentIdAsync(1))
                .ReturnsAsync(finding);

            var result =
                await _service
                .GetByAppointmentAsync(1);

            result.Should()
                .NotBeNull();

            result.AppointmentId
                .Should().Be(1);
        }

        [Fact]
        public async Task
            GetByAppointment_Should_Throw_When_Not_Found()
        {
            _repoMock
                .Setup(x =>
                    x.GetByAppointmentIdAsync(1))
                .ReturnsAsync(
                    (DoctorFinding?)null);

            Func<Task> act =
                async () =>
                    await _service
                    .GetByAppointmentAsync(1);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("Not found");
        }

        [Fact]
        public async Task
            Update_Should_Update_When_Exists()
        {
            var finding =
                new DoctorFinding
                {
                    FindingId = 1,
                    Observations = "Old"
                };

            var dto =
                new UpdateDoctorFindingDto
                {
                    Observations = "New",
                    Recommendations =
                        "Updated"
                };

            _repoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(finding);

            _repoMock
                .Setup(x =>
                    x.UpdateAsync(finding))
                .Returns(
                    Task.CompletedTask);

            var result =
                await _service
                .UpdateAsync(1, dto);

            result.Observations
                .Should().Be("New");
        }

        [Fact]
        public async Task
            Update_Should_Throw_When_Not_Found()
        {
            _repoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    (DoctorFinding?)null);

            var dto =
                new UpdateDoctorFindingDto();

            Func<Task> act =
                async () =>
                    await _service
                    .UpdateAsync(1, dto);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Finding not found");
        }

        [Fact]
        public async Task
            Delete_Should_Delete_When_Exists()
        {
            var finding =
                new DoctorFinding
                {
                    FindingId = 1
                };

            _repoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(finding);

            _repoMock
                .Setup(x =>
                    x.DeleteAsync(finding))
                .Returns(
                    Task.CompletedTask);

            await _service
                .DeleteAsync(1);

            _repoMock.Verify(
                x => x.DeleteAsync(
                    finding),
                Times.Once);
        }

        [Fact]
        public async Task
            Delete_Should_Throw_When_Not_Found()
        {
            _repoMock
                .Setup(x =>
                    x.GetByIdAsync(1))
                .ReturnsAsync(
                    (DoctorFinding?)null);

            Func<Task> act =
                async () =>
                    await _service
                    .DeleteAsync(1);

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage(
                    "Finding not found");
        }

        [Fact]
        public async Task
            GetMyReports_Should_Return_List()
        {
            var patient =
                new Patient
                {
                    PatientId = 1,
                    UserId = 1
                };

            var findings =
                new List<DoctorFinding>
                {
                    new()
                    {
                        FindingId = 1
                    },
                    new()
                    {
                        FindingId = 2
                    }
                };

            _patientRepoMock
                .Setup(x =>
                    x.GetByUserIdAsync(1))
                .ReturnsAsync(patient);

            _repoMock
                .Setup(x =>
                    x.GetByPatientIdAsync(1))
                .ReturnsAsync(findings);

            var result =
                await _service
                .GetByPatientIdAsync();

            result.Should()
                .HaveCount(2);
        }
    }
}