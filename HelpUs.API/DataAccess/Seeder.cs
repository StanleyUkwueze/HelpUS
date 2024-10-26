using HelpUs.API.Models;

namespace HelpUs.API.DataAccess
{
    public static class Seeder
    {
        public async static Task SeedDatabase(AppDbContext context)
        {
            var users = new List<User>();
            var password = "P@ssW0rd";
            var role = "Donor";

            if (!context.Users.Any())
            {
                users = new List<User> {
                    new User
                    {
                        UserName = "Admin",
                        Email = "admin@gmail.com",
                        PaswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                        Role = "Admin",
                    },
                   new User
                   {
                    UserName = "Peter Ayo",
                    Email = "peterayo@gmail.com",
                    PaswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    Role = role,
                   },
                    new User
                   {
                    UserName = "Faith Riwan",
                    Email = "Riwan@gmail.com",
                    PaswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    Role = role,
                   },
                     new User
                   {
                    UserName = "Emeka Garba",
                    Email = "Emeka@gmail.com",
                    PaswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    Role = role,
                   }
               }; 
                
             context.Users.AddRange(users);
            await context.SaveChangesAsync();
            }
        }
    }
}
