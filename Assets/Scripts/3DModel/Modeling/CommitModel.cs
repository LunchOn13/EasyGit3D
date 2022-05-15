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

        private string message;
        private string author;
        private string date;

        // 커밋 관련 오브젝트
        private CommitPanel commitPanel;
        private Message commitMessage;

        private void Awake()
        {
            current = GetComponent<MeshRenderer>();
            commitPanel = GameObject.Find("Commit Panel").GetComponent<CommitPanel>();
            commitMessage = GameObject.Find("Commit Message").GetComponent<Message>();
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

        // 커밋 정보 적용
        public void SetInformation(string _title, string _author, string _date)
        {
            message = _title;
            author = _author;
            date = _date;
        }

        // 마우스 오버 시 커밋메시지 표시
        private void OnMouseEnter()
        {
            current.material = highlight;
            commitMessage.SetMessage(message, Input.mousePosition);
        }

        // 마우스 벗어나면 텍스트 숨김
        private void OnMouseExit()
        {
            current.material = current.material = finish;
            commitMessage.SetMessage("", transform.position);
        }

        // 클릭 시 패널 표시
        private void OnMouseDown()
        {
            commitMessage.SetMessage("", transform.position);
            commitPanel.ShowPanel(Input.mousePosition, message, author, date);
        }
    }
}