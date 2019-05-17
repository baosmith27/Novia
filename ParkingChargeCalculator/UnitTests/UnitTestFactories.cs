using System.Collections.Generic;
using Entities;
using Enums;
using Factories;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestParkingChargeCalculator
{
    [TestClass]
    public class UnitTestFactories
    {
        [TestMethod]
        public void Get_CarPark_Succeeds()
        {
            IEnumerable<KeyValuePair<ParkingStrategyType, IParkingStrategy>> parkingStrategies = GetShortStayStrategy();
            var carPark = CarParkFactory.Create(parkingStrategies);
            Assert.IsNotNull(carPark);
        }

        [TestMethod]
        public void Get_ShortStay_ParkingStrategy_Succeeds()
        {
            var startHours = 8;
            var endHours = 19;
            var startMinutes = 10;
            var endMinutes = 11;
            var charge = 1.1;
            var chargingUnit = ChargingUnit.Hour;
            var strategy = ParkingStrategyFactory.Create(ParkingStrategyType.ShortStay, startHours,
                startMinutes, endHours, endMinutes, charge, chargingUnit, GetParkingIntervalCalculator());

            Assert.IsTrue(strategy.Unit == chargingUnit, "Charging unit should have been " + chargingUnit.ToString());
            Assert.AreEqual(strategy.ChargePerUnit, charge, "Charge per unit should have been "+ charge);
            Assert.AreEqual(strategy.ChargingPeriodStartHours, startHours, "Start Hours should have been " + startHours);
            Assert.AreEqual(strategy.ChargingPeriodStartMinutes, startMinutes, "Start Minutes should have been " + startMinutes);
            Assert.AreEqual(strategy.ChargingPeriodEndHours, endHours, "End Hours should have been " + endHours);
            Assert.AreEqual(strategy.ChargingPeriodEndMinutes, endMinutes, "End Minutes should have been " + endMinutes);

        }

        [TestMethod]
        public void Get_LongStay_ParkingStrategy_Succeeds()
        {
            var startHours = 1;
            var endHours = 23;
            var startMinutes = 10;
            var endMinutes = 11;
            var charge = 7.5;
            var chargingUnit = ChargingUnit.Day;
            var strategy = ParkingStrategyFactory.Create(ParkingStrategyType.LongStay, startHours,
                startMinutes, endHours, endMinutes, charge, chargingUnit, GetParkingIntervalCalculator());

            Assert.IsTrue(strategy.Unit == chargingUnit, "Charging unit should have been " + chargingUnit.ToString());
            Assert.AreEqual(strategy.ChargePerUnit, charge, "Charge per unit should have been " + charge);
            Assert.AreEqual(strategy.ChargingPeriodStartHours, startHours, "Start Hours should have been " + startHours);
            Assert.AreEqual(strategy.ChargingPeriodStartMinutes, startMinutes, "Start Minutes should have been " + startMinutes);
            Assert.AreEqual(strategy.ChargingPeriodEndHours, endHours, "End Hours should have been " + endHours);
            Assert.AreEqual(strategy.ChargingPeriodEndMinutes, endMinutes, "End Minutes should have been " + endMinutes);

        }

        private IEnumerable<KeyValuePair<ParkingStrategyType, IParkingStrategy>> GetShortStayStrategy()
        {
            var strategy = ParkingStrategyFactory.Create(ParkingStrategyType.ShortStay,8,0,18,0,1.1,ChargingUnit.Hour, GetParkingIntervalCalculator());
            yield return new KeyValuePair<ParkingStrategyType, IParkingStrategy>(ParkingStrategyType.ShortStay, strategy);
        }

        private IEnumerable<KeyValuePair<ParkingStrategyType, IParkingStrategy>> GetLongStayStrategy()
        {
            var strategy = ParkingStrategyFactory.Create(ParkingStrategyType.LongStay, 0, 0, 0, 0, 7.5, ChargingUnit.Day, GetParkingIntervalCalculator());
            yield return new KeyValuePair<ParkingStrategyType, IParkingStrategy>(ParkingStrategyType.LongStay, strategy);
        }

        private IParkingIntervalCalculator GetParkingIntervalCalculator()
        {
            return new ParkingIntervalCalculator();
        }
    }
}
