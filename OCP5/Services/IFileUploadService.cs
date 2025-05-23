using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace OCP5.Services;

public interface IFileUploadService
{
    public Task<string?> UploadFileAsync(IFormFile? formFile, string folderName);
    public FileResult? GetImage(string folderName, string? fileName);
    public void DeleteFile(string folderName, string fileName);
}

public class FileUploadService(IWebHostEnvironment environment) : IFileUploadService
{
    public async Task<string?> UploadFileAsync(IFormFile? formFile, string folderName)
    {
        if (formFile is { Length: > 0 } && !string.IsNullOrEmpty(folderName) && !string.IsNullOrWhiteSpace(formFile.FileName))
        {
            if (formFile.ContentType is "image/png" or "image/jpeg" or "image/jpg" or "image/webp")
            {
                try
                {
                    var directoryPath = Path.Combine(environment.ContentRootPath, folderName);
                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    var filePath = Path.Combine(directoryPath,
                        $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{Path.GetExtension(formFile.FileName)}");

                    await using var stream = File.Create(filePath);
                    await formFile.CopyToAsync(stream);
                    return Path.GetFileName(filePath);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        
        return null;
    }
    
    public FileResult? GetImage(string folderName, string fileName)
    {
        try
        {
            const string defaultImageName = "Image_not_available.png";
            var directoryPath = Path.Combine(environment.ContentRootPath, folderName);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            
            var filePath = Path.Combine(directoryPath, defaultImageName);
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName) || ! new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var fileNameContentType))
            {
                if (File.Exists(filePath) && new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var defaultImageContentType))
                    return new PhysicalFileResult(filePath, defaultImageContentType);
            }
            else
            {
                filePath = Path.Combine(directoryPath, fileName);
                if (File.Exists(filePath))
                {
                    return new PhysicalFileResult(filePath, fileNameContentType);
                }

                filePath = Path.Combine(directoryPath, defaultImageName);
                if (File.Exists(filePath) && new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var contentType1))
                {
                    return new PhysicalFileResult(filePath, contentType1);
                }
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    public void DeleteFile(string folderName, string fileName)
    {
        var directoryPath = Path.Combine(environment.ContentRootPath, folderName);
        if (Directory.Exists(directoryPath))
        {
            var filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}