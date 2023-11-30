using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxConnTesteApp.Models
{
    public class Constant
    {
        private readonly IEnumerable<TipoProdutoModel> TipoProdutoModel = new List<TipoProdutoModel>()
        {
            new TipoProdutoModel {Id = 0, Descricao = "Selecione Um Tipo de Produto"}
           ,new TipoProdutoModel {Id = 1, Descricao = "Celular"}
           ,new TipoProdutoModel {Id = 2, Descricao = "Tablet"}
           ,new TipoProdutoModel {Id = 3, Descricao = "Notebook"}

        };

        public IEnumerable<TipoProdutoModel> GetListTipoProduto()
        {
            return TipoProdutoModel;
        }
    }
}
