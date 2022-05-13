using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBranch : MonoBehaviour
{
    [SerializeField] Material highlight;

    private GameObject dragObject;

    private float startY;
    private Vector3 point;

    // 드래그용 게임 오브젝트 설정
    public void Initialize()
    {
        dragObject = Instantiate(gameObject);

        // 드래그되는 오브젝트의 스크립트 비활성화
        dragObject.GetComponent<DragBranch>().enabled = false;

        // 하이라이트 적용
        foreach (MeshRenderer renderer in dragObject.GetComponentsInChildren<MeshRenderer>())
            renderer.material = highlight;

        // 위상 적용
        startY = GameObject.Find("Master").GetComponent<Transform>().position.y;

        // 숨기기
        dragObject.SetActive(false);
    }

    // 브랜치와 같은 지평선의 좌표
    private Vector3 RatioApplied(Vector3 point)
    {
        float ratio = (Camera.main.transform.position.y - startY) / (Camera.main.transform.position.y - point.y);

        float resultX = (point.x - Camera.main.transform.position.x) * ratio + Camera.main.transform.position.x;
        float resultY = startY;
        float resultZ = (point.z - Camera.main.transform.position.z) * ratio + Camera.main.transform.position.z;

        return new Vector3(resultX, resultY, resultZ);
    }

    private void OnMouseDown()
    {
        // 보이게 하기
        dragObject.SetActive(true);
    }

    private void OnMouseDrag()
    {
        // 마우스 위치 계산
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        dragObject.transform.position = RatioApplied(point);
    }

    private void OnMouseUp()
    {
        // 숨기기
        dragObject.SetActive(false);
    }
}