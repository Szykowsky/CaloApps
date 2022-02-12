using CaloApps.Meals.Commands;
using CaloApps.Meals.Models;
using CaloApps.Meals.Queries;
using MediatR;
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

        [HttpGet("{dietId}")]
        public async Task<IActionResult> GetMeals([FromRoute] Guid dietId, [FromQuery] DateType? dateType, int? dayNumber, int? monthNumber)
        {
            var mealSearchModel = dateType == null || dateType == DateType.None ? null
                : new MealsFilter
                {
                    DateType = dateType,
                    DayNumber = dayNumber,
                    MonthNumber = monthNumber,
                };

            var result = await this.mediator.Send(new GetMeals.Query 
            { 
                DietId = dietId, 
                MealsFilterModel = mealSearchModel,
            });
            return Ok(result);
        }
    }
}
