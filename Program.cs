using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureAuthentication(builder);
ConfigureMVC(builder);
ConfigureServices(builder);

var app = builder.Build();
LoadConfiguration(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

static void LoadConfiguration(WebApplication app)
{
  Configuration.JWTKey = app.Configuration.GetValue<string>("JWTKey");
  Configuration.APIKeyHeaderParameterName = app.Configuration.GetValue<string>("APIKeyHeaderParameterName");
  Configuration.APIKey = app.Configuration.GetValue<string>("APIKey");

  var SMTP = new Configuration.SMTPConfiguration();
  app.Configuration.GetSection("SMTP").Bind(SMTP);
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