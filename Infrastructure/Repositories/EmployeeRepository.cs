using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : EfRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ClientInfoSystemDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<Employee> GetEmployeeByName(string name)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Name == name);
            return employee;
        }
    }
}
