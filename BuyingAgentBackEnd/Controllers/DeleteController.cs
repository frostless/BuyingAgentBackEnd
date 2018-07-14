using BuyingAgentBackEnd.Models;
using BuyingAgentBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuyingAgentBackEnd.Controllers
{
    [Route("api/delete")]
    public class DeleteController : Controller
    {
        private IBuyingAgentDelete _buyingAgentDelete;
		private IBuyingAgentCheckIfSaved _buyingAgentCheckIfSaved;
		private ILogger<AddNewController> _logger;
		public DeleteController(IBuyingAgentDelete buyingAgentDelete,
								ILogger<AddNewController> logger,
								IBuyingAgentCheckIfSaved buyingAgentCheckIfSaved)
        {
			_buyingAgentDelete = buyingAgentDelete;
            _logger = logger;
			_buyingAgentCheckIfSaved = buyingAgentCheckIfSaved;
		}
        [HttpDelete]
        public IActionResult DeleteEntity([FromBody] DeleteEntity deleteEntity)
        {
			_buyingAgentDelete.DeleteEntity(deleteEntity.Id, deleteEntity.Entity);
            if (!_buyingAgentCheckIfSaved.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return Ok();
        }
    }
}