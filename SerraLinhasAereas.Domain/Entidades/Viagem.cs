using SerraLinhasAereas.Domain.IRepository;
using System;

namespace SerraLinhasAereas.Domain.Entidades
{
    public class Viagem
    {
        public string CodigoDaReserva { get; set; }
        public DateTime DataDaCompra { get; set; }
        public decimal ValorTotal { get; set; }
        public long CpfCliente { get; set; }
        public bool TemVolta { get; set; }
        public int IdPassagemDeIda { get; set; }
        public int IdPassagemDeVolta { get; set; }
        public string ResumoDaViagem { get; set; }

        public Viagem()
        {

        }

        public Viagem(string codigoDaReserva, DateTime dataDaCompra, decimal valorTotal, long cpfCliente, bool temVolta, int idPassagemDeIda, Passagem passagemDeIda)
        {
            CodigoDaReserva = codigoDaReserva;
            DataDaCompra = dataDaCompra;
            ValorTotal = valorTotal;
            CpfCliente = cpfCliente;
            TemVolta = temVolta;
            IdPassagemDeIda = idPassagemDeIda;
        }

        public Viagem(string codigoDaReserva, DateTime dataDaCompra, decimal valorTotal, long cpfCliente, bool temVolta, int idPassagemDeIda, int idPassagemDeVolta, Passagem passagemDeIda, Passagem passagemDeVolta)
        {
            CodigoDaReserva = codigoDaReserva;
            DataDaCompra = dataDaCompra;
            ValorTotal = valorTotal;
            CpfCliente = cpfCliente;
            TemVolta = temVolta;
            IdPassagemDeIda = idPassagemDeIda;
            IdPassagemDeVolta = idPassagemDeVolta;
            ResumoDaViagem = $"Seu voo de {passagemDeIda.Origem} a {passagemDeIda.Destino} será dia {passagemDeIda.DataOrigem.ToShortDateString()} as {passagemDeIda.DataOrigem.ToShortTimeString()} e seu voo de volta {passagemDeVolta.Origem} a {passagemDeVolta.Destino} será dia {passagemDeVolta.DataOrigem.ToShortDateString()} as {passagemDeVolta.DataOrigem.ToShortTimeString()}";
        }
    }
}
