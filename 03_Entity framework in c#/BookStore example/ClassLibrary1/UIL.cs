using System;
using System.Collections.Generic;
using BLL;
using BOL;

namespace UIL
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<AuthorModel> myAuthors = AuthorManager.GetAllAuthors();
            foreach (AuthorModel item in myAuthors)
            {
                Console.WriteLine($"{item.AuthorName} is {item.AuthorAge} years old");
            }
        }
    }
}
