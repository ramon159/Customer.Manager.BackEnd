using System.ComponentModel.DataAnnotations;
using Target.Backend.Web.Attributes;

namespace Target.Backend.Web.DTO
{
    public class ClienteEnderecoDTO
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
    }
}
