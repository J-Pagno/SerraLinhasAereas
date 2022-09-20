using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Domain.Entidades.Structs
{
    public struct Endereco
    {
        public long Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
    }
}
