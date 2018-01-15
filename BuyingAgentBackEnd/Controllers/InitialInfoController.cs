using Microsoft.AspNetCore.Mvc;
using BuyingAgentBackEnd.Services;
using Microsoft.Extensions.Logging;

namespace BuyingAgentBackEnd.Controllers
{
    [Route("api/InitialInfo")]
    public class InitialInfoController : Controller
    {
        private IBuyingAgentRepository _buyingAgentRepository;
        private ILogger<InitialInfoController> _logger;

        public InitialInfoController(IBuyingAgentRepository buyingAgentRepository,
                                     ILogger<InitialInfoController> logger
                                     )
        {
            _buyingAgentRepository = buyingAgentRepository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var infoToDisplay = _buyingAgentRepository.GetEntities();
            return Ok(infoToDisplay);
        }
    }
}