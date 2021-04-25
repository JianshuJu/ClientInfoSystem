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
    public class InteractionController : ControllerBase
    {
        private readonly IInteractionService _interactionService;

        public InteractionController(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        [HttpGet]
        [Route("allInteractions")]
        public async Task<IActionResult> GetAllInteractions()
        {
            var interactions = await _interactionService.GetAllInteractions();
            return Ok(interactions);
        }

        [HttpPost]
        [Route("addInteraction")]
        public async Task<IActionResult> AddInteraction(InteractionRequestModel interactionRequestModel)
        {
            var interaction = await _interactionService.AddInteraction(interactionRequestModel);
            return Ok(interaction);
        }

        [HttpGet]
        [Route("deleteInteraction")]
        public async Task<IActionResult> DeleteInteraction(int interactionId)
        {
            var interaction = await _interactionService.DeleteInteraction(interactionId);
            return Ok(interaction);
        }

        [HttpPost]
        [Route("updateInteraction")]
        public async Task<IActionResult> UpdateInteraction(Interaction interaction)
        {
            var updatedInteraction = await _interactionService.UpdateInteraction(interaction);
            return Ok(updatedInteraction);
        }

        [HttpGet]
        [Route("client/{clientId:int}")]
        public async Task<IActionResult> GetInteractionsByClient(int clientId)
        {
            var interactions = await _interactionService.GetInteractionsByClient(clientId);
            return Ok(interactions);
        }

        [HttpGet]
        [Route("employee/{employeeId:int}")]
        public async Task<IActionResult> GetInteractionsByEmployee(int employeeId)
        {
            var interactions = await _interactionService.GetInteractionsByEmployee(employeeId);
            return Ok(interactions);
        }

        [HttpGet]
        [Route("detail/{interactionId:int}")]
        public async Task<IActionResult> GetInteractionDetail(int interactionId)
        {
            var interactionDetail = await _interactionService.GetInteractionDetail(interactionId);
            return Ok(interactionDetail);
        }
    }
}
