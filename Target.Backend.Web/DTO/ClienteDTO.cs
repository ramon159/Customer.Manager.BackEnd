using System;
using System.ComponentModel.DataAnnotations;

namespace Target.Backend.Web.DTO
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Este campo deve conter entre {2} a {1} caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.DateTime, ErrorMessage = "Este campo deve ter o formato de data do tipo JSON")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        // adicionar validação customizada
        public string CPF { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, 999999.99, ErrorMessage = "A renda mensal deve ser entre {1} a {2} R$")]
        public decimal RendaMensal { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public ClienteEnderecoDTO Endereco { get; set; }
    }
}
