using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LOCACOES.API.Model
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ClassificacaoIndicativa { get; set; }
        public byte Lancamento { get; set; }
        public List<Locacao> Locacoes { get; set; }

    }
}
