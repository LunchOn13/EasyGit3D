using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 커밋 정보를 표시하는 패널
/// </summary>
public class CommitPanel : MonoBehaviour
{
    [SerializeField] Text message;
    [SerializeField] Text author;
    [SerializeField] Text date;

    // 애니메이션 시간 및 간격
    [SerializeField] float duration;

    private RectTransform rect;

    private Vector2 raise;
    private float disX, disY;

    // 표시 크기 및 좌표
    private Vector2 size;
    private Vector3 point;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        size = rect.sizeDelta;
        point = transform.position;

        HidePanel();
    }

    // 패널 숨김
    public void HidePanel()
    {
        rect.sizeDelta = Vector2.zero;
    }

    // 커밋 정보 표시
    public void ShowPanel(Vector3 start, string _message, string _author, string _date)
    {
        StopAllCoroutines();

        // 초기화
        rect.sizeDelta = Vector2.zero;
        transform.position = start;

        // 커밋 정보 적용
        message.text = _message;
        author.text = _author;
        date.text = _date;

        raise.x = size.x / duration * Time.deltaTime;
        raise.y = size.y / duration * Time.deltaTime;

        disX = (point.x - start.x) / duration * Time.deltaTime;
        disY = (point.y - start.y) / duration * Time.deltaTime;

        StartCoroutine(Show());
    }

    // 표시 애니메이션
    IEnumerator Show()
    {
        while(rect.sizeDelta.x < size.x)
        {
            transform.Translate(disX, disY, 0);
            rect.sizeDelta += raise;

            yield return null;
        }

        transform.position = point;
        rect.sizeDelta = size;

        yield return null;
    }
}