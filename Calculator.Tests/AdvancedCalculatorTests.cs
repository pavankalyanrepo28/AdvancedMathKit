using System;
using System.Linq;
using Xunit;

namespace Calculator.Tests
{
    public class AdvancedCalculatorTests
    {
        private readonly AdvancedCalculator _calculator;

        public AdvancedCalculatorTests()
        {
            _calculator = new AdvancedCalculator();
        }

        [Fact]
        public void Mean_WithValidNumbers_ReturnsCorrectMean()
        {
            // Arrange
            double[] numbers = { 1, 2, 3, 4, 5 };

            // Act
            double result = _calculator.Mean(numbers);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void StandardDeviation_WithValidNumbers_ReturnsCorrectValue()
        {
            // Arrange
            double[] numbers = { 2, 4, 4, 4, 5, 5, 7, 9 };

            // Act
            double result = _calculator.StandardDeviation(numbers);

            // Assert
            Assert.Equal(2.0, result, 2); // Compare with 2 decimal precision
        }

        [Fact]
        public void Median_WithOddCount_ReturnsMiddleValue()
        {
            // Arrange
            double[] numbers = { 1, 3, 2, 5, 4 };

            // Act
            double result = _calculator.Median(numbers);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Median_WithEvenCount_ReturnsAverageOfMiddleValues()
        {
            // Arrange
            double[] numbers = { 1, 2, 3, 4 };

            // Act
            double result = _calculator.Median(numbers);

            // Assert
            Assert.Equal(2.5, result);
        }

        [Fact]
        public void ComplexMultiply_ReturnsCorrectResult()
        {
            // Arrange
            var a = (real: 3.0, imaginary: 2.0);
            var b = (real: 1.0, imaginary: 4.0);

            // Act
            var result = _calculator.ComplexMultiply(a, b);

            // Assert
            Assert.Equal(-5.0, result.real);
            Assert.Equal(14.0, result.imaginary);
        }

        [Fact]
        public void Factorial_WithValidInput_ReturnsCorrectResult()
        {
            // Act
            double result = _calculator.Factorial(5);

            // Assert
            Assert.Equal(120, result);
        }

        [Fact]
        public void Factorial_WithZero_ReturnsOne()
        {
            // Act
            double result = _calculator.Factorial(0);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Factorial_WithNegativeNumber_ThrowsArgumentException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() => _calculator.Factorial(-1));
        }

        [Fact]
        public void Combination_WithValidInput_ReturnsCorrectResult()
        {
            // Act
            double result = _calculator.Combination(5, 2);

            // Assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void MatrixMultiply_WithValidMatrices_ReturnsCorrectResult()
        {
            // Arrange
            var matrixA = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            var matrixB = new Matrix(new double[,] { { 5, 6 }, { 7, 8 } });

            // Act
            var result = _calculator.MatrixMultiply(matrixA, matrixB);

            // Assert
            Assert.Equal(19, result[0, 0]);
            Assert.Equal(22, result[0, 1]);
            Assert.Equal(43, result[1, 0]);
            Assert.Equal(50, result[1, 1]);
        }

        [Fact]
        public void MatrixInverse_WithInvertibleMatrix_ReturnsCorrectResult()
        {
            // Arrange
            var matrix = new Matrix(new double[,] { { 4, 7 }, { 2, 6 } });

            // Act
            var inverse = _calculator.MatrixInverse(matrix);

            // Assert
            Assert.Equal(0.6, inverse[0, 0], 5);
            Assert.Equal(-0.7, inverse[0, 1], 5);
            Assert.Equal(-0.2, inverse[1, 0], 5);
            Assert.Equal(0.4, inverse[1, 1], 5);
        }

        [Fact]
        public void NewtonRaphson_FindsSquareRoot()
        {
            // Arrange
            double number = 16;
            Func<double, double> f = x => x * x - number;
            Func<double, double> fPrime = x => 2 * x;

            // Act
            double result = _calculator.NewtonRaphson(f, fPrime, 1.0);

            // Assert
            Assert.Equal(4.0, result, 6);
        }

        [Fact]
        public void DotProduct_WithValidVectors_ReturnsCorrectResult()
        {
            // Arrange
            double[] v1 = { 1, 2, 3 };
            double[] v2 = { 4, 5, 6 };

            // Act
            double result = _calculator.DotProduct(v1, v2);

            // Assert
            Assert.Equal(32, result);
        }

        [Fact]
        public void CrossProduct_WithValidVectors_ReturnsCorrectResult()
        {
            // Arrange
            double[] v1 = { 1, 2, 3 };
            double[] v2 = { 4, 5, 6 };

            // Act
            double[] result = _calculator.CrossProduct(v1, v2);

            // Assert
            Assert.Equal(-3, result[0]);
            Assert.Equal(6, result[1]);
            Assert.Equal(-3, result[2]);
        }

        [Theory]
        [InlineData(new double[] { })]
        public void Mean_WithEmptySequence_ThrowsArgumentException(double[] numbers)
        {
            Assert.Throws<ArgumentException>(() => _calculator.Mean(numbers));
        }

        [Theory]
        [InlineData(new double[] { 1, 2 }, new double[] { 1, 2, 3 })]
        public void DotProduct_WithDifferentLengths_ThrowsArgumentException(double[] v1, double[] v2)
        {
            Assert.Throws<ArgumentException>(() => _calculator.DotProduct(v1, v2));
        }

        [Theory]
        [InlineData(new double[] { 1, 2 }, new double[] { 1, 2 })]
        public void CrossProduct_WithNon3DVectors_ThrowsArgumentException(double[] v1, double[] v2)
        {
            Assert.Throws<ArgumentException>(() => _calculator.CrossProduct(v1, v2));
        }
    }
} 