using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCatalogoDeJogos12.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq;
using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;


namespace ApiCatalogoDeJogos12.Repositories
{
    public class JogoSqlServerRepository : IJogoRepository

    {
        public Task Atualizar(Jogo jogo)
        {
            throw new NotImplementedException();
        }

        public Task Inserir(Jogo jogos)
        {
            throw new NotImplementedException();
        }

        public Task<List<Jogo>> Obter(int página, int quantidade)
        {
            throw new NotImplementedException();
        }

        public Task<Jogo> Obter(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Jogo>> Obter(string nome, string produtora)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public class JogoSqlServerRepository : IJogoRepository
        {
            private readonly SqlConnection sqlConnection;

            public Guid Id { get; private set; }
            public string Nome { get; private set; }
            public string Produtora { get; private set; }
            public double Preço { get; private set; }

            public JogoSqlServerRepository(IConfiguration configuration)
            {
                sqlConnection = new System.Data.SqlClient.SqlConnection(configuration.GetConnectionString("Default"));
            }

            public JogoSqlServerRepository()
            {
            }

            public JogoSqlServerRepository(Microsoft.Data.SqlClient.SqlConnection sqlConnection, Guid id, string nome, string produtora, double preço)
            {
                this.sqlConnection = sqlConnection ?? throw new ArgumentNullException(nameof(sqlConnection));
                Id = id;
                Nome = nome ?? throw new ArgumentNullException(nameof(nome));
                Produtora = produtora ?? throw new ArgumentNullException(nameof(produtora));
                Preço = preço;
            }

            public JogoSqlServerRepository(SqlConnection sqlConnection, Guid id, string nome, string produtora, double preço)
            {
                this.sqlConnection = sqlConnection ?? throw new ArgumentNullException(nameof(sqlConnection));
                Id = id;
                Nome = nome ?? throw new ArgumentNullException(nameof(nome));
                Produtora = produtora ?? throw new ArgumentNullException(nameof(produtora));
                Preço = preço;
            }

            public async Task<List<Jogo>> Obter(int pagina, int quantidade)
            {
                var jogos = new List<Jogo>();

                var comando = $"select * from Jogos order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

                await sqlConnection.OpenAsync();
                Microsoft.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(comando, sqlConnection);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (sqlDataReader.Read())
                {
                    jogos.Add(new Jogo
                    {
                        Id = (Guid)sqlDataReader["Id"],
                        Nome = (string)sqlDataReader["Nome"],
                        Produtora = (string)sqlDataReader["Produtora"],
                        Preço = (double)sqlDataReader["Preco"]
                    });
                }

                await sqlConnection.CloseAsync();

                return jogos;
            }

            public async Task<Jogo> Obter(Guid id)
            {
                Jogo jogo = null;

                var comando = $"select * from Jogos where Id = '{id}'";

                await sqlConnection.OpenAsync();
                System.Data.SqlClient.SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (sqlDataReader.Read())
                {
                    jogo = new Jogo
                    {
                        Id = (Guid)sqlDataReader["Id"],
                        Nome = (string)sqlDataReader["Nome"],
                        Produtora = (string)sqlDataReader["Produtora"],
                        Preço = (double)sqlDataReader["Preco"]
                    };
                }

                await sqlConnection.CloseAsync();

                return jogo;
            }

            public async Task<List<Jogo>> Obter(string nome, string produtora)
            {
                var jogos = new List<Jogo>();

                var comando = $"select * from Jogos where Nome = '{nome}' and Produtora = '{produtora}'";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (sqlDataReader.Read) ;
                {
                    jogos.Add(new Jogo());
                    {
                        Id = (Guid)sqlDataReader["Id"];
                        Nome = (string)sqlDataReader["Nome"];
                        Produtora = (string)sqlDataReader["Produtora"];
                        Preço = (double)sqlDataReader["Preco"];
                    };
                }

                await sqlConnection.CloseAsync();

                return jogos;
            }

            public async Task Inserir(Jogo jogo)
            {
                var comando = $"insert Jogos (Id, Nome, Produtora, Preco) values ('{jogo.Id}', '{jogo.Nome}', '{jogo.Produtora}', {jogo.Preço.ToString().Replace(",", ".")})";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                await sqlConnection.CloseAsync();
            }

            public async Task Atualizar(Jogo jogo)
            {
                var comando = $"update Jogos set Nome = '{jogo.Nome}', Produtora = '{jogo.Produtora}', Preco = {jogo.Preço.ToString().Replace(",", ".")} where Id = '{jogo.Id}'";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                await sqlConnection.CloseAsync();
            }

            public async Task Remover(Guid id)
            {
                var comando = $"delete from Jogos where Id = '{id}'";

                await sqlConnection.OpenAsync();
                SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                await sqlConnection.CloseAsync();

            }

            public void Dispose()
            {
                sqlConnection?.Close();

                sqlConnection?.Dispose();
        }

            Task<IList<Jogo>> IJogoRepository.Obter(string nome, string produtora)
            {
                throw new NotImplementedException();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar chamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: descartar estado gerenciado (objetos gerenciados).
                }

                // TODO: liberar recursos não gerenciados (objetos não gerenciados) e substituir um finalizador abaixo.
                // TODO: definir campos grandes como nulos.

                disposedValue = true;
            }
        }

        // TODO: substituir um finalizador somente se Dispose(bool disposing) acima tiver o código para liberar recursos não gerenciados.
        // ~JogoSqlServerRepository() {
        //   // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
        //   Dispose(false);
        // }

        // Código adicionado para implementar corretamente o padrão descartável.
        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
            Dispose(true);
            // TODO: remover marca de comentário da linha a seguir se o finalizador for substituído acima.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
    
