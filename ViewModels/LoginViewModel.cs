using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;
public class LoginViewModel
{
  [Required(ErrorMessage = "Email é obrigatório")]
  [EmailAddress(ErrorMessage = "Email inválido")]
  public string? Email { get; set; }
  [Required(ErrorMessage = "Password é obrigatório")]
  public string? Password { get; set; }
}
