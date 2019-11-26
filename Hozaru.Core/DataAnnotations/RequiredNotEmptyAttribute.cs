using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru
{
    public class RequiredNotEmptyAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection != null)
            {
                return collection.Count != 0;
            }

            var enumerable = value as IEnumerable;
            return enumerable != null && enumerable.GetEnumerator().MoveNext();
        }
    }
}
