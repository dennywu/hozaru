using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp.Data
{
    public static class Cities
    {
        public static IQueryable<City> GetAll()
        {
            IList<City> cities = new List<City>();
            cities.Add(new City("Batam", "Batam"));
            cities.Add(new City("Lingga", "Lingga"));
            cities.Add(new City("TanjungPinang", "Tanjung Pinang"));
            cities.Add(new City("DaboSingkep", "Dabo Singkep"));
            cities.Add(new City("TanjungBalai Karimun", "Tanjung Balai Karimun"));
            cities.Add(new City("TanjungBatu", "Tanjung Batu"));
            cities.Add(new City("TanjungUban", "Tanjung Uban"));
            cities.Add(new City("Daik", "Daik"));
            cities.Add(new City("BelakangPadang", "Belakang Padang"));
            cities.Add(new City("Natuna", "Natuna"));
            return cities.AsQueryable();
        }
    }
}
