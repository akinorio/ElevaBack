using System;
using System.Collections.Generic;

namespace CaseElite.Business.Models
{
    public class Escola : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public IEnumerable<Turma> Turmas { get; set; }
    }
}