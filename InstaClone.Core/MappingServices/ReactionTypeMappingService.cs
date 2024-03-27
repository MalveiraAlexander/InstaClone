using AutoMapper;
using InstaClone.Commons.Responses;
using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.MappingServices
{
    public class ReactionTypeMappingService : Profile
    {
        public ReactionTypeMappingService()
        {
            CreateMap<ReactionType, ReactionTypeResponse>();
        }
    }
}
