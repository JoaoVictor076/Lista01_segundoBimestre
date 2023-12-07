using H1Store.Catalogo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Domain.Entities
{
	public class Produto : EntidadeBase
	{

		#region 1 - Contrutores

		public Produto(Guid codigoID, string nome, string descricao, bool ativo, decimal valor, DateTime dataCadastro, int quantidadeEstoque)
		{
			CodigoID = codigoID;
			Nome = nome;
			Descricao = descricao;
			Ativo = ativo;
			Valor = valor;
			DataCadastro = dataCadastro;
			QuantidadeEstoque = quantidadeEstoque;
		}

		#endregion

		#region 2 - Propriedades
			public Guid CodigoID { get; private set; }
			public string Nome { get; private set; }
			public string Descricao { get; private set; }
			public bool Ativo { get; private set; }
			public decimal Valor { get; private set; }
			public DateTime DataCadastro { get; private set; }
			public int QuantidadeEstoque { get; private set; }


		#endregion

		#region 3 - Comportamentos

		public void Ativar() => Ativo = true;

		public void Desativar() => Ativo = false;

		public void AlterarDescricao(string alterarDescricao) => Descricao = alterarDescricao;

        public void AlterarNome(string novoNome) => Nome = novoNome;

        public void DebitarEstoque(int quantidade)
		{
			if (!PossuiEstoque(quantidade)) throw new Exception("Estoque insuficiente");
			QuantidadeEstoque -= quantidade;
		}

		public void ReporEstoque(int quantidade)
		{
			QuantidadeEstoque += quantidade;
		}

		public bool PossuiEstoque(int quantidade) => QuantidadeEstoque >= quantidade;

        public void AlterarPreco(decimal valor) => Valor = valor;


        #endregion
    }
}
