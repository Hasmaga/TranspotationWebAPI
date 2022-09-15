using AutoMapper;
using TranspotationAPI.Model;
using TranspotationAPI.Model.Dto;

namespace TranspotationAPI.Config
{
    // For mapping the model to dto
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Mapping Account and get user information by id dto together
            CreateMap<Account, GetUserInformationResDto>().ReverseMap();
        }
    }   

}

