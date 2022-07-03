using LOCACOES.API.Model;
using Microsoft.EntityFrameworkCore;

namespace LOCACOES.API.Database
{
    public class MySqlContext : DbContext
    {
        public MySqlContext() {}
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }
        

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Locacao> Locacaos { get; set; }
        public DbSet<Filme> Filmes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>(e =>
            {
                e.HasIndex(e => e.Cpf).IsUnique();
            });
            builder.Entity<Cliente>(e =>
            {
                e.HasMany(x => x.Locacoes).WithOne().HasForeignKey(x => x.Id_Cliente);
            });

            builder.Entity<Filme>(e =>
            {
                e.HasMany(x => x.Locacoes).WithOne().HasForeignKey(x => x.Id_Filme);
            });
        }
    }

}
