using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Repositories
{
    public class FollowerRepositoryTest : BaseRepositoryTest
    {
        protected IFollowerRepository RepositoryUnderTest;
        public FollowerRepositoryTest()
        {
            RepositoryUnderTest = new Core.Repositories.FollowerRepository(InMemoryDbContext);
        }

        [Fact]
        public async Task AddFollowerAsync_ShouldReturnTrue()
        {
            // Act
            var result = await RepositoryUnderTest.AddFollower(1, 2, default);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveFollowerAsync_ShouldReturnTrue()
        {
            // Arrange
            await RepositoryUnderTest.AddFollower(1, 2, default);

            // Act
            var result = await RepositoryUnderTest.RemoveFollower(1, 2, default);

            // Assert
            Assert.True(result);
        }
    }
}
