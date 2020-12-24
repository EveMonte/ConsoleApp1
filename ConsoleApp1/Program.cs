using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.IO;
namespace Laba16
{
    class Program
    {
        static List<uint> SieveEratosthenes(uint n)
        {
            var numbers = new List<uint>();
            //заполнение списка числами от 2 до n-1
            for (var i = 2u; i < n; i++)
            {
                numbers.Add(i);
            }

            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2u; j < n; j++)
                {
                    //удаляем кратные числа из списка
                    numbers.Remove(numbers[i] * j);
                }
            }

            return numbers;
        }

        public static int Num(int a)
        {
            return (a + 100) / 16;
        }
        public static void Factorial(int x)
        {
            ulong result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= (ulong)i;
            }
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Console.WriteLine($"Факториал числа {x} равен {result}");
            //Thread.Sleep(3000);
        }

        public static async void AsyncWriter()
        {
            using (StreamWriter writer = new StreamWriter(@"C:\Users\User\Desktop\ООП\ConsoleApp1\ConsoleApp1\asyncwriter.txt", false))
            {
                await writer.WriteLineAsync("Hello World");  // асинхронная запись в файл
            }
            Console.WriteLine("End");
        }
        static void Main(string[] args)
        {
            uint n;
            Console.WriteLine("Введите число: ");
            n = uint.Parse(Console.ReadLine());
            var taskEratosthenes = new Task<List<uint>>(() => SieveEratosthenes(n));
            taskEratosthenes.Start();
            Stopwatch sw = new Stopwatch();
            CancellationTokenSource token = new CancellationTokenSource();
            sw.Start();
            SieveEratosthenes(10000);
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalSeconds);
            sw.Restart();
            var task = new Task(() => Program.SieveEratosthenes(10000), token.Token);
            task.Start();
            Thread.Sleep(1000);
            token.Cancel();
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalSeconds);

            Task<int>[] tasks3 = new Task<int>[3]
            {
                new Task<int>(() => Num(5) ),
                new Task<int>(() => Num(12)),
                new Task<int>(() => Num(7))
            };
            foreach (var t in tasks3)
                t.Start();
            //TaskContinuationOptions.None;
            Task rezulttask = new Task(() => Console.WriteLine("Rezulttask: " + tasks3[0].Result + tasks3[1].Result + tasks3[2].Result));
            var await = rezulttask.GetAwaiter();
            rezulttask.Start();
            Task continuetask = rezulttask.ContinueWith(t => Console.WriteLine("ContinueWith"));
            await.OnCompleted(() => Console.WriteLine("Completed"));
            int[] arr = new int[100000000];
            Random rand = new Random();
            
            sw.Restart();
            Parallel.For(1, 100000000, z =>
            {
                    arr[z] = rand.Next(1,10);
                    //Console.WriteLine(arr[i]);
            });
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalSeconds);
            Console.WriteLine($"{arr[20000]}, {arr[100000]}");
            sw.Restart();
            for (int i = 0; i < 100000000; i++)
            {
                arr[i] = rand.Next(1, 10);
                //Console.WriteLine(arr[i]);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalSeconds);

            Parallel.Invoke(() => { Num(200); }, () => { Factorial(50); }, () => { SieveEratosthenes(200); });

            BlockingCollection<string> blockcoll = new BlockingCollection<string>();
            Task[] tasks1 = new Task[5]
            {
                new Task(() => {for(int i=0;i<4;i++){ Console.WriteLine("Поставщик 1 добавил на склад холодильник");blockcoll.Add("холодильник"); Thread.Sleep(2000); } } ),
                new Task(() => {for(int i=0;i<4;i++){Console.WriteLine("Поставщик 2 добавил на склад микроволновую печь");blockcoll.Add("микроволновая печь"); Thread.Sleep(2000); } }),
                new Task(() => {for(int i=0;i<4;i++){Console.WriteLine("Поставщик 3 добавил на склад утюг");blockcoll.Add("утюг"); Thread.Sleep(1200); } }),
                new Task(() => {for(int i=0;i<4;i++){Console.WriteLine("Поставщик 4 добавил на склад газовую плиту");blockcoll.Add("газовая плита"); Thread.Sleep(3000); } }),
                new Task(() => {for(int i=0;i<4;i++){Console.WriteLine("Поставщик 5 добавил на склад пылесос");blockcoll.Add("пылесос"); Thread.Sleep(1000); } })
            };
            Task[] tasks2 = new Task[10]
            {
                new Task(  () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 1 купил  " + item);
                    Thread.Sleep(3000);
                }
                } ),
                new Task( () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 2 купил  " + item);
                         Thread.Sleep(3000);
                }
                }),
                new Task( () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 3 купил  " + item);
                         Thread.Sleep(3000);
                }
                }),
                new Task( () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 4 купил  " + item);
                         Thread.Sleep(3000);
                }
                }),
                new Task( () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 5 купил  " + item);
                         Thread.Sleep(3000);
                }
                }),
                new Task( () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 6 купил  " + item);
                         Thread.Sleep(3000);
                }
                }),
                new Task( () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 7 купил  " + item);
                         Thread.Sleep(3000);
                }
                }),
                new Task( () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 8 купил  " + item);
                         Thread.Sleep(3000);
                }
                }),
                new Task( () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 9 купил  " + item);
                         Thread.Sleep(3000);
                }
                }),
                new Task( () =>
                {
                foreach (var item in blockcoll.GetConsumingEnumerable())
                {
                    Console.WriteLine("Покупатель 10 купил  " + item);
                         Thread.Sleep(3000);
                }
                })
           };
            for (int producer = 0; producer < 5; producer++)
            {
                tasks1[producer].Start();
            }
            for (int consumer = 0; consumer < 10; consumer++)
            {
                tasks2[consumer].Start();
                //tasks2[consumer].Wait();
            }
            AsyncWriter();
        }
    }
}
