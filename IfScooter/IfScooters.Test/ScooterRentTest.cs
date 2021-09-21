using NUnit.Framework;
using System;

namespace IfScooters.Test
{
    public class ScooterRentTest
    {
        [Test]
        public void StartRent_RentANewScooter_ReturnsTrue()
        {
            //Arrange
            var p = new ScooterService(); // nevar caur interface, jo tur nav paredzēts konstruktors ar ScooterService argumentu
            IRentalCompany s = new ScooterRent(p);

            //Act
            p.AddScooter("123", 2);
            s.StartRent("123");
            var q = p.GetScooterById("123");

            //Assert
            Assert.IsTrue(q.IsRented);
        }

        [Test]
        public void EndRent_ScooterIsAvailable_ReturnsIsRentedFalse()
        {
            //Arrange
            var p = new ScooterService();
            IRentalCompany s = new ScooterRent(p);

            //Act
            p.AddScooter("123", 2);
            s.StartRent("123");
            var q = p.GetScooterById("123");
            s.EndRent("123");

            //Assert
            Assert.IsFalse(q.IsRented);

        }
        [Test]
        public void EndRent_ScooterIsGivenBackToCalculateTurnover_ReturnsExpectedAmount()
        {
            //Arrange
            var p = new ScooterService();
            IRentalCompany s = new ScooterRent(p);
            var timeDiff = 15;
            var pricePerMinute = 0.2m;
            var expected = pricePerMinute * timeDiff;

            //Act
            p.AddScooter("123", pricePerMinute);
            s.StartRent("123");
            var q = p.GetHistoryById("123").StartDateTime = DateTime.Now - TimeSpan.FromMinutes(timeDiff);

            var actualTurnover = s.EndRent("123");

            //Assert
            Assert.AreEqual(expected, actualTurnover);
        }

        [Test]
        public void CalculateIncome_5ScootersEnded_ShouldReturnTotalTurnover()
        {
            //Arrange
            var p = new ScooterService();
            IRentalCompany s = new ScooterRent(p);
            var timeDiff = 15;
            var pricePerMinute = 0.2m;
            var expected = 5*(pricePerMinute * timeDiff);

            //Act
            p.AddScooter("123", pricePerMinute);
            s.StartRent("123");
            p.GetHistoryById("123").StartDateTime = DateTime.Now - TimeSpan.FromMinutes(timeDiff);
            p.AddScooter("134", pricePerMinute);
            s.StartRent("134");
            p.GetHistoryById("134").StartDateTime = DateTime.Now - TimeSpan.FromMinutes(timeDiff);
            p.AddScooter("156", pricePerMinute);
            s.StartRent("156");
            p.GetHistoryById("156").StartDateTime = DateTime.Now - TimeSpan.FromMinutes(timeDiff);
            p.AddScooter("181", pricePerMinute);
            s.StartRent("181");
            p.GetHistoryById("181").StartDateTime = DateTime.Now - TimeSpan.FromMinutes(timeDiff);
            p.AddScooter("222", pricePerMinute);
            s.StartRent("222");
            p.GetHistoryById("222").StartDateTime = DateTime.Now - TimeSpan.FromMinutes(timeDiff);

            var actualTurnover = s.CalculateIncome(1, true);

            //Assert
            Assert.AreEqual(expected, actualTurnover);
        }

        [Test]
        public void CalculateIncome_RentedOutScootersAreNotIncluded_ShouldReturnTotalTurnover()
        {
            //Arrange
            var p = new ScooterService();
            IRentalCompany s = new ScooterRent(p);

            //Act
            p.AddScooter("123", 2);
            s.StartRent("123");
            p.AddScooter("134", 2);
            s.StartRent("134");
            p.AddScooter("156", 2);
            s.StartRent("156");
            p.AddScooter("181", 2);
            s.StartRent("181");
            p.AddScooter("222", 2);
            s.StartRent("222");

            var actualTurnover = s.CalculateIncome(1, false);

            //Assert
            Assert.AreEqual(0, actualTurnover);
        }

        [Test]
        public void CompanyNameProperty_IsCalledByGet_ReturnsChangedProperty()
        {
            //Arrange
            var p = new ScooterService();
            IRentalCompany s = new ScooterRent(p) { Name = "Bolt" };

            //Act

            //Assert
            Assert.AreEqual("Bolt", s.Name);
        }
    }
}
