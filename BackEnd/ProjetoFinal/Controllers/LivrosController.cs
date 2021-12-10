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
    public class LivrosController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        #region Construtor
        public LivrosController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }
        #endregion

        #region HTTP_GET
        [HttpGet]
        public async Task<IActionResult> ListarLivros()
        {
            var livro = await _bibliotecaDbContext.Livros.ToListAsync();

            return Ok(new
            {
                data = livro.Select(x =>
                                         new
                                         {
                                             Codigo = x.Codigo,
                                             AutorId = x.CodigoAutor,
                                             Descricao = x.Descricao,
                                             ISBN = x.ISBN,
                                             AnoLancamento = x.AnoLancamento

                                         })});
        }
        #endregion

        #region HTTP_POST
        [HttpPost] 
        public async Task<IActionResult>Post(LivroInput dadosEntrada)
        {
            var livro = new Livro()
            {
                CodigoAutor = dadosEntrada.AutorId,
                Descricao = dadosEntrada.Descricao,
                ISBN = dadosEntrada.ISBN,
                AnoLancamento = dadosEntrada.AnoLancamento,
                CriadoEm = DateTime.Now
            };

            await _bibliotecaDbContext.Livros.AddAsync(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(
               new
               {
                   sucess = true,
                   data = true
               });
        }
        #endregion
        
        #region HTTP_PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, AtualizarLivroInput dadosEntrada)
        {
            var livro = await _bibliotecaDbContext.Livros.Where(x => x.Codigo == id).FirstOrDefaultAsync();
            if (livro == null)
                return NotFound("Livro não encontrado!");

            livro.Descricao = dadosEntrada.Descricao;
            livro.AnoLancamento = dadosEntrada.AnoLancamento;
            //livro.CodigoAutor = dadosEntrada.CodigoAutor;
            //livro.ISBN = dadosEntrada.ISBN;

            _bibliotecaDbContext.Livros.Update(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(livro);
        }
        #endregion

        #region HTTP_DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var livro = await _bibliotecaDbContext.Livros.Where(x => x.Codigo == id).FirstOrDefaultAsync();
            if (livro == null)
                return NotFound("Livro não existente no Banco de Dados!");

            _bibliotecaDbContext.Livros.Remove(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(
                new
                {
                    sucess = true,
                    data = true
                });
        }
        #endregion
    }

}

