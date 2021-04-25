using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;

namespace ClientInfoSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IJwtService _jwtService;

        public AccountController(IEmployeeService employeeService, IJwtService jwtService)
        {
            _employeeService = employeeService;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(EmployeeLoginRequestModel employeeLoginRequestModel)
        {
            var employee =
                await _employeeService.ValidateEmployee(employeeLoginRequestModel.Name,
                    employeeLoginRequestModel.Password);
            if (employee == null)
            {
                return Unauthorized();
            }

            var jwtToken = _jwtService.GenerateToken(employee);
            return Ok(new { token = jwtToken });
        }

    }
}
