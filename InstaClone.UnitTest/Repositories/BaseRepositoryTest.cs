using AutoMapper;
using InstaClone.Commons.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Repositories
{
    public class BaseRepositoryTest
    {
        protected SystemContext InMemoryDbContext;
        protected IMapper IMapper;

        public BaseRepositoryTest()
        {
            BuildDb();
            IMapper = new Mapper(new MapperConfiguration(config => config.AddMaps(AppDomain.CurrentDomain.GetAssemblies())));
        }

        protected void BuildDb()
        {
            var options = new DbContextOptionsBuilder<SystemContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
                .ConfigureWarnings(p => p.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            InMemoryDbContext = new SystemContext(options);
            InMemoryDbContext.Database.EnsureDeleted();
            InMemoryDbContext.Database.EnsureCreated();
        }
    }
}
