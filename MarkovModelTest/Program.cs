using System;

using MarkovModelTest;
using System.IO;

namespace Sample1
{
    class Program
    {
        static void Main(string[] args)
        {

            string data = Test1.Execute(5000);


            StreamWriter writer = File.CreateText(@"..\Data.txt");
            writer.WriteLine(data);
            writer.Close();

            Console.WriteLine("Finish");
            Console.Read();
            
        }
    }
}