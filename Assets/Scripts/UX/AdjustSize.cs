using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 드래그를 이용한 콘텐츠 크기 조정
/// </summary>
public class AdjustSize : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] GameObject icon;
    [SerializeField] Image shadow;

    [SerializeField] Color baseShadow;
    [SerializeField] Color highlightShadow;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        icon.SetActive(false);
        shadow.color = baseShadow;
    }

    public void Highlight()
    {
        icon.SetActive(true);
        shadow.color = highlightShadow;
    }

    public void Adjust()
    {
        // 콘텐츠 사이즈 조정
        content.sizeDelta = new Vector2(Input.mousePosition.x, content.sizeDelta.y);
    }
}