﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Domain
{
    public static class Constants
    {
        public const float CycleDurationMinutes = 60;

        public static readonly TimeOnly OpeningTime = new(8, 0);
        public static readonly TimeOnly ClosingTime = new(20, 0);
        public static readonly TimeSpan SlotDuration = new(0,1,0,0);
    }
}
