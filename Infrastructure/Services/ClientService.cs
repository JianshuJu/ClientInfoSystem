using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMemoryCache _memoryCache;
        private const string clientCacheKey = "clients";
        private readonly TimeSpan _cacheTimeSpan = TimeSpan.FromDays(2);

        public ClientService(IClientRepository clientRepository, IMemoryCache memoryCache)
        {
            _clientRepository = clientRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<ClientResponseModel>> GetAllClient()
        {
            var client = await _memoryCache.GetOrCreateAsync(clientCacheKey, ClientCacheCheck);
            return client;
        }

        public async Task<List<ClientResponseModel>> ClientCacheCheck(ICacheEntry cacheEntry)
        {
            cacheEntry.SlidingExpiration = _cacheTimeSpan;
            var clients = await _clientRepository.ListAllAsync();
            var clientResponses = new List<ClientResponseModel>();
            foreach (var client in clients)
            {
                
                clientResponses.Add(new ClientResponseModel
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    AddedOn = client.AddedOn,
                    Address = client.Address,
                    Phones = client.Phones
                });
            }

            return clientResponses;
        }

        public async Task<ClientResponseModel> AddClient(ClientRequestModel clientRequestModel)
        {
            var client = new Client
            {
                Name = clientRequestModel.Name,
                AddedOn = clientRequestModel.AddedOn,
                Address = clientRequestModel.Address,
                Email = clientRequestModel.Email,
                Phones = clientRequestModel.Phones
            };
            var createdClient = await _clientRepository.AddAsync(client);
            var clientResponseModel = new ClientResponseModel
            {
                Id = createdClient.Id,
                AddedOn = createdClient.AddedOn,
                Address = createdClient.Address,
                Email = createdClient.Email,
                Name = createdClient.Name,
                Phones = createdClient.Phones
            };
            return clientResponseModel;
        }

        public async Task<ClientResponseModel> DeleteClient(int clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            await _clientRepository.DeleteAsync(client);
            var clientResponseModel = new ClientResponseModel
            {
                Id = client.Id,
                AddedOn = client.AddedOn,
                Address = client.Address,
                Email = client.Email,
                Name = client.Name,
                Phones = client.Phones
            };
            return clientResponseModel;
        }

        public async Task<ClientResponseModel> UpdateClient(Client client)
        {
            var updatedClient = await _clientRepository.UpdateAsync(client);
            var clientResponseModel = new ClientResponseModel
            {
                Id = updatedClient.Id,
                AddedOn = updatedClient.AddedOn,
                Address = updatedClient.Address,
                Email = updatedClient.Email,
                Name = updatedClient.Name,
                Phones = updatedClient.Phones
            };
            return clientResponseModel;
        }
    }
}
