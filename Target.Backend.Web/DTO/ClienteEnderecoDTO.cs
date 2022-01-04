using System.ComponentModel.DataAnnotations;

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
        public string UF { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        //adicionar validação customizada
        public string CEP { get; set; }
    }
}
