using AutoMapper;
using HelpUs.API.DataAccess.ProjectRepository;
using HelpUs.API.DataTransferObjects.Requests;
using HelpUs.API.DataTransferObjects.Responses;
using HelpUs.API.Models;
using HelpUs.API.Service.ImageService;
using HelpUs.DataAccess.DataTransferObjects.Responses;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace HelpUs.API.Services.ProjectServices
{
    public class ProjectService(IphotoService photoService,IMapper mapper, IProjectRepo projectRepo) : IProjectService
    {
        public async Task<APIResponse<ProjectResponse>> AddProject(ProjectCreateDto projectCreateDto, IFormFile? Image)
        {

            if(projectCreateDto is null) 
                return new APIResponse<ProjectResponse> 
                { 
                    Message = "Kindly the request body",
                    IsSuccessful  = false 
                };

            var imageUrl = string.Empty;
            if(Image is  null && Image!.Length == 0)
                return new APIResponse<ProjectResponse> 
                {
                    IsSuccessful = false,
                    Message = "Provide an image for this project"
                };

            var projectToAdd = await projectRepo.GetByNameAsync(projectCreateDto.Name);
            if (projectToAdd is not null)
                return new APIResponse<ProjectResponse>
                {
                    IsSuccessful = false,
                    Message = "Product already exist"
                };

            var imageResult = await photoService.AddPhotoAsync(Image);
            imageUrl = imageResult is not null ? imageResult.Url.ToString() : "https://www.testurl.com";

            projectToAdd = mapper.Map<Project>(projectCreateDto);
            projectToAdd.Image = imageUrl;

            var isAdded = await projectRepo.AddAsync(projectToAdd);
            if (!isAdded) 
                return new APIResponse<ProjectResponse> 
                { 
                    IsSuccessful = false,
                    Message = "Project creation failed at db level"
                };

            var res = mapper.Map<ProjectResponse>(projectToAdd);
            return new APIResponse<ProjectResponse>
            {
                IsSuccessful = true,
                Message = "Project created",
                Data = res
            };
        }

        public async Task<APIResponse<string>> Delete(string projectId)
        {
          var projectToDelete = await projectRepo.GetByIDAsync(projectId);
            if(projectToDelete is null) 
                return new APIResponse<string> 
                { 
                    Message = "No project record found",
                    IsSuccessful = false 
                };
            var isDeleted = await projectRepo.RemoveAsync(projectToDelete);
            if (!isDeleted) 
                return new APIResponse<string> 
                {
                    Message = "Proect deletion failed",
                    IsSuccessful = false
                };
            return new APIResponse<string>
            {
                Message = "Proect deleted",
                IsSuccessful = true
            };
        }

        public async Task<APIResponse<ProjectResponse>> Edit(ProjectEditDto projectCreateDto, string id, IFormFile image)
        {
            if (string.IsNullOrEmpty(id))
                return new APIResponse<ProjectResponse> { IsSuccessful = false, Message = "Invalid project id" };
            var projectToUpdate = await projectRepo.GetByIDAsync(id);
            if (projectToUpdate is null)
                return new APIResponse<ProjectResponse>
                {
                    Message = "No project record found",
                    IsSuccessful = false
                };

            var imageUrl = string.Empty;
            var fileToUploadExist = image is not null && image.Length > 0;

            if (fileToUploadExist)
            {
                var imageResult = await photoService.AddPhotoAsync(image!);
                imageUrl = imageResult?.Url?.ToString();
            }

            projectToUpdate.Name = !string.IsNullOrEmpty(projectCreateDto.Name) ? projectCreateDto.Name! : projectToUpdate.Name;
            projectToUpdate.Description = !string.IsNullOrEmpty(projectCreateDto.Description) ? projectCreateDto.Description! : projectToUpdate.Description;
            projectToUpdate.Image = !string.IsNullOrEmpty(imageUrl) ? imageUrl! : projectToUpdate.Image;
            projectToUpdate.TargetAmount = !projectCreateDto.TargetAmount.Equals(0) ? projectCreateDto.TargetAmount : projectToUpdate.TargetAmount;
            projectToUpdate.DateUpdated = DateTime.Now;

            var isUpdated = await projectRepo.UpdateAsync(projectToUpdate);

            if (isUpdated)
            {
                var projectToreturn = mapper.Map<ProjectResponse>(projectToUpdate);
                return new APIResponse<ProjectResponse>
                {
                    Message = "project successfully updated",
                    IsSuccessful = true,
                    Data = projectToreturn
                };
            }

            return new APIResponse<ProjectResponse>
            {
                Message = "project update failed",
                IsSuccessful = false,
                Errors = new string[] { "project Update Failed" }
            };

        }

        public APIResponse<List<ProjectResponse>> GetAll()
        {
            var projects =  projectRepo.GetAll().ToList();

            if (projects is null || projects.Count == 0)
                return new APIResponse<List<ProjectResponse>>
                {
                    Message = "No record found",
                    IsSuccessful = false
                };
            return new APIResponse<List<ProjectResponse>>
            {
                Message = "Successfully fetched projects",
                IsSuccessful = true,
                Data = mapper.Map<List<ProjectResponse>>(projects)
            };
        }

        public async Task<APIResponse<ProjectResponse>> GetById(string projectId)
        {
            var project = await projectRepo.GetByIDAsync(projectId);

            if (project == null) return new APIResponse<ProjectResponse>
            {
                Message = "No record found",
                IsSuccessful = false,
            };

            return new APIResponse<ProjectResponse>
            {
                Message = "Successfully fetched project",
                IsSuccessful = true,
                Data = mapper.Map<ProjectResponse>(project)
            };
        }
    }
}
