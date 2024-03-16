using AutoMapper;
using Web_App.Server.DTOs;
using Web_App.Server.Models;

namespace Web_App.Server;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<QuestionModel, QuestionDto>();

        CreateMap<MCSACardModel, MCSACardDto>();
    }
}
