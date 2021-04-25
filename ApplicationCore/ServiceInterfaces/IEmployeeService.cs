using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseModel>> GetAllEmployees();
        Task<EmployeeRegisterResponseModel> AddEmployee(EmployeeRegisterRequestModel employeeRegisterRequestModel);
        Task<EmployeeResponseModel> DeleteEmployee(int employeeId);
        Task<EmployeeResponseModel> UpdateEmployee(Employee employee);
        Task<LoginResponseModel> ValidateEmployee(string name, string password);
    }
}