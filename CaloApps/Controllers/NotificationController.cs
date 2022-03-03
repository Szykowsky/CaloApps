using Calo.Feature.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator mediator;
        public NotificationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("diet")]
        public async Task<IActionResult> GetDietsAsync()
        {
            var result = await this.mediator.Send(new GetDietStatus.Query
            {
                UserId = Guid.Parse("6D941A0D-B901-4F9F-B204-08D9FA1EDA9B")
            });
            return Ok(result);
        }
    }
}
