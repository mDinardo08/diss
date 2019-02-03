using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Services
{
    public class RandomService : IRandomService
    {
        public Random random = new Random();

        public int getRandomNumber()
        {
            return random.Next();
        }

        public int getRandomNumberBetween(int low, int high)
        {
            return random.Next(low, high);
        }
    }
}
