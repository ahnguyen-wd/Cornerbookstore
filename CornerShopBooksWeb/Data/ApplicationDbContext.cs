using CornerShopBooksWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CornerShopBooksWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }
    }
}
