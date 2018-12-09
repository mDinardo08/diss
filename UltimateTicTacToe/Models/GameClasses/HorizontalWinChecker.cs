using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.Game
{
    public class HorizontalWinChecker: WinCheckHandler
    {
        public HorizontalWinChecker()
        {
            check = checkHorizontalWinner;
            successor = new VerticleWinChecker();
        }

        private Point checkHorizontalWinner(BoardGame game)
        {
            Point result = new Point { X = -1, Y = -1 };
            List<List<BoardGame>> board = game.getBoard();
            for (int i  = 0; i < board.Count; i++)
            {
                bool winningRow = true;
                for (int j = 1; j < board.Count; j++)
                {
                    try
                    {
                        winningRow = board[i][j - 1].getWinner() == board[i][j].getWinner();
                    }
                    catch (NoWinnerException)
                    {
                        winningRow = false;
                        break;
                    }
                }
                if (winningRow)
                {
                    result = new Point { X = i, Y = 0 };
                }
            }
            return result;
        }
    }
}
