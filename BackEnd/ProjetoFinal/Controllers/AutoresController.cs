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
    public class AutoresController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        #region Construtor
        public AutoresController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }
        #endregion

        #region HTTP_GET
        [HttpGet]
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
                                                             Sobrenome = x.Sobrenome
                                                         })});
                            }
        #endregion

        #region HTTP_POST
        [HttpPost]
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
