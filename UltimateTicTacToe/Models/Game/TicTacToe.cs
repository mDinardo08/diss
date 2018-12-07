using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public class TicTacToe : Game
    {
        public List<List<Game>> game;
        public List<Player> players;

        public Player getWinner()
        {
            Player result = checkForWinner();

            if (result == null)
            {
                throw new NoWinnerException();
            }
            return result;
        }

        private Player checkForWinner()
        {
            Player result = checkHorizontalWinner();
            if (result == null)
            {
                result = checkVerticalWinner();
            }
            if (result == null)
            {
                result = checkDiagonalWinner();
            }
            return result;
        }

        private Player checkDiagonalWinner()
        {
            Player result = checkTopLeftDiagonal();
            if (result == null)
            {
                result = checkBottomLeftDiagonal();
            }
            return result;
        }

        private Player checkBottomLeftDiagonal()
        {
            bool diagWinner = true;
            Player result = null;
            for (int i = game.Count - 1; i > 1; i--)
            {
                try
                {
                    diagWinner = game[game.Count - i - 1][i].getWinner() == game[game.Count - i][i - 1].getWinner();
                }
                catch (NoWinnerException)
                {
                    diagWinner = false;
                    break;
                }
                if (diagWinner)
                {
                    result = game[game.Count - 1][0].getWinner();
                }
            }

            return result;
        }

        private Player checkTopLeftDiagonal()
        {
            bool diagWinner = true;
            Player result = null;
            for (int i = 1; i < game.Count; i++)
            {
                try
                {
                    diagWinner = game[i - 1][i - 1].getWinner() == game[i][i].getWinner();
                }
                catch (NoWinnerException)
                {
                    diagWinner = false;
                    break;
                }
                if (diagWinner)
                {
                    result = game[0][0].getWinner();
                }
            }
            
            return result;
        }

        private Player checkVerticalWinner()
        {
            Player result = null;
            for (int col = 0; col < game.Count; col++)
            {
                bool winningCol = true;
                for (int row = 1; row < game.Count; row++)
                { 
                    try
                    {
                        winningCol = game[row - 1][col].getWinner() == game[row][col].getWinner();
                    }
                    catch (NoWinnerException)
                    {
                        winningCol = false;
                        break;
                    }
                }
                if (winningCol)
                {
                    result = game[0][col].getWinner();
                }
            }
            return result;
        }

        private Player checkHorizontalWinner()
        {
            Player result = null;
            foreach (List<Game> row in game)
            {
                bool winningRow = true;
                for (int i = 1; i < row.Count; i++)
                {
                    try
                    {
                        winningRow = row[i - 1].getWinner() == row[i].getWinner();
                    }
                    catch (NoWinnerException)
                    {
                        winningRow = false;
                        break;
                    }
                }
                if(winningRow)
                {
                    result = row[0].getWinner();
                }
            }
            return result;
        }
    }
}
