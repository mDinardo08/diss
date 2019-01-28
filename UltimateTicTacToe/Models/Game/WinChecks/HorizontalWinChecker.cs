using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game.WinCheck
{
    public class HorizontalWinChecker: WinCheckHandler
    {
        public override void setCheckFunction()
        {
            check = checkHorizontalWinner;
        }

        public override void setSuccessor()
        {
            successor = new VerticleWinChecker();
        }

        public Point checkHorizontalWinner(BoardGame game)
        {
            Point result = new Point { X = -1, Y = -1 };
            List<List<BoardGame>> board = game.getBoard();
            for (int row  = 0; row < board.Count; row++)
            {
                bool winningRow = true;
                BoardGame leftMost = board[row][0];
                if (leftMost.isWon())
                {
                    for (int col = 1; col < board.Count; col++)
                    {
                        BoardGame space = board[row][col];
                        if (space.isWon())
                        {
                            if (leftMost.getWinner().getColour() != space.getWinner().getColour())
                            {
                                winningRow = false;
                            }
                        }
                        else
                        {
                            winningRow = false;
                        }
                    }
                } else
                {
                    winningRow = false;
                }
                if (winningRow)
                {
                    result = new Point { X = row, Y = 0 };
                }
            }
            return result;
        }
    }
}
