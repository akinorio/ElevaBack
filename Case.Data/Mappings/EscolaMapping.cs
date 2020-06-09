using CaseElite.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseElite.Data.Mappings
{
    public class EscolaMapping : IEntityTypeConfiguration<Escola>
    {
        public void Configure(EntityTypeBuilder<Escola> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            // 1 : N => Escola : Turmas
            builder.HasMany(f => f.Turmas)
                .WithOne(p => p.Escola)
                .HasForeignKey(p => p.EscolaId);

            builder.ToTable("Escolas");
        }
    }
}