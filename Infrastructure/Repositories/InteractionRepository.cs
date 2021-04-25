using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InteractionRepository : EfRepository<Interaction>, IInteractionRepository
    {
        public InteractionRepository(ClientInfoSystemDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Interaction>> GetUpcoming5Interactions()
        {
            var interactions = await _dbContext.Interactions.OrderByDescending(i => i.IntDate).Take(5).ToListAsync();
            return interactions;
        }

        public async Task<List<Interaction>> GetInteractionsByClient(int clientId)
        {
            var interactions = await _dbContext.Interactions.Where(i => i.ClientId == clientId)
                .OrderByDescending(i => i.IntDate).ToListAsync();
            return interactions;
        }

        public async Task<List<Interaction>> GetInteractionsByEmployee(int employeeId)
        {
            var interactions = await _dbContext.Interactions.Where(i => i.EmpId == employeeId)
                .OrderByDescending(i => i.IntDate).ToListAsync();
            return interactions;
        }

        public async Task<InteractionDetailModel> GetInteractionDetail(int interactionId)
        {
            var interaction = await _dbContext.Interactions.FirstOrDefaultAsync(i => i.Id == interactionId);
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == interaction.ClientId);
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == interaction.EmpId);
            var interactionDetail = new InteractionDetailModel
            {
                Id = interaction.Id,
                ClientId = interaction.ClientId,
                ClientAddedOn = client.AddedOn,
                ClientAddress = client.Address,
                ClientEmail = client.Email,
                ClientName = client.Name,
                ClientPhones = client.Phones,
                EmpDesignation = employee.Designation,
                EmpId = interaction.EmpId,
                IntDate = interaction.IntDate,
                EmpName = employee.Name,
                IntType = interaction.IntType,
                Remarks = interaction.Remarks
            };
            return interactionDetail;
        }
    }
}
