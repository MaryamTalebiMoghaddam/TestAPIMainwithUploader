using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestApi.Models;
using TestApi.Services.IServices;

namespace TestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _uploadService;

        public FileController(IFileService uploadService)
        {
            _uploadService = uploadService;
        }

        /// <summary>
        /// Single File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("PostSingleFile")]
        
        public async Task<ActionResult> PostSingleFile([FromForm] FileUploadModel file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            try
            {
                await _uploadService.PostFileAsync(file.FileDetails);
                return Ok();
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Multiple File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("PostMultipleFile")]
        public async Task<ActionResult> PostMultipleFile([FromForm] List<IFormFile> fileDetails)
        {
            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                await _uploadService.PostMultiFileAsync(fileDetails);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

      

    }
}
