using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    // 모델링에 필요한 레포지토리 데이터
    [System.Serializable]
    public class RepositoryData
    {
        public BranchData[] branches;
    }

    // 모델링에 필요한 브랜치 데이터
    [System.Serializable]
    public class BranchData
    {
        public string title;
        public CommitData[] commits;
    }

    // 모델링에 필요한 커밋 데이터
    [System.Serializable]
    public class CommitData
    {
        public string title;
        public string contributor;
        public string date;
        public int type;
    }
}