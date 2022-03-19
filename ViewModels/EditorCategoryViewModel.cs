using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
  public class EditorCategoryViewModel
  {
    [Required(ErrorMessage = "O nome da categoria é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da categoria deve ter entre 3 e 100 caracteres")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "O slug da categoria é obrigatório")]
    public string? Slug { get; set; }
  }
}
