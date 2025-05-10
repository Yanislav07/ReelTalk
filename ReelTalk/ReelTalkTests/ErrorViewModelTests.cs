using ReelTalk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReelTalkTests
{
    
    public class ErrorViewModelTests
    {
        [Fact]
        public void ShowRequestId_ReturnsTrue_WhenRequestIdIsNotNullOrEmpty()
        {
            // Arrange
            var model = new ErrorViewModel { RequestId = "abc123" };

            // Act & Assert
            Assert.True(model.ShowRequestId);
        }

        [Fact]
        public void ShowRequestId_ReturnsFalse_WhenRequestIdIsNull()
        {
            // Arrange
            var model = new ErrorViewModel { RequestId = null };

            // Act & Assert
            Assert.False(model.ShowRequestId);
        }

        [Fact]
        public void ShowRequestId_ReturnsFalse_WhenRequestIdIsEmpty()
        {
            // Arrange
            var model = new ErrorViewModel { RequestId = "" };

            // Act & Assert
            Assert.False(model.ShowRequestId);
        }
    }
}
