using H1Store.Catalogo.Application.ViewModels;
using H1Store.Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Application.Interfaces
{
	public interface IProdutoService
	{
        IEnumerable<ProdutoViewModel> ObterTodos();
        Task<ProdutoViewModel> ObterProdutoCodigo(Guid id);
        Task<IEnumerable<ProdutoViewModel>> ObterPorNome(string nomeProduto);
        void Adicionar(NovoProdutoViewModel novoProdutoViewModel);
        Task Atualizar(NovoProdutoViewModel novoProdutoViewModel);
        Task Remover(Guid id);
        Task Ativar(Guid id);
        Task Desativar(Guid id);
        Task AlterarPreco(Guid id, decimal valor);
        Task AtualizarEstoque(Guid id, int quantidade);
    }
}
