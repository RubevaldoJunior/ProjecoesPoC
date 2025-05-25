using Microsoft.EntityFrameworkCore;
using ProjecoesPoC.Models;

namespace ProjecoesPoC.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CruzamentoSexoLocal> CruzamentoSexoLocals {  get; set; }
        public DbSet<CruzamentoLocalIdade> CruzamentoLocalIdades { get; set; }
        public DbSet<CruzamentoSexoIdade> CruzamentoSexoIdades { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CruzamentoSexoIdade>().Property(csi => csi.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CruzamentoLocalIdade>().Property(csi => csi.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CruzamentoSexoIdade>().Property(csi => csi.Id).ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder);
        }
    }
}
