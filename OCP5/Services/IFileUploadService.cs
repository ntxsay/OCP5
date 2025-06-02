using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace OCP5.Services;

public interface IFileUploadService
{
    /// <summary>
    /// Met en ligne un fichier dans le dossier spécifié.
    /// </summary>
    /// <param name="formFile"></param>
    /// <param name="folderName"></param>
    /// <param name="contentTypes"></param>
    /// <param name="maxSize"></param>
    /// <returns></returns>
    /// <remarks>IMPORTANT : A des fins de test uniquement, en production veuillez vérifier la signature du fichier</remarks>
    public Task<string?> UploadFileAsync(IFormFile? formFile, string folderName, string[] contentTypes, long maxSize);

    /// <summary>
    /// Récupère un fichier du dossier spécifié.
    /// </summary>
    /// <param name="folderName"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public FileResult? GetFile(string folderName, string? fileName);

    /// <summary>
    /// Supprime un fichier du dossier spécifié.
    /// </summary>
    /// <param name="folderName"></param>
    /// <param name="fileName"></param>
    public void DeleteFile(string folderName, string fileName);
}

public class FileUploadService(IWebHostEnvironment environment, ILogger<IFileUploadService> logger) : IFileUploadService
{
    private const string DefaultImageName = "Image_not_available.png";
    private const string DefaultImageContentType = "image/png";

    public async Task<string?> UploadFileAsync(IFormFile? formFile, string folderName, string[] contentTypes,
        long maxSize)
    {
        if (formFile == null)
        {
            logger.LogInformation("Le fichier fourni est vide.");
            return null;
        }

        if (formFile.Length == 0 || formFile.Length > maxSize)
        {
            logger.LogWarning("Le fichier fourni est vide ou dépasse la taille maximale autorisée de {maxSize} octets.",
                maxSize);
            return null;
        }

        if (string.IsNullOrEmpty(folderName) || string.IsNullOrWhiteSpace(folderName))
        {
            logger.LogWarning("Le nom du dossier fourni dans lequel sera stocké le fichier est vide ou invalide.");
            return null;
        }

        if (contentTypes.Length == 0)
        {
            logger.LogWarning("La liste des types de contenu autorisés est vide ou invalide.");
            return null;
        }

        if (!contentTypes.Contains(formFile.ContentType, StringComparer.OrdinalIgnoreCase))
        {
            logger.LogWarning(
                "Le type de contenu du fichier fourni ({contentType}) n'est pas autorisé. Types autorisés : {contentTypes}",
                formFile.ContentType, string.Join(", ", contentTypes));
            return null;
        }

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

    public FileResult? GetFile(string folderName, string? fileName)
    {
        try
        {
            var directoryPath = Path.Combine(environment.ContentRootPath, folderName);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var provider = new FileExtensionContentTypeProvider();

            string filePath;
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName) ||
                !provider.TryGetContentType(fileName, out var fileNameContentType))
            {
                filePath = Path.Combine(directoryPath, DefaultImageName);
                if (File.Exists(filePath))
                {
                    logger.LogWarning(
                        "Aucun fichier n'a été fournit ou bien le type fichier n'est pas valide, l'image par défaut sera utilisée à la place.");
                    return new PhysicalFileResult(filePath, DefaultImageContentType);
                }
            }
            else
            {
                filePath = Path.Combine(directoryPath, fileName);
                if (File.Exists(filePath))
                    return new PhysicalFileResult(filePath, fileNameContentType);

                filePath = Path.Combine(directoryPath, DefaultImageName);
                if (File.Exists(filePath))
                {
                    logger.LogWarning(
                        "Le fichier {fileName} n'existe pas dans le dossier {folderName}, l'image par défaut sera utilisée par défaut.",
                        fileName, folderName);
                    return new PhysicalFileResult(filePath, DefaultImageContentType);
                }
            }

            logger.LogWarning("L'image par défaut n'existe pas.");
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
        if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrWhiteSpace(fileName))
        {
            if (!fileName.Equals(DefaultImageName, StringComparison.OrdinalIgnoreCase))
            {
                var directoryPath = Path.Combine(environment.ContentRootPath, folderName);
                if (Directory.Exists(directoryPath))
                {
                    var filePath = Path.Combine(directoryPath, fileName);
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            File.Delete(filePath);
                        }
                        catch (Exception e)
                        {
                            logger.LogError(e.Message);
                        }
                    }
                    else
                    {
                        logger.LogWarning("Le fichier {fileName} n'existe pas dans le dossier {folderName}.", fileName, folderName);
                    }
                }
                else
                {
                    logger.LogWarning("Le dossier {folderName} n'existe pas.", folderName);
                }
            }
            else
            {
                logger.LogWarning("L'image par défaut et ne peut pas être supprimé.");
            }
        }
        else
        {
            logger.LogWarning("Le nom du fichier à supprimer n'est pas renseigné.");
        }
    }
}