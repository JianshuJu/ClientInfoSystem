using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<List<EmployeeResponseModel>> GetAllEmployees()
        {
            var employees = await _employeeRepository.ListAllAsync();
            var empResponses = new List<EmployeeResponseModel>();
            foreach (var employee in employees)
            {
                empResponses.Add(new EmployeeResponseModel
                {
                    Id = employee.Id,
                    Designation = employee.Designation,
                    Name = employee.Name
                });
            }

            return empResponses;
        }

        public async Task<EmployeeRegisterResponseModel> AddEmployee(EmployeeRegisterRequestModel employeeRegisterRequestModel)
        {
            var employee = new Employee
            {
                Name = employeeRegisterRequestModel.Name,
                Designation = employeeRegisterRequestModel.Designation,
                Password = employeeRegisterRequestModel.Password
            };
            var createdEmployee = await _employeeRepository.AddAsync(employee);
            var employeeResponseModel = new EmployeeRegisterResponseModel
            {
                Name = createdEmployee.Name,
                Designation = createdEmployee.Designation
            };
            return employeeResponseModel;
        }

        public async Task<EmployeeResponseModel> DeleteEmployee(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            await _employeeRepository.DeleteAsync(employee);
            var employeeResponseModel = new EmployeeResponseModel
            {
                Name = employee.Name,
                Designation = employee.Designation,
                Id = employeeId
            };
            return employeeResponseModel;

        }

        public async Task<EmployeeResponseModel> UpdateEmployee(Employee employee)
        {
            var updatedEmployee = await _employeeRepository.UpdateAsync(employee);
            var employeeResponseModel = new EmployeeResponseModel
            {
                Name = updatedEmployee.Name,
                Designation = updatedEmployee.Designation,
                Id = updatedEmployee.Id
            };
            return employeeResponseModel;
        }

        public async Task<LoginResponseModel> ValidateEmployee(string name, string password)
        {
            var dbEmployee = await _employeeRepository.GetEmployeeByName(name);
            if (dbEmployee==null)
            {
                return null;
            }

            if (password==dbEmployee.Password)
            {
                var employeeLoginResponseModel = new LoginResponseModel
                {
                    Name = dbEmployee.Name, Designation = dbEmployee.Designation, Id = dbEmployee.Id
                };
                return employeeLoginResponseModel;
            }

            return null;
        }
    }
}
