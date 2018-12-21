using System;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Services
{
    public class GameService : IGameService
    {
        public BoardGameDTO processMove(BoardGame game)
        {
            return new BoardGameDTO { Winner = game.getWinner() };
        }
    }
}
