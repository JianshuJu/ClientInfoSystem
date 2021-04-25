using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;


namespace ApplicationCore.RepositoryInterfaces
{
    public interface IInteractionRepository : IAsyncRepository<Interaction>
    {
        Task<IEnumerable<Interaction>> GetUpcoming5Interactions();
        Task<List<Interaction>> GetInteractionsByClient(int clientId);
        Task<List<Interaction>> GetInteractionsByEmployee(int employeeId);
        Task<InteractionDetailModel> GetInteractionDetail(int interactionId);
    }
}
