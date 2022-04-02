using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Testing1
{
    class Program
    {
        static void example() // input 없이 
        {
            string text = "AuthorString\nDataString\tTitleString ExString";
            char[] sep = { '\n', '\t', ' '};  // 이것들로 토큰을 나눈다

            string[] result = text.Split(sep); 

            foreach (var item in result)   // 나눈 토큰을 출력
            {                                    //
                Console.WriteLine(item); //
            }                                   //

            JObject sonSpec = new JObject(
                new JProperty("Author", result[0]),
                new JProperty("Data", result[1]),
                new JProperty("Title", result[2]),
                new JProperty("ExTest", result[3])
            );
            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\test1_4.json", sonSpec.ToString());
        }
        static void example(string input) // input이 입력시  
        {                       
            string text = input;
                                //줄 바꿈 삭제가 안됨...
            char[] sep = {/*'\r', '\n', '\t',*/ ' '}; 
            string[] result = text.Split(sep, 15); // input을 ' '로 15개로 나눈다

            JObject sonSpec = new JObject(          // json 파일의 내용들을 string문자로 만들기
                  new JProperty("Commit?", result[0].Equals("commit")? "true": "false")
                  ,new JProperty("Commit_address", result[1])
                  ,new JProperty("Author", result[3])
                  ,new JProperty("Date", result[8] +" " +result[9] +" " +result[10] +" " +result[12] )
                  ,new JProperty("Time", result[11])
                  ,new JProperty("Title", result[14])
              );
            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\test2.json", sonSpec.ToString());
            // json의 내용이 담긴 내용을 실제 json파일로 생성
        }
        static void Main(string[] args)
        {
            string text = Console.ReadLine();   // 문자 열을 입력
            example(text);
            Console.WriteLine("### Main The End ###");
        }
    }
}