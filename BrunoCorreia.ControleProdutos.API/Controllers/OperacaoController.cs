using BrunoCorreia.ControleProdutos.Core.DTO;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Operacao;
using BrunoCorreia.ControleProdutos.Core.Repository;
using BrunoCorreia.ControleProdutos.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.API.Controllers
{
    [ApiController]
    [Route("api/Operacao")]
    public class OperacaoController : Controller
    {
        IOperacaoRepository _operacaoRepo;
        IEstoqueRepository _estoqueRepo;

        public OperacaoController(IOperacaoRepository operacaoRepo, IEstoqueRepository estoqueRepository)
        {
            _operacaoRepo = operacaoRepo;
            _estoqueRepo = estoqueRepository;
        }

        [HttpPost]
        [Route("Venda")]
        public async Task<ActionResult> VendaProduto(OperacaoRequest operacao)
        {
            if (operacao == null || operacao.ItensOperacao.Count == 0)
                return BadRequest();

            var operacaoService = new OperacaoService(_estoqueRepo, _operacaoRepo);
            return Ok(await operacaoService.EfetuarVenda(operacao));
        }

        [HttpPost]
        [Route("Compra")]
        public async Task<ActionResult> CompraProduto(OperacaoRequest operacao)
        {
            if (operacao == null || operacao.ItensOperacao.Count == 0)
                return BadRequest();

            var operacaoService = new OperacaoService(_estoqueRepo, _operacaoRepo);
            return Ok(await operacaoService.EfetuarCompra(operacao));
        }
    }
}
