using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.Game.Players;
using UltimateTicTacToe.Models.Game.WinCheck;

namespace UltimateTicTacToe.Models.Game
{
    public class TicTacToe : CompositeGame
    {
        private IWinChecker winChecker;
        private Point2D boardFilter;

        public TicTacToe(IWinChecker winChecker)
        {
            this.winChecker = winChecker;
        }

        public override void makeMove(Move move)
        {
            board[move.possition.X][move.possition.Y].makeMove(move.next);
            boardFilter = move.next.possition;
        }

        public override Player getWinner()
        {
            owner = owner == null ? winChecker.checkForWin(this) : owner;
            if (owner == null)
            {
                throw new NoWinnerException();
            }
            return owner;
        }
        
        public override List<Move> getAvailableMoves()
        {
            List<Move> availableMoves = new List<Move>();
            if (boardFilter != null)
            {
                List<Move> subBoardMoves = getSector(boardFilter).getAvailableMoves();
                availableMoves = subBoardMoves.Count > 0 ? subBoardMoves : getMovesFromAllSubBoards();
            }
            else if (winChecker.checkForWin(this) == null)
            {
                availableMoves = getMovesFromAllSubBoards();
            }
            return availableMoves;
        }

        private List<Move> getMovesFromAllSubBoards()
        {
            List<Move> result = new List<Move>();
            for (int y = 0; y < board.Count; y++)
            {
                for (int x = 0; x < board[y].Count; x++)
                {
                    List<Move> subMoves = board[y][x].getAvailableMoves();
                    subMoves.ForEach((Move m) => result.Add(new Move
                    {
                        next = m,
                        possition = new Point2D { X = x, Y = y }
                    }));
                }
            }
            return result;
        }
    }
}
