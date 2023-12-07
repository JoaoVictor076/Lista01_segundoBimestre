using AutoMapper;
using H1Store.Catalogo.Data.Providers.MongoDb.Collections;
using H1Store.Catalogo.Data.Providers.MongoDb.Interfaces;
using H1Store.Catalogo.Domain.Entities;
using H1Store.Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Data.Repository
{
	public class FornecedorRepository : IFornecedorRepository
	{

		private readonly IMongoRepository<FornecedorCollection> _fornecedorRepository;
		private readonly IMapper _mapper;

		public FornecedorRepository(IMongoRepository<FornecedorCollection> fornecedorRepository,
			IMapper mapper
			)
		{
			_fornecedorRepository = fornecedorRepository;
			_mapper = mapper;
		}

		public async Task Adicionar(Fornecedor fornecedor)
		{
			var novoFornecedorCollection = _mapper.Map<FornecedorCollection>(fornecedor);
			await _fornecedorRepository.InsertOneAsync(novoFornecedorCollection);
		}

		public Task Atualizar(Fornecedor fornecedor)
		{
			throw new NotImplementedException();
		}

		public Task<Fornecedor> ObterPorCnpj(string cnpj)
		{
			throw new NotImplementedException();
		}

		public Task<Fornecedor> ObterPorId(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Fornecedor>> ObterTodos()
		{
			var listaDeFornecedores = _fornecedorRepository.FilterBy(filtro => true);
			
			return _mapper.Map<IEnumerable<Fornecedor>>(listaDeFornecedores);

		}

		public Task Remover(Fornecedor fornecedor)
		{
			throw new NotImplementedException();
		}
	}
}
