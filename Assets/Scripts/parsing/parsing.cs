using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

public class parsing {

    public Dictionary<string, commit> branches = new Dictionary<string, commit>();


    public Dictionary<string, commit> parsedDic()
    {
        return this.branches;
    }

    public void parseLog(string s)
    {
        // git log 명령어로 string json 형식으로 아예 받아온다
        //  [] 붙인다

        s = s.Insert(0, "[");
        s = s.Insert(s.Length, "]");

        JArray json = JArray.Parse(s);
        File.WriteAllText(@"D:\test.json", json.ToString());

        string nowRef = "";
        foreach (var cont in json)
        {
            //UnityEngine.Debug.Log(cont);
            if (cont["ref"].ToString() != "")
            {
                //UnityEngine.Debug.Log(nowRef +" " + cont["ref"].ToString());
                nowRef = cont["ref"].ToString();
            }
            //UnityEngine.Debug.Log(nowRef);
            commit tmpCommit = new commit(
                cont["CommitHash"],
                cont["Author"],
                cont["authorDate"],
                cont["Message"],
                nowRef,
                cont["ParentHash"]
                );

            branches.Add(cont["CommitHash"].ToString(), tmpCommit);
        }

        //foreach(var tmp in branches)
        //{
        //    string ss = tmp.Key + " parenthash is ";
        //    foreach( var tt in tmp.Value.theRef)
        //    {
        //        ss += tt.ToString() + " ";
        //    }
        //    UnityEngine.Debug.Log(ss);
        //}
        
    }


    void constructData(string s)
    {

    }



    void parseStatus(string s)
    {

    }

    // 굳이?
    void parseBranch(string s)
    {

    }

   
}
