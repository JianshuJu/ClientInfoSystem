using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;

namespace ClientInfoSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route("addEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeRegisterRequestModel employeeRegisterRequestModel)
        {
            var employee = await _employeeService.AddEmployee(employeeRegisterRequestModel);
            return Ok(employee);
        }

        [HttpGet]
        [Route("allEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("deleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var employee = await _employeeService.DeleteEmployee(employeeId);
            return Ok(employee);
        }

        [HttpPost]
        [Route("updateEmployee")]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            var updatedEmployee = await _employeeService.UpdateEmployee(employee);
            return Ok(updatedEmployee);
        }
    }
}
