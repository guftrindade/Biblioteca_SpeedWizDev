using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Context;
using ProjetoFinal.InputModel;
using ProjetoFinal.Models;
using ProjetoFinal.Services.Auth.JWT;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutoresController : ControllerBase
    {
        #region Campos
        private readonly BibliotecaDbContext _bibliotecaDbContext;
        #endregion

        #region Construtor
        public AutoresController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }
        #endregion

        #region Metodo_Listar_Autores
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarAutores()
        {
            var autor = await _bibliotecaDbContext.Autores.ToListAsync();

            return Ok( new
                            {
                                data = autor.Select(x =>
                                                         new
                                                         {
                                                             AutorId = x.Codigo,
                                                             Nome = x.Nome,
                                                             NomeCompleto = x.Nome + " " + x.Sobrenome
                                                         })});
                            }
        #endregion

        #region Metodo_Cadastrar_Autores
        [HttpPost]
        [Authorize(Roles = RolesUsuario.Admin)]
        public async Task<IActionResult> CadastrarAutor(AutorInput dadosEntrada)
        {
            var autor = new Autor()
            {
                Nome = dadosEntrada.Nome,
                Sobrenome = dadosEntrada.Sobrenome,
                CriadoEm = DateTime.Now
            };

            await _bibliotecaDbContext.Autores.AddAsync(autor);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(
                    new
                    {
                        success = true,
                        data = new
                        {
                            codigoAutor = autor.Codigo,
                            nomeAutor = autor.Nome
                        }
                    });
        }
        #endregion
    }
}
