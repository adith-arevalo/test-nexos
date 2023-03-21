using ApiAutores.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiAutores
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutorLibro>()
                .HasKey(al => new { al.AutorId, al.LibroId });
            modelBuilder.Entity<Autor>()
              .HasIndex(a => a.Email)
              .IsUnique();
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<AutorLibro> AutorLibro { get; set; }
    }
}
