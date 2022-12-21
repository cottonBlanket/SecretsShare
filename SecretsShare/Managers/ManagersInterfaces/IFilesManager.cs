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
        public Task<Guid> UploadFileAsync(UploadFileModel model, IFormFile file);
        public Task<Guid> UploadTextFile(UploadFileModel model, UploadTextModel text);
        //public TextFileResponse ViewTextFile(File file);
        public void CascadeDelete(File file);
        public File GetFile(string id);
        public Task<SuccessResponse> UpdateCascade(Guid id);
        public SuccessResponse DeleteFile(Guid id);
    }
}