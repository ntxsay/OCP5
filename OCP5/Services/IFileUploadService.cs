using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace OCP5.Services;

public interface IFileUploadService
{
    public Task<string?> UploadFileAsync(IFormFile? formFile, string folderName, string[] contentTypes, long maxSize);
    public FileResult? GetFile(string folderName, string? fileName);
    public void DeleteFile(string folderName, string fileName);
}

public class FileUploadService(IWebHostEnvironment environment, ILogger<IFileUploadService> logger) : IFileUploadService
{
    public async Task<string?> UploadFileAsync(IFormFile? formFile, string folderName, string[] contentTypes, long maxSize)
    {
        if (formFile != null && formFile.Length > 0 && formFile.Length <= maxSize && !string.IsNullOrEmpty(folderName) && !string.IsNullOrWhiteSpace(folderName))
        {
            if (contentTypes.Contains(formFile.ContentType, StringComparer.OrdinalIgnoreCase))
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
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                    return null;
                }
            }
        }
        
        return null;
    }
    
    public FileResult? GetFile(string folderName, string? fileName)
    {
        try
        {
            const string defaultImageName = "Image_not_available.png";
            var directoryPath = Path.Combine(environment.ContentRootPath, folderName);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            
            var provider = new FileExtensionContentTypeProvider();

            var filePath = Path.Combine(directoryPath, defaultImageName);
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName) || ! provider.TryGetContentType(fileName, out var fileNameContentType))
            {
                if (File.Exists(filePath) && provider.TryGetContentType(filePath, out var defaultImageContentType))
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
                if (File.Exists(filePath) && provider.TryGetContentType(filePath, out var contentType1))
                {
                    return new PhysicalFileResult(filePath, contentType1);
                }
            }

            return null;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
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