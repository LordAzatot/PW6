using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Repository<T>
    {
        private readonly List<T> items = new();
        public delegate bool Criteria(T item);

        public void Add(T item) => items.Add(item);
        public List<T> Find(Criteria criteria) => items.Where(item => criteria(item)).ToList();
    }

    class Program2
    {
        static void Main()
        {
            var repository = new Repository<string>();

            repository.Add("Apple");
            repository.Add("Banana");
            repository.Add("Cherry");
            repository.Add("Apricot");

            var result = repository.Find(item => item.StartsWith("A"));

            Console.WriteLine("Результат пошуку:");
            foreach (var item in result)
                Console.WriteLine(item);
        }
    }
}