using Calo.Feature.MetabolicRate.Commands;
using Calo.Feature.MetabolicRate.Models;
using Calo.Feature.MetabolicRate.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Calo.API.Controllers;

[Route("api/[controller]")]
[Authorize]
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

    [HttpGet]
    public async Task<IActionResult> GetMetabolicRate()
    {
        var result = await this.mediator.Send(new GetMetabolicRate.Query
        {
            UserId = Guid.Parse(this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMetabolicRate([FromBody] MetabolicRateModel.BaseModel model)
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

    [HttpPut]
    public async Task<IActionResult> UpdateMetabolicRate([FromBody] MetabolicRateModel.UpdateModel model)
    {
        var result = await this.mediator.Send(new UpdateMetabolicRate.Command
        {
            Id = model.Id,
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
