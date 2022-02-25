using CaloApps.Diets.Commands;
using CaloApps.Diets.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaloApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietController : ControllerBase
    {
        private readonly IMediator mediator;
        public DietController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddDietAsync([FromBody] AddDiet.Command addDietCommand)
        {
            var result = await this.mediator.Send(addDietCommand);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetDietsAsync([FromRoute] Guid userId)
        {
            var result = await this.mediator.Send(new GetDiets.Query
            {
                UserId = userId
            });
            return Ok(result);
        }
    }
}
