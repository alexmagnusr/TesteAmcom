using Questao5.Domain.Entities;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Repository
{
    public interface IMovimentoRepository
    {
        Task Create(Movimento movimento);
        Task<IEnumerable<Movimento>> Get(string idConta);
    }
}