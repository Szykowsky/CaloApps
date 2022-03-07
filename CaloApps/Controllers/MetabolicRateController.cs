using Calo.Feature.MetabolicRate.Commands;
using Calo.Feature.MetabolicRate.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Calo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetabolicRateController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;
        public MetabolicRateController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetDietsAsync([FromBody] MetabolicRateModel.BaseModel model)
        {
            var result = await this.mediator.Send(new CreateMetabolicRate.Command
            {
                Age = model.Age,
                Weight = model.Weight,
                Gender = model.Gender,
                Growth = model.Growth,
                Activity = model.Activity,
                Formula = model.Formula,
                UserId = Guid.Parse(this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            });
            return Ok(result);
        }
    }
}
