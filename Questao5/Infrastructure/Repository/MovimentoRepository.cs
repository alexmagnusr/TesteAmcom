using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task Create(Movimento movimento)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            
            var teste = connection.ExecuteAsync("INSERT INTO movimento (idcontacorrente, datamovimento, tipomovimento, valor)" +
                "VALUES (@IdConta, @DataMovimento, @TipoMovimento, @Valor);", movimento);

            await connection.ExecuteAsync("INSERT INTO movimento (idcontacorrente, datamovimento, tipomovimento, valor)" +
                "VALUES (@IdConta, @DataMovimento, @TipoMovimento, @Valor);", movimento);

        }
        public async Task<IEnumerable<Movimento>> Get(string idConta)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
           
            var teste = connection.QueryAsync<Movimento>("SELECT * FROM Movimento where idcontacorrente = " + idConta + ";");

            return await connection.QueryAsync<Movimento>("SELECT * FROM Movimento where idcontacorrente = " + idConta + ";");
        }

    }
}
