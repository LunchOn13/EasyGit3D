using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 카메라 움직임을 담당
    /// </summary>
    public class CameraMove : MonoBehaviour
    {
        private Camera mainCamera;
        private Transform main;
        
        private static Transform target = null;

        // 마우스 휠 정도
        private float scroll;

        // 마우스 휠 속도
        [SerializeField] float speed;

        // 마우스 휠 한계
        [SerializeField] float min;
        [SerializeField] float max;

        private void Start()
        {
            mainCamera = Camera.main;
            main = mainCamera.transform;

            //main.transform.position = start.position;
            //main.transform.rotation = start.rotation;
        }

        private void Update()
        {
            // 마우스 휠로 필드 뷰 조정
            scroll = -Input.GetAxis("Mouse ScrollWheel") * speed;

            if (mainCamera.fieldOfView <= min && scroll < 0)
                mainCamera.fieldOfView = min;
            else if (mainCamera.fieldOfView >= max && scroll > 0)
                mainCamera.fieldOfView = max;
            else
                mainCamera.fieldOfView += scroll;

            if (target == null)
                return;

            main.transform.position = Vector3.Lerp(main.transform.position, target.position, Time.deltaTime);
            main.transform.rotation = Quaternion.Lerp(main.transform.rotation, target.rotation, Time.deltaTime);
        }

        public static void SetTarget(Transform targetTransform)
        {
            target = targetTransform;
        }
    }
}