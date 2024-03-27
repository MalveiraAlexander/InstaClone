using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Core.Repositories;
using InstaClone.Core.Services;
using System;

namespace InstaClone
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {

            #region Service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IReactionTypeService, ReactionTypeService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFollowerRepository, FollowerRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IReactionTypeRepository, ReactionTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostReactionRepository, PostReactionRepository>();
            #endregion

            return services;
        }
    }
}
