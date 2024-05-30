using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FederalMap
{
    /// <summary>
    /// Представляет город с различными атрибутами, такими как тип города, название, тип региона, название региона и федеральный округ.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Тип города.
        /// </summary>
        public string cityType;

        /// <summary>
        /// Название города.
        /// </summary>
        public string cityName;

        /// <summary>
        /// Тип региона.
        /// </summary>
        public string regionType;

        /// <summary>
        /// Название региона.
        /// </summary>
        public string regionName;

        /// <summary>
        /// Название федерального округа.
        /// </summary>
        public string federalDistrict;

        /// <summary>
        /// Инициализирует новый экземпляр класса City.
        /// </summary>
        public City(string cityType, string cityName, string regionType, string regionName, string federalDistrict)
        {
            this.cityType = cityType;
            this.cityName = cityName;
            this.regionType = regionType;
            this.regionName = regionName;
            this.federalDistrict = federalDistrict;
        }

        /// <summary>
        /// Получает или задает тип города.
        /// </summary>
        public string CityType { get => cityType; set => cityType = value; }

        /// <summary>
        /// Получает или задает название города.
        /// </summary>
        public string CityName { get => cityName; set => cityName = value; }

        /// <summary>
        /// Получает или задает тип региона.
        /// </summary>
        public string RegionType { get => regionType; set => regionType = value; }

        /// <summary>
        /// Получает или задает название региона.
        /// </summary>
        public string RegionName { get => regionName; set => regionName = value; }

        /// <summary>
        /// Получает или задает название федерального округа.
        /// </summary>
        public string FederalDistrict { get => federalDistrict; set => federalDistrict = value; }

        /// <summary>
        /// Определяет, равен ли заданный объект текущему объекту.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is City city &&
                   cityType == city.cityType &&
                   cityName == city.cityName &&
                   regionType == city.regionType &&
                   regionName == city.regionName &&
                   federalDistrict == city.federalDistrict &&
                   FederalDistrict == city.FederalDistrict;
        }

        /// <summary>
        /// Служит хэш-функцией по умолчанию.
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = -1788374202;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(cityType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(cityName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(regionType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(regionName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(federalDistrict);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FederalDistrict);
            return hashCode;
        }

        public static List<City> LoadCitiesFromDatabase(string path)
        {
            List<City> x = new List<City>();
            SQLiteConnection connection = new SQLiteConnection("Data Source =" + path);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT city_type, city, region_type, region, federal_district FROM cities";

            // Читаем данные из базы данных и добавляем их в список cities
            using (var rd1 = command.ExecuteReader())
            {
                while (rd1.Read())
                {
                    x.Add(new City(rd1.GetString(0), rd1.GetString(1), rd1.GetString(2), rd1.GetString(3), rd1.GetString(4)));
                }
            }
            connection.Close();
            return x;
        }
    }
}
