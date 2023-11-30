using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoxConnTesteApp.Models
{
    public class Produto
    {
        [Required(ErrorMessage = "O Campo Código é Obrigatório.")]
        [Range(1, 9999, ErrorMessage = "Código deve estar entre 1 e 9999")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O Campo Descrição é Obrigatório.")]
        [Display(Name = "Descricao")]
        [StringLength(80, ErrorMessage = "Tamanho máximo de 100 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O Campo Tipo de Produto é Obrigatório.")]
        [Display(Name = "Tipo de Produto")]
        public string TipoProduto { get; set; }

        [Required(ErrorMessage = "O Campo Data de lançamento é Obrigatório.")]
        [Display(Name = "Data de Lançamento")]
        [DataType(DataType.Date)]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "O Campo Valor é Obrigatório.")]
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public double Valor { get; set; }
    }
}
