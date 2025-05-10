using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReelTalk.Controllers;
using ReelTalk.Models;
using Xunit;

public class AdministrationControllerTests
{
    private Mock<UserManager<IdentityUser>> GetMockUserManager()
    {
        var store = new Mock<IUserStore<IdentityUser>>();
        return new Mock<UserManager<IdentityUser>>(
            store.Object, null, null, null, null, null, null, null, null
        );
    }

    private Mock<RoleManager<IdentityRole>> GetMockRoleManager()
    {
        var store = new Mock<IRoleStore<IdentityRole>>();
        return new Mock<RoleManager<IdentityRole>>(
            store.Object, null, null, null, null
        );
    }

   

    [Fact]
    public async Task AlterRole_ChangesUserRoleAndRedirects()
    {
        // Arrange
        var mockUserManager = GetMockUserManager();
        var mockRoleManager = GetMockRoleManager();

        var fakeUser = new IdentityUser { Id = "1", Email = "test@example.com" };

        mockUserManager.Setup(m => m.FindByIdAsync("1"))
                       .ReturnsAsync(fakeUser);

        mockUserManager.Setup(m => m.GetRolesAsync(fakeUser))
                       .ReturnsAsync(new List<string> { "User" });

        mockUserManager.Setup(m => m.RemoveFromRoleAsync(fakeUser, "User"))
                       .ReturnsAsync(IdentityResult.Success);

        mockUserManager.Setup(m => m.AddToRoleAsync(fakeUser, "Admin"))
                       .ReturnsAsync(IdentityResult.Success);

        var controller = new AdministrationController(mockUserManager.Object, mockRoleManager.Object);

        // Act
        var result = await controller.AlterRole("1", "Admin");

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }
}
