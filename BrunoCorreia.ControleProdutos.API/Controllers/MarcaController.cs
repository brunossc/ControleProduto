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
    [Route("api/Marca")]
    public class MarcaController : Controller
    {

        IMarcaRepository _narcaRepo;

        public MarcaController(IMarcaRepository marcaRepo)
        {
            _narcaRepo = marcaRepo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(MarcaRequest marca)
        {
            if (marca == null || String.IsNullOrWhiteSpace(marca.Nome))
                return BadRequest();

            var novaMarca = new Marca();
            novaMarca.Nome = marca.Nome;

            var result = await _narcaRepo.Add(novaMarca);
            await _narcaRepo.SaveChanges();
            return Ok(result.Id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var marca = await _narcaRepo.First(c => c.Id == id);

            if (marca == null)
                return NotFound();

            var novaMarca = new MarcaResponse();
            novaMarca.Id = id;
            novaMarca.Nome = marca.Nome;
 
            return Ok(novaMarca);
        }
    }
}

