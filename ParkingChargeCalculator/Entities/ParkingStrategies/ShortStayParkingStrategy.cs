using Enums;
using Interfaces;
using System;

namespace Entities.ParkingStrategies
{
    /*
     * This parking strategy charges hourly between certain hours and on weekdays only
    */
    public class ShortStayParkingStrategy : IParkingStrategy
    {
        private readonly IParkingIntervalCalculator _parkingIntervalCalculator;
        public ChargingUnit Unit { get; private set; }

        public double ChargePerUnit { get; private set; }

        public int ChargingPeriodStartHours { get; private set; }

        public int ChargingPeriodStartMinutes { get; private set; }

        public int ChargingPeriodEndHours { get; private set; }

        public int ChargingPeriodEndMinutes { get; private set; }

        public ShortStayParkingStrategy(int startHours, int startMinutes,
            int endHours, int endMinutes, double charge, ChargingUnit chargingUnit,
            IParkingIntervalCalculator parkingIntervalCalculator)
        {
            Unit = chargingUnit;
            ChargePerUnit = charge;
            ChargingPeriodStartHours = startHours;
            ChargingPeriodStartMinutes = startMinutes;
            ChargingPeriodEndHours = endHours;
            ChargingPeriodEndMinutes = endMinutes;
            _parkingIntervalCalculator = parkingIntervalCalculator;
        }

        public double CalculateCharge(DateTime startParkingPeriod, DateTime endParkingPeriod)
        {
            return _parkingIntervalCalculator.NumberOfHours(startParkingPeriod, endParkingPeriod, ChargingPeriodStartHours, ChargingPeriodEndHours) * ChargePerUnit;
        }
    }
}
