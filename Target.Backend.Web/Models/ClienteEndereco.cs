using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Target.Backend.Web.Models
{
    public class ClienteEndereco
    {
        [Key]
        [Column("ClienteEnderecoId")]
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public int ClienteId { get; set; }
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
    }
}
