using _3x3GameBackendWebAppASP.Models;

namespace _3x3GameBackendWebAppASP.Reps
{
    public class CellRepository
    {
        private readonly GameDbContext _context;
        public CellRepository(GameDbContext context) => _context = context;

        public async Task<IList<Cell>> GetAll() => await _context.Cells.ToListAsync();

        public async Task ClearAll()
        {
            await _context.Cells.ForEachAsync(cell => cell.State = null); 
            await _context.SaveChangesAsync();
        }

        public async Task Update(Cell cell) 
        {
            var oldCell = await _context.Cells.FindAsync(cell.Id);
            if(oldCell is not null && oldCell.State is null)
            {
                oldCell.State = cell.State;
                await _context.SaveChangesAsync();
            }
        }
    }
}
