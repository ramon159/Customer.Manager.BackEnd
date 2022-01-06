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
            CreateMap<ClienteDTO, ClienteEndereco>()
                .ForMember(d =>
                d,
                opt => opt.MapFrom(s => s.Endereco))
                .ForMember(d =>
                    d.Bairro,
                    opt => opt.MapFrom(s => s.Endereco.Bairro))
                .ForMember(d =>
                    d.Cidade,
                    opt => opt.MapFrom(s => s.Endereco.Cidade))
                .ForMember(d =>
                    d.Complemento,
                    opt => opt.MapFrom(s => s.Endereco.Complemento))
                .ForMember(d =>
                    d.UF,
                    opt => opt.MapFrom(s => s.Endereco.UF))
                .ForMember(d =>
                    d.CEP,
                    opt => opt.MapFrom(s => s.Endereco.CEP))
                .ReverseMap();
        }
    }
}
