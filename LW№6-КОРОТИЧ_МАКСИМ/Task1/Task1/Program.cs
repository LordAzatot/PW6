using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Calculator<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        public delegate T Operation(T a, T b);

        public T Add(T a, T b) => PerformOperation(a, b, (x, y) => (dynamic)x + (dynamic)y);
        public T Subtract(T a, T b) => PerformOperation(a, b, (x, y) => (dynamic)x - (dynamic)y);
        public T Multiply(T a, T b) => PerformOperation(a, b, (x, y) => (dynamic)x * (dynamic)y);
        public T Divide(T a, T b)
        {
            if ((dynamic)b == 0) throw new DivideByZeroException("Ділення на нуль неможливе.");
            return PerformOperation(a, b, (x, y) => (dynamic)x / (dynamic)y);
        }

        private T PerformOperation(T a, T b, Operation operation) => operation(a, b);
    }

    class Program1
    {
        static void Main()
        {
            var calculator = new Calculator<double>();

            Console.WriteLine("Додавання: " + calculator.Add(5.5, 2.5));
            Console.WriteLine("Віднімання: " + calculator.Subtract(10.0, 3.0));
            Console.WriteLine("Множення: " + calculator.Multiply(4.2, 3.0));
            Console.WriteLine("Ділення: " + calculator.Divide(20.0, 4.0));

            var intCalculator = new Calculator<int>();
            Console.WriteLine("\nДодавання (int): " + intCalculator.Add(3, 7));
        }
    }
}
