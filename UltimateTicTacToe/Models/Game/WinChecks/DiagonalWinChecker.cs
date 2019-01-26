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
            for (int i = board.Count - 1; i > 1; i--)
            {
                try
                {
                    diagWinner = board[board.Count - i - 1][i].getWinner().getColour() == board[board.Count - i][i - 1].getWinner().getColour();
                }
                catch (NoWinnerException)
                {
                    diagWinner = false;
                    break;
                }
                if (diagWinner)
                {
                    result = new Point { X = board.Count - 1, Y = 0 };
                }
            }

            return result;
        }

        private Point checkTopLeftDiagonal(List<List<BoardGame>> board)
        {
            bool diagWinner = true;
            Point result = new Point { X = -1, Y = -1 };
            for (int i = 1; i < board.Count; i++)
            {
                try
                {
                    diagWinner = board[i - 1][i - 1].getWinner().getColour() == board[i][i].getWinner().getColour();
                }
                catch (NoWinnerException)
                {
                    diagWinner = false;
                    break;
                }
                if (diagWinner)
                {
                    result = new Point { X = 0, Y = 0 } ;
                }
            }

            return result;
        }
    }
}
