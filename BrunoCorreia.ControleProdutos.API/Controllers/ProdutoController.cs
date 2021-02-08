using BrunoCorreia.ControleProdutos.Core.DTO;
using BrunoCorreia.ControleProdutos.Core.Entidades;
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
    [Route("api/Produto")]
    public class ProdutoController : Controller
    {
        IProdutoRepository _produtoRepo;

        public ProdutoController(IProdutoRepository produtoRepo)
        {
            _produtoRepo = produtoRepo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProdutoRequest produto)
        {
            if (produto == null || String.IsNullOrWhiteSpace(produto.Nome))
                return BadRequest();

            var novoProduto = new Produto();
            novoProduto.Nome = produto.Nome;
            novoProduto.MarcaId = produto.MarcaId;
            novoProduto.CategoriaId = produto.CategoriaId;
            novoProduto.Preco = produto.Preco;

            var result = await _produtoRepo.Add(novoProduto);
            await _produtoRepo.SaveChanges();
            return Ok(result.Id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var produto = await _produtoRepo.First(c => c.Id == id);

            if (produto == null)
                return NotFound();

            var novoProduto = new ProdutoResponse();
            novoProduto.Id = id;
            novoProduto.Nome = produto.Nome;
            novoProduto.PrecoSemReajusteCategoria = produto.Preco;
            novoProduto.Marca = new MarcaResponse() { Id = produto.MarcaId, Nome = produto.Marca.Nome };
            novoProduto.Categoria = new CategoriaResponse()
            {
                Id = produto.Categoria.Id,
                Nome = produto.Categoria.Nome,
                TipoReajuste = produto.Categoria.Reajuste.TipoReajuste,
                Porcentagem = produto.Categoria.Reajuste.Porcentagem,
                ReajusteVigente = produto.Categoria.Reajuste.Vigente()
            };

            return Ok(novoProduto);
        }
    }
}

