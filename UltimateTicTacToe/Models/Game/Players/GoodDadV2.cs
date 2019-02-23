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
            List<int> places = new List<int>();
            if (opponentRating.moves != null)
            {
                foreach (MoveDBO move in opponentRating.moves)
                {
                    places.Add(move.place);
                }
            }
            double stddiv = standardDiv(average, places);
            stddiv *= 2;
            places = new List<int>();
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
            List<int> filtered = filteredPlaces(stddiv, places, average);
            if (filtered.Count == 0)
            {
                return nodes[random.getRandomNumberBetween(0, nodes.Count)];
            } else
            {
                return nodes[filtered[random.getRandomNumberBetween(0, filtered.Count)]];
            }
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
