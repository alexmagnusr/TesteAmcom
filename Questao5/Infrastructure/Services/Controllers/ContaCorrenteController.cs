using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ILogger<ContaCorrenteController> _logger; 
        private readonly IMovimentoRepository movimentoRepository;

        public ContaCorrenteController(IMovimentoRepository movimentoRepository, ILogger<ContaCorrenteController> logger)
        {
            this.movimentoRepository = movimentoRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Movimento>> GetSaldo(string idConta)
        {
            if (idConta.Equals(""))
                throw new NotImplementedException("Id da conta é obrigatório");

            return (IEnumerable<Movimento>)await movimentoRepository.Get(idConta);
        }


        [HttpPost(Name = "PostMovimento")]
        public async Task PostMovimento([FromBody] Movimento movimento)
        {
            await movimentoRepository.Create(movimento);
        }
    }
}