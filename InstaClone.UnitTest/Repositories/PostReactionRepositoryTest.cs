using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Repositories
{
    public class PostReactionRepositoryTest : BaseRepositoryTest
    {
        protected IPostReactionRepository RepositoryUnderTest;
        public PostReactionRepositoryTest()
        {
            RepositoryUnderTest = new Core.Repositories.PostReactionRepository(InMemoryDbContext);
        }

        [Fact]
        public async Task AddPostReactionAsync_ShouldAddPostReaction()
        {
            // Arrange
            var postReaction = new PostReaction
            {
                PostId = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"),
                UserId = 1,
                ReactionTypeId = 1
            };

            // Act
            await RepositoryUnderTest.AddReaction(postReaction, default);

            // Assert
            var postReactionFromDb = InMemoryDbContext.PostReactions.FirstOrDefault();
            Assert.NotNull(postReactionFromDb);
        }

        [Fact]
        public async Task DeletePostReactionAsync_ShouldDeletePostReaction()
        {
            // Arrange
            var postReaction = new PostReaction
            {
                PostId = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"),
                UserId = 1,
                ReactionTypeId = 1
            };
            InMemoryDbContext.PostReactions.Add(postReaction);
            await InMemoryDbContext.SaveChangesAsync();

            // Act
            await RepositoryUnderTest.DeleteReaction(1, default);

            // Assert
            var postReactionFromDb = InMemoryDbContext.PostReactions.FirstOrDefault();
            Assert.Null(postReactionFromDb);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPostReaction()
        {
            // Arrange
            var postReaction = new PostReaction
            {
                PostId = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"),
                UserId = 1,
                ReactionTypeId = 1
            };
            InMemoryDbContext.PostReactions.Add(postReaction);
            await InMemoryDbContext.SaveChangesAsync();

            // Act
            var result = await RepositoryUnderTest.GetByIdAsync(1, default);

            // Assert
            Assert.NotNull(result);
        }
    }
}
