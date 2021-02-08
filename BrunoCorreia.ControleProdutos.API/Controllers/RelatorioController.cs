using BrunoCorreia.ControleProdutos.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.API.Controllers
{
    [ApiController]
    [Route("api/Relatorio")]
    public class RelatorioController : Controller
    {
        IOperacaoRepository _operacaoRepo;
        public RelatorioController(IOperacaoRepository operacaoRepo)
        {
            _operacaoRepo = operacaoRepo;
        }

        [HttpGet]
        [Route("RelatorioSemanal")]
        public async Task<ActionResult> Get()
        {
            var operacoes = await _operacaoRepo.Find(c => c.DataOcorrencia >= DateTime.Now.Date.AddDays(-7));
            return Ok(operacoes);
        }
    }
}
