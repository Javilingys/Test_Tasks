using AutoMapper;
using Pong.API.Entities;
using Pong.API.Models.Dtos.Messages;

namespace Pong.API.Infrastructure.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PongMessage, PongMessageToReturnDto>();
            CreateMap<CreatePongMessageDto, PongMessage>();
        }
    }
}
