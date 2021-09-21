using NUnit.Framework;
using System;

namespace IfScooters.Test
{
    class RideHistoryTest
    {
        [Test]
        public void RideHistoryConstructor_CreateNewInstance_ReturnsNewInstance()
        {
            //Arrange
            var expectedTurnover = 0;
            var expectedId = "123";

            //Act
            var actual = new RideHistory("123");
            //Assert
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedTurnover, actual.Turnover);
        }

        [Test]
        public void CalculateScooterRideTime_LessThanOneDay_ReturnsCorrectTime()
        {
            //Arrange
            var minutesAdded = 10;
            var daysAdded = 0;

            var compare = new RideHistory("541");
            var comparableMinute = DateTime.Now;
            var inputMinute = comparableMinute + TimeSpan.FromMinutes(minutesAdded);
            var comparableDay = DateTime.Now;
            var inputDay = comparableDay + TimeSpan.FromDays(daysAdded);
            var inputHour = DateTime.Now.Hour;
            var inputMonth = DateTime.Now.Month;
            var inputYear = DateTime.Now.Year;

            //Act
            compare.EndDateTime = new DateTime(inputYear, inputMonth, inputDay.Day, inputHour, inputMinute.Minute, 00);
            compare.CalculateScooterRideTime();
            //Assert
            Assert.AreEqual(minutesAdded, compare.RideMinutes);
            Assert.AreEqual(daysAdded, compare.RideDays);
        }

        [Test]
        public void CalculateScooterRideTime_TwoDays_ReturnsCorrectTime()
        {
            //Arrange
            var minutesAdded = 15;
            var daysAdded = 2;
            var compare = new RideHistory("123");
            var comparableMinute = DateTime.Now;
            var inputMinute = comparableMinute + TimeSpan.FromMinutes(minutesAdded);
            var comparableDay = DateTime.Now;
            var inputDay = comparableDay + TimeSpan.FromDays(daysAdded);
            var inputHour = DateTime.Now.Hour;
            var inputMonth = DateTime.Now.Month;
            var inputYear = DateTime.Now.Year;

            //Act
            compare.EndDateTime = new DateTime(inputYear, inputMonth, inputDay.Day, inputHour, inputMinute.Minute, 00);
            compare.CalculateScooterRideTime();
            //Assert
            Assert.AreEqual(minutesAdded, compare.RideMinutes);
            Assert.AreEqual(daysAdded, compare.RideDays);
        }

        [Test]
        public void CalculateScooterTurnover_TwoFullDaysAndSomeMinutes_ReturnsCorrectTurnover()
        {
            //Arrange
            var minutesAdded = 15;
            var daysAdded = 2;
            var compare = new RideHistory("123");
            var comparableMinute = DateTime.Now;
            var inputMinute = comparableMinute + TimeSpan.FromMinutes(minutesAdded);
            var comparableDay = DateTime.Now;
            var inputDay = comparableDay + TimeSpan.FromDays(daysAdded);
            var inputHour = DateTime.Now.Hour;
            var inputMonth = DateTime.Now.Month;
            var inputYear = DateTime.Now.Year;
            var p = new ScooterService();
            var s = new ScooterRent(p);

            //Act
            p.AddScooter("123", 1);
            s.StartRent("123");
            var q = p.GetScooterById("123");

            compare.EndDateTime = new DateTime(inputYear, inputMonth, inputDay.Day, inputHour, inputMinute.Minute, 00);

            compare.CalculateScooterRideTime();
            compare.CalculateScooterTurnover(q);

            //Assert
            Assert.AreEqual(55, compare.Turnover);
        }

        [Test]
        public void CalculateScooterTurnover_15MinutesAndSmallPrice_ReturnsCorrectTurnover()
        {
            //Arrange
            var minutesAdded = 15;
            var daysAdded = 0;

            decimal pricePerMinute = 0.23m;
            var expected = pricePerMinute * minutesAdded;
            var compare = new RideHistory("123");
            var comparableMinute = DateTime.Now;
            var inputMinute = comparableMinute + TimeSpan.FromMinutes(minutesAdded);
            var comparableDay = DateTime.Now;
            var inputDay = comparableDay + TimeSpan.FromDays(daysAdded);
            var inputHour = DateTime.Now.Hour;
            var inputMonth = DateTime.Now.Month;
            var inputYear = DateTime.Now.Year;
            var p = new ScooterService();
            var s = new ScooterRent(p);

            //Act
            p.AddScooter("123", pricePerMinute);
            s.StartRent("123");
            var q = p.GetScooterById("123");

            compare.EndDateTime = new DateTime(inputYear, inputMonth, inputDay.Day, inputHour, inputMinute.Minute, 00);

            compare.CalculateScooterRideTime();
            compare.CalculateScooterTurnover(q);

            //Assert
            Assert.AreEqual(expected, compare.Turnover);
        }
    }
}
