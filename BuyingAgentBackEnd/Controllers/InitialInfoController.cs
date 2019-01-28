﻿using Microsoft.AspNetCore.Mvc;
using BuyingAgentBackEnd.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BuyingAgentBackEnd.Controllers
{
    [Route("api/InitialInfo")]
	[Authorize]
	public class InitialInfoController : Controller
    {
        private IBuyingAgentReports _buyingAgentReports;
        private ILogger<InitialInfoController> _logger;

        public InitialInfoController(IBuyingAgentReports buyingAgentReports,
                                     ILogger<InitialInfoController> logger
                                     )
        {
			_buyingAgentReports = buyingAgentReports;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var infoToDisplay = _buyingAgentReports.GetEntities();
            return Ok(infoToDisplay);
        }
    }
}