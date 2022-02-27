using Calo.Feature.Meals.Commands;
using Calo.Feature.Meals.Models;
using Calo.Feature.Meals.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetMeals([FromRoute] Guid dietId, [FromQuery] MealModels.DateType? dateType, int? dayNumber, int? monthNumber)
        {
            var mealSearchModel = dateType == null || dateType == MealModels.DateType.None ? null
                : new MealModels.Filter
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

        [HttpPatch("{dietId}")]
        public async Task<IActionResult> PatchMeals([FromRoute] Guid dietId, [FromBody] JsonPatchDocument<IDictionary<Guid, MealModels.Patch>> patchDocument)
        {
            var result = await this.mediator.Send(new PatchMeals.Command
            {
                DietId = dietId,
                PatchMealsModel = patchDocument
            });

            return Ok(result);
        }
    }
}
