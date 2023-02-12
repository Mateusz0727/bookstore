using AutoMapper;
using Bookshop.App.Helpers;
using Bookshop.App.Models.Auth;
using Bookshop.App.Models.Book;
using Bookshop.App.Models.User;
using Bookshop.App.Services.Auth;
using Bookshop.App.Services.User;
using Bookshop.Configuration;
using Bookshop.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Bookshop.App.Controllers.Auth
{
    [AllowAnonymous]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly JWTConfig _config;

        public AuthController(AuthService authService, JWTConfig config, UserService userService)
        {
            _userService = userService;
            _authService = authService;
            _config = config;
        }
        #region Login()
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserTokens>> Login([FromBody] LoginFormModel model)
        {
            if (ModelState.IsValid)
            {
                var Token = new UserTokens();
                var entity = _userService.GetByEmail(model.Email);
                if (entity != null)
                {
                    var result = _authService.Login(model, entity);
                    if (result)
                    {
                        return Ok(CreateToken(entity));
                    }
                    else
                    {
                        return BadRequest($"wrong password");
                    }
                }
            }

            return StatusCode(401, "[[[Nazwa użytkownika lub hasło są nieprawidłowe.]]]");
        }
        #region CreateToken()
        private UserTokens CreateToken(User user)
        {
            var Token = new UserTokens();

            Token = JWTHelper.GenTokenKey(new UserTokens()
            {
                EmailId = user.Email,
                GuidId = Guid.NewGuid(),
                UserName = user.UserName,
                Id = user.Id,

            }, _config, user);
            SetTokenCookie(Token);
            return Token;
        }
        #endregion
        #region SetTokenCookie()
        private void SetTokenCookie(UserTokens token)
        {



        }
        #endregion
        #endregion
        #region Register()
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<object> Register([FromBody] RegisterFormModel model)
        {

            var entity = _userService.Create(model);

            return Created($"~api/users/{entity.Id}",entity);
           
        }

        
        #endregion
       
    }
}
