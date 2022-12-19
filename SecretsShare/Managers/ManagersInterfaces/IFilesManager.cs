using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SecretsShare.HelperObject;
using SecretsShare.Models;
using SecretsShare.DTO;

namespace SecretsShare.Managers.ManagersInterfaces
{
    public interface IFilesManager
    {
        public Task<string> UploadFileAsync(UploadFileModel model, IFormFile file);
        public Task<string> UploadTextFile(UploadFileModel model, UploadTextModel text);
        public Task<Uri> ViewTextFile();
        public File DownLoadFile(string uri);
    }
}