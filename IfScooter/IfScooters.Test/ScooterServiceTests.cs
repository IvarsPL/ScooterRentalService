using NUnit.Framework;

namespace IfScooters.Test
{
    public class ScooterServiceTests
    {
        [Test]
        public void AddScooter_AddNewScooter_ReturnsTrue()
        {
            //Arrange
            var equals = new Scooter("145", 2); //new instance of scooter
            var myList = new ScooterService();

            //Act
            myList.AddScooter("145", 2);
            var scooter = myList.GetScooterById("145");

            //Assert
            Assert.AreEqual(equals.Id, scooter.Id);
            Assert.AreEqual(equals.PricePerMinute, scooter.PricePerMinute);
        }


        [Test]
        public void RemoveScooter_ReturnsTrue()
        {
            //Arrange
            var myList = new ScooterService();
            //Act
            myList.AddScooter("145", 2);
            myList.AddScooter("1523", 2);
            myList.AddScooter("1583", 2);
            myList.RemoveScooter("145");

            //Assert
            Assert.Throws<MyException>(() => myList.GetScooterById("145"));
        }

        [Test]
        public void ScooterServiceProperties_Initialize_ReturnsTrue()
        {
            //Arrange
            var myList = new ScooterService { Id = "123", PricePerMinute = 23 };
            //Act
            //Assert
            Assert.AreEqual("123", myList.Id);
            Assert.AreEqual(23, myList.PricePerMinute);
        }

        [Test]
        public void GetScooterById_YearEndResults_ReturnsTrue()
        {
            //Arrange
            var myList = new ScooterService();
            //Act
            myList.AddScooter("213", 23);
            var result = myList.GetScooters();
            var actual = result[0].Id;
            //Assert
            Assert.AreEqual("213", actual);
        }
    }
}