public class MessageInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<MessageViewModel>, MessageValidator>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageUnitOfWork, MessageUnitOfWork>();
    }
}
