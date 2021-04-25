using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class InteractionService : IInteractionService

    {
        private readonly IInteractionRepository _interactionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;

        public InteractionService(IInteractionRepository interactionRepository, IEmployeeRepository employeeRepository, IClientRepository clientRepository)
        {
            _interactionRepository = interactionRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
        }

        public async Task<List<Interaction>> GetAllInteractions()
        {
            var interactions = await _interactionRepository.ListAllAsync();
            return (List<Interaction>)interactions;
        }

        public async Task<InteractionDetailModel> AddInteraction(InteractionRequestModel interactionRequestModel)
        {
            var interaction = new Interaction
            {
                ClientId = interactionRequestModel.ClientId,
                EmpId = interactionRequestModel.EmpId,
                IntDate = interactionRequestModel.IntDate,
                IntType = interactionRequestModel.IntType,
                Remarks = interactionRequestModel.Remarks
            };
            var createdInteraction = await _interactionRepository.AddAsync(interaction);
            var client = await _clientRepository.GetByIdAsync(createdInteraction.ClientId);
            var employee = await _employeeRepository.GetByIdAsync(createdInteraction.EmpId);
            var interactionDetailModel = new InteractionDetailModel
            {
                Id = createdInteraction.Id,
                IntDate = createdInteraction.IntDate,
                IntType = createdInteraction.IntType,
                Remarks = createdInteraction.Remarks,
                ClientAddedOn = client.AddedOn,
                ClientAddress = client.Address,
                ClientEmail = client.Email,
                ClientId = client.Id,
                ClientName = client.Name,
                ClientPhones = client.Phones,
                EmpDesignation = employee.Designation,
                EmpId = employee.Id,
                EmpName = employee.Name
            };
            return interactionDetailModel;
        }

        public async Task<InteractionDetailModel> DeleteInteraction(int interactionId)
        {
            var interaction = await _interactionRepository.GetByIdAsync(interactionId);
            await _interactionRepository.DeleteAsync(interaction);
            var client = await _clientRepository.GetByIdAsync(interaction.ClientId);
            var employee = await _employeeRepository.GetByIdAsync(interaction.EmpId);
            var interactionDetailModel = new InteractionDetailModel
            {
                Id = interaction.Id,
                IntDate = interaction.IntDate,
                IntType = interaction.IntType,
                Remarks = interaction.Remarks,
                ClientAddedOn = client.AddedOn,
                ClientAddress = client.Address,
                ClientEmail = client.Email,
                ClientId = client.Id,
                ClientName = client.Name,
                ClientPhones = client.Phones,
                EmpDesignation = employee.Designation,
                EmpId = employee.Id,
                EmpName = employee.Name
            };
            return interactionDetailModel;

        }

        public async Task<InteractionDetailModel> UpdateInteraction(Interaction interaction)
        {
            var updatedInteraction = await _interactionRepository.UpdateAsync(interaction);
            var client = await _clientRepository.GetByIdAsync(updatedInteraction.ClientId);
            var employee = await _employeeRepository.GetByIdAsync(updatedInteraction.EmpId);
            var interactionDetailModel = new InteractionDetailModel
            {
                Id = updatedInteraction.Id,
                IntDate = updatedInteraction.IntDate,
                IntType = updatedInteraction.IntType,
                Remarks = updatedInteraction.Remarks,
                ClientAddedOn = client.AddedOn,
                ClientAddress = client.Address,
                ClientEmail = client.Email,
                ClientId = client.Id,
                ClientName = client.Name,
                ClientPhones = client.Phones,
                EmpDesignation = employee.Designation,
                EmpId = employee.Id,
                EmpName = employee.Name
            };
            return interactionDetailModel;
        }

        public async Task<List<Interaction>> GetInteractionsByClient(int clientId)
        {
            var interactions = await _interactionRepository.GetInteractionsByClient(clientId);
            return interactions;
        }

        public async Task<List<Interaction>> GetInteractionsByEmployee(int employeeId)
        {
            var interactions = await _interactionRepository.GetInteractionsByEmployee(employeeId);
            return interactions;
        }

        public async Task<InteractionDetailModel> GetInteractionDetail(int interactionId)
        {
            var interactionDetail = await _interactionRepository.GetInteractionDetail(interactionId);
            return interactionDetail;
        }
    }
}
