using ApiCatalogoDeJogos12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos12.Repositories
{
    interface IJogoRepository : IDisposable
    {
        Task<List<Jogo>> Obter(int página, int quantidade);
        Task<Jogo> Obter(Guid id);
        Task<IList<Jogo>> Obter(string nome, string produtora);
        Task Inserir(Jogo jogos);
        Task Atualizar(Jogo jogo);
        Task Remover(Guid id);
    }
}
