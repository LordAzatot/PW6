using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class FunctionCache<TKey, TResult>
    {
        private readonly Dictionary<TKey, (TResult Result, DateTime Expiration)> cache = new();
        private readonly TimeSpan cacheDuration;

        public FunctionCache(TimeSpan cacheDuration)
        {
            this.cacheDuration = cacheDuration;
        }

        public TResult GetOrExecute(TKey key, Func<TKey, TResult> func)
        {
            if (cache.ContainsKey(key) && cache[key].Expiration > DateTime.Now)
            {
                return cache[key].Result;
            }

            var result = func(key);
            cache[key] = (result, DateTime.Now + cacheDuration);
            return result;
        }
    }

    class Program3
    {
        static void Main()
        {
            var cache = new FunctionCache<int, int>(TimeSpan.FromSeconds(10));

            int ExpensiveFunction(int x)
            {
                Console.WriteLine($"Виконуємо обчислення для {x}...");
                return x * x;
            }

            Console.WriteLine("Результат: " + cache.GetOrExecute(5, ExpensiveFunction));
            Console.WriteLine("Результат: " + cache.GetOrExecute(5, ExpensiveFunction)); // Отримаємо з кешу
        }
    }
}
