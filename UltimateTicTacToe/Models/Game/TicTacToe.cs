﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToe.Models.Game
{
    public class TicTacToe : CompositeGame
    {
        public List<List<BoardGame>> board;
        public IWinChecker winChecker;

        public TicTacToe(IWinChecker winChecker)
        {
            this.winChecker = winChecker;
        }

        public void makeMove(Move move)
        {
            board[move.possition.X][move.possition.Y].makeMove(move.next);
        }

        public List<List<BoardGame>> getBoard()
        {
            return board;
        }


        public BoardGame getSector(Point2D point)
        {
            return board[point.X][point.Y];
        }

        public Player getWinner()
        {
            Player result = winChecker.checkForWin(this);
            if (result == null)
            {
                throw new NoWinnerException();
            }
            return result;
        }

        public void setBoard(List<List<BoardGame>> board)
        {
            this.board = board;
        }
    }
}
