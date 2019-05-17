using Entities;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTestParkingIntervalCalculator
    {
        [TestMethod]
        public void Create_Succeeds()
        {
            Assert.IsNotNull(GetParkingIntervalCalculator(), "Calculator was null");
        }

        [TestMethod]
        public void DaysBetweenDates_SameDay_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startDate = new DateTime(2019, 5, 17, 7, 50, 00);
            var endDate = new DateTime(2019, 5, 17, 8, 00, 00);
            var days = calculator.NumberOfPartDays(startDate, endDate);

            Assert.AreEqual(1, days, "Should be 1 part day between dates");
        }

        [TestMethod]
        public void DaysBetweenDates_NextDay_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startDate = new DateTime(2019, 5, 17, 7, 50, 00);
            var endDate = new DateTime(2019, 5, 18, 8, 00, 00);
            var days = calculator.NumberOfPartDays(startDate, endDate);

            Assert.AreEqual(2, days, "Should be 2 part days between dates");
        }

        [TestMethod]
        public void DaysBetweenDates_NextDay_SameTime_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startDate = new DateTime(2019, 5, 17, 7, 50, 00);
            var endDate = new DateTime(2019, 5, 18, 7, 50, 00);
            var days = calculator.NumberOfPartDays(startDate, endDate);

            Assert.AreEqual(2, days, "Should be 2 part days between dates");
        }

        [TestMethod]
        public void HoursBetween_SameDay_WithinChargingPeriod_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startTime = new DateTime(2019, 5, 17, 9, 00, 00);
            var endTime = new DateTime(2019, 5, 17, 11, 0, 00);
            var hours = calculator.NumberOfHours(startTime, endTime, 8, 18);

            Assert.AreEqual(2, hours, "Should be 2 hours");
        }

        [TestMethod]
        public void Weekend_HoursBetween_SameDay_WithinChargingPeriod_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startTime = new DateTime(2019, 5, 18, 9, 00, 00);
            var endTime = new DateTime(2019, 5, 18, 11, 0, 00);
            var hours = calculator.NumberOfHours(startTime, endTime, 8, 18);

            Assert.AreEqual(0, hours, "Should be 0 hours");
        }

        [TestMethod]
        public void HoursBetween_SameDay_EarlierThanChargingPeriod_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startTime = new DateTime(2019, 5, 17, 7, 00, 00);
            var endTime = new DateTime(2019, 5, 17, 8, 0, 00);
            var hours = calculator.NumberOfHours(startTime, endTime, 8, 18);

            Assert.AreEqual(0, hours, "Should be 0 hours");
        }

        [TestMethod]
        public void HoursBetween_SameDay_LaterThanChargingPeriod_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startTime = new DateTime(2019, 5, 17, 18, 00, 00);
            var endTime = new DateTime(2019, 5, 17, 19, 0, 00);
            var hours = calculator.NumberOfHours(startTime, endTime, 8, 18);

            Assert.AreEqual(0, hours, "Should be 0 hours");
        }

        [TestMethod]
        public void HoursBetween_TwoDay_BeginsEarlierThanChargingPeriod_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startTime = new DateTime(2019, 5, 16, 7, 00, 00);
            var endTime = new DateTime(2019, 5, 17, 9, 0, 00);
            var hours = calculator.NumberOfHours(startTime, endTime, 8, 18);

            Assert.AreEqual(11, hours, $"Should be 11 hours but got {hours}");
        }

        [TestMethod]
        public void HoursBetween_TwoDay_BeginsLaterThanChargingPeriod_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startTime = new DateTime(2019, 5, 16, 9, 00, 00);
            var endTime = new DateTime(2019, 5, 17, 9, 0, 00);
            var hours = calculator.NumberOfHours(startTime, endTime, 8, 18);

            Assert.AreEqual(10, hours, $"Should be 10 hours but got {hours}");
        }

        [TestMethod]
        //·  A short stay from 07/09/2017 16:50:00 to 09/09/2017 19:15:00 
        // note the 9th is a saturday
        public void Novia_Example_Succeeds()
        {
            var calculator = GetParkingIntervalCalculator();
            var startTime = new DateTime(2017, 9, 7, 16, 50, 00);
            var endTime = new DateTime(2017, 9, 9, 19, 15, 00);
            var hours = calculator.NumberOfHours(startTime, endTime, 8, 18);
            double expectedHours = 11 + ((double)10 / (double)60);
            Assert.AreEqual(expectedHours, hours, $"Should be {expectedHours} hours but got {hours}");
        }

        private IParkingIntervalCalculator GetParkingIntervalCalculator()
        {
            return new ParkingIntervalCalculator();
        }
    }
}
