using System;
using Xunit;

namespace XunitTestDemo.Tests
{
    public class Calculator_Test
    {
        [Fact]
        public void Add_Test()
        {
            // Arrange
            var sut = new Calculator(); // sut - System Under Test

            // Act
            var result = sut.Add(2, 3);

            // Assert
            Assert.Equal(expected: 5, actual: result);
        }
    }
}
