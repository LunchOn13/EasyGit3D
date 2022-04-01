using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Testing1
{
    class Program
    {
        static void example() // input 없이 
        {
            string text = "ABC\nDEF\tGHI JKL";
            char[] sep = { '\n', '\t', ' ' };  // 이것들로 토큰을 나눈다

            string[] result = text.Split(sep); 
            foreach (var item in result)
            {
                Console.WriteLine(item); // 나눈 토큰을 출력
            }
            JObject sonSpec = new JObject(
                new JProperty("score", 9),
                new JProperty("name", "손흥민"),
                new JProperty("number", 7)
            );
            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\test.json", sonSpec.ToString());
        }
        static void example(string input) // input이 있을시  
        {
            char[] sep = { '\n', '\t', ' ' }; // 똑같이 input을 나눈다

            string[] result = input.Split(sep);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
       
        static void Main(string[] args)
        {
            
            //string maintext = Console.ReadLine(); // 터미널에서 입력 받을수 있다.

            example();
            //Console.WriteLine("###HelloWorld###");
            //example(maintext);
            Console.WriteLine("HelloWorld!");
        }
    }
}