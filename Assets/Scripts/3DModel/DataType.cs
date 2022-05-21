using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    // 모델링에 필요한 레포지토리 데이터
    public class RepositoryData
    {
        public string checkout;
        public List<BranchData> branches;
    }

    // 모델링에 필요한 브랜치 데이터
    public class BranchData
    {
        public string title;
        public List<CommitData> commits;

        public BranchData(string _title)
        {
            title = _title;
            commits = new List<CommitData>();
        }
    }

    // 모델링에 필요한 커밋 데이터
    public class CommitData
    {
        public string message;
        public string author;
        public string date;

        public CommitData(string _message, string _author, string _date)
        {
            message = _message;
            author = _author;
            date = _date;
        }
    }

    // 모델링에 필요한 수정사항 데이터
    public class ModifiedData
    {
        public string title;
        public string author;
        public string date;
        public string type;
    }


    // 변경사항 종류
    public enum StatusCategory
    {
        Added, Modified, Deleted, Untracked
    }
}