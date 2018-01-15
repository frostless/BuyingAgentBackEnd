
using Microsoft.AspNetCore.Mvc;
using BuyingAgentBackEnd.Models;
using Microsoft.AspNetCore.JsonPatch;
using BuyingAgentBackEnd.Services;
using Microsoft.Extensions.Logging;

namespace BuyingAgentBackEnd.Controllers
{
    [Route("api/update")]
    public class UpdateExistingController : Controller
    {
        private IBuyingAgentRepository _buyingAgentRepository;
        private ILogger<UpdateExistingController> _logger;

        public UpdateExistingController(IBuyingAgentRepository buyingAgentRepository,
            ILogger<UpdateExistingController> logger)
        {
            _buyingAgentRepository = buyingAgentRepository;
            _logger = logger;
        }


        [HttpPatch("visits/{visitId}")]
        public IActionResult PartiallyUpdateVisit(int visitId,
            [FromBody] JsonPatchDocument<VisitDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            if (!_buyingAgentRepository.IfVisitExist(visitId)) return NotFound();

            var visitEntity = _buyingAgentRepository.GetVisit(visitId);

            var visitToPatch = AutoMapper.Mapper.Map<VisitDto>(visitEntity);

            patchDoc.ApplyTo(visitToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            AutoMapper.Mapper.Map(visitToPatch, visitEntity);

            if (!_buyingAgentRepository.Save()) return StatusCode(500, "A problem happened while handling your request.");

            return NoContent();

        }
    }
}