using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 마우스 오버 시 커밋 메시지 표시
    /// </summary>
    public class Message : MonoBehaviour
    {
        private static Message instance;
        public static Message Instance
        {
            get
            {
                if (null == instance)
                    return null;

                return instance;
            }
        }

        private Text message;

        [SerializeField] float distance;

        private void Awake()
        {
            if(null == instance)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            message = GetComponent<Text>();
            message.text = "";
        }

        public void SetMessage(string text, Vector3 position)
        {
            // 텍스트 크기 설정
            message.text = text;
            message.rectTransform.sizeDelta = new Vector2(message.preferredWidth, message.preferredHeight);

            float width = message.rectTransform.sizeDelta.x;

            // 텍스트 위치 설정
            transform.position = position;
            if (position.x + distance + width > Screen.width)
                transform.Translate(-distance - width / 2, 0, 0);
            else
                transform.Translate(distance + width / 2, 0, 0);
        }
    }
}