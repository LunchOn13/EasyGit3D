using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 마우스 커서에 반응하는 하이라이트 팝업
    /// </summary>
    public class Popup : MonoBehaviour
    {
        [SerializeField] GameObject popUp;
        [SerializeField] TextMeshPro content;

        private void OnMouseEnter()
        {
            Debug.Log("OVER");

            popUp.SetActive(true); 
        }

        private void OnMouseExit()
        {
            popUp.SetActive(false);
        }

        // 내용 설정
        public void SetText(string text)
        {
            content.text = text;
            //popUp.SetActive(false);
        }
    }
}