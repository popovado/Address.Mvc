using Address.Mvc.Models;
using AutoMapper;
using static Address.Mvc.Models.DadataResponse;

namespace Address.Mvc.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DadataRequest, string>().ConvertUsing(src => src.RawAddress);
            CreateMap<string, DadataResponse>();

            /*CreateMap<DadataRequest, DadataRequest>()
            .ForMember(dest => dest.query, opt => opt.MapFrom(src => src.RawAddress));
            CreateMap<DadataResponse, DadataAddressResponse>()
                .ForMember(dest => dest.StandardizedAddress, opt => opt.MapFrom(src => src.result));*/
        }
    }
}