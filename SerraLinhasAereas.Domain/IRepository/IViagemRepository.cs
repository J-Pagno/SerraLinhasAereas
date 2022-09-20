using SerraLinhasAereas.Domain.Entidades;
using System.Collections.Generic;

namespace SerraLinhasAereas.Domain.IRepository
{
    public interface IViagemRepository
    {
        public void MarcarViagem(Viagem viagem);
        public List<Viagem> BuscarViagemDeClienteExistente(long cpf);
        public void RemarcarViagem(Viagem viagem);
        public int CompararPassagens(int idIda, int idVolta);
    }
}
