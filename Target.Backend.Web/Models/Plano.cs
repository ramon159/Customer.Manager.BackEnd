using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Target.Backend.Web.Models
{
    public class Plano
    {
        [Key]
        [Column("PlanoId")]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; } 
    }
}
