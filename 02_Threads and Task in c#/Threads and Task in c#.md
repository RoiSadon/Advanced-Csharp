# sync version: 
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class Program
    {

        static void print1(object limit)
        {
            Console.WriteLine("Print1 ThreadID: " + Thread.CurrentThread.ManagedThreadId);

            for (int i = 0; i < (int)limit; i++)
            {
                Console.WriteLine("print1: " +i);
                Thread.Sleep(1000);
            }
        }
        static void print2(object limit)
        {
            Console.WriteLine("Print2 ThreadID: " + Thread.CurrentThread.ManagedThreadId);

            for (int i = 0; i < (int)limit; i++)
            {
                Console.WriteLine("print2: " + i);
                Thread.Sleep(1000);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread: "+ Thread.CurrentThread.ManagedThreadId);
            print1(3);
            print2(4);
            Console.WriteLine("End of main");
        }
    }
}
```

# Async version:
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class Program
    {

        static void print1(object limit)
        {
            string threadDesc = "Print1 ThreadID: " + Thread.CurrentThread.ManagedThreadId +
                " Thread name: " + Thread.CurrentThread.Name;
            Console.WriteLine("\n---------------start "+ threadDesc);

            for (int i = 0; i < (int)limit; i++)
            {
                Console.WriteLine($"Print1 in thread {Thread.CurrentThread.Name} - iteration in {i}");
                Thread.Sleep(1000);
            }
            Console.WriteLine("\n-------------end " + threadDesc);
        }
        static void print2(object limit)
        {
            string threadDesc = "Print2 ThreadID: " + Thread.CurrentThread.ManagedThreadId +
                " Thread name: " + Thread.CurrentThread.Name;
            Console.WriteLine("\n---------------start " + threadDesc);

            for (int i = 0; i < (int)limit; i++)
            {
                Console.WriteLine($"Print2 in thread {Thread.CurrentThread.Name} - iteration in {i}");
                Thread.Sleep(1000);
            }
            Console.WriteLine("\n-------------end " + threadDesc);
        }

        static void Main(string[] args)
        {
            // create instance of threads: (gets function)
            Thread t1 = new Thread(print1);
            Thread t2 = new Thread(print2);

            // Give priority to threads:
            t1.Priority = ThreadPriority.Highest;
            t2.Priority = ThreadPriority.Lowest;

            // IsBackground - if thread will still run when all the others stopped
            t1.IsBackground = false; // default
            t2.IsBackground = true;

            // Activate thread:
            t1.Start(2);
            t2.Start(5);
            Console.WriteLine("End of main");

        }
      
    }
}
```
# Thread's array:
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class Program
    {

        static void asyncMsg(object obj)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Thread with index {obj}");
        }

        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            Thread[] threadArray = new Thread[num];
            for (int i = 0; i < threadArray.Length; i++)
            {
                threadArray[i] = new Thread(asyncMsg);
                threadArray[i].Start(i);
            }
        }
    }
}
```
# The thread lock:
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class Program
    {

        static Queue<int> q = new Queue<int>();

        static void Main(string[] args)
        {
            Thread t1 = new Thread(Add);
            t1.Start();

            Thread t2 = new Thread(Sub);
            t2.Start();
        }

        static void Add()
        {
            Console.WriteLine("Add start");
            lock(q)
            {
                for (int i = 0; i < 5; i++)
                {
                    q.Enqueue(1);
                    Console.WriteLine("add" + q.Count);
                    Thread.Sleep(1000);
                }
            }
            Console.WriteLine("Add End");
        }

        static void sub()
        {
            Console.WriteLine("Sub start");
            lock(q)
            {
                for (int i = 0; i < 5; i++)
                {
                    q.Dequeue();
                    Console.WriteLine("Sub: " + q.Count);
                    Thread.Sleep(1000);
                }
            }
        }

        static void ForExample()
        {
            // Primitive value can't be in lock
            int a = 9;
            // lock(a)  - will get error!

            // object can be in lock
            object o = new object();
            lock (o)
            {
                // no thread can use now in o. 
            }
            // other threads can use o. 

            try
            {
                Monitor.Enter(o);
            }
            finally
            {
                Monitor.Exit(o);
            }
            // Better to use lock and not monitor
        }
    }
}
```
# Join()
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class Program
    {

        static void Test()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} START");
            Thread.Sleep(1000);
            Console.WriteLine($"{Thread.CurrentThread.Name} END");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("MAIN START");

            Thread t1 = new Thread(Test) { Name = "t1" };
            Thread t2 = new Thread(Test) { Name = "t2" };
            Thread t3 = new Thread(Test) { Name = "t3" };

            // join() - allows one thread to wait until
            // another thread completes its execution. 

            t1.Start();
            t1.Join();

            t2.Start();
            t2.Join();

            t3.Start();
            t3.Join();

            Console.WriteLine("Main end");
        }

    }
}
```
# Dead - lock:
```cs
using System;
using System.Collections.Generic;
using System.Threading;

