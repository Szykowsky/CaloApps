using CaloApps.Meals.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaloApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMediator mediator;
        public MealController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddMealAsync([FromBody] AddMeal.Command addMealCommand) 
        {
            var result = await this.mediator.Send(addMealCommand);
            return Ok(result);
        }
    }
}
