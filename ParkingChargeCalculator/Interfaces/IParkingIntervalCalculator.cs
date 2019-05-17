using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IParkingIntervalCalculator
    {
        int NumberOfPartDays(DateTime start, DateTime end);
        double NumberOfHours(DateTime start, DateTime end, int chargingStartHours, int chargingEndHours);
    }
}
