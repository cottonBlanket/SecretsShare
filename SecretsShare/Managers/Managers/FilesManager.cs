using System;
using System.IO;
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
        
        public string UploadFile(UploadFileModel model, IFormFile file)
        {
            var uploadPath = $"..\\Files\\{file.Name}";
            var fileEntity = _mapper.Map<File>(model);
            fileEntity.Uri = uploadPath;
            fileEntity.FileType = "File";

            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
    
            _filesRepository.Add(fileEntity);
            return fileEntity.Uri;
        }

        public Task<Uri> DownloadFile()
        {
            throw new System.NotImplementedException();
        }

        public Uri UploadTextFile()
        {
            throw new System.NotImplementedException();
        }

        public Task<Uri> ViewTextFile()
        {
            throw new System.NotImplementedException();
        }
    }
}