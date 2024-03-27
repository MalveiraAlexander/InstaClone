using InstaClone.Commons.Exceptions;
using InstaClone.Commons.Filters;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Commons.Models;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Responses;
using InstaClone.Core.Repositories;
using InstaClone.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Services
{
    public class CommentServiceTest : BaseServiceTest
    {
        protected CommentService ServiceUnderTest;
        protected Mock<ICommentRepository> RepositoryMock;

        public CommentServiceTest() : base()
        {
            RepositoryMock = new Mock<ICommentRepository>();

            ServiceUnderTest = new CommentService(RepositoryMock.Object, IMapper);
        }

        [Fact]
        public async Task CreateAsync_WhenCommentRequestIsValid_ReturnsCommentResponse()
        {
            // Arrange
            var commentRequest = new CommentRequest
            {
                Text = "Test Content",
                PostId = Guid.NewGuid(),
                UserId = 1
            };

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                Text = commentRequest.Text,
                PostId = commentRequest.PostId,
                UserId = commentRequest.UserId,
            };

            RepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Comment>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment)
                .Callback<Comment, CancellationToken>((c, ct) =>
                {
                    c.Id = comment.Id;
                    c.CreationDate = comment.CreationDate;
                });


            // Act
            var result = await ServiceUnderTest.CreateAsync(commentRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(comment.Text, result.Text);
            Assert.Equal(comment.Id, result.Id);
            Assert.NotNull(result);
            Assert.IsType<CommentResponse>(result); 
        }

        [Fact]
        public async Task UpdateAsync_WhenCommentRequestIsValid_ReturnsCommentResponse()
        {
            // Arrange
            var commentRequest = new CommentRequest
            {
                Text = "Test Content",
                PostId = Guid.NewGuid(),
                UserId = 1
            };

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                Text = commentRequest.Text,
                PostId = commentRequest.PostId,
                UserId = commentRequest.UserId,
            };

            RepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<CommentRequest>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment)
                .Callback<CommentRequest, Guid, CancellationToken>((c, id, ct) =>
                {
                    c.Text = comment.Text;
                });

            // Act
            var result = await ServiceUnderTest.UpdateAsync(commentRequest, comment.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(comment.Text, result.Text);
            Assert.Equal(comment.Id, result.Id);
            Assert.NotNull(result);
            Assert.IsType<CommentResponse>(result);
        }

        [Fact]
        public async Task DeleteAsync_WhenCommentIdIsValid_ReturnsCommentResponse()
        {
            // Arrange
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                Text = "Test Content",
                PostId = Guid.NewGuid(),
                UserId = 1,
            };

            RepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment);

            // Act
            var result = await ServiceUnderTest.DeleteAsync(comment.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(comment.Text, result.Text);
            Assert.Equal(comment.Id, result.Id);
            Assert.NotNull(result);
            Assert.IsType<CommentResponse>(result);
        }

        [Fact]
        public async Task GetByIdCommentAsync_WhenCommentIdIsValid_ReturnsCommentResponse()
        {
            // Arrange
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                Text = "Test Content",
                PostId = Guid.NewGuid(),
                UserId = 1,
            };

            RepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment);

            // Act
            var result = await ServiceUnderTest.GetByIdAsync(comment.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(comment.Text, result.Text);
            Assert.Equal(comment.Id, result.Id);
            Assert.NotNull(result);
            Assert.IsType<CommentResponse>(result);
        }

        [Fact]
        public async Task GetAllAsync_WhenFilterIsValid_ReturnsResponse()
        {
            // Arrange
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                Text = "Test Content",
                PostId = Guid.NewGuid(),
                UserId = 1,
            };
            long count = 1;

            RepositoryMock.Setup(repo => repo.GetByFiltersAsync(It.IsAny<CommentFilter>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((new List<Comment> { comment }, count));

            // Act
            var result = await ServiceUnderTest.GetAllAsync(new CommentFilter { UserId = 1 }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Data);
            Assert.Equal(comment.Text, result.Data.First().Text);
            Assert.Equal(comment.Id, result.Data.First().Id);
            Assert.NotNull(result);
            Assert.IsType<Response<CommentResponse>>(result);
        }
    }


}
