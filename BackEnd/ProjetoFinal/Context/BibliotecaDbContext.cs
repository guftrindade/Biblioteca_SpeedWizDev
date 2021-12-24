using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;

namespace ProjetoFinal.Context
{
    public class BibliotecaDbContext : DbContext
    {
        #region DbSets_TabelasBancoDeDados
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        #endregion

        #region Construtor
        public BibliotecaDbContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BibliotecaDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
