using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        
        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromQuery]UploadFileModel model, IFormFile file)
        {
            try
            {
                var id = await _filesManager.UploadFileAsync(model, file);
                var uri = new Uri($"{Request.Scheme}://{Request.Host}/id/{id}");
                return Ok(uri);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
            
        }
        
        //[Authorize]
        [HttpPost("uploadText")]
        public async Task<IActionResult> UploadText([FromQuery] UploadFileModel model, [FromBody] UploadTextModel text)
        {
            try
            {
                var id = await _filesManager.UploadTextFile(model, text);
                var uri = new Uri($"{Request.Scheme}://{Request.Host}/id/{id}");
                return Ok(uri);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateFileCascadeDelete(Guid id)
        {
            var response = await _filesManager.UpdateCascade(id);
            if (response.Success)
                return Ok();
            return BadRequest();
        }
        
        [Authorize]
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