using AutoMapper;
using Target.Backend.Web.DTO;
using Target.Backend.Web.Models;

namespace Target.Backend.Web.Mappings
{
    public class PlanoProfiler : Profile
    {
        public PlanoProfiler()
        {
            CreateMap<PlanoDTO, Plano>()
                .ReverseMap();
        }
    }
}
