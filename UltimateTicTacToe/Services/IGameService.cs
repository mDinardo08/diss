using System.Collections.Generic;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;

namespace UltimateTicTacToe.Services
{
    public interface IGameService
    {
        BoardGameDTO processMove(BoardGame game, Player Ai);
    }
}
