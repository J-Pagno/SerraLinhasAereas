using Microsoft.AspNetCore.Mvc;
using SerraLinhasAereas.Domain.Entidades;
using SerraLinhasAereas.Domain.Entidades.Status;
using SerraLinhasAereas.Domain.IRepository;
using SerraLinhasAereas.Infra.Data.Repository;
using System;
using System.Collections.Generic;

namespace SerraLinhasAereas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        IClienteRepository _clienteRepository = new ClienteRepository();

        [HttpPost]
        public IActionResult PostCliente([FromBody] Cliente cliente)
        {
            try
            {
                _clienteRepository.RegistrarCliente(cliente);
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Created("localhost:5001/api/Cliente", cliente);
        }

        [HttpPut]
        public IActionResult PutClienteExistente(long cpf, [FromBody] Cliente cliente)
        {
            try
            {
                _clienteRepository.AtualizarClienteExistente(cliente);
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Created($"localhost:5001/api/Cliente/{cpf}", cliente);
        }

        [HttpGet]
        public IActionResult GetTodosOsClientes()
        {
            List<Cliente> listaDeClientes = _clienteRepository.BuscarTodosOsClientes();

            if (listaDeClientes is null)
                return BadRequest(new Resposta("C02", "Nenhum resultado encontrado!"));

            return Ok(listaDeClientes);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetClientePorCpf(long cpf)
        {
            Cliente cliente = _clienteRepository.BuscarClientePorCPF(cpf);

            if (cliente is null)
                return BadRequest("Nenhum item encontrado");

            return Ok(cliente);
        }

        [HttpDelete("{cpf}")]
        public IActionResult DeleteClientePorCpf(long cpf)
        {
            try
            {
                _clienteRepository.DeletarClienteExistente(cpf);
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return new NoContentResult();
        }

        private Resposta GetResposta(Exception e)
        {
            switch (e.GetType().ToString())
            {
                case "System.Data.SqlClient.SqlException":
                    return new Resposta("C01", "Valor enviado inválido!");

                default:
                    return new Resposta("C00", "Erro ao inserir o regitro no banco de dados");
            }
        }
    }
}
