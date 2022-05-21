using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

public class parsing {

    public Dictionary<string, commit> branches = new Dictionary<string, commit>();

    public List<Status> StatusList = new List<Status>();

    private int branchAB_p = 0;
    private int branchAB_m = 0;

    private string nowBranch = null;
    private string upstream = null;
    private string oid = null;


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
        
    }



    public List<Status> GetStatusList()
    {
        return this.StatusList;
    }


    /*
     * status 를 파싱하여 list에 저장한다
     * 
     * Status의 형식은 X Y path
     */
    public void parseStatus(string s)
    {
        UnityEngine.Debug.Log("start parse Status");
        StatusList.Clear();

        // 임시 방편
        if (s == "$")
            return;

        StringReader strReader = new StringReader(s);
        string theLine = null;
        while(true)
        {
            theLine = strReader.ReadLine();
            Debug.Log("LINE: " + theLine);

            if(theLine == null)
                break;

            Status tmp = new Status(theLine[0], theLine[1], theLine.Substring(2));
            StatusList.Add(tmp);
        }
    }

    public int GetBranchAB_p()
    {
        return this.branchAB_p;
    }

    public int GetBranchAB_m()
    {
        return this.branchAB_m;
    }

    public string GetnowBranch()
    {
        return this.nowBranch;
    }

    public string GetUpstream()
    {
        return this.upstream;
    }

    public string GetOid()
    {
        return this.oid;
    }

    /*
     * upstream과 비교하여 현재 로컬 branch의 commit 상태를 지시한다.
     */
    public void parseAB(string s)
    {
        StringReader strReader = new StringReader(s);
        string theLine = null;
        int count = 0;
        while(true)
        {
            theLine = strReader.ReadLine();
            if (theLine == null || count > 4)
                break;
            count++;
            string[] tmp = theLine.Split(' ');

            switch(count)
            {
                case 1:
                    this.oid = tmp[2];
                    break;
                case 2:
                    this.nowBranch = tmp[2];
                    break;
                case 3:
                    this.upstream = tmp[2];
                    break;
                case 4:
                    this.branchAB_p = Int32.Parse(tmp[2].Substring(1));
                    this.branchAB_m = Int32.Parse(tmp[3]);
                    break;
            }
        }

    }
}
