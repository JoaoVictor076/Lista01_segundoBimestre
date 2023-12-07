using H1Store.Catalogo.Application.Interfaces;
using H1Store.Catalogo.Application.ViewModels;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml.Linq;

namespace H1Store.Catalogo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet(Name = "ObterTodos")]
        public IActionResult Get()
        {
            return Ok(_produtoService.ObterTodos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var produto = await _produtoService.ObterProdutoCodigo(id);
            return Ok(produto);
        }

        [HttpGet("BuscarPorNome/{nome}")]
        public async Task<IActionResult> ObterPorNome(string nome)
        {
            var produtos = await _produtoService.ObterPorNome(nome);

            if (produtos.Any())
            {
                return Ok(produtos);
            }
            else
            {
                return NotFound("Nenhum produto encontrado com o valor digitado.");
            }
        }

        [HttpPost]
        public IActionResult Post(NovoProdutoViewModel novoProdutoViewModel)
        {
            _produtoService.Adicionar(novoProdutoViewModel);

            return Ok("Registro adicionado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, NovoProdutoViewModel novoProdutoViewModel)
        {
            novoProdutoViewModel.CodigoId = id;
            _produtoService.Atualizar(novoProdutoViewModel);

            return Ok("Registro atualizado");
        }

        [HttpPut("AtualizarEstoque/{id}/{quantidade}")]
        public async Task<IActionResult> AtualizaEstoque(Guid id, int quantidade)
        {
            await _produtoService.AtualizarEstoque(id, quantidade);

            return Ok("Estoque do produto alterado");
        }

        [HttpPut("AlterarPreco/{id}/{novoPreco}")]
        public async Task<IActionResult> AlterarPreco(Guid id, decimal novoPreco)
        {
            await _produtoService.AlterarPreco(id, novoPreco);

            return Ok("Preço do produto alterado");
        }

        [HttpPut]
        [Route("Ativar/{id}")]
        public async Task<IActionResult> Ativa(Guid id)
        {
            await _produtoService.Ativar(id);
            return Ok("Produto ativado");
        }

        [HttpPut]
        [Route("Desativar/{id}")]
        public async Task<IActionResult> Desativa(Guid id)
        {
            await _produtoService.Desativar(id);
            return Ok("Produto desativado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _produtoService.Remover(id);
            return Ok("Produto apagado com sucesso!");
        }

    }
}
