using AuthCodingChallengeApi.Model;
using AuthCodingChallengeApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AuthCodingChallengeApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        private readonly UserRepository userRepository = new();

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            var user = await userRepository.Get(model.UserName, model.Password);
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }
    }
}
