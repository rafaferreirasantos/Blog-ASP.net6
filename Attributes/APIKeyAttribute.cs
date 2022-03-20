using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Attributes
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
  public class APIKeyAttribute : Attribute, IAsyncActionFilter
  {
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      if (!context.HttpContext.Request.Headers.TryGetValue(Configuration.APIKeyHeaderParameterName, out var APIKeyValue))
      {
        context.Result = new ContentResult
        {
          StatusCode = 401,
          Content = "Acesso não autorizado"
        };
        return;
      }
      if (!Configuration.APIKey.Equals(APIKeyValue))
      {
        context.Result = new ContentResult
        {
          StatusCode = 403,
          Content = "Acesso não autorizado"
        };
        return;
      }
      await next();
    }
  }
}
