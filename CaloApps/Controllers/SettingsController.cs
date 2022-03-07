using Calo.Feature.UserSettings.Commands;
using Calo.Feature.UserSettings.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Calo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SettingsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetDietsAsync([FromBody] SettingModel.BaseModel model)
        {
            var result = await this.mediator.Send(new CreateOrUpdateUserSettings.Command
            {
                Age = model.Age,
                Weight = model.Weight,
                Gender = model.Gender,
                Growth = model.Growth,
                UserId = Guid.Parse(this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            });
            return Ok(result);
        }
    }
}
