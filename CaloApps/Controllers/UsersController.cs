using CaloApps.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaloApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Command createUserCommand)
        {
            var result = await this.mediator.Send(createUserCommand);
            if(!result.RequestStatus.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
