using SerraLinhasAereas.Domain.Entidades;
using SerraLinhasAereas.Domain.IRepository;
using SerraLinhasAereas.Infra.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SerraLinhasAereas.Infra.Data.DAO
{
    public class PassagemDAO
    {
        public void AdicionarPassagem(Passagem passagem)
        {
            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT INTO PASSAGENS(ORIGEM, DESTINO, VALOR, DATA_DE_ORIGEM, DATA_DE_DESTINO, DISPONIVEL) VALUES(@Origem, @Destino, @Valor, @DataDeOrigem, @DataDeDestino, 0)";

                    comando.CommandText = sql;

                    ConverterObjetoEmSql(comando, passagem);

                    comando.ExecuteNonQuery();
                }

            }
        }

        public Passagem AtualizarPassagemExistente(Passagem passagem)
        {
            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PASSAGENS SET ORIGEM = @Origem, DESTINO = @Destino, VALOR = @Valor, DATA_DE_ORIGEM = @DataDeOrigem, DATA_DE_DESTINO = @DataDeDestino WHERE ID = @Id";

                    comando.CommandText = sql;

                    ConverterObjetoEmSql(comando, passagem);

                    comando.ExecuteNonQuery();
                }

            }

            return passagem;
        }

        public List<Passagem> BuscarPassagensPorData(DateTime dataPassagem)
        {
            List<Passagem> listaDePassagens = new();

            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT * FROM PASSAGENS WHERE DATA_DE_ORIGEM = @DataDeOrigem";

                    comando.Parameters.AddWithValue("@DataDeOrigem", dataPassagem);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        listaDePassagens.Add(ConverterSqlEmObjeto(leitor));
                    }
                }

            }
            return listaDePassagens;
        }

        public List<Passagem> BuscarPassagensPorDestino(string destinoPassagem)
        {
            List<Passagem> listaDePassagens = new();

            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT * FROM PASSAGENS WHERE DESTINO Like @Destino";

                    comando.Parameters.AddWithValue("@Destino", destinoPassagem);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        listaDePassagens.Add(ConverterSqlEmObjeto(leitor));
                    }
                }

            }
            return listaDePassagens;
        }

        public List<Passagem> BuscarPassagensPorOrigem(string origemPassagem)
        {
            List<Passagem> listaDePassagens = new();

            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT * FROM PASSAGENS WHERE ORIGEM Like @Origem";

                    comando.Parameters.AddWithValue("@Origem", origemPassagem);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        listaDePassagens.Add(ConverterSqlEmObjeto(leitor));
                    }
                }
            }
            return listaDePassagens;
        }

        public List<Passagem> BuscarTodasAsPassagem()
        {
            List<Passagem> listaDePassagens = new();

            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT * FROM PASSAGENS";

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        listaDePassagens.Add(ConverterSqlEmObjeto(leitor));
                    }
                }

            }
            return listaDePassagens;
        }

        public void ConverterObjetoEmSql(SqlCommand comando, Passagem passagem)
        {
            comando.Parameters.AddWithValue("@Id", passagem.Id);
            comando.Parameters.AddWithValue("@Origem", passagem.Origem);
            comando.Parameters.AddWithValue("@Destino", passagem.Destino);
            comando.Parameters.AddWithValue("@Valor", passagem.Valor);
            comando.Parameters.AddWithValue("@DataDeOrigem", passagem.DataOrigem);
            comando.Parameters.AddWithValue("@DataDeDestino", passagem.DataDestino);
        }

        public Passagem ConverterSqlEmObjeto(SqlDataReader leitor)
        {
            Passagem passagem = new();


            passagem.Id = int.Parse(leitor["ID"].ToString());
            passagem.Origem = (leitor["ORIGEM"].ToString());
            passagem.Destino = (leitor["DESTINO"].ToString());
            passagem.Valor = decimal.Parse(leitor["VALOR"].ToString());
            passagem.DataOrigem = DateTime.Parse(leitor["DATA_DE_ORIGEM"].ToString());
            passagem.DataDestino = DateTime.Parse(leitor["DATA_DE_DESTINO"].ToString());


            return passagem;
        }
    }
}
