using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Context;
using ProjetoFinal.InputModel;
using ProjetoFinal.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : ControllerBase
    {
        #region Campos
        private readonly BibliotecaDbContext _bibliotecaDbContext;
        #endregion

        #region Construtor
        public UsuariosController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }
        #endregion

        #region Metodo_Listar_Usuarios
        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuario = await _bibliotecaDbContext.Usuarios.ToListAsync();

            return Ok(new
            {
                data = usuario.Select(x =>
                                         new
                                         {
                                             CodigoRole = x.CodigoRole,
                                             Nome = x.Nome,
                                             Email = x.Email,
                                             CriadoEm = x.CriadoEm
                                         }),
            });
        }
        #endregion

        #region Metodo_Cadastrar_Usuarios

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
