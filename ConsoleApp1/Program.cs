using System;
using BlogManagement.Common.Common;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintSth(Roles.Administrator.ToString());
        }

        static void PrintSth(string s) => Console.WriteLine(s);
    }
}
