using System;
using System.IO;
using System.Threading.Tasks;
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
        public async Task<IActionResult> UploadFile([FromQuery]UploadFileModel model, IFormFile file)
        {
            try
            {
                var uri = await _filesManager.UploadFileAsync(model, file);
                return Ok(uri);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
            
        }
        
        [HttpGet("downloadFile")]
        public FileStreamResult DownloadFile([FromQuery] string uri)
        {
            var file = _filesManager.DownLoadFile(uri);
            var fileType="application/octet-stream";
            if (file != null)
            {
                var fileStream = new FileStream(file.Path, FileMode.Open);
                return File(fileStream, fileType, file.Name);
            }

            return new FileStreamResult(Stream.Null, fileType);
        }
        
        [HttpPost("uploadText")]
        public async Task<IActionResult> UploadText([FromQuery] UploadFileModel model, [FromBody] UploadTextModel text)
        {
            var uri = await _filesManager.UploadTextFile(model, text);
            return Ok(uri);//uri
        }
        
        [HttpGet("downloadText")]
        public FileStreamResult ViewTextFile([FromQuery]Guid userId, [FromQuery]Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}