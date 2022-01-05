using System.ComponentModel.DataAnnotations;
using Target.Backend.Web.Attributes;

namespace Target.Backend.Web.DTO
{
    public class ClienteEnderecoDTO
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Este campo deve conter entre {2} a {1} caracteres")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Este campo deve conter entre {2} a {1} caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Este campo deve conter entre {2} a {1} caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Este campo deve conter entre {2} a {1} caracteres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Este campo deve conter exatamente {1} caracteres")]
        [RegularExpression(@"[A-Z]{2}$", ErrorMessage = "Este campo aceita apenas caracteres maiúsculos")]
        [UF(ErrorMessage = "UF inválida")]
        public string UF { get; set; }

        [StringLength(8, MinimumLength = 8, ErrorMessage = "Este campo deve conter exatamente {1} caracteres, sem pontuação")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [CEP(ErrorMessage = "CEP inválido")]
        public string CEP { get; set; }
    }
}
