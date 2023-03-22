using _3x3GameBackendWebAppASP.Models;
using _3x3GameBackendWebAppASP.Reps;

namespace _3x3GameBackendWebAppASP.Services
{
    public class CellService
    {
        private readonly CellRepository _cellRepository;
        public CellService(CellRepository cellRepository) => _cellRepository = cellRepository;

        public async Task<IList<Cell>> GetCells() => await _cellRepository.GetAll();
        public async Task<IList<Cell>> StartNewGame() {
            await _cellRepository.ClearAll();
            return await _cellRepository.GetAll();
        }
        public async Task<bool?> MakeMove(int id) {
            bool? winner = await FindWinner();
            if (winner.HasValue)
                return winner;

            await _cellRepository.Update(
                new Cell(id, await GetNextMovePlayerState()));

            return await FindWinner();
        }

        private async Task<bool> GetNextMovePlayerState()
        {
            var cells = await _cellRepository.GetAll();
            int xCount = cells.Count(IsXorO(true));
            int oCount = cells.Count(IsXorO(false));
            // x <= o -> next X else next O
            return xCount <= oCount;
        }
        private Func<Cell, bool> IsXorO(bool XorO) => 
            cell => cell.State.HasValue && cell.State.Value == XorO;
        private const int center = 4;
        private async Task<bool?> FindWinner()
        {
            bool? winner = null;

            var cells = await _cellRepository.GetAll();
            states = cells.Select(c => c.State).ToList();

            int first = 0;
            int last = states.Count - 1;
            var args = new List<(int here, int near1, int near2)>()
            {
                (center, 4, -4),//проверка всех линий через центральную клетку начиная с диагонали по часовой стрелке (4 шт.)
                (first, 3, 6),//проверка линий, включающих первую клетку (2 шт.)
                (last, -3, -6),//проверка линий, включающих последнюю клетку (2 шт.)
            };
            foreach (var arg in args)
            {
                winner = FindWinnerForCurrentState(arg);
                if (winner.HasValue)
                    break;
            }

            return winner;
        }
        private static IList<bool?>? states;
        private static bool? FindWinnerForCurrentState((int here, int near1, int near2) arg)
        { 
            if (states?[arg.here] is not bool hereState)
                return null;
            while(arg.near1 != 0)
            {
                if (hereState == states[arg.here + arg.near1] &&
                    hereState == states[arg.here + arg.near2]) 
                    return hereState;

                if(arg.here == center)
                {
                    arg.near1--;
                    arg.near2++;
                }
                else
                {
                    arg.near1 /= 3;
                    arg.near2 /= 3;
                }
            }
            return null;
        }
    }
}
