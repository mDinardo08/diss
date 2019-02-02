using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game.WinCheck
{
    public class DiagonalWinChecker: WinCheckHandler
    {

        public override void setSuccessor()
        {
        }

        public override void setCheckFunction()
        {
            check = checkDiagonalWinner;
        }

        public Point checkDiagonalWinner(BoardGame game)
        {
            List<List<BoardGame>> board = game.getBoard();
            Point result = checkTopLeftDiagonal(board);
            if (result.X == -1)
            {
                result = checkBottomLeftDiagonal(board);
            }
            return result;
        }

        private Point checkBottomLeftDiagonal(List<List<BoardGame>> board)
        {
            bool diagWinner = true;
            Point result = new Point { X = -1, Y = -1 };
            BoardGame bottomLeft = board[board.Count - 1][0];
            if (bottomLeft.isWon())
            {
                for (int i = 0; i < board.Count - 1; i++)
                {
                    BoardGame space = board[i][board.Count - 1 - i];
                    if (space.isWon())
                    {
                        if (space.getWinner() != bottomLeft.getWinner())
                        {
                            diagWinner = false;
                        }
                    }
                    else
                    {
                        diagWinner = false;
                    }
                }
            }else
            {
                diagWinner = false;
            }
            if (diagWinner)
            {
                result = new Point { X = 2, Y = 0 };
            }
            return result;
        }

        private Point checkTopLeftDiagonal(List<List<BoardGame>> board)
        {
            bool diagWinner = true;
            Point result = new Point { X = -1, Y = -1 };
            BoardGame topLeft = board[0][0];
            if (topLeft.isWon())
            {
                for(int i = 1; i< board.Count; i++)
                {
                    BoardGame space = board[i][i];
                    if (space.isWon())
                    {
                        if(space.getWinner() != topLeft.getWinner())
                        {
                            diagWinner = false;
                        }
                    }
                    else
                    {
                        diagWinner = false;
                    }
                }
            }
            else
            {
                diagWinner = false;
            }
            if (diagWinner)
            {
                result = new Point { X = 0, Y = 0 };
            }
            return result;
        }
    }
}
