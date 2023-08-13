using TestApi.Models;
using TestApi.Services.IServices;

namespace TestApi.Services;

public class FileService : IFileService
{

    private IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task PostFileAsync(IFormFile file)
    {
        try
        {

            if (file == null || file.Length <= 0)
            {
                throw new Exception("No file uploaded");
            }

            string[] allowedExtensions = { ".pdf", ".docx" };
            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new Exception("Invalid file type. Only PDF and Word documents are allowed.");
            }

            // Creates a unique filename for the uploaded file
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Specify the target directory to save the uploaded file
            var uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");

            // Creates the target directory if it doesn't exist
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Creates the full path for the uploaded file
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Saves the uploaded file to the target path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }


        }
        catch (Exception)
        {
            throw;
        }
    }

    
    public async Task PostMultiFileAsync(List<IFormFile> fileData)
    {
        try
        {
            foreach (IFormFile file in fileData)
            {
                if (file == null || file.Length <= 0)
                {
                    throw new Exception("No file uploaded");
                }
                string[] allowedExtensions = { ".pdf", ".docx" };
                string fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new Exception("Invalid file type. Only PDF and Word documents are allowed.");
                }

                var fileDetails = new FileDetails()
                {

                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                };

                var uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");

                // Creates the target directory if it doesn't exist
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Creates the full path for the uploaded file
                var filePath = Path.Combine(uploadDirectory, file.FileName);

                // Saves the uploaded file to the target path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                
            }
            
        }
        catch (Exception)
        {
            throw;
        }
    }
}
