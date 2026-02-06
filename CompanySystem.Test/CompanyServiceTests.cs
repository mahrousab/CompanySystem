using AutoMapper;
using CompanySystem.Application.DTOS;
using CompanySystem.Application.Interfaces;
using CompanySystem.Application.services;
using CompanySystem.Domains.Exceptions;
using CompanySystem.Domains.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Test
{
    public class CompanyServiceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly Mock<ILoggerManager> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CompanyService _service;
        private readonly Mock<ICompanyRepository> _companyRepoMock;
        public CompanyServiceTests()
        {
            _repositoryMock = new Mock<IRepositoryManager>();
            _loggerMock = new Mock<ILoggerManager>();
            _mapperMock = new Mock<IMapper>();
            _companyRepoMock = new Mock<ICompanyRepository>();
            _service = new CompanyService(_repositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetCompanyAsync_ShouldReturnCompany_WhenCompanyExists()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var companyEntity = new Company { Id = companyId, Name = "Test Company" };
            var companyDto = new CompanyDto(companyId, "Test Company", "Qairo");
            _repositoryMock.Setup(repo => repo.Company.GetCompanyAsync(companyId, false))
                .ReturnsAsync(companyEntity);
            _mapperMock.Setup(mapper => mapper.Map<CompanyDto>(companyEntity))
                .Returns(companyDto);
            // Act
            var result = await _service.GetCompanyAsync(companyId, false);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(companyId, result.Id);
            Assert.Equal("Test Company", result.Name);
        }

        [Fact]
        public async Task GetCompanyAsync_ShouldThrowCompanyNotFoundException_WhenCompanyDoesNotExist()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.Company.GetCompanyAsync(companyId, false))
                .ReturnsAsync((Company)null);
            // Act & Assert
            await Assert.ThrowsAsync<CompanyNotFoundException>(() => _service.GetCompanyAsync(companyId, false));
        }


        [Fact]
        public async Task CreateCompanyAsync_ShouldReturnCreatedCompany()
        {
            // Arrange
            var companyForCreation = new CompanyForCreationDto(
                "New Company",
                "123 Main St",
                "Egypt",
                null
            );
            var companyEntity = new Company { Id = Guid.NewGuid(), Name = "New Company", Address = "123 Main St" };
            var companyDto = new CompanyDto(companyEntity.Id, "New Company", "123 Main St");
            _mapperMock.Setup(mapper => mapper.Map<Company>(companyForCreation))
                .Returns(companyEntity);
            _repositoryMock.Setup(repo => repo.Company.CreateCompany(companyEntity));
            _mapperMock.Setup(mapper => mapper.Map<CompanyDto>(companyEntity))
                .Returns(companyDto);
            // Act
            var result = await _service.CreateCompanyAsync(companyForCreation);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(companyEntity.Id, result.Id);
            Assert.Equal("New Company", result.Name);
        }

     
        [Fact]
        public async Task GetAllCompaniesAsync_ShouldReturnListOfCompanies()
        {
            // Arrange
            var companyEntities = new List<Company>
            {
                new Company { Id = Guid.NewGuid(), Name = "Company 1" },
                new Company { Id = Guid.NewGuid(), Name = "Company 2" }
            };
            var companyDtos = new List<CompanyDto>
            {
                new CompanyDto(companyEntities[0].Id, "Company 1", "Address 1"),
                new CompanyDto(companyEntities[1].Id, "Company 2", "Address 2")
            };
            _repositoryMock.Setup(repo => repo.Company.GetAllCompaniesAsync(false))
                .ReturnsAsync(companyEntities);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CompanyDto>>(companyEntities))
                .Returns(companyDtos);
            // Act
            var result = await _service.GetAllCompaniesAsync(false);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }


        [Fact]
        public async Task GetAllCompaniesAsync_ShouldReturnEmptyList_WhenNoCompaniesExist()
        {
            // Arrange
            var companyEntities = new List<Company>();
            var companyDtos = new List<CompanyDto>();
            _repositoryMock.Setup(repo => repo.Company.GetAllCompaniesAsync(false))
                .ReturnsAsync(companyEntities);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CompanyDto>>(companyEntities))
                .Returns(companyDtos);
            // Act
            var result = await _service.GetAllCompaniesAsync(false);
            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateCompanyAsync_ShouldThrowCompanyNotFoundException_WhenCompanyDoesNotExist()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var companyForUpdate = new CompanyForUpdateDto(
                "Updated Company",
                "456 Main St",
                "Egypt",
                null
            );
            _repositoryMock.Setup(repo => repo.Company.GetCompanyAsync(companyId, true))
                .ReturnsAsync((Company)null);
            // Act & Assert
            await Assert.ThrowsAsync<CompanyNotFoundException>(() => _service.UpdateCompanyAsync(companyId, companyForUpdate, false));
        }

        [Fact]
        public async Task DeleteCompanyAsync_ShouldDeleteCompany_WhenCompanyExists()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var companyEntity = new Company { Id = companyId, Name = "Test Company" };
            _repositoryMock.Setup(repo => repo.Company.GetCompanyAsync(companyId, false))
                .ReturnsAsync(companyEntity);
            _repositoryMock.Setup(repo => repo.Company.DeleteCompany(companyEntity));
            _repositoryMock.Setup(repo => repo.SaveAsync());
            // Act
            await _service.DeleteCompanyAsync(companyId, false);
            // Assert
            _repositoryMock.Verify(repo => repo.Company.GetCompanyAsync(companyId, false), Times.Once);
            _repositoryMock.Verify(repo => repo.Company.DeleteCompany(companyEntity), Times.Once);
            _repositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
        }
    }
}