using System;
using System.Threading.Tasks;
using SecretsShare.HelperObject;

namespace SecretsShare.Managers.ManagersInterfaces
{
    public interface IFilesManager
    {
        public Task<SuccessResponse> UploadFile();
        public Task<Uri> DownloadFile();
        public Task<SuccessResponse> UploadTextFile();
        public Task<Uri> ViewTextFile();
    }
}