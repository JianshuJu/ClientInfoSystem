using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
        Task<Employee> GetEmployeeByName(string name);

    }
}
