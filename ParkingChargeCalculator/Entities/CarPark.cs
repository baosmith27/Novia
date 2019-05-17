using Enums;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class CarPark : ICarPark
    {
        public IEnumerable<KeyValuePair<ParkingStrategyType, IParkingStrategy>> ParkingStrategies
        {
            get; private set;
        }

        public CarPark(IEnumerable<KeyValuePair<ParkingStrategyType, IParkingStrategy>> parkingStrategies)
        {
            ParkingStrategies = parkingStrategies;
        }

        public double CalculateParkingCharge(ParkingStrategyType parkingVisitType, DateTime startTime, DateTime endTime)
        {
            var strategy = ParkingStrategies.Where(s => s.Key == parkingVisitType).FirstOrDefault();
            return strategy.Value.CalculateCharge(startTime, endTime);
        }
    }
}
