using AutoMapper;
using TranspotationAPI.Model;
using TranspotationAPI.Model.Dto;

namespace TranspotationAPI.Config
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Account, GetUserInformationResDto>().ReverseMap();
        }
    }   

}

