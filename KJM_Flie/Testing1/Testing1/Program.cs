using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Testing1
{
    class Program
    {
        static string Log_seting()
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
        static void Log_Information() // "git log" 하면 나오는 commi의 정보
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
            else
            {
                Console.WriteLine("### 그 외 ###");
            }
            // json 파일의 내용들을 string문자로 만들기  
            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\Git_Log.json", sonSpec.ToString());
            // json의 내용이 담긴 내용을 실제 json파일로 생성

            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine(result[i] + " : " +i);
            }

        }
        static void Branch(string input) // "git branch" 하면 나오는 것들
        {
            JObject sonSpec = new JObject();

            // step 1 먼저 토큰 나누기
            char[] sep = { '\r', '\n', '\t', ' ' };
            string[] result = input.Split(sep);
            List<String> wirtestring = new List<String>();

            // step 2 branch의 경우 check가 어디로 되어있는지 확인 And 
            int StarIndex = 0;
            for(int i = 0; i < result.Length; i++)
            {
                if (!result[i].Equals("*"))
                    wirtestring.Add(result[i]);
            }

            sonSpec.Add(new JProperty("Check", result[StarIndex + 1]));
            sonSpec.Add(new JProperty("member", wirtestring));
            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\Git_Branch.json", sonSpec.ToString());
        }

        static void Main(string[] args)
        {
            string text = Console.ReadLine();   // 문자 열을 입력
            Branch(text); // git branch 전용
            //Log_Information(); // git log 전용
        }
    }
}

//##########################################################################
//                          무제점 한 라인 만 입력이 되서 엔터가 안먹힘.