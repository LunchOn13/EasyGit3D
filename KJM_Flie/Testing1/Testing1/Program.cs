using System;

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
            string maintext = Console.ReadLine();

            //example();
            Console.WriteLine("###HelloWorld###");
            //example(maintext);
            Console.WriteLine("HelloWorld!");
        }
    }
}
