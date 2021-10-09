using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos12.InputModel
{
    public class JogoInputModel

    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = " O NOME DO JOGO DEVE CONTER ENTRE 3 E 100 CARACTERES")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O NOME DA PRODUTORA DEVE CONTER ENTRE 3 100 CARACTERES")]
        public string Produtora { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O PREÇO DEVE SER NO MÍNIMO 1 REAL E NO MÁXIMO 1000 REAIS")]
        public double Preço { get; set; }

    }


}
