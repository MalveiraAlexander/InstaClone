using InstaClone.Commons.Exceptions;
using InstaClone.Commons.Helpers;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Models;
using InstaClone.Commons.Requests;
using InstaClone.Core.Services;
using Microsoft.EntityFrameworkCore.InMemory.Internal;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Services
{
    public class AuthServiceTest : BaseServiceTest
    {

        protected AuthService ServiceUnderTest;
        protected Mock<IUserRepository> RepositoryMock;
        protected IConfiguration Configuration;

        

        public AuthServiceTest() : base()
        {
            RepositoryMock = new Mock<IUserRepository>();
            var inMemorySettings = new Dictionary<string, string> {
                {"JWT:Expiration", "5"},
                {"JWT:Audience", "instaClone"},
                {"JWT:Issuer", "instaClone"},
            };
            Configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(inMemorySettings)
                                .Build();

            ServiceUnderTest = new AuthService(RepositoryMock.Object, IMapper, Configuration);
        }

        [Fact]
        public async Task LoginAsync_WhenUserNotFound_ThrowsNotFoundException()
        {
            // Arrange
            RepositoryMock.Setup(x => x.GetByNickNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((User)null);

            // Act
            Func<Task> act = async () => await ServiceUnderTest.LoginAsync(new LoginRequest(), new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task LoginAsync_WhenPasswordIsIncorrect_ThrowsUnauthorizedException()
        {
            // Arrange

            var user = new User
            {
                Id = 1,
                NickName = "Test",
                Password = "Test"
            };
            RepositoryMock.Setup(x => x.GetByNickNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            

            // Act
            Func<Task> act = async () => await ServiceUnderTest.LoginAsync(new LoginRequest { Nickname = "Test", Password = "test" }, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<UnauthorizedException>(act);
        }

        [Fact]
        public async Task LoginAsync_WhenPasswordIsCorrect_ReturnsToken()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                NickName = "Test",
                Password = CryptoHelper.HashMD5("Test")
            };
            RepositoryMock.Setup(x => x.GetByNickNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

            // Act
            var result = await ServiceUnderTest.LoginAsync(new LoginRequest { Nickname = "Test", Password = "Test" }, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
        }
    }
}
