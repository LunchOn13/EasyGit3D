using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Testing1
{
    class Program
    {
        static string Log_seting() // 여러줄로 받기 위한 함수
        {
            string output = "";
            for (int i = 0; i < 6; i++)
            {
                string ins = Console.ReadLine();
                if (0 != i)
                    output += ins;
                output += " ";
            }
            return output;
        }
        static void Commit() // "git log" 하면 나오는 commi의 정보
        {
            string text = Log_seting();
            string[] result = text.Split(' '); // input을 ' '로 나눈다
            JObject sonSpec = new JObject();

            if(result.Length == 21)
            {
                sonSpec.Add(new JProperty("CommitMassage", result[18]));
                sonSpec.Add(new JProperty("DateAndTime", result[7] + " " + result[8] + " " + result[9] + " " + result[11] + " " + result[10]));
                sonSpec.Add(new JProperty("Author", result[2]));
            }
            // json 파일의 내용들을 string문자로 만들기  
            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\Commit.json", sonSpec.ToString());
            //return sonSpec;
        }
        static void Repository(string input) // "git branch" 하면 나오는 것들
        {
            JObject sonSpec = new JObject();

            char[] sep = { '\r', '\n', '\t', ' ' };
            string[] result = input.Split(sep);

            List<String> Buser = new List<String>();
            int StarIndex = 0;
            
            for(int i = 0; i < result.Length; i++)
            {
                if (!result[i].Equals("*"))
                    Buser.Add(result[i]);
            }

            JObject member = new JObject();

            for (int i = 0; i < Buser.Count; i++)
            {
                JObject mem = new JObject();
                int x;
                JObject str = new JObject();
                Console.WriteLine(Buser[i]+"의 커밋수 입력");
            
                x = int.Parse(Console.ReadLine());
                for(int j = 0; j < x; j++)
                {
                    //str.Add(Commit());
                }
                mem.Add(new JProperty(Buser[i]+"", str));
                member.Add(mem);
            }




            sonSpec.Add(new JProperty("Check", result[StarIndex + 1]));
            sonSpec.Add(new JProperty("member", member));

            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\XXX.json", sonSpec.ToString());
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("branch의 리스트를 한줄로 입력 하기");
            //string text = Console.ReadLine();   // 문자 열을 입력 //* KJM main user1 user2
            //Repository(text); // git branch 전용


           Commit(); // git log 전용
        }
    }
}
/*
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
 */
