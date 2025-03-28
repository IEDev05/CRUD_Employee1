using Microsoft.EntityFrameworkCore;

namespace CrudOpration.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
    

}
