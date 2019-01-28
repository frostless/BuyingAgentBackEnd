using BuyingAgentBackEnd.Models;
using BuyingAgentBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace BuyingAgentBackEnd.Controllers
{
    [Route("api/addnew")]
	[Authorize]
	public class AddNewController : Controller
    {
        private IBuyingAgentCheckIfSaved _buyingAgentCheckIfSaved;
		private IBuyingAgentSave _buyingAgentSave;
        private ILogger<AddNewController> _logger;

        public AddNewController(IBuyingAgentCheckIfSaved buyingAgentCheckIfSaved,
								IBuyingAgentSave buyingAgentSave,
								ILogger<AddNewController> logger)
        {
			_buyingAgentCheckIfSaved = buyingAgentCheckIfSaved;
			_buyingAgentSave = buyingAgentSave;
			_logger = logger;
        }

        [HttpPost("newtransaction", Name = "SaveNewTransaction")]
        public IActionResult NewTransaction(
           [FromBody] TransactionDto newTransaction)
        {
            if (newTransaction == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transactionToSave = AutoMapper.Mapper.Map<Entities.Transaction>(newTransaction);

			// repository logic to save the new transaction
			_buyingAgentSave.SaveNewEntity(transactionToSave);
			_buyingAgentSave
				.SaveNewTPWithTransaction(transactionToSave.Id, newTransaction.ProductsInfo);

            if (!_buyingAgentCheckIfSaved.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var transactionToReturn = AutoMapper.Mapper.Map<Models.TransactionDto>(transactionToSave);

            return Ok(new
            {transactionToReturn });
        }

        [HttpPost("newcustomer", Name = "SaveNewCustomer")]
        public IActionResult NewCustomer(
          [FromBody] CustomerDto newCustomer)
        {
            if (newCustomer == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customerToSave = AutoMapper.Mapper.Map<Entities.Customer>(newCustomer);

			_buyingAgentSave.SaveNewEntity(customerToSave);

            if (!_buyingAgentCheckIfSaved.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var customerToReturn = AutoMapper.Mapper.Map<Models.CustomerDto>(customerToSave);

            return Ok(new { id = customerToSave.Id, customerToReturn });

        }

        [HttpPost("newPost", Name = "SaveNewPost")]
        public IActionResult NewPost(
       [FromBody] PostDto newPost)
        {
            if (newPost == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var postToSave = AutoMapper.Mapper.Map<Entities.Post>(newPost);

			_buyingAgentSave.SaveNewEntity(postToSave);

            if (!_buyingAgentCheckIfSaved.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var postToReturn = AutoMapper.Mapper.Map<Models.PostDto>(postToSave);

            return Ok(new { id = postToSave.Id, postToReturn });

        }

        [HttpPost("newVisit", Name = "SaveNewVisit")]
        public IActionResult NewVisit(
      [FromBody] VisitDto newVisit)
        {
            if (newVisit == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var visitToSave = AutoMapper.Mapper.Map<Entities.Visit>(newVisit);

			_buyingAgentSave.SaveNewEntity(visitToSave);

            if (!_buyingAgentCheckIfSaved.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var visitToReturn = AutoMapper.Mapper.Map<Models.VisitDto>(visitToSave);

            return Ok(new { id = visitToSave.Id, visitToReturn });
        }

        [HttpPost("newProduct", Name = "SaveNewProduct")]
        public IActionResult NewProduct(
    [FromBody] ProductDto newProduct)
        {
            if (newProduct == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productToSave = AutoMapper.Mapper.Map<Entities.Product>(newProduct);

			_buyingAgentSave.SaveNewEntity(productToSave);
        

            if (!_buyingAgentCheckIfSaved.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var productToReturn = AutoMapper.Mapper.Map<Models.ProductDto>(productToSave);


            return Ok(new { id = productToSave.Id, productToReturn });
        }

        [HttpPost("newCategory", Name = "SaveNewCategory")]
        public IActionResult NewCategory(
    [FromBody] CategoryDto newCategory)
        {
            if (newCategory == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categoryToSave = AutoMapper.Mapper.Map<Entities.Category>(newCategory);

			_buyingAgentSave.SaveNewEntity(categoryToSave);

            if (!_buyingAgentCheckIfSaved.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var categoryToReturn = AutoMapper.Mapper.Map<Models.CategoryDto>(categoryToSave);

            return Ok(new { id = categoryToSave.Id, categoryToReturn });
        }

		[HttpPost("newShop", Name = "SaveNewShop")]
		public IActionResult NewShop(
   [FromBody] ShopDto newShop)
		{
			if (newShop == null) return BadRequest();

			if (!ModelState.IsValid) return BadRequest(ModelState);

			var shopToSave = AutoMapper.Mapper.Map<Entities.Shop>(newShop);

			_buyingAgentSave.SaveNewEntity(shopToSave);

			if (!_buyingAgentCheckIfSaved.Save())
			{
				return StatusCode(500, "A problem happened while handling your request.");
			}

			var shopToReturn = AutoMapper.Mapper.Map<Models.ShopDto>(shopToSave);

			return Ok(new { id = shopToSave.Id, shopToReturn });
		}

	}

}
