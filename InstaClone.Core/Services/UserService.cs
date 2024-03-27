using AutoMapper;
using InstaClone.Commons.Filters;
using InstaClone.Commons.Helpers;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Responses;
using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaClone.Core.Repositories;
using InstaClone.Commons.Exceptions;

namespace InstaClone.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IFollowerRepository followerRepository;
        private readonly IGenericRepository<User> userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository repository, 
                           IFollowerRepository followerRepository, 
                           IGenericRepository<User> userRepository,
                           IMapper mapper)
        {
            this.repository = repository;
            this.followerRepository = followerRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserResponse> CreateAsync(UserRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            request.Password = CryptoHelper.HashMD5(request.Password);
            return mapper.Map<UserResponse>(await repository.CreateAsync(mapper.Map<User>(request), cancellationToken));
        }

        public async Task<UserResponse> UpdateAsync(UserRequest request, int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            request.Password = CryptoHelper.HashMD5(request.Password);
            return mapper.Map<UserResponse>(await repository.UpdateAsync(request, id, cancellationToken));
        }

        public async Task<UserResponse> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<UserResponse>(await repository.DeleteAsync(id, cancellationToken));
        }

        public async Task<UserResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await repository.GetByIdAsync(id, cancellationToken);
            var response = mapper.Map<UserResponse>(result);
            response.PostCount = result.Posts.Count;
            var followers = await followerRepository.GetFollowersAsync(id, cancellationToken);
            response.FollowerCount = followers is not null ? followers.Count : 0;
            response.Followers = mapper.Map<List<UserFollower>>(followers);
            return response;
        }

        public async Task<Response<UserResponse>> GetAllAsync(UserFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await repository.GetByFiltersAsync(filter, cancellationToken);
            return new Response<UserResponse> { Data = mapper.Map<List<UserResponse>>(result.data), Count = result.count };
        }

        public async Task<bool> AddFollow(int userId, int followerId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var followerUserExist = await userRepository.ExistByIdAsync(followerId);
            var userExist = await userRepository.ExistByIdAsync(userId);
            if (!followerUserExist)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "Follower user not found");
            }
            if (!userExist)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "User not found");
            }
            await followerRepository.AddFollower(userId, followerId, cancellationToken);
            return true;
        }

        public async Task<bool> RemoveFollow(int userId, int followerId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var followerUserExist = await userRepository.ExistByIdAsync(followerId);
            var userExist = await userRepository.ExistByIdAsync(userId);
            if (!followerUserExist)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "Follower user not found");
            }
            if (!userExist)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "User not found");
            }
            await followerRepository.RemoveFollower(userId, followerId, cancellationToken);
            return true;
        }
    }
}
