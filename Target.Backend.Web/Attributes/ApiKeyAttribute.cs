using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Target.Backend.Web.Attributes
{
    /// <summary>
    /// Atributo de autenticação via Api Key
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "X-API-Key";


        public async Task OnActionExecutionAsync(
           ActionExecutingContext context,
           ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var apiKey = configuration.GetValue<string>(key: "ApiKey");
            
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey não encontrada" 
                };
                return;
            }


            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
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
