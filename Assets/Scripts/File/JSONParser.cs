//using Newtonsoft.Json.Linq;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//public class JSONParser
//{
//    public class JsonFile
//    {
//        public string checkout { get; set; }
//        public List<Branch> branches { get; set; }

//    }
//    public class Branch
//    {
//        public string title { get; set; }
//        public List<Commit_info> commits { get; set; }
//    }
//    public class Commit_info
//    {
//        public string message { get; set; }
//        public string author { get; set; }
//        public string date { get; set; }
//    }

//    private string checking;

//    // OUTPUT을 토대로 파싱
//    private string LogSetting(string log)
//    {
//        string line;
//        string output = "";
//        StringReader stringReader = new StringReader(log);
//        while (true)
//        {
//            line = stringReader.ReadLine();
//            if (line == null)
//                break;

//            output += line;
//            output += " ";
//        }

//        return output;
//    }



//    class Program
//    {
//        static string checking;
//        static string Log_seting() // 여러줄로 받기 위한 함수
//        {
//            string output = "";
//            for (int i = 0; i < 6; i++)
//            {
//                string ins = Console.ReadLine();
//                output += ins;
//                output += " ";
//            }
//            return output;
//        }

//        static List<Commit_info> Commit(int count) // "git log" 명령의 output
//        {
//            Console.WriteLine("commit 입력");
//            List<Commit_info> ListCommit = new List<Commit_info>();

//            for (int k = 0; k < count; k++)
//            {
//                string text = Log_seting();
//                string[] result = text.Split(' '); // input을 ' '로 나눈다
//                JObject sonSpec = new JObject();

//                // 일반 (commit 1929a17f0f1473eb781e84310e947539c9fe4016 (HEAD -> KJM, origin/KJM))  X
//                // commit 메시지가 뛰엄뛰엄 되있을수 있기떄문에 같이 묶이용
//                string commit_massage = "";
//                for (int i = 19; i < result.Length; i++)
//                    commit_massage += result[i];

//                ListCommit.Add(new Commit_info()
//                {
//                    message = commit_massage
//                    ,
//                    author = result[3]
//                    ,
//                    date = result[8] + " " + result[9] + " " + result[10] + " " + result[12] + " " + result[11]
//                });
//            }
//            //string strJson = JsonConvert.SerializeObject(ListCommit.ToArray(), Formatting.Indented);
//            return ListCommit;
//        }

//        static List<String> Repository(string input) // "git branch" 명령의 output
//        {
//            char[] sep = { '\r', '\n', '\t', ' ' };
//            string[] result = input.Split(sep);

//            List<String> Buser = new List<String>();
//            int StarIndex = 0;

//            for (int i = 0; i < result.Length; i++)
//            {
//                if (!result[i].Equals("*"))
//                    Buser.Add(result[i]);
//            }

//            checking = result[StarIndex + 1];
//            return Buser;
//        }

//        static void Main(string[] args)
//        {
//            List<String> BranchList = new List<String>();
//            List<Branche> Branche = new List<Branche>();
//            List<JsonFile> JsonFile = new List<JsonFile>();
//            JObject sonSpec = new JObject();

//            Console.WriteLine("branch의 리스트를 한줄로 입력 하기");
//            string text = Console.ReadLine();   // 문자 열을 입력 //* KJM main
//            BranchList = Repository(text); // git branch 전용

//            for (int i = 0; i < BranchList.Count; i++)
//            {
//                Console.WriteLine(BranchList[i] + "의 커밋 갯수 입력");
//                int commitCount = int.Parse(Console.ReadLine());

//                //Console.WriteLine(CommitOfBranch);

//                Branche.Add(new Branche()
//                {
//                    title = BranchList[i]
//                    ,
//                    commits = Commit(commitCount)
//                });
//            }
//            Console.WriteLine("파일명 입력");
//            string fliename = Console.ReadLine();

//            //JsonFile.Add(new JsonFile()
//            //{
//            //    checkout = checking
//            //    ,
//            //    branches = Branche
//            //});
//            //string strJsonmain = JsonConvert.SerializeObject(JsonFile.ToArray(), Formatting.Indented);
//            //File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\" + fliename + ".json", strJsonmain);

//            JsonFile js = new JsonFile { checkout = checking, branches = Branche };
//            var j3 = JObject.FromObject(js);
//            File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\" + fliename + ".json", j3.ToString());
//        }
//    }
//}