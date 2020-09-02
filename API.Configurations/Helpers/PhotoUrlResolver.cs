using API.Models;
using API.Models.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace API.Configurations.Helpers {
    public class PhotoUrlResolver : IValueResolver<Product, ProductToReturnDto, string> {
        private readonly IConfiguration _config;
        public PhotoUrlResolver (IConfiguration config) {
            _config = config;
        }

        public string Resolve (Product source, ProductToReturnDto destination, string destMember, ResolutionContext context) {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
         }
    }
}