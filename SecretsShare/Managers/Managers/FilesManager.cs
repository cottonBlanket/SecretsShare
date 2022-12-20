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
        public async Task<Guid> UploadFileAsync(UploadFileModel model, IFormFile file)
        {
            var fileEntity = _mapper.Map<File>(model);
            var path = $"..\\Files\\File\\{Guid.NewGuid()}.{file.FileName.Split('.').Last()}";
            fileEntity.Name = file.FileName;
            fileEntity.Path = path;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
    
            var id = await _filesRepository.AddAsync(fileEntity);
            return id;
        }

        public async Task<Guid> UploadTextFile(UploadFileModel model, UploadTextModel text)
        {
            var fileEntity = _mapper.Map<File>(model);
            var unique = Guid.NewGuid();
            var fileInfo = new FileInfo($"..\\Files\\TextFile\\{unique}.txt");
            fileEntity.Name = text.Name;
            fileEntity.Path = fileInfo.Name;
            using (StreamWriter sw = fileInfo.CreateText())
            {
                await sw.WriteAsync(text.Text);
            }

            var id = await _filesRepository.AddAsync(fileEntity);
            return id;
        }

        public TextFileResponse ViewTextFile(File file)
        {
            using (StreamReader reader = new StreamReader(file.Path))
            {
                var fileResponse = new TextFileResponse()
                {
                    Name = file.Name,
                    Text = reader.ReadToEnd()
                };
                if (file.Cascade)
                {
                    _filesRepository.OnCascadeDelete(file);
                    System.IO.File.Delete(file.Path);
                }
                return fileResponse;
            }
        }

        public File GetFile(string id)
        {
            //обработать исключение с noconntent
            var file = _filesRepository.GetById(Guid.Parse(id));
            if (file is null)
                return null;
            if (file.Cascade)
            {
                _filesRepository.OnCascadeDelete(file);
                System.IO.File.Delete(file.Path);
            }
                
            return file;
        }

        public async Task<SuccessResponse> UpdateCascade(Guid fileId)
        {
            var file = _filesRepository.GetById(fileId);
            if (file is null)
                return new SuccessResponse(false);
            file.Cascade = !file.Cascade;
            var id = await _filesRepository.UpdateAsync(file);
            return new SuccessResponse(file.Id == id);
        }

        public SuccessResponse DeleteFile(Guid id)
        {
            var file = _filesRepository.GetById(id);
            if (file is null)
                return new SuccessResponse(false);
            _filesRepository.DeleteFile(file);
            System.IO.File.Delete(file.Path);
            return new SuccessResponse(true);
        }
    }
}