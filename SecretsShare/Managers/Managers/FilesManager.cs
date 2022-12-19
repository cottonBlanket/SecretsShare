using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SecretsShare.DTO;
using SecretsShare.HelperObject;
using SecretsShare.Managers.ManagersInterfaces;
using SecretsShare.Models;
using SecretsShare.Repositories.Interfaces;
using File = SecretsShare.DTO.File;

namespace SecretsShare.Managers.Managers
{
    public class FilesManager: IFilesManager
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IMapper _mapper;

        public FilesManager(IFilesRepository filesRepository, IMapper mapper)
        {
            _filesRepository = filesRepository;
            _mapper = mapper;
        }
        //доработать проблему с именами повторяющимися
        public async Task<string> UploadFileAsync(UploadFileModel model, IFormFile file)
        {
            var fileEntity = _mapper.Map<File>(model);
            var path = $"..\\Files\\File\\{fileEntity.GetType().GUID}.{file.FileName.Split('.').Last()}";
            fileEntity.Name = file.FileName;
            fileEntity.Uri = $"https://SecretsShare/File/id={file.GetHashCode()}";
            fileEntity.Path = path;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
    
            var id = await _filesRepository.Add(fileEntity);
            return fileEntity.Uri;
        }

        public Task<FileStream> DownloadFile(string uri)
        {
            throw new NotImplementedException();
        }

        public Uri UploadTextFile(UploadFileModel model, string text)
        {
            throw new NotImplementedException();
            //var path = new FileInfo($"..\\Files\\File\\{model.}");
        }

        public Task<Uri> ViewTextFile()
        {
            throw new System.NotImplementedException();
        }

        public File DownLoadFile(string uri)
        {
            //обработать исключение с noconntent
            var file = _filesRepository.GetAll().FirstOrDefault(x => x.Uri == uri);
            if (file is null)
                return null;
            if(file.Cascade)
                _filesRepository.OnCascadeDelete(file);
            return file;
        }
    }
}