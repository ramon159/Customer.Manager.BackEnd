using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Target.Backend.Web.Attributes;

namespace Target.Backend.Web.Models
{
    public class Cliente 
    {
        [Key]
        [Column("ClienteId")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Este campo deve conter entre {2} a {1} caracteres")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.DateTime, ErrorMessage = "Este campo deve ter o formato de data do tipo JSON")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [CPF(ErrorMessage = "CPF inválido")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Este campo deve conter exatamente {1} caracteres, sem pontuação")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, 999999.99, ErrorMessage = "A renda mensal deve ser entre {1} a {2} R$")]
        public decimal RendaMensal { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public int? PlanoId { get; set; }
        public virtual Plano Plano { get; set; }
        public ClienteEndereco Endereco { get; set; }
    }
}
