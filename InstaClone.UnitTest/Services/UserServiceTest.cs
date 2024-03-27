using InstaClone.Commons.Filters;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Models;
using InstaClone.Commons.Requests;
using InstaClone.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Services
{
    public class UserServiceTest : BaseServiceTest
    {
        protected UserService ServiceUnderTest;
        protected Mock<IUserRepository> RepositoryMock;
        protected Mock<IFollowerRepository> FollowerRepositoryMock;
        protected Mock<IGenericRepository<User>> GenericRepositoryMock;

        public UserServiceTest() : base()
        {
            RepositoryMock = new Mock<IUserRepository>();
            FollowerRepositoryMock = new Mock<IFollowerRepository>();
            GenericRepositoryMock = new Mock<IGenericRepository<User>>();

            ServiceUnderTest = new UserService(RepositoryMock.Object, FollowerRepositoryMock.Object, GenericRepositoryMock.Object, IMapper);
        }

        [Fact]
        public async Task CreateAsync_WhenUserRequestIsValid_ShouldReturnUserResponse()
        {
            // Arrange
            var userRequest = new UserRequest
            {
                NickName = "Test",
                Password = "Test"
            };

            var user = new User
            {
                Id = 1,
                NickName = userRequest.NickName,
                Password = userRequest.Password
            };

            RepositoryMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await ServiceUnderTest.CreateAsync(userRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.NickName, result.NickName);
            Assert.Equal(user.Password, result.Password);
        }

        [Fact]
        public async Task UpdateAsync_WhenUserRequestIsValid_ShouldReturnUserResponse()
        {
            // Arrange
            var userRequest = new UserRequest
            {
                NickName = "Test",
                Password = "Test"
            };

            var user = new User
            {
                Id = 1,
                NickName = userRequest.NickName,
                Password = userRequest.Password
            };

            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<UserRequest>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await ServiceUnderTest.UpdateAsync(userRequest, 1, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.NickName, result.NickName);
            Assert.Equal(user.Password, result.Password);
        }

        [Fact]
        public async Task DeleteAsync_WhenUserIdIsValid_ShouldReturnUserResponse()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                NickName = "Test",
                Password = "Test"
            };

            RepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await ServiceUnderTest.DeleteAsync(1, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.NickName, result.NickName);
            Assert.Equal(user.Password, result.Password);
        }

        [Fact]
        public async Task GetByIdAsync_WhenUserIdIsValid_ShouldReturnUserResponse()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                NickName = "Test",
                Password = "Test"
            };

            RepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await ServiceUnderTest.GetByIdAsync(1, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.NickName, result.NickName);
            Assert.Equal(user.Password, result.Password);
        }

        [Fact]
        public async Task GetAllAsync_WhenFilterIsValid_ShouldReturnResponse()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                NickName = "Test",
                Password = "Test"
            };

            var userFilter = new UserFilter
            {
                NickName = "Test"
            };

            RepositoryMock.Setup(x => x.GetByFiltersAsync(It.IsAny<UserFilter>(), It.IsAny<CancellationToken>())).ReturnsAsync((new List<User> { user }, 1));

            // Act
            var result = await ServiceUnderTest.GetAllAsync(userFilter, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Data);
            Assert.Equal(1, result.Count);
            Assert.Equal(user.Id, result.Data.First().Id);
            Assert.Equal(user.NickName, result.Data.First().NickName);
            Assert.Equal(user.Password, result.Data.First().Password);
        }

        [Fact]
        public async Task AddFollow_WhenRequestIsValid_ShouldReturnTrue()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                NickName = "Test",
                Password = "Test"
            };

            bool success = true;

            RepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            FollowerRepositoryMock.Setup(x => x.AddFollower(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(success);

            // Act
            var result = await ServiceUnderTest.AddFollow(1, 2, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveFollow_WhenRequestIsValid_ShouldReturnTrue()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                NickName = "Test",
                Password = "Test"
            };

            bool success = true;

            RepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            FollowerRepositoryMock.Setup(x => x.RemoveFollower(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(success);

            // Act
            var result = await ServiceUnderTest.RemoveFollow(1, 2, CancellationToken.None);

            // Assert
            Assert.True(result);
        }
    }
}
