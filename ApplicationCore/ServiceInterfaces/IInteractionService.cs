using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IInteractionService
    {
        Task<List<Interaction>> GetAllInteractions();
        Task<InteractionDetailModel> AddInteraction(InteractionRequestModel interactionRequestModel);
        Task<InteractionDetailModel> DeleteInteraction(int interactionId);
        Task<InteractionDetailModel> UpdateInteraction(Interaction interaction);
        Task<List<Interaction>> GetInteractionsByClient(int clientId);
        Task<List<Interaction>> GetInteractionsByEmployee(int employeeId);
        Task<InteractionDetailModel> GetInteractionDetail(int interactionId);

    }
}