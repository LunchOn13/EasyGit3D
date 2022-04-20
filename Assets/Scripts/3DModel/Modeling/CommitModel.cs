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

        [SerializeField] Material highlight;

        private MeshRenderer current;

        private string title;
        private string contributor;
        private string date;

        // 커밋 관련 오브젝트
        private CommitPanel panel;
        private CommitMessage message;

        private void Awake()
        {
            current = GetComponent<MeshRenderer>();
            panel = GameObject.Find("CommitPanel").GetComponent<CommitPanel>();
            message = GameObject.Find("CommitMessage").GetComponent<CommitMessage>();
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

        // 마우스 오버 시 커밋메시지 표시
        private void OnMouseEnter()
        {
            current.material = highlight;
            message.SetMessage("Sample", Input.mousePosition);
        }

        // 마우스 벗어나면 텍스트 숨김
        private void OnMouseExit()
        {
            current.material = current.material = finish;
            message.SetMessage("", transform.position);
        }

        // 클릭 시 패널 표시
        private void OnMouseDown()
        {
            message.SetMessage("", transform.position);
            panel.ShowPanel(Input.mousePosition, "Sameple Message", "Sample Author", "Sample Date");
        }
    }
}