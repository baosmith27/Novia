using Entities;
using Enums;
using Factories;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingChargeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Novia Parking Charge Calculator");

            var parkingStrategies = GetShortStayStrategy().Concat(GetLongStayStrategy());
            var carPark = CarParkFactory.Create(parkingStrategies);

            //·  A short stay from 07 / 09 / 2017 16:50:00 to 09 / 09 / 2017 19:15:00 would cost £12.28
            var startTime = new DateTime(2017, 9, 7, 16, 50, 0);
            var endTime = new DateTime(2017, 9, 9, 19, 15, 0);
            var charge = carPark.CalculateParkingCharge(ParkingStrategyType.ShortStay, startTime, endTime);
            Console.WriteLine($"Calculation for short term parking from {startTime.ToString("dd/MMM/yyyy HH:mm")} to {endTime.ToString("dd/MMM/yyyy HH:mm")} is: {charge.ToString("#.#0")}");

            //·  A long stay from 07 / 09 / 2017 07:50:00 to 09 / 09 / 2017 05:20:00 would cost £22.50
            startTime = new DateTime(2017, 9, 7, 07, 50, 0);
            endTime = new DateTime(2017, 9, 9, 05, 20, 0);
            charge = carPark.CalculateParkingCharge(ParkingStrategyType.LongStay, startTime, endTime);
            Console.WriteLine($"Calculation for long term parking from {startTime.ToString("dd/MMM/yyyy HH:mm")} to {endTime.ToString("dd/MMM/yyyy HH:mm")} is: {charge.ToString("#.#0")}");

            Console.WriteLine("\n\n\nPress eny key to EXIT");
            Console.ReadLine();
        }

        private static IEnumerable<KeyValuePair<ParkingStrategyType, IParkingStrategy>> GetShortStayStrategy()
        {
            var strategy = ParkingStrategyFactory.Create(ParkingStrategyType.ShortStay, 8, 0, 18, 0, 1.1, ChargingUnit.Hour, GetParkingIntervalCalculator());
            yield return new KeyValuePair<ParkingStrategyType, IParkingStrategy>(ParkingStrategyType.ShortStay, strategy);
        }

        private static IEnumerable<KeyValuePair<ParkingStrategyType, IParkingStrategy>> GetLongStayStrategy()
        {
            var strategy = ParkingStrategyFactory.Create(ParkingStrategyType.LongStay, 0, 0, 0, 0, 7.5, ChargingUnit.Day, GetParkingIntervalCalculator());
            yield return new KeyValuePair<ParkingStrategyType, IParkingStrategy>(ParkingStrategyType.LongStay, strategy);
        }

        private static IParkingIntervalCalculator GetParkingIntervalCalculator()
        {
            return new ParkingIntervalCalculator();
        }
    }
}
