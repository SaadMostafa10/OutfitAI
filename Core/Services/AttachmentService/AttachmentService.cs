using Microsoft.AspNetCore.Http;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtensions = new() { ".png", ".jpg", ".jpeg", ".webp", ".heic" };
        private const int _allowedMaxSize = 5 * 1024 * 1024; // 5 MB

        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            // 1] Validation: Ensure both extension and content type are valid
            var extension = Path.GetExtension(file.FileName).ToLower(); // Convert to lowercase to avoid case-sensitive issues
            var contentType = file.ContentType.ToLower(); // Convert to lowercase for consistent comparison

            bool isAllowedExtension = _allowedExtensions.Contains(extension);
            bool isAllowedMimeType = contentType.StartsWith("image/");

            // If either validation fails, return null to stop the upload process
            if (!isAllowedExtension || !isAllowedMimeType)
                return null;

            // 2] Validation For Max Size [5 MB]
            if (file.Length > _allowedMaxSize)
                return null;

            // 3] Get Located Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 4] Set Image to be Unique to block duplicates
            var fileName = $"{Guid.NewGuid()}{extension}";

            // 5] Get Full FilePath
            var filePath = Path.Combine(folderPath, fileName);

            // 6] Save File as Stream
            using var fileStream = new FileStream(filePath, FileMode.Create);

            // 7] Copy File To FileStream
            await file.CopyToAsync(fileStream);

            // 8] Return FileName to be stored in DB
            return fileName;
        }

        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }

}


