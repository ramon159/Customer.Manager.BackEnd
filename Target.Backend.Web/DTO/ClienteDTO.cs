using System;
using System.ComponentModel.DataAnnotations;
using Target.Backend.Web.Attributes;

namespace Target.Backend.Web.DTO
{
    public class ClienteDTO
    {

        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public decimal RendaMensal { get; set; }
        public ClienteEnderecoDTO Endereco { get; set; }
    }
}
