using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성,
    /// 기능 : 브랜치 제작 관련 패널
    /// </summary>
    public class BranchMakingPanel : MonoBehaviour
    {
        [SerializeField] Text branch;

        public void MakeBranch()
        {
            GitFunction.MakeBranch(branch.text);
        }
    }
}