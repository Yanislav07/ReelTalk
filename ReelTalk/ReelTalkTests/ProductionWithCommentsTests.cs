using System.Collections.Generic;
using ReelTalk.Data;
using ReelTalk.Models;


namespace ReelTalk.Tests.Models
{
    public class ProductionWithCommentsTests
    {
        [Fact]
        public void Constructor_SetsProductionAndComments()
        {
            // Arrange
            var production = new Production { Id = 1, Title = "Test Movie" };
            var comments = new List<Comment>
            {
                new Comment { Id = 1, Content = "Great!" },
                new Comment { Id = 2, Content = "Not bad." }
            };

            // Act
            var model = new ProductionWithComments(production, comments);

            // Assert
            Assert.Equal(production, model.Production);
            Assert.Equal(comments, model.Comments);
        }

        [Fact]
        public void CommentProperty_CanBeSetAndRetrieved()
        {
            // Arrange
            var comment = new Comment { Id = 99, Content = "Latest comment" };
            var model = new ProductionWithComments(new Production(), new List<Comment>());

            // Act
            model.Comment = comment;

            // Assert
            Assert.Equal(comment, model.Comment);
        }
    }
}
