using P06VehicleDealership.Shared.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06VehicleDealership.Shared.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(UserLoginDTO userLoginDto);

        Task<ServiceResponse<int>> Register(UserRegisterDTO userRegisterDTO);

        Task<ServiceResponse<bool>> ChangePassword(string newPassword);

        void SetAuthToken(string authToken);

    }
}
