using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using ReelTalk.Controllers;
using ReelTalk.Data;
using ReelTalk.Models;

namespace ReelTalk.Tests.Controllers
{
    public class WatchlistsControllerTests
    {
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly ApplicationDbContext _context;
        private readonly WatchlistsController _controller;

        public WatchlistsControllerTests()
        {
            // Mock UserManager
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            );

            // Set up the in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase("TestDatabase")
                            .Options;

            _context = new ApplicationDbContext(options);
            _controller = new WatchlistsController(_context, _mockUserManager.Object);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenWatchlistIsNull()
        {
            // Act
            var result = await _controller.Details(999);  // ID doesn't exist

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithWatchlist()
        {
            // Arrange
            var user = new ApplicationUser { Id = "user1", UserName = "user1@test.com" };
            var watchlist = new Watchlist { Id = 1, UserId = "user1" };

            // Seed the in-memory database
            _context.Users.Add(user);
            _context.Watchlists.Add(watchlist);
            await _context.SaveChangesAsync();  // Ensure the data is saved

            // Act
            var result = await _controller.Details(1);  // Pass the correct ID

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Watchlist>(viewResult.Model);

            Assert.Equal(watchlist.Id, model.Id);  // Ensure the correct watchlist is returned
        }


        [Fact]
        public async Task Create_ReturnsRedirectToAction_WhenValidModelState()
        {
            // Arrange
            var watchlist = new Watchlist { UserId = "user1" };

            // Act
            var result = await _controller.Create(watchlist);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);  // Should redirect to Index
        }

        [Fact]
        public async Task Create_ReturnsView_WhenInvalidModelState()
        {
            // Arrange
            _controller.ModelState.AddModelError("UserId", "Required");
            var watchlist = new Watchlist();

            // Act
            var result = await _controller.Create(watchlist);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(watchlist, viewResult.Model);  // Ensure the model is returned with the View
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenWatchlistIsNull()
        {
            // Act
            var result = await _controller.Delete(999);  // ID doesn't exist

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesWatchlistAndRedirects()
        {
            // Arrange
            var watchlist = new Watchlist { UserId = "user1" };
            _context.Watchlists.Add(watchlist);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteConfirmed(watchlist.Id);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);  // Should redirect to Index

            var deletedWatchlist = await _context.Watchlists.FindAsync(watchlist.Id);
            Assert.Null(deletedWatchlist);  // Ensure the watchlist was removed
        }
    }
}
