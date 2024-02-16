using AutoMapper;

namespace AI_Course_InventorySystem.Models.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap(); // Optional, enables mapping from ProductDto to Product
        }
    }
}
