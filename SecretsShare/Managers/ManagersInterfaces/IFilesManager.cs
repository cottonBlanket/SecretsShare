using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SecretsShare.HelperObject;
using SecretsShare.Models;

namespace SecretsShare.Managers.ManagersInterfaces
{
    public interface IFilesManager
    {
        public string UploadFile(UploadFileModel model, IFormFile file);
        public Task<Uri> DownloadFile();
        public Uri UploadTextFile();
        public Task<Uri> ViewTextFile();
    }
}