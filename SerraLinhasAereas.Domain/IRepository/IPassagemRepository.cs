using System;
using System.Collections.Generic;
using SerraLinhasAereas.Domain.Entidades;

namespace SerraLinhasAereas.Domain.IRepository
{
    public interface IPassagemRepository
    {
        public void AdicionarPassagem(Passagem passagem);
        public List<Passagem> BuscarTodasAsPassagem();
        public List<Passagem> BuscarPassagensPorData(DateTime dataPassagem);
        public List<Passagem> BuscarPassagensPorOrigem(string origemPassagem);
        public List<Passagem> BuscarPassagensPorDestino(string destinoPassagem);
        public Passagem AtualizarPassagemExistente(Passagem passagem);
    }
}
