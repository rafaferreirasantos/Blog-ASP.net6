using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
LoadConfiguration(builder);
ConfigureAuthentication(builder);
ConfigureMVC(builder);
ConfigureServices(builder);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

static void LoadConfiguration(WebApplicationBuilder builder)
{
    Configuration.JWTKey = builder.Configuration.GetValue<string>("JWTKey");
    Configuration.APIKeyHeaderParameterName = builder.Configuration.GetValue<string>("APIKeyHeaderParameterName");
    Configuration.APIKey = builder.Configuration.GetValue<string>("APIKey");

    var SMTP = new Configuration.SMTPConfiguration();
    builder.Configuration.GetSection("SMTP").Bind(SMTP);
    Configuration.SMTP = SMTP;
}
static void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}
static void ConfigureMVC(WebApplicationBuilder builder)
{
    builder.Services
      .AddControllers()
      .ConfigureApiBehaviorOptions(options =>
      {
          options.SuppressModelStateInvalidFilter = true;
      });
}
static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<TokenService>();
    builder.Services.AddTransient<EmailService>();
    builder.Services.AddDbContext<BlogDataContext>();
}