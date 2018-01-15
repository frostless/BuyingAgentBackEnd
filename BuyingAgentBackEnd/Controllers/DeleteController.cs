using BuyingAgentBackEnd.Models;
using BuyingAgentBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuyingAgentBackEnd.Controllers
{
    [Route("api/delete")]
    public class DeleteController : Controller
    {
        private IBuyingAgentRepository _buyingAgentRepository;
        private ILogger<AddNewController> _logger;
        public DeleteController(IBuyingAgentRepository buyingAgentRepository,
            ILogger<AddNewController> logger)
        {
            _buyingAgentRepository = buyingAgentRepository;
            _logger = logger;
        }
        [HttpDelete]
        public IActionResult DeleteEntity([FromBody] DeleteEntity deleteEntity)
        {
            _buyingAgentRepository.DeleteEntity(deleteEntity.Id, deleteEntity.Entity);
            if (!_buyingAgentRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return Ok();
        }
    }
}