namespace the_therad_lock
{
    class Program
    {

        //Queue - fifo - first in first out
        //Global - can be modified by any thread
        static Queue<int> q1 = new Queue<int>();
        static Queue<int> q2 = new Queue<int>();

        //"Thread Main"
        static void Main(string[] args)
        {

            Thread t1 = new Thread(FuncQ1);
            t1.Start();

            Thread t2 = new Thread(FuncQ2);
            t2.Start();


        }

        static void FuncQ1()
        {
            Console.WriteLine("-------FuncQ1 start--------");


            lock (q1)
            {
                for (int i = 0; i < 5; i++)
                {
                    q1.Enqueue(1);
                    Console.WriteLine("add to q1: " + q1.Count);
                    Thread.Sleep(500);
                }

                lock (q2)
                {
                    Console.WriteLine(q2.Count);
                }
            }


            Thread.Sleep(2500);
            Console.WriteLine("-------FuncQ1 end--------");
        }

        static void FuncQ2()
        {

            Console.WriteLine("-------FuncQ2 start--------");

            lock (q2)
            {
                for (int i = 0; i < 5; i++)
                {
                    q2.Enqueue(1);
                    Console.WriteLine("add to q2: " + q2.Count);
                    Thread.Sleep(500);
                }

                lock (q1)
                {
                    Console.WriteLine(q1.Count);
                }
            }

            Console.WriteLine("-------FuncQ2 end--------");
        }
    }
}
```
# Task
.NET framework provides Threading.Tasks class to let you create tasks and run them asynchronously. A task is an object that represents some work that should be done. The task can tell you if the work is completed and if the operation returns a result, the task gives you the result.
```cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace the_therad_lock
{
    class Program
    {

        static void print()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write($"{Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(100);
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(print);
            Task t2 = new Task(print);

            t1.Start();
            t2.Start();
            Console.WriteLine("------------After start--------------");
            Console.WriteLine($"t1.IsCompleted: {t1.IsCompleted}, t2.IsCompleted: {t2.IsCompleted}");

            t1.Wait();
            t2.Wait();
            Console.WriteLine("\n----------After wait---------------");
            Console.WriteLine($"t1.IsCompleted: {t1.IsCompleted}, t2.IsCompleted: {t2.IsCompleted}");
        }
    }
}
```
# Task returning value
```cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace the_therad_lock
{
    class Program
    {

        static int GetSummary(object maxNum)
        {
            int sum = 0;
            for (int i = 0; i < (int)maxNum; i++)
            {
                sum += i;
                Thread.Sleep(50);
            }
            return sum;
        }

        static void Main(string[] args)
        {
            Task<int> t = new Task<int>(GetSummary, 70);
            t.Start();
            Console.WriteLine("Task statted...");
            int res = t.Result;
            Console.WriteLine("Result: "+res);
        }
    }
}
```
# Async await
```cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace the_therad_lock
{
    class Program
    {
        public static int Test()
        {
            return 1000;
        }

        static void Main(string[] args)
        {
            Task<int> t = new Task<int>(Test);
            t.Start();
            Console.WriteLine($"Main: {t.Result}");
            ResAsync();
        }

        private static async Task ResAsync()
        {
            Task<int> t = new Task<int>(Test);
            t.Start();
            Console.WriteLine($"Res: {await t}");
        }
    }
```
# Async- await
```cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace the_therad_lock
{
    class Program
    {
        private static Task<int> f1()
        {
            Console.WriteLine("Enter a number");
            int num = int.Parse(Console.ReadLine());
            Task<int> res = f2(num);
            res.Start();
            return res;
        }

        private static Task<int> f2(int num)
        {
            return new Task<int>(() => (int)num * 2);
        }

        static void Main(string[] args)
        {
            Task<Task<int>> t = new Task<Task<int>>(f1);
            t.Start();
            Console.WriteLine(t.Result.Result);
        }
    }
}
```

