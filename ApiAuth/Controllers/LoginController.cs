using ApiAuth.Models;
using ApiAuth.Repositories;
using ApiAuth.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiAuth.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            //Recupera o usuário
            var user = UserRepository.Get(model.Username, model.Password);

            //Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            //Gera o Token
            var token = TokenService.GenerateToken(user);

            //Oculta a senha
            user.Password = "";

            //Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
