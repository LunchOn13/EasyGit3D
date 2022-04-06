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

        // git log 하면 나오는 commi의 정보
        static void Log_Information(string input) 
        {
            string text = input;
            char[] sep = { '\r', '\n', '\t', ' ' };
            string[] result = text.Split(sep, 15); // input을 ' '로 15개로 나눈다

            JObject sonSpec = new JObject(          // json 파일의 내용들을 string문자로 만들기
                  new JProperty("CommitMassage", result[14])
                  , new JProperty("DateAndTime", result[8] + " " + result[9] + " " + result[10] + " " + result[12] +" "+ result[11])
                  , new JProperty("Author", result[3])
                  
              );
            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\Git_Log.json", sonSpec.ToString());
            // json의 내용이 담긴 내용을 실제 json파일로 생성
        }

        static void Branch(string input)
        {
            char[] sep = { '\r', '\n', '\t', ' ' };
            string[] result = input.Split(sep);

            int StarIndex = 0;
            for(int i = 0; i < result.Length; i++)
                if (result[i].Equals("*")) StarIndex = i;

            JObject sonSpec = new JObject();

            sonSpec.Add(new JProperty("Check", result[StarIndex + 1]));

            for (int i = 0; i < result.Length; i++)
            {
                if(!result[i].Equals("*"))
                    sonSpec.Add(new JProperty("Branchs"+i, result[i]));
            }

            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\Git_Branch.json", sonSpec.ToString());
        }



        static void Main(string[] args)
        {
            Console.WriteLine("지금은 test중이라서 임시로 이런 식으로 진행");
            Console.WriteLine("log, branch 둘중에서 하나만 입력하셈");

            string input1 = Console.ReadLine();

            if (input1.Equals("branch"))
            {
                Console.WriteLine("내용 입력");
                string text = Console.ReadLine();   // 문자 열을 입력
                // 몇개인지는 나중에 for문 돌리기
                Branch(text); // git branch 전용
            }
            else if (input1.Equals("log"))
            {
                string text = Console.ReadLine();   // 문자 열을 입력
                Log_Information(text); // git log 전용
            }





        }
    }
}

//##########################################################################
//                          무제점 한 라인 만 입력이 되서 엔터가 안먹힘.