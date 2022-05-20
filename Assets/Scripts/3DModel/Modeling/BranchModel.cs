using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 브랜치 모델링
    /// </summary>
    public class BranchModel : MonoBehaviour
    {
        [SerializeField] GameObject commitModel;
        [SerializeField] GameObject statusModel;

        // 변경 사항 모델의 시작 트랜스폼
        [SerializeField] Transform addedTransform;
        [SerializeField] Transform modifiedTransform;
        [SerializeField] Transform deletedTransform;
        [SerializeField] Transform untrackedTransform;

        [SerializeField] Transform start;

        public Transform cameraTransform;

        // 변경사항 종류별 개수
        private int addedCount;
        private int modifiedCount;
        private int deletedCount;
        private int untrackedCount;

        // 커밋 개수
        private int count;

        // Y축 간격
        [SerializeField] float distance;

        // 브랜치 이름
        [SerializeField] TextMeshPro title;

        // 체크아웃 버튼
        private GameObject checkout;

        // 스테이지 패널
        private GameObject stage;

        private void Start()
        {
            addedCount = 0;
            modifiedCount = 0;
            deletedCount = 0;
            untrackedCount = 0;
            
            count = 0;
        }

        // 커밋 오브젝트 생성
        public void MakeCommit(CommitData data)
        {
            GameObject newObject = Instantiate(commitModel);
            CommitModel newCommit = newObject.GetComponent<CommitModel>();
            newObject.transform.parent = transform;

            // 모델 좌표 설정 및 정보 적용
            newObject.transform.position = start.position;
            newObject.transform.Translate(0f, count * distance, 0f);
            newCommit.SetInformation(data.message, data.author, data.date);
            count++;
        }

        // 변경사항 오브젝트 생성
        public void MakeStatusModel(Status status)
        {
            StatusModel newStatus = null;

            switch (status.GetYstatus())
            {
                // Added
                case 'A':
                    newStatus = BuildedStatusModel(addedTransform, ref addedCount, StatusCategory.Added);
                    break;

                // Modified
                case 'M':
                    newStatus = BuildedStatusModel(modifiedTransform, ref modifiedCount, StatusCategory.Modified);
                    break;

                // Deleted
                case 'D':
                    newStatus = BuildedStatusModel(deletedTransform, ref deletedCount, StatusCategory.Deleted);
                    break;

                // Untracked
                case '?':
                    newStatus = BuildedStatusModel(untrackedTransform, ref untrackedCount, StatusCategory.Untracked);
                    break;

                default:
                    break;
            }

            newStatus.SetInformation(status.GetPath());
        }

        private StatusModel BuildedStatusModel(Transform start, ref int count, StatusCategory type)
        {
            GameObject newObject = Instantiate(statusModel);
            newObject.transform.position = start.position;
            newObject.transform.Translate(0f, count * distance, 0f);
            newObject.transform.parent = transform;

            StatusModel newStatus = newObject.GetComponent<StatusModel>();
            newStatus.Initialize();
            newStatus.ApplyMaterial(type);
            newStatus.SetBelongBranch(this);

            count++;

            return newStatus;
        }

        // 체크아웃 버튼 참조
        public void LoadCheckout(GameObject button)
        {
            checkout = button;
        }

        // 스테이지 패널 참조
        public void LoadStage(GameObject panel)
        {
            stage = panel;
        }

        // 브랜치 이름 적용
        public void ApplyTitle(string _title)
        {
            title.text = _title;
        }

        private void OnMouseDown()
        {
            CameraMove.SetTarget(cameraTransform);
            RepositoryModel.focus = title.text;

            // 작업 브랜치인지에 따라 체크아웃 버튼, 스테이지 관리창 설정
            checkout.SetActive(title.text != RepositoryModel.checkout);
            stage.SetActive(title.text == RepositoryModel.checkout);
        }
    }
}