﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTicTacToe.Services
{
    public interface IRandomService
    {
        int getRandomNumber();

        int getRandomNummberBetween(int low, int high);
    }
}
