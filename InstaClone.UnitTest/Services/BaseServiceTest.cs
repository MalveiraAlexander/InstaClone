using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Services
{
    public class BaseServiceTest
    {
        protected IMapper IMapper;

        public BaseServiceTest()
        {
            IMapper = new Mapper(new MapperConfiguration(config => config.AddMaps(AppDomain.CurrentDomain.GetAssemblies())));
        }
    }
}
