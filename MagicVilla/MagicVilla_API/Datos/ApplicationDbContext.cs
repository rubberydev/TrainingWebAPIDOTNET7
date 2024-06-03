using MagicVilla_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
                
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Villa>().HasData(
                new Villa() { Id = 1, Nombre = "Villa 1", Detalle = "Detalle de la villa 1", ImagenUrl = string.Empty,Amenidad="any",Tarifa=200, Ocupantes = 5, MetrosCuadrados = 50, FechaCreacion=DateTime.Now, FechaActualizacion=DateTime.Now },
                new Villa() { Id = 2, Nombre = "Premium vista a la playa", Detalle = "Detalle de la villa premium", ImagenUrl = string.Empty, Amenidad = "any",Tarifa=150, Ocupantes = 5, MetrosCuadrados = 50, FechaCreacion = DateTime.Now, FechaActualizacion = DateTime.Now },
                new Villa() { Id = 3, Nombre = "Villa 3", Detalle = "Detalle de la villa 3", ImagenUrl = string.Empty, Amenidad = "any",Tarifa=200, Ocupantes = 5, MetrosCuadrados = 50, FechaCreacion = DateTime.Now, FechaActualizacion = DateTime.Now }
                );
        }

    }
}
