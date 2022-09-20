using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Domain.Entidades.Status
{
    public class Resposta
    {
        public string Code { get; set; }

        public string Descricao { get; set; }

        public Resposta(string code, string descricao)
        {
            Code = code;
            Descricao = descricao;
        }
    }
}
