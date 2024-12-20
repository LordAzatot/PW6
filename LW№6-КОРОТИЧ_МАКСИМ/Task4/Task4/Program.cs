using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class TaskScheduler<TTask, TPriority> where TPriority : IComparable<TPriority>
    {
        private readonly SortedDictionary<TPriority, Queue<TTask>> taskQueue = new();

        public delegate void TaskExecution(TTask task);

        public void AddTask(TTask task, TPriority priority)
        {
            if (!taskQueue.ContainsKey(priority))
            {
                taskQueue[priority] = new Queue<TTask>();
            }
            taskQueue[priority].Enqueue(task);
        }

        public void ExecuteNext(TaskExecution execute)
        {
            if (!taskQueue.Any())
            {
                Console.WriteLine("Немає завдань у черзі.");
                return;
            }

            var highestPriority = taskQueue.Keys.First();
            var task = taskQueue[highestPriority].Dequeue();
            if (!taskQueue[highestPriority].Any())
            {
                taskQueue.Remove(highestPriority);
            }

            execute(task);
        }
    }

    class Program4
    {
        static void Main()
        {
            var scheduler = new TaskScheduler<string, int>();

            scheduler.AddTask("Завдання 1", 2);
            scheduler.AddTask("Завдання 2", 1);
            scheduler.AddTask("Завдання 3", 3);

            scheduler.ExecuteNext(task => Console.WriteLine($"Виконуємо: {task}"));
            scheduler.ExecuteNext(task => Console.WriteLine($"Виконуємо: {task}"));
            scheduler.ExecuteNext(task => Console.WriteLine($"Виконуємо: {task}"));

            Console.WriteLine("\nДодайте нове завдання з пріоритетом:");
            Console.Write("Опис завдання: ");
            string newTask = Console.ReadLine();
            Console.Write("Пріоритет (число): ");
            int priority = int.Parse(Console.ReadLine());

            scheduler.AddTask(newTask, priority);
            scheduler.ExecuteNext(task => Console.WriteLine($"Виконуємо: {task}"));
        }
    }
}
