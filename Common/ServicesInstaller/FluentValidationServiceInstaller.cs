using Common.AssemplyScanning;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Configurations
{
    public class FluentValidationServiceInstaller : IInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidation(options =>
            {
                options.AutomaticValidationEnabled = true;
                options.DisableDataAnnotationsValidation = true;
            });
        }
    }
}
