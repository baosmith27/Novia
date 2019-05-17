using System;
using Entities;
using Enums;
using Factories;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestParkingChargeCalculator
{
    [TestClass]
    public class UnitTestLongStay
    {
        [TestMethod]
        public void Calculate_Part1Day_Succeeds()
        {
            var strategy = GetLongStayStrategy(7.5);
            var startParkingPeriod = new DateTime(2019,5,17,7,0,0);
            var endParkingPeriod = new DateTime(2019, 5, 17, 8, 0, 0);
            var parkingCharge = strategy.CalculateCharge(startParkingPeriod, endParkingPeriod);
            Assert.AreEqual(7.5, parkingCharge);
        }

        [TestMethod]
        public void Calculate_ExactNextDay_Succeeds()
        {
            var strategy = GetLongStayStrategy(7.5);
            var startParkingPeriod = new DateTime(2019, 5, 17, 7, 0, 0);
            var endParkingPeriod = new DateTime(2019, 5, 18, 7, 0, 0);
            var parkingCharge = strategy.CalculateCharge(startParkingPeriod, endParkingPeriod);
            Assert.AreEqual(15, parkingCharge);
        }

        [TestMethod]
        public void Calculate_EarlierTimeNextDay_Succeeds()
        {
            var strategy = GetLongStayStrategy(7.5);
            var startParkingPeriod = new DateTime(2019, 5, 17, 7, 0, 0);
            var endParkingPeriod = new DateTime(2019, 5, 18, 6, 0, 0);
            var parkingCharge = strategy.CalculateCharge(startParkingPeriod, endParkingPeriod);
            Assert.AreEqual(15, parkingCharge);
        }

        [TestMethod]
        public void Calculate_LaterTimeNextDay_Succeeds()
        {
            var strategy = GetLongStayStrategy(7.5);
            var startParkingPeriod = new DateTime(2019, 5, 17, 7, 0, 0);
            var endParkingPeriod = new DateTime(2019, 5, 18, 8, 0, 0);
            var parkingCharge = strategy.CalculateCharge(startParkingPeriod, endParkingPeriod);
            Assert.AreEqual(15, parkingCharge);
        }

        [TestMethod]
        //·  A long stay from 07/09/2017 07:50:00 to 09/09/2017 05:20:00 would cost £22.50
        public void Calculate_NoviaExample_Succeeds()
        {
            var strategy = GetLongStayStrategy(7.5);
            var startParkingPeriod = new DateTime(2017, 9, 7, 7, 50, 0);
            var endParkingPeriod = new DateTime(2017, 9, 9, 5, 20, 0);
            var parkingCharge = strategy.CalculateCharge(startParkingPeriod, endParkingPeriod);
            Assert.AreEqual(22.5, parkingCharge);
        }

        private IParkingStrategy GetLongStayStrategy(double charge)
        {
            return ParkingStrategyFactory.Create(ParkingStrategyType.LongStay, 0, 0, 0, 0, charge, ChargingUnit.Day, GetParkingIntervalCalculator());            
        }

        private IParkingIntervalCalculator GetParkingIntervalCalculator()
        {
            return new ParkingIntervalCalculator();
        }
    }
}
