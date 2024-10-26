
using HelpUs.DataAccess.DataTransferObjects.Responses;
using HelpUs.Service.DataTransferObjects.Requests;
using HelpUs.Service.DataTransferObjects.Responses;

namespace HelpUs.Service.UserService
{
    public interface IUserService
    {
        Task<APIResponse<UserResponse>> GetUserByEmail(string email);
        Task<APIResponse<UserResponse>> Login(LoginRequest request);
        Task<APIResponse<CreatedUserResponse>> CreateUser(CreateUserRequest createUserRequest);
    }
}
