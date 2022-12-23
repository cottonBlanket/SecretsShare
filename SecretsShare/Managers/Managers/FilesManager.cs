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
    /// <summary>
    /// a class for executing query logic related to files
    /// </summary>
    public class FilesManager: IFilesManager
    {
        /// <summary>
        /// abstraction of an object for executing database queries to a file's table
        /// </summary>
        private readonly IFilesRepository _filesRepository;
        
        /// <summary>
        /// abstraction of an object that maps an object of one type into an object of another type
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// initializes the file service class
        /// </summary>
        /// <param name="filesRepository">service for working with a database of files</param>
        /// <param name="mapper">mapping service</param>
        public FilesManager(IFilesRepository filesRepository, IMapper mapper)
        {
            _filesRepository = filesRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// creates the entity of the file uploaded by the user
        /// </summary>
        /// <param name="model">file information model</param>
        /// <param name="file">загруженный файл</param>
        /// <returns>unique identifier of the created entity</returns>
        public async Task<Guid> UploadFileAsync(UploadFileModel model, IFormFile file)
        {
            var fileEntity = _mapper.Map<File>(model);
            var path = $"..\\Files\\File\\{Guid.NewGuid()}.{file.FileName.Split('.').Last()}";
            fileEntity.Name = file.FileName;
            fileEntity.Path = path;
            fileEntity.FileType = "File";
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
    
            var id = await _filesRepository.AddAsync(fileEntity);
            return id;
        }

        /// <summary>
        /// creates an entity for the text uploaded by the user
        /// </summary>
        /// <param name="model">the model of information about the uploaded text</param>
        /// <param name="text">model of the uploaded text</param>
        /// <returns>unique identifier of the created entity</returns>
        public async Task<Guid> UploadTextFile(UploadFileModel model, UploadTextModel text)
        {
            var fileEntity = _mapper.Map<File>(model);
            var unique = Guid.NewGuid();
            var fileInfo = new FileInfo($"..\\Files\\TextFile\\{unique}.txt");
            fileEntity.Name = text.Name;
            fileEntity.Path = $"..\\Files\\TextFile\\{fileInfo.Name}";
            fileEntity.FileType = "TextFile";
            using (StreamWriter sw = fileInfo.CreateText())
            {
                await sw.WriteAsync(text.Text);
            }

            var id = await _filesRepository.AddAsync(fileEntity);
            return id;
        }

        /// <summary>
        /// deletes a file if the user specified its automatic deletion upon receipt
        /// </summary>
        /// <param name="file">the entity of the file</param>
        public void CascadeDelete(File file)
        {
            if (file.Cascade)
            {
                _filesRepository.DeleteFile(file);
                System.IO.File.Delete(file.Path);
                //it was not possible to implement a complete removal from the error of multi-user access to the file
            }
        }

        /// <summary>
        /// returns a file by its unique identifier
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <returns>the entity of the file</returns>
        public File GetFile(Guid id) => _filesRepository.GetById(id);

        /// <summary>
        /// sets the opposite value for the value that determines whether to delete the file after accessing it
        /// </summary>
        /// <param name="fileId">unique identifier of the file entity</param>
        /// <returns>response to the update</returns>
        public async Task<SuccessResponse> UpdateCascade(Guid fileId)
        {
            var file = _filesRepository.GetById(fileId);
            if (file is null)
                return new SuccessResponse(false);
            file.Cascade = !file.Cascade;
            var id = await _filesRepository.UpdateAsync(file);
            return new SuccessResponse(file.Id == id);
        }

        /// <summary>
        /// deletes a file
        /// </summary>
        /// <param name="id">unique identifier of the file entity</param>
        /// <returns>response to the deletion request</returns>
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