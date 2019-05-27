
<div dir="rtl">

צור אפליקציה המתאימה למשרד דואר
האפליקציה תשמש לספק ניהול תור לקוחות
ישנם 3 סוגי תור
1. שירות בנק
2. שירות החלפת כספים
3. שירות שליחת וקבלת דואר

צור לולאה הרצה 10 פעמים וקולטת עבור כל לקוח את שמו ולאיזה תור הוא רוצה לפנות
הרץ פלט המזמן בכל פעם לקוח בהתאם לתורו

</div>

```csharp
using System;
using System.Collections.Generic;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Queue<string>> Server = new Dictionary<string, Queue<string>>(0);
            Server.Add("Bank", new Queue<string>());
            Server.Add("Mail", new Queue<string>());
            Server.Add("Post", new Queue<string>());

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Enter your name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter your service (Bank / Mail / Post )");
                string choice = Console.ReadLine();

                if (Server.ContainsKey(choice))
                    Server[choice].Enqueue(name);
            }
                int cnt = 0;
                while(cnt<Server.Count)
                {
                    cnt = 0;
                    foreach (KeyValuePair<string,Queue<string>> item in Server)
                    {
                        if (item.Value.Count > 0)
                            Console.WriteLine($"Queue {item.Key} is waiting for {item.Value.Dequeue()}");
                        else
                            cnt++;
                    }
                    Console.WriteLine("------------------------------");
                }
            }
    }
}
```