using Microsoft.VisualStudio.TestTools.UnitTesting;
using FederalMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FederalMap.Tests
{
    [TestClass()]
    public class CityTests
    {
        [TestMethod()]
        public void LoadCitiesFromDatabaseTest()
        {
            // Arrange
            List<City> expectedCities = new List<City>
            {
                new City("г", "Калининград", "обл", "Калининградская", "Северо-Западный"),
                new City("г", "Таруса", "обл", "Калужская", "Центральный"),
                new City("г", "Владивосток", "край", "Приморский", "Дальневосточный"),
                new City("г", "Горно-Алтайск", "респ", "Алтай", "Сибирский")
            };
            List<City> actualCities = new List<City>();

            // Act
            actualCities = City.LoadCitiesFromDatabase("test_cities.db");

            // Assert
            Assert.AreEqual(expectedCities.Count, actualCities.Count);

            for (int i = 0; i < expectedCities.Count; i++)
            {
                Assert.AreEqual(expectedCities[i].CityType, actualCities[i].CityType);
                Assert.AreEqual(expectedCities[i].CityName, actualCities[i].CityName);
                Assert.AreEqual(expectedCities[i].RegionType, actualCities[i].RegionType);
                Assert.AreEqual(expectedCities[i].RegionName, actualCities[i].RegionName);
                Assert.AreEqual(expectedCities[i].FederalDistrict, actualCities[i].FederalDistrict);
            }
        }
    }
}