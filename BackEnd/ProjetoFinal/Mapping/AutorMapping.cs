using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFinal.Models;

namespace ProjetoFinal.Mapping
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Nome).HasColumnType("VARCHAR(60)").IsRequired();
            builder.Property(x => x.Sobrenome).HasColumnType("VARCHAR(60)").IsRequired();
            builder.Property(x => x.CriadoEm).HasColumnType("DATETIME").IsRequired();
            builder.ToTable("Autores");
        }
    }
}
