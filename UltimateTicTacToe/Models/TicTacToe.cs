using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models
{
    public class TicTacToe : Game
    {
        public List<List<Game>> game;
        public List<Player> players;

        public Player getWinner()
        {
            Player result = checkHorizontalWinner();
            if (result == null)
            {
                throw new NoWinnerException();
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
