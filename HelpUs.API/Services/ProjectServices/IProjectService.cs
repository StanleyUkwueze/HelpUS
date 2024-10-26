using HelpUs.API.DataTransferObjects.Requests;
using HelpUs.API.DataTransferObjects.Responses;
using HelpUs.DataAccess.DataTransferObjects.Responses;

namespace HelpUs.API.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<APIResponse<ProjectResponse>> AddProject(ProjectCreateDto projectCreateDto, IFormFile? Image);
        APIResponse<List<ProjectResponse>> GetAll();
        Task<APIResponse<ProjectResponse>> Edit(ProjectEditDto projectCreateDto, string id, IFormFile image);
        Task<APIResponse<string>> Delete(string projectId);
        Task<APIResponse<ProjectResponse>> GetById(string projectId);
    }
}