using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEducationPlatform.BLL.Dtos.ApplicationUserDto;
using OnlineEducationPlatform.BLL.Manger.Accounts;

namespace OnlineEducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManger AccountManger;
        public AccountsController(IAccountManger accountManger)
        {
            AccountManger=accountManger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDto logindto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await AccountManger.Login(logindto);
            if (result.IsAuthenticated==false)
            {
                return BadRequest(result.message);
            }
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegesterDto regesterDto)
        {
            //model is good and correct 
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await AccountManger.Register(regesterDto);
            if(result.IsAuthenticated==false)
            {
                return BadRequest(result.message );
            }
            return Ok(result);
        }

    }
}
