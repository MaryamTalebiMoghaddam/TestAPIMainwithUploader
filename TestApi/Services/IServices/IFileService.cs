using TestApi.Models;

namespace TestApi.Services.IServices
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData);

        public Task PostMultiFileAsync(List<IFormFile> fileData);
    }
}
