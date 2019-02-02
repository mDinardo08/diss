using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.MCTS;

namespace UltimateTicTacToe.Services
{
    public interface NodeService
    {
        List<INode> process(BoardGame game, PlayerColour colour);
    }
}
