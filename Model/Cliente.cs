using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LOCACOES.API.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Cpf { get; set; }

        
        public DateTime DataNascimento { get; set; }
        public List<Locacao> Locacoes { get; set; }

        //[JsonProperty("dateOfBirth")]
        //public string data { get; set; }
        //[JsonIgnore]
        //public DateTime DAOQWE { get { return DateTime.Parse(data, new CultureInfo("pt-BR")); }  }
    }
}
