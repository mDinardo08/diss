using Newtonsoft.Json;
using System.Collections.Generic;
using UltimateTicTacToe.Models.MCTS;

namespace UltimateTicTacToe.Models.Game.Players
{
    public interface Player
    {
        string getName();
        void setName(string name);
        void setColour(PlayerColour colour);
        PlayerColour getColour();
        INode makeMove(BoardGame game, List<INode> nodes);
        PlayerType getPlayerType();
    }
}
