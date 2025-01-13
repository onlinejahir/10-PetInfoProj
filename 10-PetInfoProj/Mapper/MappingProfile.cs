using _10_PetInfoProj.ViewModels.AnimalTypeVM;
using _10_PetInfoProj.ViewModels.PetVM;
using AutoMapper;
using PetInfo.Models.EntityModels;

namespace _10_PetInfoProj.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PetViewModel, Pet>().ReverseMap();
            CreateMap<PetEditViewModel, Pet>().ReverseMap();
            CreateMap<AnimalTypeViewModel, AnimalType>().ReverseMap();
        }
    }
}
