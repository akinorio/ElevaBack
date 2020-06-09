using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaseElite.Api.ViewModels
{
    public class EscolaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public IEnumerable<TurmaViewModel> Turmas { get; set; }
    }
}