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
            type = PlayerType.MINEFIELD;
            userId = (int)type;
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
            if (move.next.possition != null)
            {
                BoardGame subBoard = board[move.possition.X][move.possition.Y];
                score = getScore(subBoard, move.next);
            } else
            {
                score = evaluateMove(move, board);
                if (game.isWon())
                {
                    if (game.getWinner() == colour)
                    {
                        score += 3;
                    }
                }
            }
            return score;
        }

        private int evaluateMove(Move move, List<List<BoardGame>> board)
        {
            int score = 0;
            Point2D pos = move.possition;
            for (int col = 0; col < board.Count; col++)
            {
                if (col != pos.Y)
                {
                    BoardGame subBoard = board[pos.X][col];
                    score += getScoreForSubBoard(subBoard);
                }
            }
            for (int row = 0; row < board.Count; row++)
            {
                if (row != pos.X)
                {
                    BoardGame subBoard = board[row][pos.Y];
                    score += getScoreForSubBoard(subBoard);
                }
            }
            if (pos.X == pos.Y)
            {
                for (int i = 0; i < board.Count; i++)
                {
                    BoardGame subBoard = board[i][i];
                    if (i != pos.X)
                    {
                        score += getScoreForSubBoard(subBoard);
                    }
                }
            }
            if (2-pos.X == pos.Y)
            {
                for (int i = 0; i < board.Count; i++)
                {
                    BoardGame subBoard = board[2-i][i];
                    if (i != pos.Y)
                    {
                        score += getScoreForSubBoard(subBoard);
                    }
                }
            }
            return score;
        }

        private int getScoreForSubBoard(BoardGame subBoard)
        {
            int score = 0;
            if (subBoard.isWon())
            {
                if (subBoard.getWinner() == colour)
                {
                    score++;
                } else
                {
                    score--;
                }
            }
            return score;
        }
    }
}
