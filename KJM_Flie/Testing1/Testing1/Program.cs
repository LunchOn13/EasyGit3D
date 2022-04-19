using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Testing1
{
    class Program
    {
        static string checking;
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
        static JObject Commit() // "git log" 명령의 output
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
            //File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\Commit.json", sonSpec.ToString());
            return sonSpec;
        }
        static List<String> Repository(string input) // "git branch" 명령의 output
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

            //JObject member = new JObject();
            //
            //for (int i = 0; i < Buser.Count; i++)
            //{
            //    JObject mem = new JObject();
            //    int x;
            //    JObject str = new JObject();
            //    Console.WriteLine(Buser[i]+"의 커밋수 입력");
            //
            //    x = int.Parse(Console.ReadLine());
            //    for(int j = 0; j < x; j++)
            //    {
            //        //str.Add(Commit());
            //    }
            //    mem.Add(new JProperty(Buser[i]+"", str));
            //    member.Add(mem);
            //}
            checking = result[StarIndex + 1];
            sonSpec.Add(new JProperty("Check", result[StarIndex + 1]));
            sonSpec.Add(new JProperty("member", Buser));

            //File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\Branch.json", sonSpec.ToString());
            return Buser;
        }
        static void Main(string[] args)
        {
            List<String> BranchList = new List<String>();
            JObject sonSpec = new JObject();
            JObject Cfile = new JObject();


            Console.WriteLine("branch의 리스트를 한줄로 입력 하기");
            string text = Console.ReadLine();   // 문자 열을 입력 //* KJM main
            BranchList = Repository(text); // git branch 전용

            for(int i = 0; i< BranchList.Count; i++)
            {
                Console.WriteLine(BranchList[i] + "의 커밋 갯수 입력");
                int x = int.Parse(Console.ReadLine());

                JObject li = new JObject();
                for (int j = 0; j < x; j++)
                    li.Add(new JProperty(j +"", Commit()));

                Cfile.Add(new JProperty(BranchList[i] + "", li));
            }

            sonSpec.Add(new JProperty("Cheke", checking));
            sonSpec.Add(new JProperty("member", Cfile));

            Console.WriteLine("파일명 입력");
            string fliename = Console.ReadLine();
            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\"
+fliename +".json", sonSpec.ToString());
        }
    }
}

//commit 개수 추출하는 명령어
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