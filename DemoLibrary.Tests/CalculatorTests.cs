using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//because we are using this library to get to Calculator
using DemoLibrary;
using Xunit;

namespace DemoLibrary.Tests
{
    public class CalculatorTests
    {
        [Theory] //allows us to pass in multiple datasets to test our method multiple times
        [InlineData(4,3,7)] //dataset one value1, value2, expected result
        [InlineData(21, 5.25, 26.25)]
        //test those wacky values
        [InlineData(double.MaxValue, 5, double.MaxValue)]
        public void Add_SimpleValuesShouldCalculate(double x, double y, double expected)
        {
            // Arrange -- this is the value I expect to be returned from the add method
            //double expected = 5;

            // Act -- this the action and value that we actually get back
            double actual = Calculator.Add(x, y);

            // Assert -- here is what we expect, here is what we got, are they equal?
            //Assert requires a using statement for xUnit
            Assert.Equal(expected, actual);
        }

        [Theory] //allows us to pass in multiple datasets to test our method multiple times
        [InlineData(8,4,2)]
        [InlineData(10.5, 2.1, 5)]
        public void Divide_SimpleValuesShouldCalculate(double x, double y, double expected)
        {
            // Arrange
            //using our expected from dataset above [InlineData(8,4,2)]

            // Act
            double actual = Calculator.Divide(x, y);

            // Assert -- here is what we expect, here is what we got, are they equal?
            Assert.Equal(expected, actual);
        }

        [Fact] //this is a test that is run in the Test Runner (use for one result only, not a dataset)
        public void Divide_DivideByZero()
        {
            // Arrange
            double expected = 0;

            // Act
            double actual = Calculator.Divide(15, 0);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
