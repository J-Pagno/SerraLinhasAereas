using SerraLinhasAereas.Domain.Entidades;
using SerraLinhasAereas.Domain.Entidades.Structs;
using SerraLinhasAereas.Domain.IRepository;
using SerraLinhasAereas.Infra.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SerraLinhasAereas.Infra.Data.DAO
{
    public class ClienteDAO : IClienteRepository
    {
        public void RegistrarCliente(Cliente cliente)
        {
            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT INTO CLIENTES(CPF, NOME, SOBRENOME, CEP, RUA, BAIRRO, NUMERO, COMPLEMENTO) VALUES(@Cpf, @Nome, @Sobrenome, @Cep, @Rua, @Bairro, @Numero, @Complemento)";

                    comando.CommandText = sql;

                    ConverterObjetoEmSql(comando, cliente);

                    comando.ExecuteNonQuery();
                }

            }
        }

        public void AtualizarClienteExistente(Cliente cliente)
        {
            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE CLIENTES SET NOME = @Nome, SOBRENOME = @Sobrenome, CEP = @Cep, RUA = @Rua, BAIRRO = @Bairro, NUMERO = @Numero, COMPLEMENTO = @Complemento WHERE CPF = @Cpf";

                    comando.CommandText = sql;

                    ConverterObjetoEmSql(comando, cliente);

                    comando.ExecuteNonQuery();

                }

            }
        }

        public List<Cliente> BuscarTodosOsClientes()
        {
            List<Cliente> listaDeClientes = new();

            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT CPF, NOME, SOBRENOME, CEP, RUA, BAIRRO, NUMERO, COMPLEMENTO FROM CLIENTES";

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        listaDeClientes.Add(ConverterSqlEmObjeto(leitor));
                    }
                }

            }

            return listaDeClientes;
        }

        public Cliente BuscarClientePorCPF(long cpf)
        {
            Cliente cliente = new();

            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT CPF, NOME, SOBRENOME, CEP, RUA, BAIRRO, NUMERO, COMPLEMENTO FROM CLIENTES WHERE CPF = @Cpf";

                    comando.CommandText = sql;

                    comando.Parameters.AddWithValue("@Cpf", cpf);

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        cliente = ConverterSqlEmObjeto(leitor);
                    }
                }

            }

            return cliente;
        }

        public void DeletarClienteExistente(long cpf)
        {
            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"DELETE FROM CLIENTES WHERE CPF = @Cpf";

                    comando.CommandText = sql;

                    comando.Parameters.AddWithValue("@Cpf", cpf);

                    comando.ExecuteNonQuery();
                }

            }
        }

        private void ConverterObjetoEmSql(SqlCommand comando, Cliente cliente)
        {
            comando.Parameters.AddWithValue("@Cpf", cliente.Cpf);
            comando.Parameters.AddWithValue("@Nome", cliente.Nome);
            comando.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
            comando.Parameters.AddWithValue("@Cep", cliente.Endereco.Cep);
            comando.Parameters.AddWithValue("@Rua", cliente.Endereco.Rua);
            comando.Parameters.AddWithValue("@Bairro", cliente.Endereco.Bairro);
            comando.Parameters.AddWithValue("@Numero", cliente.Endereco.Numero);
            comando.Parameters.AddWithValue("@Complemento", cliente.Endereco.Complemento);
        }

        private Cliente ConverterSqlEmObjeto(SqlDataReader leitor)
        {
            Cliente cliente = new();
            Endereco endereco = new();

            cliente.Cpf = long.Parse(leitor["CPF"].ToString());
            cliente.Nome = leitor["NOME"].ToString();
            cliente.Sobrenome = leitor["SOBRENOME"].ToString();
            endereco.Cep = long.Parse(leitor["CEP"].ToString());
            endereco.Rua = leitor["RUA"].ToString();
            endereco.Bairro = leitor["BAIRRO"].ToString();
            endereco.Numero = int.Parse(leitor["NUMERO"].ToString());
            endereco.Complemento = leitor["COMPLEMENTO"].ToString();
            cliente.Endereco = endereco;

            return cliente;
        }


    }
}
