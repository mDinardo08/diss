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
    public class MineFieldPlayer : AbstractPlayer
    {
        public MineFieldPlayer(IRandomService random, IDatabaseProvider provider) : base(random, provider)
        {
        }

        protected override INode decideMove(BoardGame game, List<INode> nodes, RatingDTO opponentRating)
        {
            List<INode> best = new List<INode>();
            int max = int.MinValue;
            foreach (INode node in nodes)
            {
                int score = getScore(node.getGame(), node.getMove());
                if (max < score)
                {
                    best = new List<INode>();
                    max = score;
                    best.Add(node);
                } else if (max == score)
                {
                    best.Add(node);
                }
            }
            return best[random.getRandomNumberBetween(0, best.Count)];
        }

        private int getScore(BoardGame game, Move move)
        {
            int score = 0;
            List<List<BoardGame>> board = game.getBoard();
            if (move.next != null)
            {
                BoardGame subBoard = board[move.possition.X][move.possition.Y];
                score = getScore(subBoard, move.next);
            } else
            {
                score = evaluateMove(move, board);
            }
            return score;
        }

        private int evaluateMove(Move move, List<List<BoardGame>> board)
        {
            int score = 0;
            for (int x = move.possition.X - 1; x <= move.possition.X + 1; x++)
            {
                for (int y = move.possition.Y - 1; x <= move.possition.Y + 1; y++)
                {
                    if (x != move.possition.X && y != move.possition.Y)
                    {
                        if (x > 0 && x < board.Count && y > 0 && y < board.Count)
                        {
                            if (board[x][y].isWon())
                            {
                                if (board[x][y].getWinner() == colour)
                                {
                                    score++;
                                }
                            }
                        }
                    }
                }
            }
            return score;
        }
    }
}
