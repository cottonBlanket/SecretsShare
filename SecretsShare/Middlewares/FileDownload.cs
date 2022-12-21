// using System.IO;
// using System.Linq;
// using System.Text;
// using System.Text.RegularExpressions;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Routing;
// using Microsoft.Extensions.FileProviders;
// using SecretsShare.Managers.ManagersInterfaces;
//
// namespace SecretsShare.Middlewares
// {
//     public class FileDownload
//     {
//         private readonly RequestDelegate _next;
//
//         public FileDownload(RequestDelegate next)
//         {
//             _next = next;
//         }
//
//         public async Task Invoke(HttpContext context, IFilesManager filesManager)
//         {
//             var path = context.Request.Path.ToString();
//             if (path.Contains("/id/"))
//             {
//                 var id = path.Split("/").Last();
//                 var file = filesManager.GetFile(id);
//                 if (file is { FileType: "File" })
//                 {
//                     System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
//                     {
//                         FileName = file.Name,
//                         Inline = true
//                     };
//                     context.Response.Headers.Add("Content-Disposition", cd.ToString());
//                     await context.Response.SendFileAsync(file.Path, CancellationToken.None);
//                 }
//                 else if (file is { FileType: "TextFile" })
//                 {
//                     var text = filesManager.ViewTextFile(file);
//                     var response = new StringBuilder();
//                     response.Append($"<h1 style=\"text-align:center;\">{text.Name}</h1>");
//                     response.Append($"<p style=\"width:1170px;margin-right: 30px;margin-left:30px;\">{text.Text}</p>");
//                     context.Response.Headers.Add("Content-Type", "text/html charset=utf-8");
//                     System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
//                     {
//                         FileName = file.Name,
//                         Inline = true
//                     };
//                     context.Response.Headers.Add("Content-Disposition", cd.ToString());
//                     await context.Response.WriteAsync(response.ToString());
//                 }
//                 else
//                     context.Response.StatusCode = 404;
//             }
//             await _next(context);
//         }
//     }
// }