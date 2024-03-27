using InstaClone.Commons.Requests;
using InstaClone.Commons.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest login, CancellationToken cancellationToken);
    }
}
