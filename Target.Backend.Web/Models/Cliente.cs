using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Target.Backend.Web.Models
{
    public class Cliente 
    {
        [Key]
        [Column("ClienteId")]
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }  
        public string CPF { get; set; }
        public decimal RendaMensal { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public int? PlanoId { get; set; }
        public virtual Plano Plano { get; set; }
        public ClienteEndereco Endereco { get; set; }
    }
}
