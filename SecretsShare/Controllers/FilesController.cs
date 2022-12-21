using System;
using System.IO;
using System.Text;
using SecretsShare.Attributes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretsShare.DTO;
using SecretsShare.Managers.ManagersInterfaces;
using SecretsShare.Models;
using File = SecretsShare.DTO.File;

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
        
        //[Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromQuery]UploadFileModel model, IFormFile file)
        {
            try
            {
                var id = await _filesManager.UploadFileAsync(model, file);
                var uri = new Uri($"{Request.Scheme}://{Request.Host}/id={id}");
                return Ok(uri);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
            
        }
        
        [Authorize]
        [HttpPost("uploadText")]
        public async Task<IActionResult> UploadText([FromQuery] UploadFileModel model, [FromBody] UploadTextModel text)
        {
            try
            {
                var id = await _filesManager.UploadTextFile(model, text);
                var uri = new Uri($"{Request.Scheme}://{Request.Host}/api/files/id={id}");
                return Ok(uri);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> DownloadFile(string id)
        {
            var file = _filesManager.GetFile(id);
            if (file == null)
                return NotFound();
            if (file.FileType == "TextFile")
            {
                using var reader = new StreamReader(file.Path);
                var response = new StringBuilder();
                response.Append($"<h1 style=\"text-align:center;\">{file.Name}</h1>");
                response.Append($"<p style=\"width:1170px;margin-right: 30px;margin-left:30px;\">{reader.ReadToEnd()}</p>");
                await Response.WriteAsync(response.ToString());
                _filesManager.CascadeDelete(file);
                return Ok();
            }
            if(file.FileType == "File")
            {
                var fileType="application/octet-stream";
                var fileStream = new FileStream(file.Path, FileMode.Open);
                return File(fileStream, fileType, file.Name);
            }

            return NoContent();
        }

        //[Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateFileCascadeDelete(Guid id)
        {
            var response = await _filesManager.UpdateCascade(id);
            if (response.Success)
                return Ok();
            return BadRequest();
        }
        
        //[Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteFile(Guid id)
        {
            var response = _filesManager.DeleteFile(id);
            if (response.Success)
                return Ok();
            return BadRequest();
        }
    }
}