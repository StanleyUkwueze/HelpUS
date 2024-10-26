using HelpUs.API.DataAccess.Repository;
using HelpUs.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpUs.API.DataAccess.ProjectRepository
{
    public class ProjectRepo: GenericRepository<Project>, IProjectRepo
    {
        private readonly AppDbContext _context;
        public ProjectRepo(AppDbContext context): base(context) 
        {
            _context = context;
        }

        public async Task<Project> GetByNameAsync(string projectName)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Name == projectName);
        }
    }
}
