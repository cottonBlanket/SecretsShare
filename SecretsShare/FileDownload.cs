using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;
using SecretsShare.Managers.ManagersInterfaces;

namespace SecretsShare
{
    public class FileDownload
    {
        private readonly RequestDelegate _next;

        public FileDownload(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IFilesManager filesManager)
        {
            var path = context.Request.Path.ToString();
            if(path.Contains("/id/"))
            {
                var id = path.Split("/").Last();
                var file = filesManager.GetFile(id);
                if (file != null)
                {
                    var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                    var fileinfo = fileProvider.GetFileInfo(file.Path);
                    System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = file.Name,
                        Inline = true
                    };
 
                    context.Response.Headers.Add("Content-Disposition", cd.ToString());
                    await context.Response.SendFileAsync(file.Path, CancellationToken.None);
                }
                else
                    await context.Response.WriteAsync("File not found");
            }
                
            await _next(context);
        }
    }
}