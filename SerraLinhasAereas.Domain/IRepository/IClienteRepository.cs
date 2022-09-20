using SerraLinhasAereas.Domain.Entidades;
using System.Collections.Generic;

namespace SerraLinhasAereas.Domain.IRepository
{
    public interface IClienteRepository
    {
        public void RegistrarCliente(Cliente cliente);
        public Cliente BuscarClientePorCPF(long cpf);
        public List<Cliente> BuscarTodosOsClientes();
        public void AtualizarClienteExistente(Cliente cliente);
        public void DeletarClienteExistente(long cpf);
    }
}
