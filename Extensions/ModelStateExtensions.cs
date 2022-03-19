using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Extensions;
public static class ModelStateExtensions
{
  public static List<string> GetErrors(this ModelStateDictionary modelState)
    => modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
}
