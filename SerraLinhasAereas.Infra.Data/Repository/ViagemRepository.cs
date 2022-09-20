using SerraLinhasAereas.Domain.Entidades;
using SerraLinhasAereas.Domain.IRepository;
using SerraLinhasAereas.Infra.Data.DAO;
using System;
using System.Collections.Generic;

namespace SerraLinhasAereas.Infra.Data.Repository
{
    public class ViagemRepository : IViagemRepository
    {
        private ViagemDAO _viagemDAO;

        public ViagemRepository()
        {
            _viagemDAO = new ViagemDAO();
        }

        public List<Viagem> BuscarViagemDeClienteExistente(long cpf)
        {
            return _viagemDAO.BuscarViagemDeClienteExistente(cpf);
        }

        public void MarcarViagem(Viagem viagem)
        {
            _viagemDAO.MarcarViagem(viagem);
        }

        public void RemarcarViagem(Viagem viagem)
        {
            _viagemDAO.RemarcarViagem(viagem);
        }

        public int CompararPassagens(int idIda, int idVolta)
        {
            DateTime dataIda = _viagemDAO.GetPassagemPeloId(idIda).DataOrigem;
            DateTime dataVolta = _viagemDAO.GetPassagemPeloId(idIda).DataDestino;

            int compararPassagens = DateTime.Compare(dataIda, dataVolta);

            return compararPassagens;
        }
    }
}
