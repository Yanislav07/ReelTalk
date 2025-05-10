using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReelTalk.Controllers;
using ReelTalk.Data;
using ReelTalk.Models;
using System.Threading.Tasks;
using System.Collections.Generic;



public class CommentsControllerTests
{
    private ApplicationDbContext GetDbContextWithData()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // ensure isolated db per test
            .Options;

        var context = new ApplicationDbContext(options);

        // Seed a production
        var production = new Production
        {
            Id = 1,
            Title = "Test Movie",
            Description = "Test description",
            ImagePath = "/images/test.jpg",
            Type = "Movie"
        };

        // Seed a comment linked to the production
        context.Comments.Add(new Comment
        {
            Id = 1,
            Content = "Test comment",
            ProductionId = 1,
            Production = production,
            UserId = "user1"
        });

        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        // Arrange
        var context = GetDbContextWithData();
        var controller = new CommentsController(context);

        // Act
        var result = await controller.Details(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenCommentNotFound()
    {
        // Arrange
        var context = GetDbContextWithData();
        var controller = new CommentsController(context);

        // Act
        var result = await controller.Details(99); // ID that doesn't exist

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithComment()
    {
        // Arrange
        var context = GetDbContextWithData();
        var controller = new CommentsController(context);

        // Act
        var result = await controller.Details(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Comment>(viewResult.ViewData.Model);
        Assert.Equal(1, model.Id);
    }
}
