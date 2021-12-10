using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Context;
using ProjetoFinal.InputModel;
using ProjetoFinal.Models;
using System;
using System.Threading.Tasks;

namespace ProjetoFinal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        #region Construtor
        public UsuariosController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }
        #endregion

        #region HTTP_POST
        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario(UsuarioInput dadosEntrada)
        {
            var usuario = new Usuario()
            {
                CodigoRole = dadosEntrada.CodigoRole,
                Nome = dadosEntrada.Nome,
                Email = dadosEntrada.Email,
                Senha = dadosEntrada.Senha,
                CriadoEm = DateTime.Now
            };

            await _bibliotecaDbContext.Usuarios.AddAsync(usuario);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok("Usuário adicionado com sucesso!");
        }
        #endregion
    }
}
