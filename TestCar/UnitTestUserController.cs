using Cars.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestCar
{
    public class UnitTestUserController
    {
        private readonly UserController _controller = new();

        [Theory]
        [InlineData(10, 2, 5)]
        [InlineData(20, 4, 5)]
        [InlineData(9, 3, 3)]
        [InlineData(0, 5, 0)]
        [InlineData(-10, 2, -5)]
        [InlineData(10, -2, -5)]
        [InlineData(-10, -2, 5)]
        public void Sub_ValidInputs_ReturnsExpectedResult(
            int a, int b, double expected)
        {
            // Act
            var result = _controller.Sub(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sub_DivideByZero()
        {
            // Arrange
            int a = 10, b = 0;

            // Act
            var res = _controller.Sub(a, b);

            // Assert
            var badRequest = Assert.IsType<BadRequestResult>(res.Result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void Sub_IntegerDivision_ReturnsTruncatedResult()
        {
            // Act
            var result = _controller.Sub(5, 2);
                
            // Assert
            Assert.Equal(2.0, result);
        }
    }
}