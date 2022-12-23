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
    /// <summary>
    /// controller for routing requests related to files
    /// </summary>
    [ApiController]
    [Route("api/files")]
    public class FilesController: ControllerBase
    {
        /// <summary>
        /// abstraction of an object for working with files
        /// </summary>
        private readonly IFilesManager _filesManager;

        /// <summary>
        /// initializes the file controller
        /// </summary>
        /// <param name="filesManager">service for working with files</param>
        public FilesController(IFilesManager filesManager)
        {
            _filesManager = filesManager;
        }
        
        /// <summary>
        /// method of processing the file upload path
        /// </summary>
        /// <param name="model">the model of information about the uploaded file</param>
        /// <param name="file">uploadable file</param>
        /// <returns>the uri by which the downloaded file can be uploaded</returns>
        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromQuery]UploadFileModel model, IFormFile file)
        {
            try
            {
                var id = await _filesManager.UploadFileAsync(model, file);
                var uri = new Uri($"{Request.Scheme}://{Request.Host}/api/files/id={id}");
                return Ok(uri);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
            
        }
        
        /// <summary>
        /// method of processing the text loading path
        /// </summary>
        /// <param name="model">the model of information about the uploaded text</param>
        /// <param name="text">the model of the uploaded text</param>
        /// <returns>the uri with which the uploaded text can be viewed</returns>
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

        /// <summary>
        /// a method for processing the path for uploading a file or viewing the uploaded text
        /// </summary>
        /// <param name="id">unique identifier of the uploaded data</param>
        /// <returns>File or html page for viewing text</returns>
        [HttpGet("id={id:guid}")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var file = _filesManager.GetFile(id);
            if (file == null)
                return NotFound();
            Response.Headers.Append("IsDownload", "true");
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
                //_filesManager.CascadeDelete(file);//deleting only from the database!!!
                return File(fileStream, fileType, file.Name);
            }

            return NoContent();
        }
        
        /// <summary>
        /// updates the value for the file to delete the file when it is received or not
        /// </summary>
        /// <param name="id">unique identifier of the uploaded data</param>
        /// <returns>status response code of the file update request</returns>
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateFileCascadeDelete(Guid id)
        {
            var response = await _filesManager.UpdateCascade(id);
            if (response.Success)
                return Ok();
            return BadRequest();
        }
        
        /// <summary>
        /// deletes a file
        /// </summary>
        /// <param name="id">unique identifier of the uploaded data</param>
        /// <returns>status response code of the file deletion request</returns>
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