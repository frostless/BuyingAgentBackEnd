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
        private IBuyingAgentRepository _buyingAgentRepository;
        private ILogger<AddNewController> _logger;

        public ReportsController(IBuyingAgentRepository buyingAgentRepository,
            ILogger<AddNewController> logger)
        {
            _buyingAgentRepository = buyingAgentRepository;
            _logger = logger;
        }

        [HttpGet("monthsProfit/{Id?}")]
        public IActionResult monthsProfit(int Id)
        {
            if (Id == 0) Id = DateTime.Now.Year;
            if (!_buyingAgentRepository.IfMonthsProfitExit(Id)) return NotFound();
            var monthsProfit = _buyingAgentRepository.GetMonthsProfit(Id);
            return Ok(monthsProfit);
        }

        [HttpGet("topCustomer")]
        public IActionResult topCustomer()
        {
            var topCustomer = _buyingAgentRepository.GetTopCustomer();
            return Ok(topCustomer);
        }

        [HttpGet("topProduct")]
        public IActionResult topProduct()
        {
           var topProduct = _buyingAgentRepository.GetTopProduct();
            return Ok(topProduct);
        }

        [HttpGet("topPost")]
        public IActionResult topPost()
        {
            var topPost = _buyingAgentRepository.GetTopPost();
            return Ok(topPost);
        }

        [HttpGet("topVisit")]
        public IActionResult topVisit()
        {
            var topVisit = _buyingAgentRepository.GetTopVisit();
            return Ok(topVisit);
        }

        [HttpGet("allProfit")]
        public IActionResult allProfit()
        {
            var allProfit = _buyingAgentRepository.GetallProfit();
            return Ok(allProfit);
        }

        [HttpGet("transactionsNum")]
        public IActionResult transactionsNum()
        {
            return Ok(_buyingAgentRepository.GetTranactionsNum());
        }

        [HttpGet("visitsNum")]
        public IActionResult visitsNum()
        {
            return Ok(_buyingAgentRepository.GetVisitsNum());
        }
    }
}