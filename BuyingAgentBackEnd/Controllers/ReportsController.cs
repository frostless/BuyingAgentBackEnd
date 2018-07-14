using BuyingAgentBackEnd.Entities;
using BuyingAgentBackEnd.Models;
using BuyingAgentBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BuyingAgentBackEnd.Controllers
{
	[Route("api/reports")]
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

		[HttpGet("formularProfit")]
		public IActionResult formularProfit()
		{
			var formularProfit = _buyingAgentReports.GetFormulaProfit();
			return Ok(formularProfit);
		}

		[HttpGet("supplementsProfit")]
		public IActionResult supplementsProfit()
		{
			var supplementsProfit = _buyingAgentReports.GetSupplementsProfit();
			return Ok(supplementsProfit);
		}
	}
}