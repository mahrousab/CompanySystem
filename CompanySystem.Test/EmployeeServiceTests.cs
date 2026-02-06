using AutoMapper;
using CompanySystem.Application.DTOS;
using CompanySystem.Application.Interfaces;
using CompanySystem.Application.services;
using CompanySystem.Domains.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Test
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IRepositoryManager> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDataShaper<EmployeeDto>> _shaperMock;
        private readonly EmployeeService _service;
        private readonly Mock<ILoggerManager> _loggerMock;

        private readonly Mock<IEmployeeRepository> _employeeRepoMock = new Mock<IEmployeeRepository>();
        public EmployeeServiceTests()
        {
            _repoMock = new Mock<IRepositoryManager>();
            _mapperMock = new Mock<IMapper>();
            _shaperMock = new Mock<IDataShaper<EmployeeDto>>();
            _loggerMock = new Mock<ILoggerManager>();
            _service = new EmployeeService(_repoMock.Object, new Mock<ILoggerManager>().Object,
                                          _mapperMock.Object, _shaperMock.Object);
        }

        [Fact]
        public async Task CreateEmployeeForCompanyAsync_ShouldReturnEmployeeDto_WhenCompanyExists()
        {

            var companyId = Guid.NewGuid();
            var employeeForCreation = new EmployeeForCreationDto("John Doe", 25, "Developer");
            var employeeEntity = new Domains.Models.Employee { Id = Guid.NewGuid(), Name = "John Doe", Position = "Developer" };
            var employeeDto = new EmployeeDto(employeeEntity.Id, "John Doe", 25, "Developer");


            var employeeRepoMock = new Mock<IEmployeeRepository>();

            _repoMock.Setup(r => r.Employee).Returns(employeeRepoMock.Object);


            _repoMock.Setup(r => r.Company.GetCompanyAsync(companyId, false))
                     .ReturnsAsync(new Domains.Models.Company { Id = companyId });

            _mapperMock.Setup(m => m.Map<Domains.Models.Employee>(employeeForCreation))
                       .Returns(employeeEntity);

            _mapperMock.Setup(m => m.Map<EmployeeDto>(employeeEntity))
                       .Returns(employeeDto);

            // 2. Act
            var result = await _service.CreateEmployeeForCompanyAsync(companyId, employeeForCreation, false);

            // 3. Assert
            Assert.NotNull(result);
            Assert.Equal(employeeDto.Id, result.Id);

            employeeRepoMock.Verify(x => x.CreateEmployeeForCompany(companyId, employeeEntity), Times.Once);
        }


        [Fact]
        public async Task CreateEmployeeForCompanyAsync_ShouldThrowCompanyNotFoundException_WhenCompanyDoesNotExist()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var employeeForCreation = new EmployeeForCreationDto("John Doe", 25, "Developer");
            _repoMock.Setup(r => r.Company.GetCompanyAsync(companyId, false))
                     .ReturnsAsync((Domains.Models.Company)null);
            // Act & Assert
            await Assert.ThrowsAsync<CompanySystem.Domains.Exceptions.CompanyNotFoundException>(
                () => _service.CreateEmployeeForCompanyAsync(companyId, employeeForCreation, false));

        }
        [Fact]

       
        public async Task UpdateEmployeeForCompanyAsync_ShouldThrowCompanyNotFoundException_WhenCompanyDoesNotExist()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var employeeForUpdate = new EmployeeForUpdateDto("Jane Doe", 30, "Manager");
            _repoMock.Setup(r => r.Company.GetCompanyAsync(companyId, false))
                     .ReturnsAsync((Domains.Models.Company)null);
            // Act & Assert
            await Assert.ThrowsAsync<CompanySystem.Domains.Exceptions.CompanyNotFoundException>(
                () => _service.UpdateEmployeeForCompanyAsync(companyId, employeeId, employeeForUpdate, false, false));
        }

        public async Task GetEmployeeForCompanyAsync_ShouldReturnEmployeeDto_WhenCompanyAndEmployeeExist()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var employeeEntity = new Domains.Models.Employee { Id = employeeId, Name = "John Doe", Position = "Developer" };
            var employeeDto = new EmployeeDto(employeeId, "John Doe", 25, "Developer");
            _repoMock.Setup(r => r.Company.GetCompanyAsync(companyId, false))
                     .ReturnsAsync(new Domains.Models.Company { Id = companyId });
            _repoMock.Setup(r => r.Employee.GetEmployeeAsync(companyId, employeeId, false))
                     .ReturnsAsync(employeeEntity);
            _mapperMock.Setup(m => m.Map<EmployeeDto>(employeeEntity))
                       .Returns(employeeDto);
            // Act
            var result = await _service.GetEmployeeAsync(companyId, employeeId, false);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(employeeDto.Id, result.Id);
        }

        [Fact]
        public async Task GetEmployeeForCompanyAsync_ShouldThrowEmployeeNotFoundException_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            _repoMock.Setup(r => r.Company.GetCompanyAsync(companyId, false))
                     .ReturnsAsync(new Domains.Models.Company { Id = companyId });
            _repoMock.Setup(r => r.Employee.GetEmployeeAsync(companyId, employeeId, false))
                     .ReturnsAsync((Domains.Models.Employee)null);
            // Act & Assert
            await Assert.ThrowsAsync<CompanySystem.Domains.Exceptions.EmployeeNotFoundException>(
                () => _service.GetEmployeeAsync(companyId, employeeId, false));
        }
        [Fact]
        
        public async Task DeleteEmployeeForCompanyAsync_ShouldWork_WhenEmployeeExists()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var company = new Company { Id = companyId };
            var employee = new Employee { Id = employeeId, CompanyId = companyId };

          
            var employeeRepoMock = new Mock<IEmployeeRepository>();

         
            _repoMock.Setup(r => r.Employee).Returns(employeeRepoMock.Object);

        
            _repoMock.Setup(r => r.Company.GetCompanyAsync(companyId, false))
                     .ReturnsAsync(company);

            employeeRepoMock.Setup(r => r.GetEmployeeAsync(companyId, employeeId, false))
                            .ReturnsAsync(employee);

            // Act
            await _service.DeleteEmployeeForCompanyAsync(companyId, employeeId, false);

            // Assert
            employeeRepoMock.Verify(r => r.DeleteEmployee(employee), Times.Once);
            _repoMock.Verify(r => r.SaveAsync(), Times.Once);
        }
        [Fact]
        public async Task DeleteEmployeeForCompanyAsync_ShouldThrowEmployeeNotFoundException_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            _repoMock.Setup(r => r.Company.GetCompanyAsync(companyId, false))
                     .ReturnsAsync(new Domains.Models.Company { Id = companyId });
            _repoMock.Setup(r => r.Employee.GetEmployeeAsync(companyId, employeeId, false))
                     .ReturnsAsync((Domains.Models.Employee)null);
            // Act & Assert
            await Assert.ThrowsAsync<CompanySystem.Domains.Exceptions.EmployeeNotFoundException>(
                () => _service.DeleteEmployeeForCompanyAsync(companyId, employeeId, false));
        }
    }
}