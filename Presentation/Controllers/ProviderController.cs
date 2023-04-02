using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProviders()
        {
            return Ok(await _providerService.GetAllProvidersAsync());
        }
    }
}