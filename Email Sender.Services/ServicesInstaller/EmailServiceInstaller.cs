using Common.AssemplyScanning;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class EmailServiceInstaller : IInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //Add Config for Required Email
        services.Configure<IdentityOptions>(
            opts => opts.SignIn.RequireConfirmedEmail = true
            );
        //Add Email Configs
        var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        services.AddSingleton(emailConfig);

        //Add Email Services
        services.AddScoped<IEmailService, EmailService>();

        services.Configure<DataProtectionTokenProviderOptions>(
        opts => opts.TokenLifespan = TimeSpan.FromHours(10));
    }
}