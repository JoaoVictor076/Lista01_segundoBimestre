using AutoMapper;
using H1Store.Catalogo.Data.Providers.MongoDb.Collections;
using H1Store.Catalogo.Data.Providers.MongoDb.Interfaces;
using H1Store.Catalogo.Domain.Entities;
using H1Store.Catalogo.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Data.Repository
{
	public class ProdutoRepository : IProdutoRepository
	{
		private readonly IMongoRepository<ProdutoCollection> _produtoRepository;

        private readonly IMapper _mapper;

		#region - Construtores
		public ProdutoRepository(IMongoRepository<ProdutoCollection> produtoRepository, IMapper mapper)
		{
			_produtoRepository = produtoRepository;
			_mapper = mapper;
		}
        #endregion

        #region - Funções
        public async Task Adicionar(Produto produto)
        {
            ProdutoCollection produtoCollection = new ProdutoCollection();
            produtoCollection.CodigoId = produto.CodigoId;
            produtoCollection.Nome = produto.Nome;
            produtoCollection.Descricao = produto.Descricao;
            produtoCollection.Ativo = produto.Ativo;
            produtoCollection.Valor = produto.Valor;
            produtoCollection.DataCadastro = produto.DataCadastro;
            produtoCollection.QuantidadeEstoque = produto.QuantidadeEstoque;

            await _produtoRepository.InsertOneAsync(produtoCollection);
        }

        public async Task AlterarPreco(Produto produto, decimal valor)
        {
            var EncontrarProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);
            var Precoproduto = EncontrarProduto.FirstOrDefault();
            Precoproduto.Valor = produto.Valor;
            await _produtoRepository.ReplaceOneAsync(_mapper.Map<ProdutoCollection>(Precoproduto));
        }

        public async Task Ativar(Produto produto)
        {
            var EncontrarProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);
            var AtivarProduto = EncontrarProduto.FirstOrDefault();
            AtivarProduto.Ativo = produto.Ativo;
            await _produtoRepository.ReplaceOneAsync(_mapper.Map<ProdutoCollection>(AtivarProduto));
        }

        public async Task AtualizarEstoque(Produto produto, int quantidade)
        {
            var EncontrarProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);
            var EstoqueProduto = EncontrarProduto.FirstOrDefault();
            EstoqueProduto.QuantidadeEstoque = produto.QuantidadeEstoque;

            await _produtoRepository.ReplaceOneAsync(_mapper.Map<ProdutoCollection>(EstoqueProduto));
        }

        public async Task Atualizar(Produto produto)
        {
            var EncontrarProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);
            var Atualizar = EncontrarProduto.FirstOrDefault();

            if (Atualizar == null)
            {
                throw new ApplicationException("Produto não correspondente.");
            }

            Atualizar.Nome = produto.Nome;
            Atualizar.Descricao = produto.Descricao;
            Atualizar.Ativo = produto.Ativo;
            Atualizar.Valor = produto.Valor;
            Atualizar.QuantidadeEstoque = produto.QuantidadeEstoque;

            await _produtoRepository.ReplaceOneAsync(_mapper.Map<ProdutoCollection>(Atualizar));
        }

        public async Task Desativar(Produto produto)
        {
            var EncontrarProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);
            if (EncontrarProduto == null) throw new ApplicationException("Este produto não existe mais.");
            var produtoCollect = _mapper.Map<ProdutoCollection>(produto);
            produtoCollect.Id = EncontrarProduto.FirstOrDefault().Id;
            await _produtoRepository.ReplaceOneAsync(produtoCollect);
        }

        public async Task<IEnumerable<Produto>> ObterPorNome(string nomeProduto)
        {
            var EncontrarProdutoNome = _produtoRepository.FilterBy(filter => filter.Nome.Contains(nomeProduto));
            return _mapper.Map<IEnumerable<Produto>>(EncontrarProdutoNome);
        }

        public async Task<Produto> ObterProdutoCodigo(Guid id)
        {
            var EncontrarProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == id);
            var produto = _mapper.Map<Produto>(EncontrarProduto.FirstOrDefault());
            return produto;
        }

        public IEnumerable<Produto> ObterTodos()
        {
            var produtoList = _produtoRepository.FilterBy(filter => true);

            List<Produto> lista = new List<Produto>();
            foreach (var item in produtoList)
            {
                lista.Add(new Produto(item.CodigoId, item.Nome, item.Descricao, item.Ativo, item.Valor, item.DataCadastro, item.QuantidadeEstoque));
            }
            return lista;

        }

        public async Task Remover(Produto produto)
        {
            var EncontrarProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);
            var AtualizarProduto = EncontrarProduto.FirstOrDefault();

            if (AtualizarProduto == null)
            {
                throw new ApplicationException("Produto não encontrado.");
            }
            var filtro = Builders<ProdutoCollection>.Filter.Eq(p => p.CodigoId, produto.CodigoId);

            await _produtoRepository.DeleteOneAsync(filtro);
        }
        #endregion

    }
}
