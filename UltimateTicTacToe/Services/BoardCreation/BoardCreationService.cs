using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTicTacToe.Models.DTOs;
using UltimateTicTacToe.Models.Game;

namespace UltimateTicTacToe.Services
{
    public interface BoardCreationService
    {
        BoardGame createBoardGame(BoardGameDTO gameDto);
        BoardGame createBoardGame(int size);
        BoardGame createBoardGame(List<List<JObject>> game);
    }
}
