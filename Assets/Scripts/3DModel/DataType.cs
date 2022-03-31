using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
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