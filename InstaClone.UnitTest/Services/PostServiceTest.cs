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
    public class PostServiceTest : BaseServiceTest
    {
        protected PostService ServiceUnderTest;
        protected Mock<IPostRepository> RepositoryMock;
        protected Mock<IPostReactionRepository> PostReactionRepositoryMock;

        public PostServiceTest() : base()
        {
            RepositoryMock = new Mock<IPostRepository>();
            PostReactionRepositoryMock = new Mock<IPostReactionRepository>();
            ServiceUnderTest = new PostService(RepositoryMock.Object, PostReactionRepositoryMock.Object, IMapper);
        }

        [Fact]
        public async Task CreateAsync_WhenPostRequestIsValid_ReturnsPostResponse()
        {
            // Arrange
            var postRequest = new PostRequest
            {
                Description = "Test Description",
                FileUrl = "Test Image",
                UserId = 1
            };

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Description = postRequest.Description,
                FileUrl = postRequest.FileUrl,
                UserId = postRequest.UserId
            };

            RepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>())).ReturnsAsync(post);

            // Act
            var result = await ServiceUnderTest.CreateAsync(postRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(post.Id, result.Id);
            Assert.Equal(post.Description, result.Description);
            Assert.Equal(post.FileUrl, result.FileUrl);
        }

        [Fact]
        public async Task UpdateAsync_WhenPostRequestIsValid_ReturnsPostResponse()
        {
            // Arrange
            var postRequest = new PostRequest
            {
                Description = "Test Description",
                FileUrl = "Test Image",
                UserId = 1
            };

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Description = postRequest.Description,
                FileUrl = postRequest.FileUrl,
                UserId = postRequest.UserId
            };

            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<PostRequest>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(post);

            // Act
            var result = await ServiceUnderTest.UpdateAsync(postRequest, post.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(post.Id, result.Id);
            Assert.Equal(post.Description, result.Description);
            Assert.Equal(post.FileUrl, result.FileUrl);
        }

        [Fact]
        public async Task DeleteAsync_WhenPostIdIsValid_ReturnsPostResponse()
        {
            // Arrange
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Description = "Test Description",
                FileUrl = "Test Image",
                UserId = 1
            };

            RepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(post);

            // Act
            var result = await ServiceUnderTest.DeleteAsync(post.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(post.Id, result.Id);
            Assert.Equal(post.Description, result.Description);
            Assert.Equal(post.FileUrl, result.FileUrl);
        }

        [Fact]
        public async Task GetByIdAsync_WhenPostIdIsValid_ReturnsPostResponse()
        {
            // Arrange
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Description = "Test Description",
                FileUrl = "Test Image",
                UserId = 1
            };

            RepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(post);

            // Act
            var result = await ServiceUnderTest.GetByIdAsync(post.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(post.Id, result.Id);
            Assert.Equal(post.Description, result.Description);
            Assert.Equal(post.FileUrl, result.FileUrl);
        }

        [Fact]
        public async Task GetAllAsync_WhenFilterIsValid_ReturnsResponse()
        {
            // Arrange
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Description = "Test Description",
                FileUrl = "Test Image",
                UserId = 1
            };

            var filter = new PostFilter
            {
                UserId = 1
            };

            var posts = new List<Post> { post };

            RepositoryMock.Setup(x => x.GetByFiltersAsync(It.IsAny<PostFilter>(), It.IsAny<CancellationToken>())).ReturnsAsync((posts, 1));

            // Act
            var result = await ServiceUnderTest.GetAllAsync(filter, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Data);
            Assert.Equal(posts.Count, result.Data.Count);
            Assert.Equal(posts.First().Id, result.Data.First().Id);
            Assert.Equal(posts.First().Description, result.Data.First().Description);
            Assert.Equal(posts.First().FileUrl, result.Data.First().FileUrl);
        }

        [Fact]
        public async Task ReactAsync_WhenCalled_ReturnsTrue()
        {
            // Arrange
            var postReaction = new PostReaction
            {
                Id = Guid.NewGuid(),
                PostId = Guid.NewGuid(),
                UserId = 1,
                ReactionTypeId = 1
            };


            PostReactionRepositoryMock.Setup(x => x.AddReaction(It.IsAny<PostReaction>(), It.IsAny<CancellationToken>())).ReturnsAsync(postReaction);

            // Act
            var postReactionRequest = new PostReactionRequest
            {
                PostId = postReaction.PostId,
                UserId = postReaction.UserId,
                ReactionTypeId = postReaction.ReactionTypeId
            };
            var result = await ServiceUnderTest.ReactAsync(postReactionRequest, CancellationToken.None);

            // Assert
            Assert.True(result);
        }
    }
}
