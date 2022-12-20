using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SecretsShare.Managers.ManagersInterfaces;
using SecretsShare.Models;

namespace SecretsShare.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController: ControllerBase
    {
        private readonly IFilesManager _filesManager;

        public FilesController(IFilesManager filesManager)
        {
            _filesManager = filesManager;
        }
        
        [HttpPost("upload")]
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

        [HttpPost("uploadText")]
        public async Task<IActionResult> UploadText([FromQuery] UploadFileModel model, [FromBody] UploadTextModel text)
        {
            var uri = await _filesManager.UploadTextFile(model, text);
            return Ok(uri);//uri
        }
        
        [HttpGet("downloadText")]
        public IActionResult ViewTextFile([FromQuery]string uri)
        {
            var file = _filesManager.ViewTextFile(uri);
            if (file is null)
                return NoContent();
            return Ok(file);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateFileCascadeDelete(string uri)
        {
            var response = await _filesManager.UpdateCascade(uri);
            if (response.Success)
                return Ok();
            return BadRequest();
        }
        
        [HttpDelete("delete")]
        public IActionResult DeleteFile(string uri)
        {
            var response = _filesManager.DeleteFile(uri);
            if (response.Success)
                return Ok();
            return BadRequest();
        }
    }
}