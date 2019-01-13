using Newtonsoft.Json;

namespace UltimateTicTacToe.Models.Game.Players
{
    public interface Player
    {
        string getName();
        BoardGame makeMove(BoardGame game);
        PlayerType getPlayerType();
    }
}
