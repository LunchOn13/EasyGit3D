using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : GIT OUTPUT을 JSON 데이터로 변환
/// </summary>
public class JSONParser
{
    // 브랜치 정보 변환
    public void GetBranch(string output)
    {
        JObject sonSpec = new JObject();

        char[] sep = { '\r', '\n', '\t', ' ' };
        string[] result = output.Split(sep);

        List<string> Buser = new List<string>();
        int StarIndex = 0;

        for (int i = 0; i < result.Length; i++)
        {
            if (!result[i].Equals("*"))
                Buser.Add(result[i]);
        }

        //checking = result[StarIndex + 1];
        sonSpec.Add(new JProperty("checkout", result[StarIndex + 1]));
        sonSpec.Add(new JProperty("branches", Buser));

        //File.WriteAllText(@"C:\Users\82103\Desktop\Swimming_on_git\KJM_Flie\Branch.json", sonSpec.ToString());
        //return Buser;
    }
}
