using InstaClone.Commons.Filters;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Models;
using InstaClone.Commons.Requests;
using InstaClone.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Repositories
{
    public class UserRepositoryTest : BaseRepositoryTest
    {
        protected IUserRepository RepositoryUnderTest;
        public UserRepositoryTest()
        {
            RepositoryUnderTest = new UserRepository(InMemoryDbContext, IMapper);
        }

        public List<User> GetDefaultUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    NickName = "Test",
                    Password = "Test",
                    CreationDate = new DateTime(2021, 1, 1)
                },
                new User
                {
                    Id = 2,
                    NickName = "Test 2",
                    Password = "Test 2",
                    CreationDate = new DateTime(2021, 1, 1)
                },
                new User
                {
                    Id = 3,
                    NickName = "Test 3",
                    Password = "Test 3",
                    CreationDate = new DateTime(2021, 1, 1)
                },
                new User
                {
                    Id = 4,
                    NickName = "Test 4",
                    Password = "Test 4",
                    CreationDate = new DateTime(2021, 1, 1)
                },
            };
        }

        [Fact]
        public async Task CreateAsync_ReturnsUserResponse()
        {
            // Arrange
            var user = new User
            {
                NickName = "Test",
                Password = "Test",
                CreationDate = DateTime.UtcNow
            };

            // Act
            var result = await RepositoryUnderTest.CreateAsync(user, default);

            var entity = InMemoryDbContext.Users.FirstOrDefault(x => x.Id == result.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsUserResponse()
        {
            // Arrange

            var request = new UserRequest
            {
                NickName = "Test Update",
                Password = "Test Update"
            };

            InMemoryDbContext.AddRange(GetDefaultUsers());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.UpdateAsync(request, 1, default);
            var entity = InMemoryDbContext.Users.FirstOrDefault(x => x.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, entity);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsUserResponse()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultUsers());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.DeleteAsync(1, default);
            var entity = InMemoryDbContext.Users.FirstOrDefault(x => x.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.Null(entity);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsUserResponse()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultUsers());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.GetByIdAsync(1, default);
            var entity = InMemoryDbContext.Users.FirstOrDefault(x => x.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, entity);
        }

        [Fact]
        public async Task GetByNickNameAsync_ReturnsUserResponse()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultUsers());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.GetByNickNameAsync("Test", default);
            var entity = InMemoryDbContext.Users.FirstOrDefault(x => x.NickName == "Test");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, entity);
        }

        [Fact]
        public async Task GetByNickNameAsync_ReturnsNull()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultUsers());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.GetByNickNameAsync("Test 5", default);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByFilters()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultUsers());
            InMemoryDbContext.SaveChanges();

            var filter = new UserFilter
            {
                NickName = "Test"
            };

            // Act
            var result = await RepositoryUnderTest.GetByFiltersAsync(filter, default);

            // Assert
            Assert.NotEmpty(result.data);
            Assert.Equal(4, result.data.Count);
        }



    }
}
