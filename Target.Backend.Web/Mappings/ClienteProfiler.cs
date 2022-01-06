using AutoMapper;
using Target.Backend.Web.DTO;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Mappings
{
    public class ClienteProfiler : Profile
    {
        public ClienteProfiler()
        {
            CreateMap<ClienteDTO, Cliente>()
                .ForMember(dest => dest.Endereco, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ClienteEnderecoDTO, ClienteEndereco>()
                .ReverseMap();
        }
    }
}
