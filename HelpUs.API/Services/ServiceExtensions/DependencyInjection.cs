using HelpUs.API.Common;
using HelpUs.API.DataAccess.ProjectRepository;
using HelpUs.API.DataAccess.Repository;
using HelpUs.API.JWT;
using HelpUs.API.Service.ImageService;
using HelpUs.API.Service.UserService;
using HelpUs.API.Services.ProjectServices;
using HelpUs.API.Services.TransactionServices;
using HelpUs.DataAccess.UserRepository;
using HelpUs.Service.MailService;
using HelpUs.Service.UserService;
using HelpUs.Services.MailService;

namespace IfinionBackendAssessment.Service.ServiceExtensions
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IEMailService, EMailService>();
            Services.AddScoped<IJWTService, JWTService>();
            Services.AddHttpContextAccessor();
            Services.AddScoped<IProjectRepo, ProjectRepo>();
            Services.AddScoped<HelperMethods>();
            Services.AddScoped<IphotoService, PhotoService>();
            Services.AddScoped<IProjectService, ProjectService>();
            Services.AddScoped<ITransactionService, TransactionService>();
            Services.AddHttpClient();
            Services.AddMemoryCache();
        }
    }
}
