using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace FederalMap
{
    /// <summary>
    /// Главный класс формы для приложения FederalMap.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Конструктор класса Form1.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Список городов, загруженных из базы данных.
        /// </summary>
        private List<City> cities = new List<City>();

        /// <summary>
        /// Обрабатывает событие Load формы. 
        /// Инициализирует данные, загружая их из базы данных SQLite и заполняет элементы управления.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //// Создаем подключение к базе данных SQLite
            //SQLiteConnection connection = new SQLiteConnection("Data Source = cities_db.db");
            //connection.Open();
            //var command = connection.CreateCommand();
            //command.CommandText = @"SELECT city_type, city, region_type, region, federal_district FROM cities";

            //// Читаем данные из базы данных и добавляем их в список cities
            //using (var rd1 = command.ExecuteReader())
            //{
            //    while (rd1.Read())
            //    {
            //        cities.Add(new City(rd1.GetString(0), rd1.GetString(1), rd1.GetString(2), rd1.GetString(3), rd1.GetString(4)));
            //    }
            //}
            //connection.Close();

            cities = City.LoadCitiesFromDatabase("cities_db.db");

            // Устанавливаем источник данных для dataGridView1
            dataGridView1.DataSource = cities;

            // Группируем данные по федеральным округам и заполняем график
            var query = cities.GroupBy(city => city.federalDistrict, city => city.cityName);
            this.chart1.Series[0].Points.Clear();
            foreach (IGrouping<string, string> cityGroup in query)
            {
                this.chart1.Series["Series1"].Points.AddXY(cityGroup.Key, cityGroup.Count());
            }

            // Настраиваем выпадающий список
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("Все города Дальневосточного ФО (их кол-во в определенном регионе)");
            comboBox1.Items.Add("Все города, которые находятся в республиках (их кол-во в определенном регионе)");
            comboBox1.Items.Add("Все города Республики Бурятия");
            comboBox1.Items.Add("Все города на букву С (их кол-во в определенном регионе)");
            comboBox1.Items.Add("Город с самым коротким названием (их кол-во в определенном регионе)");
            comboBox1.Items.Add("Город с самым длинным названием (их кол-во в определенном регионе)");
            comboBox1.Items.Add("Количество городов с четным числом букв в названии (их кол-во в определенном регионе)");

            // Настраиваем изображение на pictureBox1
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        /// <summary>
        /// Обрабатывает событие SelectedIndexChanged элемента comboBox1.
        /// Обновляет dataGridView2 и chart1 на основе выбранного фильтра.
        /// </summary>
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                // Фильтрация по Дальневосточному федеральному округу
                IEnumerable<City> numQuery = cities.Where(n => n.federalDistrict == "Дальневосточный");
                this.dataGridView2.DataSource = numQuery.ToArray();

                // Группировка по регионам и обновление графика
                var query = numQuery.GroupBy(city => city.regionName, city => city.cityName);
                this.chart1.Series[0].Points.Clear();
                foreach (IGrouping<string, string> cityGroup in query)
                {
                    this.chart1.Series["Series1"].Points.AddXY(cityGroup.Key, cityGroup.Count());
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                // Фильтрация по республикам
                IEnumerable<City> numQuery = cities.Where(n => n.regionType == "Респ");
                this.dataGridView2.DataSource = numQuery.ToArray();

                // Группировка по регионам и обновление графика
                var query = numQuery.GroupBy(city => city.regionName, city => city.cityName);
                this.chart1.Series[0].Points.Clear();
                foreach (IGrouping<string, string> cityGroup in query)
                {
                    this.chart1.Series["Series1"].Points.AddXY("Респ " + cityGroup.Key, cityGroup.Count());
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                // Фильтрация по Республике Бурятия
                IEnumerable<City> numQuery = cities.Where(n => n.regionName == "Бурятия");
                this.dataGridView2.DataSource = numQuery.ToArray();

                // Группировка по названиям городов и обновление графика
                var query = numQuery.GroupBy(city => city.cityName, city => city.cityName);
                this.chart1.Series[0].Points.Clear();
                foreach (IGrouping<string, string> cityGroup in query)
                {
                    this.chart1.Series["Series1"].Points.AddXY(cityGroup.Key, cityGroup.Count());
                }
            }
            if (comboBox1.SelectedIndex == 3)
            {
                // Фильтрация по городам, начинающимся на букву С
                IEnumerable<City> numQuery = cities.Where(n => n.cityName[0] == 'С');
                this.dataGridView2.DataSource = numQuery.ToArray();

                // Группировка по регионам и обновление графика
                var query = numQuery.GroupBy(city => city.regionName, city => city.cityName);
                this.chart1.Series[0].Points.Clear();
                foreach (IGrouping<string, string> cityGroup in query)
                {
                    this.chart1.Series["Series1"].Points.AddXY(cityGroup.Key, cityGroup.Count());
                }
            }
            if (comboBox1.SelectedIndex == 4)
            {
                // Фильтрация по городам с самым коротким названием
                IEnumerable<City> numQuery = cities.Where(n => n.cityName.Length == cities.Min(n1 => n1.cityName.Length));
                this.dataGridView2.DataSource = numQuery.ToArray();

                // Группировка по регионам и обновление графика
                var query = numQuery.GroupBy(city => city.regionName, city => city.cityName);
                this.chart1.Series[0].Points.Clear();
                foreach (IGrouping<string, string> cityGroup in query)
                {
                    this.chart1.Series["Series1"].Points.AddXY(cityGroup.Key, cityGroup.Count());
                }
            }
            if (comboBox1.SelectedIndex == 5)
            {
                // Фильтрация по городам с самым длинным названием
                IEnumerable<City> numQuery = cities.Where(n => n.cityName.Length == cities.Max(n1 => n1.cityName.Length));
                this.dataGridView2.DataSource = numQuery.ToArray();

                // Группировка по регионам и обновление графика
                var query = numQuery.GroupBy(city => city.regionName, city => city.cityName);
                this.chart1.Series[0].Points.Clear();
                foreach (IGrouping<string, string> cityGroup in query)
                {
                    this.chart1.Series["Series1"].Points.AddXY(cityGroup.Key, cityGroup.Count());
                }
            }
            if (comboBox1.SelectedIndex == 6)
            {
                // Фильтрация по городам с четным количеством букв в названии
                IEnumerable<City> numQuery = cities.Where(n => n.cityName.Length % 2 == 0);
                this.dataGridView2.DataSource = numQuery.ToArray();

                // Группировка по регионам и обновление графика
                var query = numQuery.GroupBy(city => city.regionName, city => city.cityName);
                this.chart1.Series[0].Points.Clear();
                foreach (IGrouping<string, string> cityGroup in query)
                {
                    this.chart1.Series["Series1"].Points.AddXY(cityGroup.Key, cityGroup.Count());
                }
            }
        }
    }
}
