namespace Blog;
public static class Configuration
{
  public static string JWTKey = "";
  public static string APIKeyHeaderParameterName = "";
  public static string APIKey = "";
  public static SMTPConfiguration SMTP { get; set; } = null!;

  public class SMTPConfiguration
  {
    public string Host { get; set; } = "";
    public int Port { get; set; }
    public string User { get; set; } = "";
    public string Password { get; set; } = "";
  }
}
