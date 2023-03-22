using _3x3GameBackendWebAppASP.Models;
using _3x3GameBackendWebAppASP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace _3x3GameBackendWebAppASP.Controllers
{
    [ApiController, Route("api/cells")]
    public class CellController : ControllerBase
    {
        private readonly ILogger<CellController> _logger;
        private readonly CellService _cellService;

        public CellController(ILogger<CellController> logger, CellService cellService)
        {
            _logger = logger;
            _cellService = cellService;
        }

        [HttpGet]
        public async Task<IList<Cell>> GetCells() => await _cellService.GetCells();

        [HttpPut("move")]
        public async Task<bool?> MakeMove(int id)
        {
            _logger.Log(LogLevel.Debug, "Move is making...");
            var winner = await _cellService.MakeMove(id);
            if (!winner.HasValue)
                Response.StatusCode = (int)HttpStatusCode.Accepted;
            return winner;
        }

        [HttpDelete("start")]
        public async Task<IList<Cell>> StartNewGame()
        {
            _logger.Log(LogLevel.Debug, "Game is starting...");
            return await _cellService.StartNewGame();
        }
    }
}