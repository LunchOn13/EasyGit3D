using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 커밋 모델링
    /// </summary>
    public class CommitModel : MonoBehaviour
    {
        [SerializeField] Material baseMaterial;
        [SerializeField] Material highlightMaterial;

        private MeshRenderer current;

        private string message;
        private string author;
        private string date;

        // 커밋 관련 오브젝트
        private CommitPanel commitPanel;

        private void Awake()
        {
            current = GetComponent<MeshRenderer>();
            commitPanel = GameObject.Find("Commit Panel").GetComponent<CommitPanel>();
        }

        // 커밋 정보 적용
        public void SetInformation(string _title, string _author, string _date)
        {
            message = _title;
            author = _author;
            date = _date;
        }

        private void OnMouseEnter()
        {
            current.material = highlightMaterial;
            Message.Instance.SetMessage(message, Input.mousePosition);
        }

        private void OnMouseExit()
        {
            current.material = baseMaterial;
            Message.Instance.SetMessage("", transform.position);
        }

        // 클릭 시 패널 표시
        private void OnMouseDown()
        {
            Message.Instance.SetMessage("", transform.position);
            commitPanel.ShowPanel(Input.mousePosition, message, author, date);
        }
    }
}