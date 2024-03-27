using InstaClone.Commons.Filters;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Models;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Responses;
using InstaClone.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.UnitTest.Repositories
{
    public class PostRepositoryTest : BaseRepositoryTest
    {
        protected IPostRepository RepositoryUnderTest;
        public PostRepositoryTest()
        {
            RepositoryUnderTest = new PostRepository(InMemoryDbContext, IMapper);
        }

        public List<Post> GetDefaultPosts()
        {
            return new List<Post>
            {
                new Post
                {
                    Id = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"),
                    Description = "Test",
                    FileUrl = "Test",
                    CreationDate = new DateTime(15454548852),
                    UserId = 1,
                    User = new User
                    {
                        Id = 1,
                        NickName = "Test",
                        Password = "Test",
                        CreationDate = new DateTime(15454548852),
                    },
                },
                new Post
                {
                    Id = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba2"),
                    Description = "Test",
                    FileUrl = "Test",
                    CreationDate = new DateTime(15454548852),
                    UserId = 2,
                    User = new User
                    {
                        Id = 2,
                        NickName = "Test",
                        Password = "Test",
                        CreationDate = new DateTime(15454548852),
                    },
                },
                new Post
                {
                    Id = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba3"),
                    Description = "Test",
                    FileUrl = "Test",
                    CreationDate = new DateTime(15454548852),
                    UserId = 3,
                    User = new User
                    {
                        Id = 3,
                        NickName = "Test",
                        Password = "Test",
                        CreationDate = new DateTime(15454548852),
                    },
                },
            };
        }

        [Fact]
        public async Task CreatePostAsync_ShouldReturnPostResponse()
        {
            // Arrange
            var post = new Post
            {
                Description = "Test Description",
                FileUrl = "Test Image",
                UserId = 1,
                CreationDate = new DateTime(2021, 1, 1)
            };

            // Act
            var result = await RepositoryUnderTest.CreateAsync(post, default);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Post>(result);
        }

        [Fact]
        public async Task UpdatePostAsync_ShouldReturnPostResponse()
        {
            // Arrange
            var postRequest = new PostRequest
            {
                Description = "Updated Description",
                FileUrl = "Updated Image",
                UserId = 1
            };

            InMemoryDbContext.AddRange(GetDefaultPosts());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.UpdateAsync(postRequest, new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"), default);

            var updatedEntity = InMemoryDbContext.Posts.FirstOrDefault(p => p.Id == new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"));

            // Assert
            Assert.Equal(result, updatedEntity);
        }

        [Fact]
        public async Task DeletePostAsync_ShouldReturnPostResponse()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultPosts());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.DeleteAsync(new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"), default);

            var deletedEntity = InMemoryDbContext.Posts.FirstOrDefault(p => p.Id == new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"));

            // Assert
            Assert.Null(deletedEntity);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPostResponse()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultPosts());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.GetByIdAsync(new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"), default);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Post>(result);
        }

        [Fact]
        public async Task GetByFiltersAsync_ShouldReturnPostResponse()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultPosts());
            InMemoryDbContext.SaveChanges();

            var filter = new PostFilter
            {
                UserId = 1
            };

            // Act
            var result = await RepositoryUnderTest.GetByFiltersAsync(filter, default);

            var entities = InMemoryDbContext.Posts.Where(p => p.UserId == 1).ToList();

            // Assert
            Assert.Equal(result.data, entities);
        }
    }
}
