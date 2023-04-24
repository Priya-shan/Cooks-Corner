using Microsoft.EntityFrameworkCore;
using Online_Recipe_Website.Models;

namespace Online_Recipe_Website.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RecipeModel> Recipes { get; set; }
    }
}
