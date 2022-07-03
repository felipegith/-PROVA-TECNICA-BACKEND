using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LOCACOES.API.Model
{
    public class Locacao
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Filme { get; set; }
        public DateTime DataLocacao { get; set; } = DateTime.Now;
        public DateTime DataDevolucao { get; set; }
        
    }
}
