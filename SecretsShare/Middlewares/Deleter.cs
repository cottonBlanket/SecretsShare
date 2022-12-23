using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SecretsShare.Managers.ManagersInterfaces;

namespace SecretsShare.Middlewares
{
    public class Deleter
    {
        private readonly RequestDelegate _next;
    
        public Deleter(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IFilesManager filesManager)
        {
            await _next.Invoke(context);
            if (context.Response.Headers.ContainsKey("IsDownload"))
            {
                var id = context.Request.Headers[":path"].ToString().Split('=').Last();
                filesManager.DeleteFile(Guid.Parse(id));
            }
        }
    }
}