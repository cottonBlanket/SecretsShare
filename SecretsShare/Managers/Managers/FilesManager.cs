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
            var unique = Guid.NewGuid();
            var fileInfo = new FileInfo($"..\\Files\\TextFile\\{unique}.txt");
            fileEntity.Name = text.Name;
            fileEntity.Uri = $"https://SecretsShare/File/id={unique}";
            fileEntity.Path = fileInfo.FullName;
            using (StreamWriter sw = fileInfo.CreateText())
            {
                await sw.WriteAsync(text.Text);
            }

            var id = await _filesRepository.Add(fileEntity);
            return fileEntity.Uri;
        }

        public TextFileResponse ViewTextFile(string uri)
        {
            var file = _filesRepository.GetByUrlOrDefault(uri);
            if (file is null)
                return null;
            if(file.Cascade)
                _filesRepository.OnCascadeDelete(file);
            using (StreamReader reader = new StreamReader(file.Path))
            {
                var fileResponse = new TextFileResponse()
                {
                    Name = file.Name,
                    Text = reader.ReadToEnd()
                };
                return fileResponse;
            }
        }

        public File DownLoadFile(string uri)
        {
            //обработать исключение с noconntent
            var file = _filesRepository.GetByUrlOrDefault(uri);
            if (file is null)
                return null;
            if(file.Cascade)
                _filesRepository.OnCascadeDelete(file);
            return file;
        }
    }
}