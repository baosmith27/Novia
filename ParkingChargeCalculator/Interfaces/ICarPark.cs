using Enums;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICarPark
    {
        IEnumerable<KeyValuePair<ParkingStrategyType, IParkingStrategy>> ParkingStrategies
        {
            get;
        }

        double CalculateParkingCharge(ParkingStrategyType parkingVisitType, DateTime startTime, DateTime endTime);
    }
}
