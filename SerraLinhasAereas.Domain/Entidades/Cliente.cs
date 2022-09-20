using SerraLinhasAereas.Domain.Entidades.Structs;
using System.ComponentModel.DataAnnotations;

namespace SerraLinhasAereas.Domain.Entidades
{
    public class Cliente
    {
        public long Cpf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeCompleto { get { return Nome + " " + Sobrenome; } }
        public Endereco Endereco { get; set; }

        public Cliente()
        {

        }

        public Cliente(long cpf, string nome, string sobrenome, Endereco endereco)
        {
            Cpf = cpf;
            Nome = nome;
            Sobrenome = sobrenome;
            Endereco = endereco;
        }
    }
}
