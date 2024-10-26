using HelpUs.API.DataAccess.Repository;
using HelpUs.API.Models;

namespace HelpUs.DataAccess.UserRepository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUserName(string userName);
        Task<User> GetUserById(int Id);
    }
}
