using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.MCTS;

namespace UltimateTicTacToe.Services
{
    public class NodeCreationService : INodeCreationService
    {
        private IGameService gameService;

        public NodeCreationService(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public INode createNode(BoardGame game)
        {
            return new Node(game, null, null, gameService);
        }
    }
}
