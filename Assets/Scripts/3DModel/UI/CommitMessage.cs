using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 마우스 오버 시 커밋 메시지 표시
/// </summary>
public class CommitMessage : MonoBehaviour
{
    [SerializeField] Text message;
    [SerializeField] float distance;

    private void Start()
    {
        message.text = "";
    }

    // 커밋 메시지에 따라 텍스트 크기 및 위치 설정
    public void SetMessage(string text, Vector3 position)
    {
        // 텍스트 크기 설정
        message.text = text;
        message.rectTransform.sizeDelta.Set(message.preferredWidth, message.preferredHeight);

        float width = message.rectTransform.sizeDelta.x;

        // 텍스트 위치 설정
        transform.position = position;
        if (position.x + distance + width > Screen.width)
            transform.Translate(-distance - width / 2, 0, 0);
        else
            transform.Translate(distance + width / 2, 0, 0);
    }
}
