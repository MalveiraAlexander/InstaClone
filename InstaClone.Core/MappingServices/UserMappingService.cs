using AutoMapper;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Responses;
using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.MappingServices
{
    public class UserMappingService : Profile
    {
        public UserMappingService()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>().ForMember(dest => dest.Followers, orig => orig.Ignore());
            CreateMap<User, UserFollower>();
        }
    }
}
