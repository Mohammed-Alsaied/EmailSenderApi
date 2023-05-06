public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(DbContext dbContext) : base(dbContext)
    {
    }
}