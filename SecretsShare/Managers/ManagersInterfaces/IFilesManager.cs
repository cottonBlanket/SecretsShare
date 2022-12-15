using System.Threading.Tasks;
using SecretsShare.HelperObject;

namespace SecretsShare.Managers.ManagersInterfaces
{
    public interface IFilesManager
    {
        public Task<SuccessResponse> UploadFile();
        public Task<string> DownloadFile();
        public Task<SuccessResponse> UploadTextFile();
        public Task<string> ViewTextFile();
    }
}