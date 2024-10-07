using Microsoft.EntityFrameworkCore;
using ServidorPlatosMauiAPI.Models;

namespace ServidorPlatosMauiAPI.DBcontext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones ):base(opciones)
        {
            
        }

        public DbSet<Platos> Platos { get; set; }


    }
       
}
