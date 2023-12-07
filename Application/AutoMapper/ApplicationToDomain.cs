using AutoMapper;
using H1Store.Catalogo.Application.ViewModels;
using H1Store.Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Application.AutoMapper
{
	public class ApplicationToDomain : Profile
	{
		public ApplicationToDomain()
		{

			CreateMap<ProdutoViewModel, Produto>()
			   .ConstructUsing(q => new Produto(q.CodigoId, q.Nome,q.Descricao,q.Ativo,q.Valor,q.DataCadastro,q.QuantidadeEstoque));

			CreateMap<NovoProdutoViewModel, Produto>()
			   .ConstructUsing(q => new Produto(q.CodigoId,q.Nome, q.Descricao, q.Ativo, q.Valor, q.DataCadastro, q.QuantidadeEstoque));

		}
	}
}
