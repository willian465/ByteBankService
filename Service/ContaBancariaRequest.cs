using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public class ContaBancariaRequest
    {
        public int Cliente { get; set; }
    }
    public class ContaBancariaModel
    {
        public int Cliente { get; set; }
        public double Saldo { get; set; }
        public DateTime DataAbertura { get; set; }
        public int NumeroConta { get; set; }
    }
}
