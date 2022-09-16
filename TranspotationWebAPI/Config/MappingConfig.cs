using AutoMapper;
using TranspotationAPI.Model;
using TranspotationAPI.Model.Dto;
using TranspotationWebAPI.Model.Dto;

namespace TranspotationAPI.Config
{
    // For mapping the model to dto
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Mapping Account and get user information by id dto together
            CreateMap<Account, GetUserInformationResDto>().ReverseMap();
            // Mapping Account and get all user information dto together
            CreateMap<Account, GetAllUserInformationResDto>().ReverseMap();
        }
    }   

}

