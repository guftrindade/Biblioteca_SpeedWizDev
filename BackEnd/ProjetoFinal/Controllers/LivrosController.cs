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
    public class LivrosController : ControllerBase
    {
        #region Campos
        private readonly BibliotecaDbContext _bibliotecaDbContext;
        #endregion

        #region Construtor
        public LivrosController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }
        #endregion

        #region Metodo_Listar_Livros
        [HttpGet]
        [Authorize]

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

        #region Metodo_Cadastrar_Livros
        [HttpPost]
        [Authorize(Roles = RolesUsuario.Admin)]
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
        
        #region Metodo_Atualizar_Livros
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, AtualizarLivroInput dadosEntrada)
        {
            var livro = await _bibliotecaDbContext.Livros.Where(x => x.Codigo == id).FirstOrDefaultAsync();
            if (livro == null)
                return NotFound(new
                {
                    Status = "Falha",
                    Code = 400,
                    Data = "Livro não encontrado!"
                });


            livro.CodigoAutor = dadosEntrada.AutorId;
            livro.Descricao = dadosEntrada.Descricao;
            livro.ISBN = dadosEntrada.ISBN;
            livro.AnoLancamento = dadosEntrada.AnoLancamento;
            livro.CriadoEm = DateTime.Now;

            _bibliotecaDbContext.Livros.Update(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(livro);
        }
        #endregion

        #region Metodo_Deletar_Livros
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var livro = await _bibliotecaDbContext.Livros.Where(x => x.Codigo == id).FirstOrDefaultAsync();
            if (livro == null)
                return NotFound(new
                {
                    Status = "Falha",
                    Code = 400,
                    Data = "Livro não encontrado!"
                });

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

