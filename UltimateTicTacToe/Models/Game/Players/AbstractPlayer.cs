using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public abstract class AbstractPlayer : Player
    {
        public string name;
        public IRandomService random;
        public PlayerType type;
        public PlayerColour colour;
        public AbstractPlayer(IRandomService random)
        {
            this.random = random;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public PlayerType getPlayerType()
        {
            return type;
        }

        public void setColour(PlayerColour colour)
        {
            this.colour = colour;
        }

        public PlayerColour getColour()
        {
            return this.colour;
        }

        public INode makeMove(BoardGame game, List<INode> nodes)
        {
            INode decided = decideMove(game, nodes);
            decided.getMove().setOwner(getColour());
            return decided;
        }

        abstract protected INode decideMove(BoardGame game, List<INode> nodes);
    }
}
