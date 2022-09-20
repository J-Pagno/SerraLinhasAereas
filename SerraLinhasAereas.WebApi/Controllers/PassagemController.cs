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
    public class PassagemController : ControllerBase
    {
        private IPassagemRepository _passagemRepository = new PassagemRepository();

        [HttpGet]
        public IActionResult GetTodasAsPassagens()
        {
            List<Passagem> passagens = new();

            try
            {
                passagens = _passagemRepository.BuscarTodasAsPassagem();

                if (passagens is null)
                    return BadRequest("Nenhum item encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Ok(passagens);
        }

        [HttpGet("{aa}/{mm}/{dd}")]
        public IActionResult GetPassagensPorData(int aa, int mm, int dd)
        {
            List<Passagem> passagens = new();

            try
            {
                passagens = _passagemRepository.BuscarPassagensPorData(new DateTime(aa, mm, dd));

                if (passagens is null)
                    return BadRequest("Nenhum item encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Ok(passagens);
        }

        [HttpGet("Destino/{destino}")]
        public IActionResult GetPassagensPorDestino(string destino)
        {
            List<Passagem> passagens = new();

            try
            {
                passagens = _passagemRepository.BuscarPassagensPorDestino(destino);
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Ok(passagens);
        }

        [HttpGet("Origem/{origem}")]
        public IActionResult GetPassagensPorOrigem(string origem)
        {
            List<Passagem> passagens = new();


            try
            {
                passagens = _passagemRepository.BuscarPassagensPorOrigem(origem);

                if (passagens is null)
                    return BadRequest("Nenhum item encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Ok(passagens);
        }

        [HttpPost]
        public IActionResult PostPassagem([FromBody] Passagem passagem)
        {
            try
            {
                _passagemRepository.AdicionarPassagem(passagem);
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Created("https://localhost:5001/api/Passagem", _passagemRepository.BuscarTodasAsPassagem().FindLast(p => p.Id != 0));
        }

        [HttpPut]
        public IActionResult PutPassagem([FromBody] Passagem passagem)
        {
            try
            {
                _passagemRepository.AtualizarPassagemExistente(passagem);
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Created("https://localhost:5001/api/Passagem", passagem);
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
