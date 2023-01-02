using BasicSecurityASP.Models;
using BasicSecurityASP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicSecurityASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService beerService;

        public BeerController(IBeerService beerService)
        {
            this.beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<Beer>> Get()=> await beerService.Get();
    }
}
