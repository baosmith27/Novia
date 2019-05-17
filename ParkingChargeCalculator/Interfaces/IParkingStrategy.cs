using Enums;
using System;

namespace Interfaces
{
    public interface IParkingStrategy
    {
        ChargingUnit Unit { get; }
        double ChargePerUnit { get; }
        int ChargingPeriodStartHours { get; }
        int ChargingPeriodStartMinutes { get; }
        int ChargingPeriodEndHours { get; }
        int ChargingPeriodEndMinutes { get; }

        double CalculateCharge(DateTime startParkingPeriod, DateTime endParkingPeriod);
    }
}
