using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp.Data
{
    public static class Districtses
    {
        public static IQueryable<Districts> GetAll()
        {
            var batam = Cities.GetAll().FirstOrDefault(i => i.Code == "Batam");
            var lingga = Cities.GetAll().FirstOrDefault(i => i.Code == "Lingga");

            IList<Districts> districtes = new List<Districts>();
            districtes.Add(new Districts(batam, "Bengkong", "Bengkong"));
            districtes.Add(new Districts(batam, "BatamKota", "Batam Kota"));
            districtes.Add(new Districts(batam, "Nongsa", "Nongsa"));
            districtes.Add(new Districts(batam, "Sekupang", "Sekupang"));
            districtes.Add(new Districts(batam, "BatuAmpar", "Batu Ampar"));
            districtes.Add(new Districts(batam, "BelakangPadang", "Belakang Padang"));
            districtes.Add(new Districts(batam, "Bulang", "Bulang"));
            districtes.Add(new Districts(batam, "Galang", "Galang"));
            districtes.Add(new Districts(batam, "LubukBaja", "Lubuk Baja"));

            districtes.Add(new Districts(lingga, "Senayang", "Senayang"));
            districtes.Add(new Districts(lingga, "Singkep", "Singkep"));
            districtes.Add(new Districts(lingga, "SingkepBarat", "Singkep Barat"));
            districtes.Add(new Districts(lingga, "SingkepPesisir", "Singkep Pesisir"));
            districtes.Add(new Districts(lingga, "Selayar", "Selayar"));
            return districtes.AsQueryable();
        }
    }
}
