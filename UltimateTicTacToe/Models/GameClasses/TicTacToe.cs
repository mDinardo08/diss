using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Models.GameClasses
{
    public class TicTacToe : Game
    {
        public List<List<Game>> board;
        public List<Player> players;

        public void makeMove(Move move)
        {
            throw new NotImplementedException();
        }

        public List<List<Game>> getGame()
        {
            return board;
        }


        public Game getSector(Point point)
        {
            return board[point.X][point.Y];
        }

        public Player getWinner()
        {
            WinCheckHandler handler = new HorizontalWinChecker();
            Player result = handler.checkForWin(this);
            if (result == null)
            {
                throw new NoWinnerException();
            }
            return result;
        }

        public void setBoard(List<List<Game>> board)
        {
            this.board = board;
        }
    }
}
