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

    public class CommentRepositoryTest : BaseRepositoryTest
    {
        protected ICommentRepository RepositoryUnderTest;
        public CommentRepositoryTest()
        {
            RepositoryUnderTest = new CommentRepository(InMemoryDbContext, IMapper);
        }

        public List<Comment> GetDefaultComments()
        {
            return new List<Comment>
            {
                new Comment
                {
                    Id = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"),
                    Text = "Test",
                    CreationDate = new DateTime(15454548852),
                    UserId = 1,
                    User = new User
                    {
                        Id = 1,
                        NickName = "Test",
                        Password = "Test",
                        CreationDate = new DateTime(15454548852),
                    },
                    PostId = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3b3a")
                },
                new Comment
                {
                    Id = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba2"),
                    Text = "Test",
                    CreationDate = new DateTime(15454548852),
                    UserId = 2,
                    User = new User
                    {
                        Id = 2,
                        NickName = "Test",
                        Password = "Test",
                        CreationDate = new DateTime(15454548852),
                    },
                    PostId = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3b3b")
                },
                new Comment
                {
                    Id = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba3"),
                    Text = "Test",
                    CreationDate = new DateTime(15454548852),
                    UserId = 3,
                    User = new User
                    {
                        Id = 3,
                        NickName = "Test",
                        Password = "Test",
                        CreationDate = new DateTime(15454548852),
                    },
                    PostId = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3b3c")
                },
            };
        }



        [Fact]
        public void CreateAsyncTest()
        {
            // Arrange
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Text = "Test",
                CreationDate = DateTime.Now,
                UserId = 1,
                PostId = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3b3b")
            };

            // Act
            var result = RepositoryUnderTest.CreateAsync(comment, default);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void UpdateAsyncTest()
        {
            // Arrange
            var comment = new CommentRequest
            {
                Text = "Test Update",
                UserId = 1,
                PostId = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3b3b")
            };

            InMemoryDbContext.AddRange(GetDefaultComments());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.UpdateAsync(comment, new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"), default);

            var commentUpdated = InMemoryDbContext.Comments.FirstOrDefault(c => c.Id == new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"));

            // Assert
            Assert.Equal(result, commentUpdated);
        }

        [Fact]
        public async void DeleteAsyncTest()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultComments());
            InMemoryDbContext.SaveChanges();
            var commentToDeleted = InMemoryDbContext.Comments.FirstOrDefault(c => c.Id == new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"));

            // Act
            var result = await RepositoryUnderTest.DeleteAsync(new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"), default);

            commentToDeleted.User.Comments.Clear();
            // Assert
            Assert.Equal(result, commentToDeleted);
        }

        [Fact]
        public async void GetByIdAsyncTest()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultComments());
            InMemoryDbContext.SaveChanges();

            // Act
            var result = await RepositoryUnderTest.GetByIdAsync(new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3ba1"), default);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetByFiltersAsyncTest()
        {
            // Arrange
            InMemoryDbContext.AddRange(GetDefaultComments());
            InMemoryDbContext.SaveChanges();

            var filter = new CommentFilter
            {
                PostId = new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3b3c")
            };

            // Act
            var result = await RepositoryUnderTest.GetByFiltersAsync(filter, default);

            var commentFilters = InMemoryDbContext.Comments.Where(c => c.PostId == new Guid("f5b3b3b3-1b7b-4b3b-8b3b-3b3b3b3b3b3c"));
            // Assert
            Assert.Equal(result.data, commentFilters);
        }

    }


}
