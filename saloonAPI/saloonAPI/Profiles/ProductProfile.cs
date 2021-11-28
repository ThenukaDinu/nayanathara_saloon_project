using AutoMapper;
using saloonAPI.Models;
using saloonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<AddProduct, Product>();
            CreateMap<EditProduct, Product>()
                .ForMember(e => e.CreatedUserId, opt => opt.UseDestinationValue())
                .ForMember(e => e.Comments, opt => opt.UseDestinationValue())
                .ForMember(e => e.Likes, opt => opt.UseDestinationValue());
        }
    }
}
