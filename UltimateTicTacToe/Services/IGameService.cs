using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Services
{
    public interface IGameService
    {
        BoardGame makeMove(BoardGame game, Move move);
    }
}
