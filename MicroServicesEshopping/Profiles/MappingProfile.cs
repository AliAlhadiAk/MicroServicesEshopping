using AutoMapper;
using MicroServicesEshopping.DTO_s;
using MicroServicesEshopping.Model;

namespace MicroServicesEshopping.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book,CreateBookDTO>();
        }
    }
}
