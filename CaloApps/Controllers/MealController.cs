﻿using Calo.Feature.Meals.Commands;
using Calo.Feature.Meals.Models;
using Calo.Feature.Meals.Queries;
using Calo.Feature.Worksheet.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CaloApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public MealController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
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

        [HttpPost("export-xlsx")]
        [Authorize]
        public async Task<IActionResult> ExportMealsToExcel([FromBody] Guid dietId)
        {
            var result = await this.mediator.Send(new ExportMealsToExcel.Command
            {
                DietId=dietId,
                UserId= Guid.Parse(this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            });

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "meals.xlsx");
        }
    }
}
