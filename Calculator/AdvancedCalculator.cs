using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class Matrix
    {
        private double[,] data;
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Matrix(double[,] data)
        {
            this.data = data;
            Rows = data.GetLength(0);
            Columns = data.GetLength(1);
        }

        public double this[int i, int j]
        {
            get => data[i, j];
            set => data[i, j] = value;
        }
    }

    public class AdvancedCalculator
    {
        private const double EPSILON = 1e-10;

        // Statistical Functions
        public double Mean(IEnumerable<double> numbers)
        {
            if (!numbers.Any()) throw new ArgumentException("Sequence contains no elements");
            return numbers.Average();
        }

        public double StandardDeviation(IEnumerable<double> numbers)
        {
            if (!numbers.Any()) throw new ArgumentException("Sequence contains no elements");
            double mean = Mean(numbers);
            double sum = numbers.Sum(x => Math.Pow(x - mean, 2));
            return Math.Sqrt(sum / (numbers.Count() - 1));
        }

        public double Median(IEnumerable<double> numbers)
        {
            if (!numbers.Any()) throw new ArgumentException("Sequence contains no elements");
            var sorted = numbers.OrderBy(x => x).ToList();
            int n = sorted.Count;
            if (n % 2 == 0)
                return (sorted[n / 2 - 1] + sorted[n / 2]) / 2;
            return sorted[n / 2];
        }

        // Complex Number Operations
        public (double real, double imaginary) ComplexMultiply(
            (double real, double imaginary) a,
            (double real, double imaginary) b)
        {
            return (
                a.real * b.real - a.imaginary * b.imaginary,
                a.real * b.imaginary + a.imaginary * b.real
            );
        }

        // Advanced Mathematical Functions
        public double Factorial(int n)
        {
            if (n < 0) throw new ArgumentException("Factorial is not defined for negative numbers");
            if (n > 20) throw new ArgumentException("Number too large for factorial calculation");
            if (n == 0) return 1;
            double result = 1;
            for (int i = 1; i <= n; i++)
                result *= i;
            return result;
        }

        public double Combination(int n, int r)
        {
            if (n < r) throw new ArgumentException("n must be greater than or equal to r");
            return Factorial(n) / (Factorial(r) * Factorial(n - r));
        }

        // Matrix Operations
        public Matrix MatrixMultiply(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows)
                throw new ArgumentException("Matrix dimensions do not match for multiplication");

            double[,] result = new double[a.Rows, b.Columns];
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < b.Columns; j++)
                    for (int k = 0; k < a.Columns; k++)
                        result[i, j] += a[i, k] * b[k, j];

            return new Matrix(result);
        }

        public Matrix MatrixInverse(Matrix matrix)
        {
            if (matrix.Rows != matrix.Columns)
                throw new ArgumentException("Matrix must be square for inverse calculation");

            int n = matrix.Rows;
            double[,] augmented = new double[n, 2 * n];
            
            // Create augmented matrix [A|I]
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmented[i, j] = matrix[i, j];
                    augmented[i, j + n] = (i == j) ? 1 : 0;
                }
            }

            // Gaussian elimination
            for (int i = 0; i < n; i++)
            {
                double pivot = augmented[i, i];
                if (Math.Abs(pivot) < EPSILON)
                    throw new ArgumentException("Matrix is not invertible");

                for (int j = 0; j < 2 * n; j++)
                    augmented[i, j] /= pivot;

                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        double factor = augmented[k, i];
                        for (int j = 0; j < 2 * n; j++)
                            augmented[k, j] -= factor * augmented[i, j];
                    }
                }
            }

            // Extract inverse matrix
            double[,] inverse = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    inverse[i, j] = augmented[i, j + n];

            return new Matrix(inverse);
        }

        // Numerical Methods
        public double NewtonRaphson(Func<double, double> f, Func<double, double> fPrime, double x0, double tolerance = 1e-10, int maxIterations = 100)
        {
            double x = x0;
            int iterations = 0;

            while (iterations < maxIterations)
            {
                double fx = f(x);
                if (Math.Abs(fx) < tolerance)
                    return x;

                double fPrimeX = fPrime(x);
                if (Math.Abs(fPrimeX) < EPSILON)
                    throw new ArgumentException("Derivative too close to zero");

                x = x - fx / fPrimeX;
                iterations++;
            }

            throw new ArgumentException("Method did not converge");
        }

        // Vector Operations
        public double DotProduct(double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length)
                throw new ArgumentException("Vectors must have the same length");

            return v1.Zip(v2, (a, b) => a * b).Sum();
        }

        public double[] CrossProduct(double[] v1, double[] v2)
        {
            if (v1.Length != 3 || v2.Length != 3)
                throw new ArgumentException("Cross product is only defined for 3D vectors");

            return new double[]
            {
                v1[1] * v2[2] - v1[2] * v2[1],
                v1[2] * v2[0] - v1[0] * v2[2],
                v1[0] * v2[1] - v1[1] * v2[0]
            };
        }
    }
} 