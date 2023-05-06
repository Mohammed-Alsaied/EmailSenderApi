public class MessageUnitOfWork : BaseUnitOfWork<Message>, IMessageUnitOfWork
{
    public MessageUnitOfWork(IBaseRepository<Message> repsitory) : base(repsitory)
    {
    }
}