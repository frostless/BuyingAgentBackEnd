using BuyingAgentBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BuyingAgentBackEnd.Controllers
{
	[Route("api/reports")]
	[Authorize]
	public class ReportsController : Controller
	{
		private IBuyingAgentReports _buyingAgentReports;
		private IBuyingAgentCheckIfExisted _buyingAgentCheckIfExisted;
		private ILogger<AddNewController> _logger;

		public ReportsController(IBuyingAgentReports buyingAgentReports,
								IBuyingAgentCheckIfExisted buyingAgentCheckIfExisted,
								ILogger<AddNewController> logger)
		{
			_buyingAgentReports = buyingAgentReports;
			_buyingAgentCheckIfExisted = buyingAgentCheckIfExisted;
			_logger = logger;
		}

		[HttpGet("monthsProfit/{Id?}")]
		public IActionResult monthsProfit(int Id)
		{
			if (Id == 0) Id = DateTime.Now.Year;
			if (!_buyingAgentCheckIfExisted.IfMonthsProfitExit(Id)) return NotFound();
			var monthsProfit = _buyingAgentReports.GetMonthsProfit(Id);
			return Ok(monthsProfit);
		}

		[HttpGet("topCustomer")]
		public IActionResult topCustomer()
		{
			var topCustomer = _buyingAgentReports.GetTopCustomer();
			return Ok(topCustomer);
		}

		[HttpGet("topFiveCustomers")]
		public IActionResult topFiveCustomers()
		{
			var topCustomer = _buyingAgentReports.GetTopFiveCustomers();
			return Ok(topCustomer);
		}

		[HttpGet("topProduct")]
		public IActionResult topProduct()
		{
			var topProduct = _buyingAgentReports.GetTopProduct();
			return Ok(topProduct);
		}

		[HttpGet("topPost")]
		public IActionResult topPost()
		{
			var topPost = _buyingAgentReports.GetTopPost();
			return Ok(topPost);
		}

		[HttpGet("topVisit")]
		public IActionResult topVisit()
		{
			var topVisit = _buyingAgentReports.GetTopVisit();
			return Ok(topVisit);
		}

		[HttpGet("allProfit")]
		public IActionResult allProfit()
		{
			var allProfit = _buyingAgentReports.GetallProfit();
			return Ok(allProfit);
		}

		[HttpGet("transactionsNum")]
		public IActionResult transactionsNum()
		{
			return Ok(_buyingAgentReports.GetTranactionsNum());
		}

		[HttpGet("visitsNum")]
		public IActionResult visitsNum()
		{
			return Ok(_buyingAgentReports.GetVisitsNum());
		}

		[HttpGet("allTransactions")]
		public IActionResult allTransactions()
		{
			var allTransactions = _buyingAgentReports.GetAllTransactions();
			return Ok(allTransactions);
		}

		[HttpGet("allVisits")]
		public IActionResult allVisits()
		{
			var allVisits = _buyingAgentReports.GetAllVisits();
			return Ok(allVisits);
		}

		[HttpGet("allProducts")]
		public IActionResult allProducts()
		{
			var allProducts = _buyingAgentReports.GetAllProducts();
			return Ok(allProducts);
		}

		[HttpGet("allCategories")]
		public IActionResult allCategories()
		{
			var allCategories = _buyingAgentReports.GetAllCategories();
			return Ok(allCategories);
		}

		[HttpGet("allCustomers")]
		public IActionResult allCustomers()
		{
			var allCustomers = _buyingAgentReports.GetAllCustomers();
			return Ok(allCustomers);
		}

		[HttpGet("allShops")]
		public IActionResult allShops()
		{
			var allShops = _buyingAgentReports.GetAllShops();
			return Ok(allShops);
		}

		[HttpGet("allPosts")]
		public IActionResult allPosts()
		{
			var allPosts = _buyingAgentReports.GetAllPosts();
			return Ok(allPosts);
		}

		[HttpGet("formulaProfit")]
		public IActionResult formulaProfit()
		{
			var formulaProfit = _buyingAgentReports.GetFormulaProfit();
			return Ok(formulaProfit);
		}

		[HttpGet("supplementsProfit")]
		public IActionResult supplementsProfit()
		{
			var supplementsProfit = _buyingAgentReports.GetSupplementsProfit();
			return Ok(supplementsProfit);
		}
	}
}