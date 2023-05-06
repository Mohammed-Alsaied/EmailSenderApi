// Add services to the container.
using Common.AssemplyScanning;
using Common.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddInstallerFromAssembly<BaseServiceInstaller>(builder.Configuration);
builder.Services.AddInstallerFromAssembly<MessageInstaller>(builder.Configuration);
builder.Services.AddInstallerFromAssembly<FluentValidationServiceInstaller>(builder.Configuration);
builder.Services.AddInstallerFromAssembly<PresentationServiceInstaller>(builder.Configuration);
builder.Services.AddInstallerFromAssembly<InfrastructureServiceInstaller>(builder.Configuration);
builder.Services.AddInstallerFromAssembly<EmailServiceInstaller>(builder.Configuration);

//JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});
//Add AddAutoMapper Services
builder.Services.AddAutoMapper(typeof(Program).Assembly,
    typeof(Message).Assembly);

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
