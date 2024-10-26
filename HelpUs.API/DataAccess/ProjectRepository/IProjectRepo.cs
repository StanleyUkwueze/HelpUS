using HelpUs.API.DataAccess.Repository;
using HelpUs.API.DataTransferObjects.Requests;
using HelpUs.API.Models;

namespace HelpUs.API.DataAccess.ProjectRepository
{
    public interface IProjectRepo: IGenericRepository<Project>
    {
        Task<Project> GetByNameAsync(string projectName);
    }
}