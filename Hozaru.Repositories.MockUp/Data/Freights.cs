using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp.Data
{
    public static class Freights
    {
        public static IQueryable<Freight> GetAll()
        {
            var cities = Cities.GetAll();
            var batam = cities.Single(i => i.Code == "Batam");
            var lingga = cities.Single(i => i.Code == "Lingga");

            var districts = Districtses.GetAll();
            var sekupang = districts.FirstOrDefault(i => i.Code == "Sekupang");
            var bengkong = districts.FirstOrDefault(i => i.Code == "Bengkong");
            var batamKota = districts.FirstOrDefault(i => i.Code == "BatamKota");
            var senayang = districts.FirstOrDefault(i => i.Code == "Senayang");
            var singkep = districts.FirstOrDefault(i => i.Code == "Singkep");
            var singkepBarat = districts.FirstOrDefault(i => i.Code == "SingkepBarat");
            var singkepPesisir = districts.FirstOrDefault(i => i.Code == "SingkepPesisir");

            var JNEReg = new Expedition("REG15", "Reguler", "JNE", "JNE");
            var JNEOKE = new Expedition("OKE15", "OKE", "JNE", "JNE");
            var JNEYes = new Expedition("YES15", "YES", "JNE", "JNE");

            IList<Freight> freights = new List<Freight>();

            var freightBengkong = new Freight(batam, batam, bengkong);
            freightBengkong.AddItem(JNEReg, 7000, 2).Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
            freightBengkong.AddItem(JNEYes, 14000, 1).Id = Guid.Parse("00000000-0000-0000-0000-000000000002");
            freights.Add(freightBengkong);

            var freightSekupang = new Freight(batam, batam, sekupang);
            freightSekupang.AddItem(JNEReg, 7000, 2).Id = Guid.Parse("00000000-0000-0000-0000-000000000003");
            freightSekupang.AddItem(JNEYes, 14000, 1).Id = Guid.Parse("00000000-0000-0000-0000-000000000004");
            freights.Add(freightSekupang);

            var freightBatamKota = new Freight(batam, batam, batamKota);
            freightBatamKota.AddItem(JNEReg, 7000, 2).Id = Guid.Parse("00000000-0000-0000-0000-000000000005");
            freightBatamKota.AddItem(JNEYes, 14000, 1).Id = Guid.Parse("00000000-0000-0000-0000-000000000006");
            freights.Add(freightBatamKota);

            var freightSenayang = new Freight(batam, batam, senayang);
            freightSenayang.AddItem(JNEReg, 42000, 10).Id = Guid.Parse("00000000-0000-0000-0000-000000000007");
            freights.Add(freightSenayang);

            var freightSingkep = new Freight(batam, batam, singkep);
            freightSingkep.AddItem(JNEReg, 20000, 2).Id = Guid.Parse("00000000-0000-0000-0000-000000000008");
            freightSingkep.AddItem(JNEOKE, 22000, 4).Id = Guid.Parse("00000000-0000-0000-0000-000000000009");
            freights.Add(freightSingkep);

            var freightSingkepBarat = new Freight(batam, batam, singkepBarat);
            freightSingkepBarat.AddItem(JNEReg, 20000, 2).Id = Guid.Parse("00000000-0000-0000-0000-000000000010");
            freightSingkepBarat.AddItem(JNEOKE, 22000, 4).Id = Guid.Parse("00000000-0000-0000-0000-000000000011");
            freights.Add(freightSingkepBarat);

            var freightSingkepPesisir = new Freight(batam, batam, singkepPesisir);
            freightSingkepPesisir.AddItem(JNEReg, 20000, 2).Id = Guid.Parse("00000000-0000-0000-0000-000000000012");
            freightSingkepPesisir.AddItem(JNEOKE, 22000, 4).Id = Guid.Parse("00000000-0000-0000-0000-000000000013");
            freights.Add(freightSingkepPesisir);

            return freights.AsQueryable();
        }
    }
}
