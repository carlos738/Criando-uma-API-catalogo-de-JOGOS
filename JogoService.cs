using ApiCatalogoDeJogos12.Repositories;
using System;
using ApiCatalogoDeJogos12.Entities;
using ApiCatalogoDeJogos12.Exceptions;
using ApiCatalogoDeJogos12.InputModel;
using ApiCatalogoDeJogos12.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos12.Services
{
         public class JogoService : IJogoService
        {
            private readonly IJogoRepository _jogoRepository;

            public JogoService(IJogoRepository jogoRepository)
            {
                _jogoRepository = jogoRepository;
            }

            public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
            {
                var jogos = await _jogoRepository.Obter(pagina, quantidade);

                return jogos.Select(jogo => new JogoViewModel
                {
                    Id = jogo.Id,
                    Nome = jogo.Nome,
                    Produtora = jogo.Produtora,
                    Preco = jogo.Preço
                })
                                   .ToList();
            }

            public async Task<JogoViewModel> Obter(Guid id)
            {
                var jogo = await _jogoRepository.Obter(id);

                if (jogo == null)
                    return null;

                return new JogoViewModel
                {
                    Id = jogo.Id,
                    Nome = jogo.Nome,
                    Produtora = jogo.Produtora,
                    Preco = jogo.Preço
                };
            }

            public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
            {
                var entidadeJogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

                if (entidadeJogo.Count > 0)
                    throw new JogoJaCadastradoException();

                var jogoInsert = new Jogo
                {
                    Id = Guid.NewGuid(),
                    Nome = jogo.Nome,
                    Produtora = jogo.Produtora,
                    Preço = jogo.Preço
                };

                await _jogoRepository.Inserir(jogoInsert);

                return new JogoViewModel
                {
                    Id = jogoInsert.Id,
                    Nome = jogo.Nome,
                    Produtora = jogo.Produtora,
                    Preco = jogo.Preço
                };
            }

            public async Task Atualizar(Guid id, JogoInputModel jogo)
            {
                var entidadeJogo = await _jogoRepository.Obter(id);

                if (entidadeJogo == null)
                    throw new JogoNaoCadastradoException();

                entidadeJogo.Nome = jogo.Nome;
                entidadeJogo.Produtora = jogo.Produtora;
                entidadeJogo.Preço = jogo.Preço;

                await _jogoRepository.Atualizar(entidadeJogo);
            }

            public async Task Atualizar(Guid id, double preco)
            {
                var entidadeJogo = await _jogoRepository.Obter(id);

                if (entidadeJogo == null)
                    throw new JogoNaoCadastradoException();

                entidadeJogo.Preço = preco;

                await _jogoRepository.Atualizar(entidadeJogo);
            }

            public async Task Remover(Guid id)
            {
                var jogo = await _jogoRepository.Obter(id);

                if (jogo == null)
                    throw new JogoNaoCadastradoException();

                await _jogoRepository.Remover(id);
            }

            public void Dispose()
            {
                _jogoRepository?.Dispose();
            }
        }
    }


