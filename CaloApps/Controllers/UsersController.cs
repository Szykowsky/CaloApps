using Calo.Feature.Users.Commands;
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
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UsersController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUser([FromQuery] string login)
        {
            var result = await this.mediator.Send(createUserCommand);
            if (!result.RequestStatus.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Command createUserCommand)
        {
            var result = await this.mediator.Send(createUserCommand);
            if(!result.RequestStatus.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignInUser([FromBody] LoginUser.Command loginCommand)
        {
            var result = await this.mediator.Send(loginCommand);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenUser([FromBody] RefreshUserToken.Command refreshtokenCommand)
        {
            var result = await this.mediator.Send(refreshtokenCommand);
            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpPut("diet")]
        public async Task<IActionResult> UpdateUserCurrentDiet([FromBody] Guid dietId)
        {
            var result = await this.mediator.Send(new SaveCurrentDiet.Command
            {
                DietId = dietId,
                Userid = Guid.Parse(this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            });

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
