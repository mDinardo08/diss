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
            for (int i  = 0; i < board.Count; i++)
            {
                bool winningRow = true;
                for (int j = 1; j < board.Count; j++)
                {
                    try
                    {
                        winningRow = board[i][j - 1].getWinner().getColour() == board[i][j].getWinner().getColour();
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
