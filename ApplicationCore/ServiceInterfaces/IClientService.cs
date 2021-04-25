using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IClientService
    {
        Task<List<ClientResponseModel>> GetAllClient();
        Task<ClientResponseModel> AddClient(ClientRequestModel clientRequestModel);
        Task<ClientResponseModel> DeleteClient(int clientId);
        Task<ClientResponseModel> UpdateClient(Client client);
    }
}