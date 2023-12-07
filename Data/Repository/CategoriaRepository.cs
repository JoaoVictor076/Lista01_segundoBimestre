using AutoMapper;
using H1Store.Catalogo.Data.Providers.MongoDb.Collections;
using H1Store.Catalogo.Data.Providers.MongoDb.Interfaces;
using H1Store.Catalogo.Domain.Entities;
using H1Store.Catalogo.Domain.Interfaces;
using H1Store.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Data.Repository
{
	public class CategoriaRepository : ICategoriaRepository
	{
		private readonly IMongoRepository<CategoriaCollection> _categoriaRepository;
		private readonly IMapper _mapper;

		#region - Construtores
		public CategoriaRepository(IMongoRepository<CategoriaCollection> categoriaRepository, IMapper mapper)
		{
			_categoriaRepository = categoriaRepository;
			_mapper = mapper;
		}
		#endregion

		public async Task Adicionar(Categoria categoria)
		{
			await _categoriaRepository.InsertOneAsync(_mapper.Map<CategoriaCollection>(categoria));
		}

		public void Atualizar(Categoria categoria)
		{
			throw new NotImplementedException();
		}

		public Task Desativar(Categoria categoria)
		{
			throw new NotImplementedException();
		}

		public async Task<Categoria> ObterPorId(Guid id)
		{
			var buscaCategoria = _categoriaRepository.FilterBy(filter => filter.CodigoId == id);

			return _mapper.Map<Categoria>(buscaCategoria.FirstOrDefault());

		}

		public  IEnumerable<Categoria> ObterTodas()
		{
			var categoriaList = _categoriaRepository.FilterBy(filter => true);

			return _mapper.Map<IEnumerable<Categoria>>(categoriaList);
		}
	}
}
