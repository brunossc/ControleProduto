using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates.Categoria;
using BrunoCorreia.ControleProdutos.Core.Repository;
using BrunoCorreia.ControleProdutos.Infrastructure;
using BrunoCorreia.ControleProdutos.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.NUnitTestProject
{
    public class CategoriaUnitTest
    {
        const string NomeCategoria = "CategoriaUnitTeste";
        ICategoriaRepository _categoriaRepo;
        
        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ControleProdutosContext>()
                .UseInMemoryDatabase("TesteDb");

            _categoriaRepo = new CategoriaRepository(new ControleProdutosContext(optionsBuilder.Options));
        }

        [Test, Order(1)]
        public async Task AdicionarCategoria()
        {
            await _categoriaRepo.Add(new Core.Entidades.Aggregates.Categoria.Categoria() {
                Nome = NomeCategoria, 
                Reajuste = new Core.Entidades.Aggregates.Categoria.ReajustePrecoCategoria()
                { 
                     Porcentagem = 10,
                     TipoReajuste = Core.Entidades.Aggregates.Categoria.EnumTipoReajuste.Desconto,
                     InicioVigencia = DateTime.Now.Date,
                     FinalVigencia = DateTime.Now.Date.AddDays(90).Date
                }
            } );

            await _categoriaRepo.SaveChanges();
            Assert.IsTrue(await _categoriaRepo.Exists(m => m.Nome == NomeCategoria));
        }

        [Test, Order(2)]
        public async Task ConsultarCategoria()
        {
            Categoria categoria = await _categoriaRepo.First(c => c.Nome == NomeCategoria);
            Assert.IsTrue(categoria.Nome == NomeCategoria);
            Assert.IsTrue(categoria.Reajuste.Porcentagem == 10);
        }

        [Test, Order(3)]
        public async Task AtualizarMarca()
        {
            Categoria categoria = await _categoriaRepo.First(c => c.Nome == NomeCategoria);
            categoria.Nome = NomeCategoria + "1";
            await _categoriaRepo.SaveChanges();
            Assert.IsTrue(await _categoriaRepo.Exists(m => m.Nome == NomeCategoria + "1"));
        }

        [Test, Order(4)]
        public async Task RemoverMarca()
        {
            await _categoriaRepo.DeleteAll();
            await _categoriaRepo.SaveChanges();
            Assert.IsFalse(await _categoriaRepo.Exists(m => m.Nome == NomeCategoria));
        }
    }
}