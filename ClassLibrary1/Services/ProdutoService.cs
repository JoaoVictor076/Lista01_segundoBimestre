using AutoMapper;
using H1Store.Catalogo.Application.Interfaces;
using H1Store.Catalogo.Application.ViewModels;
using H1Store.Catalogo.Domain.Entities;
using H1Store.Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Application.Services
{
	public class ProdutoService : IProdutoService
	{
		#region - Construtores
		private readonly IProdutoRepository _produtoRepository;
		private IMapper _mapper;

		public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
		{
			_produtoRepository = produtoRepository;
			_mapper = mapper;
		}
		#endregion

		#region - Funções
		public void Adicionar(NovoProdutoViewModel novoProdutoViewModel)
		{
            var novoProduto = _mapper.Map<Produto>(novoProdutoViewModel);
            _produtoRepository.Adicionar(novoProduto);

        }

        public async Task AlterarPreco(Guid id, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor negativo ou igual a zero.");
            }

            var EncontrarProduto = await _produtoRepository.ObterProdutoCodigo(id);

            if (EncontrarProduto == null)
            {
                throw new ApplicationException("Este produto não existe.");
            }

            EncontrarProduto.AlterarPreco(valor);

            await _produtoRepository.AlterarPreco(EncontrarProduto, valor);
        }

        public async Task Ativar(Guid id)
        {
            var EncontrarProduto = await _produtoRepository.ObterProdutoCodigo(id);

            if (EncontrarProduto == null) throw new ApplicationException("Não é possível ativar um produto inexistente.");

            await _produtoRepository.Ativar(EncontrarProduto);
        }


        public async Task AtualizarEstoque(Guid id, int quantidade)
		{
            var EncontrarProduto = await _produtoRepository.ObterProdutoCodigo(id);

            if (EncontrarProduto == null)
            {
                throw new ApplicationException("Não é possível alterar o preço de um produto que não existe!");
            }

            if (EncontrarProduto.QuantidadeEstoque + quantidade < 0)
            {
                throw new ArgumentException("O estoque negativo.");
            }
            EncontrarProduto.Ativar();

            await _produtoRepository.AtualizarEstoque(EncontrarProduto, quantidade);
        }

        public async Task Atualizar(NovoProdutoViewModel novoProdutoViewModel)
        {
            var produto = _mapper.Map<Produto>(novoProdutoViewModel);
            await _produtoRepository.Atualizar(produto);
        }

        public async Task Desativar(Guid id)
		{
			var EncontrarProduto = await _produtoRepository.ObterProdutoCodigo(id);

			if(EncontrarProduto == null)  throw new ApplicationException("Não é possível desativar um produto que não existe.");

            EncontrarProduto.Desativar();

			await _produtoRepository.Desativar(EncontrarProduto);

		}

        public async Task Remover(Guid id)
        {
            var EncontrarProduto = await _produtoRepository.ObterProdutoCodigo(id);

            if (EncontrarProduto == null)
            {
                throw new ApplicationException("Não é possível deletar um produto que não existe!");
            }

            await _produtoRepository.Remover(EncontrarProduto);
        }


        public async Task<ProdutoViewModel> ObterProdutoPorCodigo(Guid id)
        {
            var produto = await _produtoRepository.ObterProdutoCodigo(id);
            return _mapper.Map<ProdutoViewModel>(produto);
        }

        public IEnumerable<ProdutoViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(_produtoRepository.ObterTodos());
        }

        public Task<ProdutoViewModel> ObterProdutoCodigo(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorNome(string nomeProduto)
        {
            if (string.IsNullOrWhiteSpace(nomeProduto))
            {
                return Enumerable.Empty<ProdutoViewModel>();
            }

            var produtos = await _produtoRepository.ObterPorNome(nomeProduto);

            var produtosViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);

            return produtosViewModel;
        }

        #endregion
    }
}
