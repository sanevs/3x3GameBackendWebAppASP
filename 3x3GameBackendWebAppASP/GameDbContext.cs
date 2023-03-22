using _3x3GameBackendWebAppASP.Models;

namespace _3x3GameBackendWebAppASP
{
    public class GameDbContext : DbContext
    {
        public DbSet<Cell> Cells =>  Set<Cell>();
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options){}
    }
}
