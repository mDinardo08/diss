using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game.WinCheck
{
    public class VerticleWinChecker: WinCheckHandler
    {
        public VerticleWinChecker()
        {
            check = checkVerticalWinner;
            successor = new DiagonalWinChecker();
        }

        private Point checkVerticalWinner(CompositeGame game)
        {
            List<List<BoardGame>> board = game.getBoard();
            Point result = new Point { X = -1, Y = -1 };
            for (int col = 0; col < board.Count; col++)
            {
                bool winningCol = true;
                for (int row = 1; row < board.Count; row++)
                {
                    try
                    {
                        winningCol = board[row - 1][col].getWinner() == board[row][col].getWinner();
                    }
                    catch (NoWinnerException)
                    {
                        winningCol = false;
                        break;
                    }
                }
                if (winningCol)
                {
                    result = new Point { X=0, Y=col };
                }
            }
            return result;
        }
    }
}
