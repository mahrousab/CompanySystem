using CompanySystem.Application.DTOS;
using CompanySystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using CompanySystem.Domains.Helper;
using System.Text.Json;
namespace CompanySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _services;
        public EmployeesController(IServiceManager services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            var pagedResult = await _services.EmployeeService.GetEmployeesAsync(companyId,
  employeeParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.employees);
        }
        [HttpGet("{id:guid}")]
        public async Task< IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = await _services.EmployeeService.GetEmployeeAsync(companyId, id,
            trackChanges: false);
            return Ok(employee);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody]
EmployeeForCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");
            var employeeToReturn =
         await   _services.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges:
            false);
            return CreatedAtRoute("GetEmployeeForCompany", new
            {
                companyId,
                id =
            employeeToReturn.Id
            },
            employeeToReturn);
        }
        [HttpDelete("{id:guid}")]
        public async Task< IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
           await  _services.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, trackChanges:
            false);
            return NoContent();
        }
        [HttpPut("{id:guid}")]
        public async Task< IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id,
[FromBody] EmployeeForUpdateDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForUpdateDto object is null");
           await _services.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee,
            compTrackChanges: false, empTrackChanges: true);
            return NoContent();
        }
        [HttpPatch("{id:guid}")]
        public async Task< IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
[FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");
            var result = await _services.EmployeeService.GetEmployeeForPatchAsync(companyId, id,
            compTrackChanges: false,
            empTrackChanges: true);
            patchDoc.ApplyTo(result.employeeToPatch);
         await   _services.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch,
            result.employeeEntity);
            return NoContent();
        }
    }
}