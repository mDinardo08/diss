using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.DataAccess;
using UltimateTicTacToe.DataAccess.DBOs;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.MCTS;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.Game.Players
{
    public class GoodDadV2 : AbstractPlayer
    {
        public GoodDadV2(IRandomService random, IDatabaseProvider provider) : base(random, provider)
        {
            type = PlayerType.GOODDADV2;
            userId = (int)type;
        }

        protected override INode decideMove(BoardGame game, List<INode> nodes, RatingDTO opponentRating)
        {
            double average = averagePlace(opponentRating.moves);
            List<int> opponentMovePlacements = new List<int>();
            if (opponentRating.moves != null)
            {
                foreach (MoveDBO move in opponentRating.moves)
                {
                    opponentMovePlacements.Add(move.place);
                }
            }
            double stddiv = standardDiv(average, opponentMovePlacements);
            stddiv *= 2;
            List<int> movePlacements = getMovePlaces(nodes);
            List<int> filtered = filteredPlaces(stddiv, movePlacements, average);
            return selectBestNode(nodes, filtered);
        }

        private INode selectBestNode(List<INode> nodes, List<int> acceptiblePlacements)
        {
            INode result;
            if (nodes.FindIndex(x => x.getReward() == 1) != -1)
            {
                result = nodes.Find(x => x.getReward() == 1);
            }
            else
            {
                if (acceptiblePlacements.Count == 0)
                {
                    result = nodes[random.getRandomNumberBetween(0, nodes.Count)];
                }
                else
                {
                    result = nodes[acceptiblePlacements[random.getRandomNumberBetween(0, acceptiblePlacements.Count)]];
                }
            }
            return result;
        }

        private List<int> getMovePlaces(List<INode> nodes)
        {
            List<int> places = new List<int>();
            for (int x = 0; x < nodes.Count; x++)
            {
                int place = 0;
                for (int y = 0; y < nodes.Count; y++)
                {
                    if (nodes[y].getReward() > nodes[x].getReward())
                    {
                        place++;
                    }
                }
                places.Add(place);
            }
            return places;
        }

        private List<int> filteredPlaces(double div, List<int> places, double average)
        {
            List<int> result = new List<int>();
            for(int x = 0; x < places.Count; x++)
            {
                if (places[x] >= (average - div) && places[x] <= (average + div))
                {
                    result.Add(x);
                }
            }
            return result;
        }
        
        private double averagePlace(List<MoveDBO> moves)
        {
            double sum = 0;
            if (moves != null)
            {
                if (moves.Count > 0)
                {
                    foreach (MoveDBO move in moves)
                    {
                        sum += move.place;
                    }
                    sum = sum / moves.Count;
                }
            }
            return sum;
        }

        private double standardDiv(double averagePlace, List<int> places)
        {
            if (places.Count > 1)
            {
                return 0;
            } else
            {
                double sum = 0;
                foreach(int place in places)
                {
                    sum += Math.Pow(averagePlace - place, 2);
                }
                sum = sum / (places.Count - 1);
                return Math.Sqrt(sum);
            }
        }
    }
}
