using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Services
{
    public interface IGameService
    {
        BoardGameDTO processMove(BoardGame game);
    }
}
