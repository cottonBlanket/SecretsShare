using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretsShare.Managers.ManagersInterfaces;
using SecretsShare.Models;

//UploadFile
//DownLoad File
//UploadText
//ViewTextFile

//DownloadModel
/*
 * UserId
 * URL
 */
namespace SecretsShare.Controllers
{
    [ApiController]
    [Route("Files")]
    public class FilesController: ControllerBase
    {
        private readonly IFilesManager _filesManager;

        public FilesController(IFilesManager filesManager)
        {
            _filesManager = filesManager;
        }
        
        [HttpPost("uploadFile")]
        public IActionResult UploadFile([FromQuery]UploadFileModel model, IFormFile file)
        {
            var uri = _filesManager.UploadFile(model, file);
            return Ok(uri);
        }
        
        [HttpGet("downloadFile")]
        public FileStreamResult DownloadFile([FromQuery]Guid userId, [FromQuery] Uri uri)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost("uploadText")]
        public IActionResult UploadText([FromQuery] UploadFileModel model, [FromBody] string text)
        {
            return Ok();//uri
        }
        
        [HttpGet("downloadText")]
        public FileStreamResult ViewTextFile([FromQuery]Guid userId, [FromQuery]Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}