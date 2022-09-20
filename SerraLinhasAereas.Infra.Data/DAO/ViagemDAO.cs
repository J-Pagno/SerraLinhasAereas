using SerraLinhasAereas.Domain.Entidades;
using SerraLinhasAereas.Domain.IRepository;
using SerraLinhasAereas.Infra.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace SerraLinhasAereas.Infra.Data.DAO
{
    public class ViagemDAO
    {
        public List<Viagem> BuscarViagemDeClienteExistente(long cpf)
        {
            List<Viagem> listaDeViagens = new();

            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT * FROM VIAGENS WHERE CPF_CLIENTE = @Cpf";

                    comando.Parameters.AddWithValue("@Cpf", cpf);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        listaDeViagens.Add(ConverterSqlEmObjeto(leitor));
                    }
                }

            }
            return listaDeViagens;
        }

        public void MarcarViagem(Viagem viagem)
        {
            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"BEGIN
	                                if NOT EXISTS 
                                        ( 
                                            SELECT 1 FROM VIAGENS 
		                                    WHERE (ID_PASSAGEM_DE_IDA = @IdPassagemDeIda OR ID_PASSAGEM_DE_VOLTA = @IdPassagemDeVolta)
                                        )
	                                BEGIN
                                        INSERT INTO VIAGENS(CODIGO_DE_RESERVA, DATA_DE_COMPRA, VALOR_TOTAL, CPF_CLIENTE, ID_PASSAGEM_DE_IDA, ID_PASSAGEM_DE_VOLTA, TEM_VOLTA) 
                                        VALUES(@CodigoDaReserva, @DataDaCompra, @ValorTotal, @CpfCliente, @IdPassagemDeIda, @IdPassagemDeVolta, @TemVolta);
                                        UPDATE PASSAGENS SET DISPONIVEL = 1 WHERE ID = @IdPassagemDeIda OR ID = @IdPassagemDeVolta;
                                    END 
                                END";

                    comando.CommandText = sql;

                    ConverterObjetoEmSql(comando, viagem);

                    comando.ExecuteNonQuery();
                }

            }
        }

        public void RemarcarViagem(Viagem viagem)
        {
            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE VIAGENS SET DATA_DE_COMPRA = @DataDaCompra, VALOR_TOTAL = @ValorTotal, CPF_CLIENTE = @CpfCliente, ID_PASSAGEM_DE_IDA = @IdPassagemDeIda, ID_PASSAGEM_DE_VOLTA = @IdPassagemDeVolta, TEM_VOLTA = @TemVolta WHERE CODIGO_DE_RESERVA LIKE @CodigoDaReserva";

                    comando.CommandText = sql;

                    ConverterObjetoEmSql(comando, viagem);

                    comando.ExecuteNonQuery();
                }

            }
        }

        private Viagem ConverterSqlEmObjeto(SqlDataReader leitor)
        {
            Viagem viagem = new();

            viagem.CodigoDaReserva = (leitor["CODIGO_DE_RESERVA"].ToString());
            viagem.DataDaCompra = DateTime.Parse(leitor["DATA_DE_COMPRA"].ToString());
            viagem.ValorTotal = decimal.Parse(leitor["VALOR_TOTAL"].ToString());
            viagem.CpfCliente = long.Parse(leitor["CPF_CLIENTE"].ToString());
            viagem.IdPassagemDeIda = int.Parse(leitor["ID_PASSAGEM_DE_IDA"].ToString());
            //Essa buceta desse caralho de id de volta vem null e da pau
            viagem.IdPassagemDeVolta = int.Parse(leitor["ID_PASSAGEM_DE_VOLTA"].ToString()) | 0;
            viagem.TemVolta = bool.Parse(leitor["TEM_VOLTA"].ToString());
            viagem.ResumoDaViagem = SetResumo(new int[] { viagem.IdPassagemDeIda, viagem.IdPassagemDeVolta }, viagem);


            return viagem;
        }

        private void ConverterObjetoEmSql(SqlCommand comando, Viagem viagem)
        {
            comando.Parameters.AddWithValue("@CodigoDaReserva", viagem.CodigoDaReserva);
            comando.Parameters.AddWithValue("@DataDaCompra", viagem.DataDaCompra);
            comando.Parameters.AddWithValue("@ValorTotal", viagem.ValorTotal);
            comando.Parameters.AddWithValue("@CpfCliente", viagem.CpfCliente);
            comando.Parameters.AddWithValue("@IdPassagemDeVolta", viagem.IdPassagemDeVolta);
            comando.Parameters.AddWithValue("@IdPassagemDeIda", viagem.IdPassagemDeIda);
            comando.Parameters.AddWithValue("@TemVolta", Convert.ToInt32(viagem.TemVolta));
        }

        public Passagem GetPassagemPeloId(int id)
        {
            Passagem passagem = new();

            using (var conexao = DBSettings.Conexao())
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT * FROM PASSAGENS WHERE ID = @Id";

                    comando.Parameters.AddWithValue("@Id", id);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        passagem = new()
                        {
                            Id = int.Parse(leitor["ID"].ToString()),
                            Origem = (leitor["ORIGEM"].ToString()),
                            Destino = (leitor["DESTINO"].ToString()),
                            Valor = decimal.Parse(leitor["VALOR"].ToString()),
                            DataOrigem = DateTime.Parse(leitor["DATA_DE_ORIGEM"].ToString()),
                            DataDestino = DateTime.Parse(leitor["DATA_DE_DESTINO"].ToString()),
                        };
                    }
                }
            }

            return passagem;
        }

        public string SetResumo(int[] passagens, Viagem viagem)
        {
            Passagem passagemIda = GetPassagemPeloId(passagens[0]),
                     passagemVolta = GetPassagemPeloId(passagens[1]);

            if (passagens[1] == 0)
                return $"Seu voo de {passagemIda.Origem} a {passagemIda.Destino} será dia {passagemIda.DataOrigem.ToShortDateString()} as {passagemIda.DataOrigem.ToShortTimeString()} e seu voo de volta de {passagemVolta.Origem} a {passagemVolta.Destino} será dia {passagemVolta.DataOrigem.ToShortDateString()} as {passagemVolta.DataOrigem.ToShortTimeString()}";
            else
                return $"Seu voo    de {passagemIda.Origem} a {passagemIda.Destino} será dia {passagemIda.DataOrigem.ToShortDateString()} as {passagemIda.DataOrigem.ToShortTimeString()}";
        }
    }
}
