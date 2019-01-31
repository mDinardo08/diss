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

        public TicTacToe(IWinChecker winChecker)
        {
            this.winChecker = winChecker;
        }

        public override void makeMove(Move move)
        {
            board[move.possition.X][move.possition.Y].makeMove(move.next);
            validateBoard();
            registerMove(move);
        }

        public override PlayerColour? getWinner()
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
            List<Move> result = new List<Move>();
            if (!isWon())
            {
                if (boardFilter == null)
                {
                    result = getMovesFromAllSubBoards();
                }
                else
                {
                    if (getSector(boardFilter).isWon() || getSector(boardFilter).isDraw())
                    {
                        result = getMovesFromAllSubBoards();
                    }
                    else
                    {
                        result = getFilteredSubBoardMoves();
                    }
                }
            }
            return result;
        }

        public override void validateBoard()
        {
            for ( int x = 0; x < board.Count; x++)
            {
                for ( int y = 0; y < board[x].Count; y++)
                {
                    board[x][y].validateBoard();
                }
            }
            owner = owner ?? winChecker.checkForWin(this);
        }

        public Point2D getBoardFilter()
        {
            return boardFilter;
        }

        private List<Move> getFilteredSubBoardMoves()
        {
            List<Move> result = new List<Move>();
            List<Move> subMoves = getSector(boardFilter).getAvailableMoves();
            subMoves.ForEach((move) =>
            {
                result.Add(nestMove(move, boardFilter));
            });
            return result;
        }

        private Move nestMove(Move move, Point2D possiton)
        {
            return new Move
            {
                next = move,
                possition = possiton
            };
        }
        private List<Move> getMovesFromAllSubBoards()
        {
            List<Move> result = new List<Move>();
            for (int x = 0; x < board.Count; x++)
            {
                for (int y = 0; y < board[0].Count; y++)
                {
                    Point2D possition = new Point2D();
                    possition.X = x;
                    possition.Y = y;
                    board[x][y].getAvailableMoves().ForEach((move) =>
                    {
                        result.Add(nestMove(move, possition));
                    });
                }
            }
            return result;
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}