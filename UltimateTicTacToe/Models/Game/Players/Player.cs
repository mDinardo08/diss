using Newtonsoft.Json;

namespace UltimateTicTacToe.Models.Game.Players
{
    public interface Player
    {
        string getName();
        void setName(string name);
        void setColour(PlayerColour colour);
        PlayerColour getColour();
        BoardGame makeMove(BoardGame game);
        PlayerType getPlayerType();
    }
}
