using SerraLinhasAereas.Domain.Entidades;
using SerraLinhasAereas.Domain.IRepository;
using SerraLinhasAereas.Infra.Data.DAO;
using System.Collections.Generic;

namespace SerraLinhasAereas.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private ClienteDAO _clienteDAO;

        public ClienteRepository()
        {
            _clienteDAO = new ClienteDAO();
        }

        public void RegistrarCliente(Cliente cliente)
        {
            _clienteDAO.RegistrarCliente(cliente);
        }

        public void AtualizarClienteExistente(Cliente cliente)
        {
            _clienteDAO.AtualizarClienteExistente(cliente);
        }

        public Cliente BuscarClientePorCPF(long cpf)
        {
            return _clienteDAO.BuscarClientePorCPF(cpf);
        }

        public List<Cliente> BuscarTodosOsClientes()
        {
            return _clienteDAO.BuscarTodosOsClientes();
        }

        public void DeletarClienteExistente(long cpf)
        {
            _clienteDAO.DeletarClienteExistente(cpf);
        }
    }
}
