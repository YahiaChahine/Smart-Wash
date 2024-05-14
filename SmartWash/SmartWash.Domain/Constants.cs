using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWash.Domain
{
    public static class Constants
    {
        public const float CycleDurationMinutes = 60;

        public static readonly TimeSpan OpeningTime = new(8, 0, 0);
        public static readonly TimeSpan ClosingTime = new(20, 0, 0);
        public static readonly TimeSpan SlotDuration = new(0,1,0,0);

        public const decimal WashingMachinePrice = 5;
        public const decimal DryingMachinePrice = 3;
    }
}
