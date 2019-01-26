﻿using Newtonsoft.Json;
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
        public Point2D boardFilter;

        public TicTacToe(IWinChecker winChecker)
        {
            this.winChecker = winChecker;
        }

        public override void makeMove(Move move)
        {
            validateBoard(move);
            board[move.possition.X][move.possition.Y].makeMove(move.next);
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
                BoardGame subBoard = getSector(boardFilter);
                try
                {
                    subBoard.getWinner();
                    availableMoves = subBoard.getAvailableMoves();
                }
                catch
                {
                    availableMoves = getMovesFromAllSubBoards();
                }
                availableMoves = availableMoves.Count > 0 ? availableMoves : getMovesFromAllSubBoards();
                availableMoves = nestMoves(availableMoves);
            }
            else if (winChecker.checkForWin(this) == null)
            {
                availableMoves = getMovesFromAllSubBoards();
            }
            return availableMoves;
        }

        public override void validateBoard(Move move)
        {
            if (move.next != null)
            {
                boardFilter = move.next.possition;
                board[move.possition.X][move.possition.Y].validateBoard(move.next);
                                 }
            owner = winChecker.checkForWin(this);
        }

        public Point2D getBoardFilter()
        {
            return boardFilter;
        }

        private List<Move> getMovesFromAllSubBoards()
        {
            List<Move> result = new List<Move>();
            for (int x = 0; x < board.Count; x++)
            {
                for (int y = 0; y < board[x].Count; y++)
                {
                    List<Move> subMoves = board[x][y].getAvailableMoves();
                    subMoves.ForEach((Move m) =>
                        result.Add(new Move
                        {
                            next = m,
                            possition = new Point2D { X = x, Y = y }
                        })
                    );
                }
            }
            return result;
        }

        private List<Move> nestMoves(List<Move> moves)
        {
            List<Move> result = new List<Move>();
            moves.ForEach((m) =>
            {
                result.Add(new Move
                {
                    possition = boardFilter,
                    next = m
                });
            });
            return result;
        }
    }
}