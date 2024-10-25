using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.Utils
{
    public static class Extensoes
    {
        public static string SomenteAlfaNumericos(this string documento)
        {
            return Regex.Replace(documento, "[^0-9a-zA-Z]+", "");
        }
    }
}
