using BrunoCorreia.ControleProdutos.Core.DTO;
using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
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
    [Route("api/Categoria")]
    public class CategoriaController : Controller
    {
        ICategoriaRepository _categoriaRepo;

        public CategoriaController(ICategoriaRepository categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoriaRequest categoria)
        {
            if (categoria == null || String.IsNullOrWhiteSpace(categoria.Nome))
                return BadRequest();

            var novaCategoria = new Categoria();
            novaCategoria.Nome = categoria.Nome;

            if (categoria.Porcentagem > 0)
            {
                novaCategoria.Reajuste = new ReajustePrecoCategoria()
                {
                    Porcentagem = categoria.Porcentagem,
                    TipoReajuste = categoria.TipoReajuste,
                    InicioVigencia = (categoria.InicioVigencia != null) ? categoria.InicioVigencia : null,
                    FinalVigencia = (categoria.FinalVigencia != null) ? categoria.FinalVigencia : null
                };
            }

            var result = await _categoriaRepo.Add(novaCategoria);
            await _categoriaRepo.SaveChanges();
            return Ok(result.Id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var categoria = await _categoriaRepo.First(c => c.Id == id);

            if (categoria == null)
                return NotFound();

            var novaCategoria = new CategoriaResponse();

            novaCategoria.Id = id;
            novaCategoria.Nome = categoria.Nome;

            if (categoria.Reajuste != null && categoria.Reajuste.Porcentagem > 0)
            {
                novaCategoria.Porcentagem = categoria.Reajuste.Porcentagem;
                novaCategoria.TipoReajuste = categoria.Reajuste.TipoReajuste;
                novaCategoria.ReajusteVigente = categoria.Reajuste.Vigente();
            }

            return Ok(novaCategoria);
        }
    }
}
