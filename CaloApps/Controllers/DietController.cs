using Calo.Feature.Diets.Commands;
using Calo.Feature.Diets.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CaloApps.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DietController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DietController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> AddDietAsync([FromBody] DietModel.Basic model)
        {
            var result = await this.mediator.Send(new AddDiet.Command
            {
                Carbohydrates = model.Carbohydrates,
                DayKcal = model.DayKcal,
                Fats = model.Fats,
                Fiber = model.Fiber,
                Minerals = model.Minerals,
                Name = model.Name,
                Protein = model.Protein,
                Vitamins = model.Vitamins,
                UserId = Guid.Parse(this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDietsAsync()
        {
            var result = await this.mediator.Send(new GetDiets.Query
            {
                UserId = Guid.Parse(this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            });
            return Ok(result);
        }
    }
}
