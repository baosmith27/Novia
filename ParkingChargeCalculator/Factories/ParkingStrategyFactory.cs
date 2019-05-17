using Entities.ParkingStrategies;
using Enums;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factories
{
    public static class ParkingStrategyFactory
    {
        public static IParkingStrategy Create(ParkingStrategyType strategyType, int startHours, int startMinutes,
            int endHours, int endMinutes, double charge, ChargingUnit chargingUnit, IParkingIntervalCalculator parkingIntervalCalculator)
        {
            switch (strategyType)
            {
                case ParkingStrategyType.LongStay:return new LongStayParkingStrategy(startHours, startMinutes, endHours, endMinutes, charge, chargingUnit, parkingIntervalCalculator);
                case ParkingStrategyType.ShortStay: return new ShortStayParkingStrategy(startHours, startMinutes, endHours, endMinutes, charge, chargingUnit, parkingIntervalCalculator);
                default: return null;
            }
        }
    }
}
