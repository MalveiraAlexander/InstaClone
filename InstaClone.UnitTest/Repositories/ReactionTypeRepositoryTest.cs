using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Models;
using InstaClone.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Repositories
{
    public class ReactionTypeRepositoryTest : BaseRepositoryTest
    {
        protected IReactionTypeRepository RepositoryUnderTest;
        public ReactionTypeRepositoryTest()
        {
            RepositoryUnderTest = new ReactionTypeRepository(InMemoryDbContext);
        }

        public List<ReactionType> GetDefaultReactionTypes()
        {
            return new List<ReactionType>
            {
                new ReactionType
                {
                    Id = 2,
                    Name = "Test"
                },
            };
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllReactionTypes()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultReactionTypes());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.GetAllAsync(default);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnReactionType()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultReactionTypes());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.GetByIdAsync(1, default);

            // Assert
            Assert.NotNull(result);
        }

    }
}
