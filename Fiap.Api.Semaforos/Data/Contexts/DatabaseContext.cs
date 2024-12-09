using Microsoft.EntityFrameworkCore;
using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<CondicaoClimaticaModel> CondicaoClimatica { get; set; }
        public virtual DbSet<SemaforoModel> Semaforo { get; set; }
        public virtual DbSet<FluxoVeiculoModel> FluxoVeiculo { get; set; }
        public virtual DbSet<UsuarioModel> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CondicaoClimaticaModel>(entity =>
            {
                entity.ToTable("tbl_condicoes_climaticas");
                entity.HasKey(e => e.CondicaoClimaticaId);
                entity.Property(e => e.Tempo).IsRequired();
                entity.HasOne(c => c.Semaforo)
                      .WithMany()
                      .HasForeignKey(c => c.SemaforoId);
                entity.Property(e => e.AtualizadoEm)
                            .HasDefaultValueSql("SYSTIMESTAMP");
            });

            modelBuilder.Entity<FluxoVeiculoModel>(entity =>
            {
                entity.ToTable("tbl_fluxo_veiculos");
                entity.HasKey(e => e.FluxoVeiculoId);
                entity.HasOne(c => c.Semaforo)
                      .WithMany()
                      .HasForeignKey(c => c.SemaforoId);
                entity.Property(e => e.AtualizadoEm)
                            .HasDefaultValueSql("SYSTIMESTAMP");
            });

            modelBuilder.Entity<SemaforoModel>(entity =>
            {
                entity.ToTable("tbl_semaforos");
                entity.HasKey(e => e.SemaforoId);
                entity.Property(e => e.Luz).IsRequired();
                entity.Property(e => e.Logradouro).IsRequired();
                entity.Property(e => e.AtualizadoEm)
                             .HasDefaultValueSql("SYSTIMESTAMP");
            });

            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("tbl_usuarios");
                entity.HasKey(e => e.UsuarioId);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Senha).IsRequired();
            });
        }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected DatabaseContext() { }
    }
}
