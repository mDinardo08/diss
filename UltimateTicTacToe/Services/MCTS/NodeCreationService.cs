using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.MCTS;

namespace UltimateTicTacToe.Services
{
    public class NodeCreationService : INodeCreationService
    {

        public INode createNode(BoardGame game, PlayerColour colour)
        {
            return new Node(game, null, null, colour);
        }
    }
}
