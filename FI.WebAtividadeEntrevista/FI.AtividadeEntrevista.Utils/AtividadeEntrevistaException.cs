using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.Utils
{
    public class AtividadeEntrevistaException : Exception
    {
        public int Codigo { get; set; }

        public AtividadeEntrevistaException() { }

        public AtividadeEntrevistaException(string message)
            : base(message) { }

        public AtividadeEntrevistaException(int codigo, string message)
            : base(message)
        {
            Codigo = codigo;
        }

        public AtividadeEntrevistaException(string message, Exception inner)
            : base(message, inner) { }
    }
}
