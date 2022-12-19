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
            var path = $"..\\Files\\File\\{Guid.NewGuid()}.{file.FileName.Split('.').Last()}";
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

        public async Task<string> UploadTextFile(UploadFileModel model, UploadTextModel text)
        {
            var fileEntity = _mapper.Map<File>(model);
            var fileInfo = new FileInfo($"..\\Files\\TextFile\\{Guid.NewGuid()}.txt");
            fileEntity.Name = text.Name;
            fileEntity.Uri = $"https://SecretsShare/File/id={text.GetHashCode()}";
            fileEntity.Path = fileInfo.FullName;
            using (StreamWriter sw = fileInfo.CreateText())
            {
                await sw.WriteAsync(text.Text);
            }

            fileInfo.CopyTo(fileEntity.Path);
            var id = await _filesRepository.Add(fileEntity);
            return fileEntity.Uri;
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