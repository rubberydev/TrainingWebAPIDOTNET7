using AutoMapper;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTO;

namespace MagicVilla_API.Utilidades
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            //create
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();

            //update
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<NumeroVilla, NumeroVillaDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaCreateDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaUpdateDto>().ReverseMap();
        }
    }
}
