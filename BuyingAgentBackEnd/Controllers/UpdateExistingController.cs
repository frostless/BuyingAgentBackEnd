
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
        private IBuyingAgentRead _buyingAgentRead;
		private IBuyingAgentCheckIfSaved _buyingAgentIfSaved;
		private IBuyingAgentCheckIfExisted _buyingAgentCheckIfExisted;
		private ILogger<UpdateExistingController> _logger;

        public UpdateExistingController(IBuyingAgentRead buyingAgentRead,
										IBuyingAgentCheckIfSaved buyingAgentIfSaved,
										IBuyingAgentCheckIfExisted buyingAgentCheckIfExisted,
										ILogger<UpdateExistingController> logger)
        {
			_buyingAgentRead = buyingAgentRead;
			_buyingAgentIfSaved = buyingAgentIfSaved;
			_buyingAgentCheckIfExisted = buyingAgentCheckIfExisted;
			_logger = logger;
        }


        [HttpPatch("visit/{visitId}")]
        public IActionResult PartiallyUpdateVisit(int visitId,
            [FromBody] JsonPatchDocument<VisitDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            if (!_buyingAgentCheckIfExisted.IfVisitExist(visitId)) return NotFound();

            var visitEntity = _buyingAgentRead.GetVisit(visitId);

            var visitToPatch = AutoMapper.Mapper.Map<VisitDto>(visitEntity);

            patchDoc.ApplyTo(visitToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            AutoMapper.Mapper.Map(visitToPatch, visitEntity);

            if (!_buyingAgentIfSaved.Save()) return StatusCode(500, "A problem happened while handling your request.");

            return NoContent();

        }
        [HttpPatch("product/{productId}")]
        public IActionResult PartiallyUpdateProduct(int productId,
            [FromBody] JsonPatchDocument<ProductDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            if (!_buyingAgentCheckIfExisted.IfProductExist(productId)) return NotFound();

            var productEntity = _buyingAgentRead.GetProduct(productId);

            var productToPatch = AutoMapper.Mapper.Map<ProductDto>(productEntity);

            patchDoc.ApplyTo(productToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            AutoMapper.Mapper.Map(productToPatch, productEntity);

            if (!_buyingAgentIfSaved.Save()) return StatusCode(500, "A problem happened while handling your request.");

            return NoContent();

        }

        [HttpPatch("transaction/{transactionId}")]
        public IActionResult PartiallyUpdateTransaction(int transactionId,
       [FromBody] JsonPatchDocument<TransactionDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            if (!_buyingAgentCheckIfExisted.IfTransactionExist(transactionId)) return NotFound();

            var transactionEntity = _buyingAgentRead.GetTransaction(transactionId);

            var transactionToPatch = AutoMapper.Mapper.Map<TransactionDto>(transactionEntity);

            patchDoc.ApplyTo(transactionToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            AutoMapper.Mapper.Map(transactionToPatch, transactionEntity);

            if (!_buyingAgentIfSaved.Save()) return StatusCode(500, "A problem happened while handling your request.");

            return NoContent();

        }

        [HttpPatch("customer/{customerId}")]
        public IActionResult PartiallyUpdateCustomer(int customerId,
     [FromBody] JsonPatchDocument<CustomerDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            if (!_buyingAgentCheckIfExisted.IfCustomerExist(customerId)) return NotFound();

            var customerEntity = _buyingAgentRead.GetCustomer(customerId);

            var customerToPatch = AutoMapper.Mapper.Map<CustomerDto>(customerEntity);

            patchDoc.ApplyTo(customerToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            AutoMapper.Mapper.Map(customerToPatch, customerEntity);

            if (!_buyingAgentIfSaved.Save()) return StatusCode(500, "A problem happened while handling your request.");

            return NoContent();

        }
    }
}