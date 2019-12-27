using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozaru.Web.Helpers.Formating
{
    internal class FormatStringToken
    {
        public string Text { get; private set; }

        public FormatStringTokenType Type { get; private set; }

        public FormatStringToken(string text, FormatStringTokenType type)
        {
            Text = text;
            Type = type;
        }
    }
}
