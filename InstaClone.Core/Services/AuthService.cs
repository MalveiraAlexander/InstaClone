using AutoMapper;
using InstaClone.Commons.Exceptions;
using InstaClone.Commons.Helpers;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthService(IUserRepository repository, IMapper mapper, IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest login, CancellationToken cancellationToken)
        {
            var user = await repository.GetByNickNameAsync(login.Nickname, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException();
            }
            string passwordHash = CryptoHelper.HashMD5(login.Password);
            if (user.Password != passwordHash)
            {
                throw new UnauthorizedException();
            }

            return GenerateTokenHelper.GenerateToken(user, configuration, mapper);
        }
    }
}
