using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Services
{
    public class WeylMiddleSquareService : IRandomService
    {
        public double GetSeed()
        {
            return (DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds;
        }

        public double getRandomNumber()
        {
            long s = 96087, w = 0, result = (long) GetSeed();
            for(int i = 0; i< 50; i++)
            {
                result *= result;
                result += Math.Abs(w += s);
                result = Math.Abs(result);
                result = MiddleSquare(result);
            }
            return result;
        }

        public long MiddleSquare(long v)
        {
            int MagnitudesAllowed = 12;
            int MagnitudesOver = (int)Math.Floor(Math.Log10(v)) - MagnitudesAllowed + 1;
            long result = v;
            for(;MagnitudesOver > 0; MagnitudesOver--)
            {
                if (MagnitudesOver % 2 == 1)
                {
                    result = result % (long)Math.Pow(10, Math.Floor(Math.Log10(result)));
                } else
                {
                    result /= 10;
                }
            }
            return result;
        }
    }
}
