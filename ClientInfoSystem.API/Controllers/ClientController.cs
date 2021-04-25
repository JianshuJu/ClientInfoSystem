using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;

namespace ClientInfoSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [Route("allClients")]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClient();
            if (!clients.Any())
            {
                return NotFound("We have no clients");
            }

            return Ok(clients);
        }

        [HttpPost]
        [Route("addClient")]
        public async Task<IActionResult> AddClient(ClientRequestModel clientRequestModel)
        {
            var client = await _clientService.AddClient(clientRequestModel);
            return Ok(client);
        }

        [HttpGet]
        [Route("deleteClient")]
        public async Task<IActionResult> DeleteClient(int clientId)
        {
            var client = await _clientService.DeleteClient(clientId);
            return Ok(client);
        }

        [HttpPost]
        [Route("updateClient")]
        public async Task<IActionResult> UpdateClient(Client client)
        {
            await _clientService.UpdateClient(client);
            return Ok(client);
        }

    }
}
