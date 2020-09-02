using API.Configurations.Helpers;
using API.Models;
using API.Models.DTO;
using AutoMapper;

namespace API.Configurations.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d =>d.ProductBrand, o => o.MapFrom(s =>s.ProductBrand.Name))
            .ForMember(d =>d.ProductType, o => o.MapFrom(s =>s.ProductType.Name))
            .ForMember(d =>d.PictureUrl, o =>o.MapFrom<PhotoUrlResolver>());
        }
    }
}