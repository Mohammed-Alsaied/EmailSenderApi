public class MessageMapperProfile : Profile
{
    public MessageMapperProfile()
    {
        CreateMap<Message, MessageViewModel>().ReverseMap();
    }
}