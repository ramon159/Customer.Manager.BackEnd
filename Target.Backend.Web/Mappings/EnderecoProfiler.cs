using AutoMapper;
using Target.Backend.Web.DTO;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Mappings
{
    public class EnderecoProfiler : Profile
    {
        public EnderecoProfiler()
        {
            AllowNullDestinationValues = false;
            
            CreateMap<ClienteEnderecoDTO, ClienteEndereco>()
                .ReverseMap();
        }

    }

}
