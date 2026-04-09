using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TherapyCenter2.DTOs.Therapy;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Implementations;

namespace TherapyCenter.Tests.Services
{
    public class TherapyServiceTests
    {
        private readonly Mock<ITherapyRepository> _therapyRepositoryMock;
        private readonly TherapyService _therapyService;

        public TherapyServiceTests()
        {
            _therapyRepositoryMock = new Mock<ITherapyRepository>();
            _therapyService = new TherapyService(_therapyRepositoryMock.Object);
        }


        [Fact]
        public async Task CreateTherapy_Should_Create_Successfully()
        {
            var dto = new CreateTherapyDto
            {
                Name = "Speech Therapy",
                Description = "Speech improvement",
                DurationMinutes = 30,
                Cost = 500
            };

            var therapy = new Therapy
            {
                TherapyId = 1,
                Name = dto.Name,
                Description = dto.Description,
                DurationMinutes = dto.DurationMinutes,
                Cost = dto.Cost
            };

            _therapyRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Therapy>()))
                .ReturnsAsync(therapy);

            var result = await _therapyService.CreateTherapyAsync(dto);

            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
        }


        [Fact]
        public async Task GetAllTherapies_Should_Return_List()
        {
            var therapies = new List<Therapy>
            {
                new Therapy { TherapyId = 1, Name = "A", Cost = 100 },
                new Therapy { TherapyId = 2, Name = "B", Cost = 200 }
            };

            _therapyRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(therapies);

            var result = await _therapyService.GetAllTherapiesAsync();

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetTherapyById_Should_Return_Therapy_When_Exists()
        {
            var therapy = new Therapy
            {
                TherapyId = 1,
                Name = "Speech Therapy"
            };

            _therapyRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(therapy);

            var result = await _therapyService.GetTherapyByIdAsync(1);

            result.Should().NotBeNull();
            result.TherapyId.Should().Be(1);
        }

        [Fact]
        public async Task GetTherapyById_Should_Throw_Exception_When_Not_Found()
        {
            _therapyRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Therapy?)null);

            Func<Task> act = async () => await _therapyService.GetTherapyByIdAsync(1);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Therapy not found");
        }


        [Fact]
        public async Task UpdateTherapy_Should_Update_When_Exists()
        {
            var existing = new Therapy
            {
                TherapyId = 1,
                Name = "Old"
            };

            var dto = new UpdateTherapyDto
            {
                Name = "New",
                Description = "Updated",
                DurationMinutes = 60,
                Cost = 1000
            };

            _therapyRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(existing);

            _therapyRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Therapy>()))
                .Returns(Task.CompletedTask);

            var result = await _therapyService.UpdateTherapyAsync(1, dto);

            result.Name.Should().Be("New");
        }

        [Fact]
        public async Task UpdateTherapy_Should_Throw_Exception_When_Not_Found()
        {
            var dto = new UpdateTherapyDto
            {
                Name = "New"
            };

            _therapyRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Therapy?)null);

            Func<Task> act = async () => await _therapyService.UpdateTherapyAsync(1, dto);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Therapy not found");
        }


        [Fact]
        public async Task DeleteTherapy_Should_Delete_When_Exists()
        {
            var therapy = new Therapy { TherapyId = 1 };

            _therapyRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(therapy);

            _therapyRepositoryMock
                .Setup(x => x.DeleteAsync(therapy))
                .Returns(Task.CompletedTask);

            await _therapyService.DeleteTherapyAsync(1);

            _therapyRepositoryMock.Verify(x => x.DeleteAsync(therapy), Times.Once);
        }

        [Fact]
        public async Task DeleteTherapy_Should_Throw_Exception_When_Not_Found()
        {
            _therapyRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Therapy?)null);

            Func<Task> act = async () => await _therapyService.DeleteTherapyAsync(1);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Therapy not found");
        }
    }
}
