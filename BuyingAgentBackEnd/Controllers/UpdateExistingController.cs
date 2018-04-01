
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


        [HttpPatch("visit/{visitId}")]
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
        [HttpPatch("product/{productId}")]
        public IActionResult PartiallyUpdateProduct(int productId,
            [FromBody] JsonPatchDocument<ProductDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            if (!_buyingAgentRepository.IfProductExist(productId)) return NotFound();

            var productEntity = _buyingAgentRepository.GetProduct(productId);

            var productToPatch = AutoMapper.Mapper.Map<ProductDto>(productEntity);

            patchDoc.ApplyTo(productToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            AutoMapper.Mapper.Map(productToPatch, productEntity);

            if (!_buyingAgentRepository.Save()) return StatusCode(500, "A problem happened while handling your request.");

            return NoContent();

        }

        [HttpPatch("transaction/{transactionId}")]
        public IActionResult PartiallyUpdateTransaction(int transactionId,
       [FromBody] JsonPatchDocument<TransactionDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            if (!_buyingAgentRepository.IfTransactionExist(transactionId)) return NotFound();

            var transactionEntity = _buyingAgentRepository.GetTransaction(transactionId);

            var transactionToPatch = AutoMapper.Mapper.Map<TransactionDto>(transactionEntity);

            patchDoc.ApplyTo(transactionToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            AutoMapper.Mapper.Map(transactionToPatch, transactionEntity);

            if (!_buyingAgentRepository.Save()) return StatusCode(500, "A problem happened while handling your request.");

            return NoContent();

        }

        [HttpPatch("customer/{customerId}")]
        public IActionResult PartiallyUpdateCustomer(int customerId,
     [FromBody] JsonPatchDocument<CustomerDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            if (!_buyingAgentRepository.IfCustomerExist(customerId)) return NotFound();

            var customerEntity = _buyingAgentRepository.GetCustomer(customerId);

            var customerToPatch = AutoMapper.Mapper.Map<CustomerDto>(customerEntity);

            patchDoc.ApplyTo(customerToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            AutoMapper.Mapper.Map(customerToPatch, customerEntity);

            if (!_buyingAgentRepository.Save()) return StatusCode(500, "A problem happened while handling your request.");

            return NoContent();

        }
    }
}