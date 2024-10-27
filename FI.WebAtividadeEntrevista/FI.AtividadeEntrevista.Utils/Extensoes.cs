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
        public static string SomenteAlfaNumericos(this string conteudo)
        {
            return Regex.Replace(conteudo, "[^0-9a-zA-Z]+", "");
        }

        public static string FormatarCPF(this string cpf)
        {
            cpf = cpf.Insert(9, "-");
            cpf = cpf.Insert(6, ".");
            cpf = cpf.Insert(3, ".");

            return cpf;
        }
    }
}
