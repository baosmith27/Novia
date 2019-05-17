using Interfaces;
using System;

namespace Entities
{
    public class ParkingIntervalCalculator : IParkingIntervalCalculator
    {
        public int NumberOfPartDays(DateTime start, DateTime end)
        {
            if (end > start)
            {
                //if dates are on the same day then return 1 day
                if (start.Date == end.Date)
                {
                    return 1;
                }
                else
                {
                    var numberOfDays = 1;
                    //get midnight the next day
                    var tmpStartDate = new DateTime(start.Year, start.Month, start.Day).AddDays(1);
                    var interval = (end - tmpStartDate);
                    numberOfDays += (int)Math.Ceiling(interval.TotalDays);
                    
                    return numberOfDays;
                }
            }
            else
            {
                return 0;
            }
        }

        public double NumberOfHours(DateTime start, DateTime end, int chargingStartHours, int chargingEndHours)
        {
            var numberOfHours = 0d;

            var chargingStartTime = new DateTime(start.Year, start.Month, start.Day, start.Hour > chargingStartHours ? start.Hour : chargingStartHours, start.Hour >= chargingStartHours ? start.Minute : 0, 0);
            //move to the beginning of the closest chargeable day
            if (chargingStartTime.DayOfWeek == DayOfWeek.Saturday || chargingStartTime.DayOfWeek == DayOfWeek.Sunday)
            {
                chargingStartTime = new DateTime(chargingStartTime.Year, chargingStartTime.Month, chargingStartTime.Day, chargingStartHours, 0, 0);
                while (chargingStartTime.DayOfWeek == DayOfWeek.Saturday || chargingStartTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    chargingStartTime = chargingStartTime.AddDays(1);
                    
                }
            }
            //get the end of the charging period
            var chargingEndTime = new DateTime(end.Year, end.Month, end.Day, end.Hour < chargingEndHours ? end.Hour : chargingEndHours, 0, 0);
            //move to end of the closest previous chargeable day
            if (chargingEndTime.DayOfWeek == DayOfWeek.Saturday || chargingEndTime.DayOfWeek == DayOfWeek.Sunday)
            {
                chargingEndTime = new DateTime(chargingEndTime.Year, chargingEndTime.Month, chargingEndTime.Day, chargingEndHours, 0, 0);
                while (chargingEndTime.DayOfWeek == DayOfWeek.Saturday || chargingEndTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    chargingEndTime = chargingEndTime.AddDays(-1);
                }
            }
            //if the end is earlier than the start then no charge it must have been a weekend
            if(chargingEndTime < chargingStartTime)
            {
                numberOfHours = 0;
            }
            else
            {
                var numberOfHoursPerDayChargeable = chargingEndHours - chargingStartHours;
                while(chargingStartTime < chargingEndTime)
                {
                    if(chargingStartTime.Date == chargingEndTime.Date)
                    {
                        var partialHours = (chargingEndTime - chargingStartTime).TotalHours;
                        numberOfHours += partialHours;
                        break;
                    }
                    else
                    {
                        numberOfHours += numberOfHoursPerDayChargeable;
                        chargingStartTime = chargingStartTime.AddDays(1);
                    }
                }
            }
            return numberOfHours;
        }
    }
}
