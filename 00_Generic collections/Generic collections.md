# Generic collections in c#:

# General functions for generic collections

1. Capacity - sets or sets the total number of elements the internal data structure can hold without resizing. 

2. Count - gets the number of elements contained in the List<T>. (for example..)

3. Add(T) - adds an object to the end of the list<T>

4. Clear() - removes all elements from List<T>

5. Contains(T) - determines whether an element is in the List<T>

# Lists
List<T> class represents the list of objects which can be accessed by index. It comes under the System.Collection.Generic namespace. List class can be used to create a collection of different types like integers, strings etc. List<T> class also provides the methods to search, sort, and manipulate lists.

* It is different from the arrays. A List<T> can be resized dynamically but arrays cannot.
* List<T> class can accept null as a valid value for reference types and it also allows duplicate elements.
* If the Count becomes equals to Capacity, then the capacity of the List increased automatically by reallocating the internal array. The existing elements will be copied to the new array before the addition of the new element.
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_list
{
    class Program
    {

        static void PrintList(List<string> myList)
        {
            Console.WriteLine("Capacity: "+myList.Capacity);
            foreach (string item in myList)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            List<string> myCart = new List<string>();
            PrintList(myCart);

            myCart.Add("Book1");
            myCart.Add("Book2");
            PrintList(myCart);

            myCart.AddRange(new string[] { "Book3", "Book4" });
            PrintList(myCart);

            myCart.Insert(2, "Book5");
            PrintList(myCart);

            myCart.InsertRange(0,new string[] { "Book6", "Book7" });
            PrintList(myCart);

            myCart.Remove("Book1");
            PrintList(myCart);

            myCart.RemoveAt(0);
            PrintList(myCart);

            myCart.RemoveRange(1, 3);
            PrintList(myCart);

            myCart.Reverse();
            PrintList(myCart);

            Console.WriteLine(myCart.IndexOf("Book8"));
        }
    }
}
```

# Queue
FIFO : Queue represents a first-in, first out collection of object. It is used when you need a first-in, first-out access of items. When you add an item in the list, it is called enqueue, and when you remove an item, it is called dequeue . This class comes under System.Collections namespace and implements ICollection, IEnumerable, and ICloneable interfaces.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            // FIFO = first in first out
            Queue<string> superQueue = new Queue<string>();

            // add a new element to the end of the queue
            superQueue.Enqueue("Bob");
            superQueue.Enqueue("Alice");
            superQueue.Enqueue("Shadi");

            Console.WriteLine(superQueue.Count);  //--> 3

            // Removes and returns the object at the beginning of queue
            string nextInQueue = superQueue.Dequeue();

            Console.WriteLine(nextInQueue + " " + superQueue.Count);  //--> Bob 2

            // Returns the object at the beginning queue without removing it.
            string peekNextInQueue = superQueue.Peek();

            Console.WriteLine(peekNextInQueue + " " +superQueue.Count);  //--> Alice 2
        }
    }
}
```

# Stack
```csharp
using System;
using System.Collections.Generic;

namespace _01_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            // LIFO = last in first out
            Stack<string> shirtsStack = new Stack<string>();

            // add a new element to the end of the stack
            shirtsStack.Push("shirt1");
            shirtsStack.Push("shirt2");
            shirtsStack.Push("shirt3");

            Console.WriteLine(shirtsStack.Count);  //--> 3

            // Removes and returns the object at the top of the stack
            string topOfStack = shirtsStack.Pop();

            Console.WriteLine(topOfStack + " " + shirtsStack.Count);  //--> shirt3 2

            // Returns the object at the top of the stack without removing it.
            string peekNext = shirtsStack.Peek();

            Console.WriteLine(peekNext + " " + shirtsStack.Count);  //--> shirt2 2
        }
    }
}
```
# Dictionary
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Dict
{
    class Program
    {
        static void Main(string[] args)
        {

            // Dictionary can contain item with an unique key 
            Dictionary<string, double> studentsGrade = new Dictionary<string, double>();

            // Adds the specified key and value to the dictionary
            studentsGrade.Add("Bob", 90.5);
            studentsGrade.Add("Alice", 100);

            Console.WriteLine($"Bob got: {studentsGrade["Bob"]}");
            Console.WriteLine($"Alice got: {studentsGrade["Alice"]}");

            var s=studentsGrade.Keys;

            foreach (KeyValuePair<string,double> item in studentsGrade)
            {
                Console.WriteLine(item.Key + " " + item.Value );
            }

            // ContainsKey Determines whether we have already this key in the dictionary
            if (!studentsGrade.ContainsKey("Alice"))
            {
                studentsGrade.Add("Alice", 100);
            }
            else
            {
                Console.WriteLine("Alice already exists");
            }
        }
    }
}
```

