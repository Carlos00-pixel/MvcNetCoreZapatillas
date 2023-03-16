using Microsoft.EntityFrameworkCore;
using MvcNetCoreZapatillas.Models;

namespace MvcNetCoreZapatillas.Data
{
    public class ZapatillasContext : DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options)
            : base(options) { }
        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<ImagenZapatilla> ImagenesZapatillas { get; set; }
        public DbSet<VistaImagenZapatilla> VistaImagenZapatillas { get; set; }

    }
}
