using System;
using Entities;
using Enums;
using Factories;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestParkingChargeCalculator
{
    [TestClass]
    public class UnitTestShortStay
    {                
        //·  A short stay from 07/09/2017 16:50:00 to 09/09/2017 19:15:00 = £12.28
        // note the 9th is a saturday
        public void Novia_Example_Succeeds()
        {
            var strategy = GetShortStayStrategy(7.5);
            var startTime = new DateTime(2017, 9, 7, 16, 50, 00);
            var endTime = new DateTime(2017, 9, 9, 19, 15, 00);
            var parkingCharge = strategy.CalculateCharge(startTime, endTime);
            var expectedCharge = 12.28f;

            Assert.AreEqual(expectedCharge, parkingCharge, $"Expected {expectedCharge} but got {parkingCharge}");            
        }

        private IParkingStrategy GetShortStayStrategy(double charge)
        {
            return ParkingStrategyFactory.Create(ParkingStrategyType.ShortStay, 8, 0, 18, 0, charge, ChargingUnit.Hour, GetParkingIntervalCalculator());
        }

        private IParkingIntervalCalculator GetParkingIntervalCalculator()
        {
            return new ParkingIntervalCalculator();
        }
    }
}
