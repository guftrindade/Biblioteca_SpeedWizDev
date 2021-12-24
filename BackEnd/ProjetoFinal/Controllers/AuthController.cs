using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Adaptadores;
using ProjetoFinal.Context;
using ProjetoFinal.Services.Auth.JWT;
using ProjetoFinal.Services.Auth.JWT.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjetoFinal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Campos
        private readonly IJwtAuthGerenciador jwtAuthGerenciador;
        private readonly BibliotecaDbContext _bibliotecaDbContext;
        #endregion

        #region Construtor
        public AuthController(IJwtAuthGerenciador jwtAuthGerenciador, BibliotecaDbContext _bibliotecaDbContext)
        {
            this.jwtAuthGerenciador = jwtAuthGerenciador;
            this._bibliotecaDbContext = _bibliotecaDbContext;
        }
        #endregion

        #region Metodo_Autenticar_Usuario
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult>Autenticar([FromBody] JwtUsuarioCredenciais jwtUsuarioCredenciais)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var usuarioAtual = await _bibliotecaDbContext.Usuarios.SingleOrDefaultAsync(
                    x => x.Email == jwtUsuarioCredenciais.Email && x.Senha == jwtUsuarioCredenciais.Senha);

                if(usuarioAtual == null)
                {
                    return NotFound(new
                    {
                        Status = "Falha",
                        Code = 400,
                        Data = "Não foi possível encontrar o Usuário!"
                    });
                }

                return Ok(
                            new
                            {
                                data = jwtAuthGerenciador.GerarToken(usuarioAtual.ParaJwtCredenciais())
                            });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Mensagem = e.Message
                });
            }
        }
        #endregion
    }
}
