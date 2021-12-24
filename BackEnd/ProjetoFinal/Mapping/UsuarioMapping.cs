using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFinal.Models;

namespace ProjetoFinal.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.CodigoRole);
            builder.Property(x => x.Nome).HasColumnType("VARCHAR(60)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("VARCHAR(60)").IsRequired();
            builder.Property(x => x.Senha).HasColumnType("VARCHAR(60)").IsRequired();
            builder.Property(x => x.CriadoEm).HasColumnType("DATETIME").IsRequired();
            builder.ToTable("Usuarios");
        }
    }
}
