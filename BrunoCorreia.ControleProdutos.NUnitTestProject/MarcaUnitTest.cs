using BrunoCorreia.ControleProdutos.Core.Entidades;
using BrunoCorreia.ControleProdutos.Core.Repository;
using BrunoCorreia.ControleProdutos.Infrastructure;
using BrunoCorreia.ControleProdutos.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.NUnitTestProject
{
    public class MarcaUnitTest
    {
        const string NomeMarca = "MarcaUnitTeste";
        IMarcaRepository _marcaRepo;
        
        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ControleProdutosContext>()
                .UseInMemoryDatabase("TesteDb");

            _marcaRepo = new MarcaRepository(new ControleProdutosContext(optionsBuilder.Options));
        }

        [Test, Order(1)]
        public async Task AdicionarMarca()
        {
            await _marcaRepo.Add(new Core.Entidades.Marca() { Nome = NomeMarca } );
            await _marcaRepo.SaveChanges();
            Assert.IsTrue(await _marcaRepo.Exists(m => m.Nome == NomeMarca));
        }

        [Test, Order(2)]
        public async Task ConsultarMarca()
        {
            Marca m = await _marcaRepo.First(m => m.Nome == NomeMarca);
            Assert.IsTrue(m.Nome == NomeMarca);
        }

        [Test, Order(3)]
        public async Task AtualizarMarca()
        {
            Marca m = await _marcaRepo.First(m => m.Nome == NomeMarca);
            m.Nome = NomeMarca + "1";
            await _marcaRepo.SaveChanges();
            Assert.IsTrue(await _marcaRepo.Exists(m => m.Nome == NomeMarca + "1"));
        }

        [Test, Order(4)]
        public async Task RemoverMarca()
        {
            Marca m = await _marcaRepo.First(m => m.Nome == NomeMarca + "1");
            await _marcaRepo.Delete(m);
            await _marcaRepo.SaveChanges();
            Assert.IsFalse(await _marcaRepo.Exists(m => m.Nome == NomeMarca + "1"));
        }
    }
}