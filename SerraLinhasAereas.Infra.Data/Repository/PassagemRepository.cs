using SerraLinhasAereas.Domain.Entidades;
using SerraLinhasAereas.Domain.IRepository;
using SerraLinhasAereas.Infra.Data.DAO;
using System;
using System.Collections.Generic;

namespace SerraLinhasAereas.Infra.Data.Repository
{
    public class PassagemRepository : IPassagemRepository
    {
        private PassagemDAO _passagemDAO;

        public PassagemRepository()
        {
            _passagemDAO = new();
        }

        public void AdicionarPassagem(Passagem passagem)
        {
            _passagemDAO.AdicionarPassagem(passagem);
        }

        public Passagem AtualizarPassagemExistente(Passagem passagem)
        {
            return _passagemDAO.AtualizarPassagemExistente(passagem);
        }

        public List<Passagem> BuscarPassagensPorData(DateTime dataPassagem)
        {
            return _passagemDAO.BuscarPassagensPorData(dataPassagem);
        }

        public List<Passagem> BuscarPassagensPorDestino(string destinoPassagem)
        {
            return _passagemDAO.BuscarPassagensPorDestino(destinoPassagem);
        }

        public List<Passagem> BuscarPassagensPorOrigem(string origemPassagem)
        {
            return _passagemDAO.BuscarPassagensPorOrigem(origemPassagem);
        }

        public List<Passagem> BuscarTodasAsPassagem()
        {
            return _passagemDAO.BuscarTodasAsPassagem();
        }
    }
}
