using ReelTalk.Models;


namespace ReelTalkTests

{
    public class UserWithRoleTests
    {
        [Fact]
        public void Can_Create_UserWithRole()
        {
            var user = new UserWithRole("1", "a@b.com", "Admin");
            Assert.Equal("Admin", user.CurrentRole); // Use Assert.Equal instead of Assert.AreEqual
        }

        
    }
}
