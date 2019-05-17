using Entities;
using Enums;
using Interfaces;
using System.Collections.Generic;

namespace Factories
{
    public static class CarParkFactory
    {
        public static ICarPark Create(IEnumerable<KeyValuePair<ParkingStrategyType, IParkingStrategy>> parkingStrategies)
        {
            return new CarPark(parkingStrategies);
        }
    }
}
