using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Services;

namespace UltimateTicTacToe.Models.MCTS
{
    public class Node: INode
    {
        public INode parent;
        public BoardGame game;
        private List<INode> children;
        private int timesVisited;
        private Move previousMove;
        private PlayerColour colour;
        private int total;
        public Node(BoardGame game, INode parent, Move move, PlayerColour colour)
        {
            this.parent = parent;
            this.game = game;
            this.previousMove = move;
            this.colour = colour;
            children = new List<INode>();
            timesVisited = 0;
        }

        public void expand()
        {
            List<Move> moves = game.getAvailableMoves();
            foreach (Move move in moves)
            {
                BoardGame clone = game.Clone() as BoardGame;
                if (previousMove != null)
                {
                    move.setOwner(alternateColour(previousMove.owner));
                } else
                {
                    move.setOwner(colour);
                }
                clone.makeMove(move);
                children.Add(new Node(clone, this, move, colour));
            }
        }

        public List<INode> getChildren()
        {
            return children;
        }

        public double getUBC1()
        {
            INode p = parent;
            double result;
            if (timesVisited > 0)
            {
                while (p.getParent() != null)
                {
                    p = p.getParent();
                } 
                result = (total / timesVisited) + (2*Math.Sqrt(Math.Log(p.getVisits()) / getVisits()));
            } else
            {
                result = Int64.MaxValue;
            }
            return result;
        }

        public int getVisits()
        {
            return timesVisited;
        }

        public bool isLeaf()
        {
            return children.Count == 0;
        }

        public void rollOut()
        {
            Random r = new Random();
            BoardGame clone = game.Clone() as BoardGame;
            PlayerColour moveColour = alternateColour(previousMove.owner);
            while (!clone.isWon() && !clone.isDraw())
            {
                List<Move> moves = clone.getAvailableMoves();
                Move decided = moves[r.Next(moves.Count)];
                decided.setOwner(moveColour);
                clone.makeMove(decided);
                moveColour = alternateColour(moveColour);
            }
            backPropagate(value(clone));
        }

        private PlayerColour alternateColour(PlayerColour? colour)
        {
            return PlayerColour.BLUE == colour ? PlayerColour.RED : PlayerColour.BLUE;
        }

        public int value(BoardGame boardGame)
        {
            List<List<BoardGame>> board = boardGame.getBoard();
            int result = 0;
            for (int x = 0; x < board.Count; x++)
            {
                for (int y = 0; y < board[x].Count; y++)
                {
                    BoardGame space = board[x][y];
                    if (board[x][y].isWon())
                    {
                        if (space.getWinner() == colour)
                        {
                            result += 10;
                        } else
                        {
                            result -= 10;
                        }
                    }
                }
            }
            if (!boardGame.isDraw())
            {
                if (boardGame.getWinner() != colour)
                {
                    result *= -1;
                }
            }
            total += result;
            return result;
        }

        public INode getParent()
        {
            return parent;
        }

        public void backPropagate(int score)
        {
            total += score;
            timesVisited++;
            parent?.backPropagate(score);
        }

        public double getReward()
        {
            return total / timesVisited;
        }
    }
}
