using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 스크롤 리스트의 콘텐츠를 체크하고 결과 반영
    /// </summary>
    public class ContentCheck : MonoBehaviour
    {
        [SerializeField] Transform content;

        // 콘텐츠 있을시 보여주는 오브젝트
        [SerializeField] GameObject show;

        protected void Start()
        {
            show.SetActive(false);
        }

        protected void Check()
        {
            // 콘텐츠 있을시 보여주기
            if (content.childCount > 0)
                show.SetActive(true);
            else
                show.SetActive(false);
        }

        protected Transform GetContent()
        {
            return content;
        }
    }
}