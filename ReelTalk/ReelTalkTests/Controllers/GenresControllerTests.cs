using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReelTalk.Controllers;
using ReelTalk.Data;
using ReelTalk.Models;
using System.Threading.Tasks;
using System.Linq;

public class GenresControllerTests
{
    private ApplicationDbContext GetContextWithData()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "GenresTestDb_" + System.Guid.NewGuid())
            .Options;

        var context = new ApplicationDbContext(options);
        context.Genres.Add(new Genre { Id = 1, Name = "Comedy" });
        context.Genres.Add(new Genre { Id = 2, Name = "Action" });
        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task Index_ReturnsViewWithGenres()
    {
        var context = GetContextWithData();
        var controller = new GenresController(context);

        var result = await controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Genre>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        var context = GetContextWithData();
        var controller = new GenresController(context);

        var result = await controller.Details(null);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenGenreNotFound()
    {
        var context = GetContextWithData();
        var controller = new GenresController(context);

        var result = await controller.Details(99);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsViewWithGenre_WhenIdIsValid()
    {
        var context = GetContextWithData();
        var controller = new GenresController(context);

        var result = await controller.Details(1);

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Genre>(viewResult.Model);
        Assert.Equal("Comedy", model.Name);
    }

    [Fact]
    public async Task Create_Post_ValidGenre_RedirectsToIndex()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("CreateTestDb_" + System.Guid.NewGuid())
            .Options;
        var context = new ApplicationDbContext(options);
        var controller = new GenresController(context);
        var newGenre = new Genre { Id = 3, Name = "Horror" };

        var result = await controller.Create(newGenre);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        Assert.Equal(1, context.Genres.Count(g => g.Name == "Horror"));
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_WhenIdMismatch()
    {
        var context = GetContextWithData();
        var controller = new GenresController(context);
        var genre = new Genre { Id = 2, Name = "Sci-Fi" };

        var result = await controller.Edit(1, genre);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteConfirmed_RemovesGenre()
    {
        var context = GetContextWithData();
        var controller = new GenresController(context);

        var result = await controller.DeleteConfirmed(1);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        Assert.False(context.Genres.Any(g => g.Id == 1));
    }
}
