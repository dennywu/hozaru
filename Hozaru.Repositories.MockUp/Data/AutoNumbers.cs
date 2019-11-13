using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp.Data
{
    public static class AutoNumbers
    {
        static IList<AutoNumber> autoNumbers = new List<AutoNumber>();

        public static AutoNumber Add(AutoNumber autoNumber)
        {
            autoNumbers.Add(autoNumber);
            return autoNumber;
        }

        public static IQueryable<AutoNumber> GetAll()
        {
            return autoNumbers.AsQueryable();
        }

        public static void Remove(AutoNumber autoNumber)
        {
            autoNumbers.Remove(autoNumber);
        }
    }
}
