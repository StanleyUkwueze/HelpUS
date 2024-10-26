using HelpUs.API.DataAccess;
using HelpUs.API.DataAccess.Repository;
using HelpUs.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpUs.DataAccess.UserRepository
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context): base(context) 
        {
          _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user!;
        }

        public async Task<User> GetUserById(int Id)
        {
            return  await _context.Users.FindAsync(Id);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            return user!;
        }
    }
}
