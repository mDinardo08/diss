using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.MCTS;

namespace UltimateTicTacToe.Services
{
    public interface INodeCreationService
    {
        INode createNode(BoardGame game);
    }
}
