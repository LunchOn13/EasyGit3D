using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    // 커밋 종류
    public enum CommitCategory
    {
        Add, Modify, Delete, Finish
    }

    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 커밋 모델링
    /// </summary>
    public class CommitModel : MonoBehaviour
    {
        [SerializeField] Material add;
        [SerializeField] Material modify;
        [SerializeField] Material delete;
        [SerializeField] Material finish;

        private MeshRenderer current;

        private string title;
        private string contributor;
        private string date;

        private void Awake()
        {
            current = GetComponent<MeshRenderer>();
        }

        // 커밋 종류에 따라 메터리얼 적용
        public void ApplyMaterial(CommitCategory category)
        {
            switch(category)
            {
                case CommitCategory.Add:
                    current.material = add;
                    break;

                case CommitCategory.Modify:
                    current.material = modify;
                    break;

                case CommitCategory.Delete:
                    current.material = delete;
                    break;

                case CommitCategory.Finish:
                    current.material = finish;
                    break;

                default:
                    break;
            }
        }
    }
}