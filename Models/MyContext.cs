using Microsoft.EntityFrameworkCore;
using System; 

namespace ProductsCategories.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<Products> TheProducts {get; set;}

        public DbSet<Categories> TheCategories {get; set;}

        public DbSet<Associations> TheAssociations {get; set;}

        
    }
}