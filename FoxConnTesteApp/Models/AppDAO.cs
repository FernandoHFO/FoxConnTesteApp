using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxConnTesteApp.Models
{
    public class AppDAO
    {
        public List<Produto> PostProduct(Produto product, List<Produto> listProduct)
        {
            try
            {
                listProduct.Add( new Produto()
                {
                    Codigo = product.Codigo
                   ,Descricao = product.Descricao
                   ,TipoProduto = product.TipoProduto
                   ,DataLancamento = product.DataLancamento
                   ,Valor = product.Valor
                });
                return listProduct;
            }
            catch (ArgumentException aex)
            {
                throw aex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
