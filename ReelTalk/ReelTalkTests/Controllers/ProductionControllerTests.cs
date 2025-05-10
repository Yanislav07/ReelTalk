using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReelTalk.Controllers;
using ReelTalk.Data;
using ReelTalk.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class ProductionsControllerTests
{
    private ApplicationDbContext GetDbContextWithData()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .Options;

        var context = new ApplicationDbContext(options);

        var genre = new Genre { Id = 1, Name = "Action" };
        context.Genres.Add(genre);

        context.Productions.Add(new Production
        {
            Id = 1,
            Title = "Test Movie",
            ReleaseYear = 2022,
            Duration = 120,
            Description = "Test description",
            ImagePath = "image.jpg",
            Type = "Movie",
            GenreId = 1,
            Genre = genre
        });

        context.SaveChanges();
        return context;
    }


    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfProductions()
    {
        var context = GetDbContextWithData();
        var controller = new ProductionsController(context);

        var result = await controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Production>>(viewResult.Model);
        Assert.Single(model);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        var context = GetDbContextWithData();
        var controller = new ProductionsController(context);

        var result = await controller.Details(null);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithProductionAndComments()
    {
        var context = GetDbContextWithData();

        // Add comment
        context.Comments.Add(new Comment
        {
            Id = 1,
            Content = "Test Comment",
            ProductionId = 1,
            User = new ApplicationUser { Id = "user1", UserName = "testuser" },
            UserId = "user1"
        });

        context.SaveChanges();

        var controller = new ProductionsController(context);

        var result = await controller.Details(1);

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ProductionWithComments>(viewResult.Model);
        Assert.Equal(1, model.Production.Id);
        Assert.Single(model.Comments);
    }

    [Fact]
    public void Create_Get_ReturnsViewResult_WithGenreList()
    {
        var context = GetDbContextWithData();
        var controller = new ProductionsController(context);

        var result = controller.Create();

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.True(viewResult.ViewData.ContainsKey("GenreId"));
    }
}
