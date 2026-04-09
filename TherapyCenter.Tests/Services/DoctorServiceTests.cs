using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TherapyCenter2.DTOs.Doctor;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Implementations;

namespace TherapyCenter.Tests.Services
{
    public class DoctorServiceTests
    {
        private readonly Mock<IDoctorRepository> _doctorRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly DoctorService _doctorService;

        public DoctorServiceTests()
        {
            _doctorRepoMock = new Mock<IDoctorRepository>();
            _userRepoMock = new Mock<IUserRepository>();

            _doctorService = new DoctorService(
                _doctorRepoMock.Object,
                _userRepoMock.Object
            );
        }


        [Fact]
        public async Task CreateDoctor_Should_Create_Doctor_Successfully()
        {
            var dto = new CreateDoctorDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "doc@test.com",
                Password = "123456",
                PhoneNumber = "9999999999",
                Specialization = "Speech",
                Bio = "Expert",
                AvailableDays = "Mon-Fri"
            };

            var createdUser = new User
            {
                UserId = 1,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            var createdDoctor = new Doctor
            {
                DoctorId = 1,
                UserId = 1,
                Specialization = dto.Specialization,
                User = createdUser
            };

            _userRepoMock
                .Setup(x => x.AddUserAsync(It.IsAny<User>()))
                .ReturnsAsync(createdUser);

            _doctorRepoMock
                .Setup(x => x.AddAsync(It.IsAny<Doctor>()))
                .ReturnsAsync(createdDoctor);

            var result = await _doctorService.CreateDoctorAsync(dto);

            result.Should().NotBeNull();
            result.Email.Should().Be(dto.Email);
            result.Specialization.Should().Be(dto.Specialization);
        }


        [Fact]
        public async Task GetAllDoctors_Should_Return_List()
        {
            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    Specialization = "A",
                    User = new User { FirstName = "A", LastName = "B", Email = "a@test.com" }
                }
            };

            _doctorRepoMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(doctors);

            var result = await _doctorService.GetAllDoctorsAsync();

            result.Should().HaveCount(1);
        }


        [Fact]
        public async Task GetDoctorById_Should_Return_Doctor_When_Exists()
        {
            var doctor = new Doctor
            {
                DoctorId = 1,
                User = new User { FirstName = "John", LastName = "Doe", Email = "test@test.com" }
            };

            _doctorRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(doctor);

            var result = await _doctorService.GetDoctorByIdAsync(1);

            result.Should().NotBeNull();
            result.DoctorId.Should().Be(1);
        }

        [Fact]
        public async Task GetDoctorById_Should_Throw_Exception_When_Not_Found()
        {
            _doctorRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Doctor?)null);

            Func<Task> act = async () => await _doctorService.GetDoctorByIdAsync(1);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Doctor not found");
        }


        [Fact]
        public async Task UpdateDoctor_Should_Update_When_Exists()
        {
            var doctor = new Doctor
            {
                DoctorId = 1,
                User = new User { FirstName = "Old", LastName = "Name" }
            };

            var dto = new UpdateDoctorDto
            {
                FirstName = "New",
                LastName = "Name",
                PhoneNumber = "9999999999",
                Specialization = "Updated"
            };

            _doctorRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(doctor);

            _doctorRepoMock
                .Setup(x => x.UpdateAsync(It.IsAny<Doctor>()))
                .Returns(Task.CompletedTask);

            var result = await _doctorService.UpdateDoctorAsync(1, dto);

            result.FullName.Should().Contain("New");
        }

        [Fact]
        public async Task UpdateDoctor_Should_Throw_Exception_When_Not_Found()
        {
            var dto = new UpdateDoctorDto();

            _doctorRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Doctor?)null);

            Func<Task> act = async () => await _doctorService.UpdateDoctorAsync(1, dto);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Doctor not found");
        }


        [Fact]
        public async Task DeleteDoctor_Should_Delete_When_Exists()
        {
            var doctor = new Doctor { DoctorId = 1 };

            _doctorRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(doctor);

            _doctorRepoMock
                .Setup(x => x.DeleteAsync(doctor))
                .Returns(Task.CompletedTask);

            await _doctorService.DeleteDoctorAsync(1);

            _doctorRepoMock.Verify(x => x.DeleteAsync(doctor), Times.Once);
        }

        [Fact]
        public async Task DeleteDoctor_Should_Throw_Exception_When_Not_Found()
        {
            _doctorRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Doctor?)null);

            Func<Task> act = async () => await _doctorService.DeleteDoctorAsync(1);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Doctor not found");
        }
    }
}
