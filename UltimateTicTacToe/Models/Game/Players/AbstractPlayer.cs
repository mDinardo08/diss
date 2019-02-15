using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.Models.DTOs;
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
        public int userId;
        public IDatabaseProvider provider;
        public AbstractPlayer(IRandomService random, IDatabaseProvider provider)
        {
            this.random = random;
            this.provider = provider;
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

        public INode makeMove(BoardGame game, List<INode> nodes, int opponentId)
        {
            RatingDTO rating = provider.getUser(opponentId) ?? new RatingDTO();
            rating.UserId = -1;
            INode decided = decideMove(game, nodes, rating);
            decided.getMove().setOwner(getColour());
            return decided;
        }

        abstract protected INode decideMove(BoardGame game, List<INode> nodes, RatingDTO opponentRating);

        public int getUserId()
        {
            return userId;
        }

        public void setUserId(int UserId)
        {
            this.userId = UserId;
        }
    }
}
