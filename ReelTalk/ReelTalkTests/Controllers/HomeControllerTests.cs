using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReelTalk.Controllers;
using ReelTalk.Data;
using ReelTalk.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class HomeControllerTests
{
    private ApplicationDbContext GetContextWithProductions()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "HomeTestDb_" + System.Guid.NewGuid())
            .Options;

        var context = new ApplicationDbContext(options);

        context.Productions.AddRange(
            new Production { Id = 1, Title = "Movie A", Description = "Desc A", ImagePath = "/a.jpg", Type = "Movie" },
            new Production { Id = 2, Title = "Movie B", Description = "Desc B", ImagePath = "/b.jpg", Type = "Series" }
        );
        context.SaveChanges();
        return context;
    }

    private HomeController GetController(ApplicationDbContext context)
    {
        var logger = new LoggerFactory().CreateLogger<HomeController>();
        return new HomeController(logger, context);
    }

    [Fact]
    public void Index_ReturnsViewWithProductions()
    {
        var context = GetContextWithProductions();
        var controller = GetController(context);

        var result = controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Production>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public void Privacy_ReturnsView()
    {
        var context = GetContextWithProductions();
        var controller = GetController(context);

        var result = controller.Privacy();

        Assert.IsType<ViewResult>(result);
    }

    
}
