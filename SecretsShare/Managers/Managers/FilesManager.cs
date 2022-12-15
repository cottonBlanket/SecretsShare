using System.Threading.Tasks;
using SecretsShare.HelperObject;
using SecretsShare.Managers.ManagersInterfaces;

namespace SecretsShare.Managers.Managers
{
    public class FilesManager: IFilesManager
    {
        public Task<SuccessResponse> UploadFile()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> DownloadFile()
        {
            throw new System.NotImplementedException();
        }

        public Task<SuccessResponse> UploadTextFile()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> ViewTextFile()
        {
            throw new System.NotImplementedException();
        }
    }
}