using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace HelpUs.API.Service.ImageService
{
    public interface IphotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile formFile);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
