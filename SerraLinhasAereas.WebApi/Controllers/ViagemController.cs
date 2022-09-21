using Microsoft.AspNetCore.Mvc;
using SerraLinhasAereas.Domain.Entidades;
using SerraLinhasAereas.Domain.Entidades.Status;
using SerraLinhasAereas.Domain.IRepository;
using SerraLinhasAereas.Infra.Data.DAO;
using SerraLinhasAereas.Infra.Data.Repository;
using System;
using System.Collections.Generic;

namespace SerraLinhasAereas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViagemController : ControllerBase
    {
        private IViagemRepository _viagemRepository = new ViagemRepository();

        [HttpGet("{cpf}")]
        public IActionResult GetViagensPorCpf(long cpf)
        {
            List<Viagem> viagens;

            try
            {
                viagens = _viagemRepository.BuscarViagemDeClienteExistente(cpf);
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Ok(viagens);
        }

        [HttpPost]
        public IActionResult PostViagem([FromBody] Viagem viagem)
        {
            if (viagem.TemVolta && viagem.IdPassagemDeVolta != 0)
            {
                int comparacao = _viagemRepository.CompararPassagens(viagem.IdPassagemDeIda, viagem.IdPassagemDeVolta);

                if (comparacao >= 0)
                    return BadRequest(new Resposta("V01", "A passagem de ida deve ser maior que a passagem de volta"));
            }
            else
                viagem.IdPassagemDeVolta = 0;


            try
            {
                _viagemRepository.MarcarViagem(viagem);
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Created("https://localhost:5001/api/Viagem", viagem);
        }

        [HttpPut]
        public IActionResult PutViagem([FromBody] Viagem viagem)
        {
            if (viagem.IdPassagemDeVolta != 0)
            {
                int comparacao = _viagemRepository.CompararPassagens(viagem.IdPassagemDeIda, viagem.IdPassagemDeVolta);

                if (comparacao >= 0)
                    return BadRequest(new Resposta("V01", "A passagem de ida deve ser maior que a passagem de volta"));
            }

            try
            {
                _viagemRepository.RemarcarViagem(viagem);
            }
            catch (Exception e)
            {
                return BadRequest(GetResposta(e));
            }

            return Created("https://localhost:5001/api/Viagem", viagem);
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
