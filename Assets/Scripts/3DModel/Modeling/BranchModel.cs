using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

        [SerializeField] Material mergeStart;
        [SerializeField] Material mergeEnd;

        private ConcurrentDictionary<MeshRenderer, Material> originalMaterial;

        private bool isDragObject = false;
        private bool mergePossible = false;
        private string mergeStartBranch, mergeEndBranch;

        // Y축 간격
        [SerializeField] float distance;
        [SerializeField] TextMeshPro title;

        private GameObject checkout;
        private GameObject stage;
        private RepositoryModel repository;

        private void Start()
        {
            addedCount = 0;
            modifiedCount = 0;
            deletedCount = 0;
            untrackedCount = 0;
            
            count = 0;
        }

        public void SetRepository(RepositoryModel _repository)
        {
            repository = _repository;
        }

        public void SaveOriginalMaterial()
        {
            if (isDragObject) return;

            originalMaterial = new ConcurrentDictionary<MeshRenderer, Material>();

            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
                originalMaterial[renderer] = renderer.material;
        }

        public void LoadOriginalMaterial()
        {
            if (isDragObject) return;

            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
                renderer.material = originalMaterial[renderer];
        }

        public void ApplyMergeStartMaterial()
        {
            if (isDragObject) return;

            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
            {
                if(renderer.gameObject.GetComponent<TextMeshPro>() == null)
                    renderer.material = mergeStart;
            }
        }

        public void ApplyMergeEndMaterial()
        {
            if (isDragObject) return;

            foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
            {
                if (renderer.gameObject.GetComponent<TextMeshPro>() == null)
                    renderer.material = mergeEnd;
            }
        }

        public void SetIsDragObject(bool flag)
        {
            isDragObject = flag;
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
            StatusModel newStatus = ClassifiedStatusModel(status.GetYstatus());

            if (newStatus != null)
                newStatus.SetInformation(status.GetPath());
            else
            {
                // 현재 스테이지된 파일
                newStatus = ClassifiedStatusModel(status.GetXstatus());
                newStatus.SetInformation(status.GetPath());
                newStatus.StageStatusModel();
                newStatus.HighlightModel();
            }
        }

        private StatusModel ClassifiedStatusModel(char status)
        {
            switch (status)
            {
                // Added
                case 'A':
                    return BuildedStatusModel(addedTransform, ref addedCount, StatusCategory.Added);

                // Modified
                case 'M':
                    return BuildedStatusModel(modifiedTransform, ref modifiedCount, StatusCategory.Modified);

                // Deleted
                case 'D':
                    return BuildedStatusModel(deletedTransform, ref deletedCount, StatusCategory.Deleted);
                    
                // Untracked
                case '?':
                    return BuildedStatusModel(untrackedTransform, ref untrackedCount, StatusCategory.Untracked);
            }

            return null;
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

        public string GetTitle()
        {
            return title.text;
        }

        public bool TryMerge()
        {
            if (!mergePossible) return false;
            
            StartCoroutine(MergeBranch());
            return true;
        }

        public IEnumerator MergeBranch()
        {
            if(mergeEndBranch != RepositoryModel.checkout)
            {
                GitFunction.Checkout(mergeEndBranch);
                while (!CMDworker.engine.output.IsReadable())
                    yield return null;
            }

            GitFunction.Merge(mergeStartBranch);
            while (!CMDworker.engine.output.IsReadable())
                yield return null;

            repository.RefreshViewModels();
            gameObject.SetActive(false);
        }

        private void OnMouseDown()
        {
            CameraMove.SetTarget(cameraTransform);
            RepositoryModel.focus = title.text;

            // 작업 브랜치인지에 따라 체크아웃 버튼, 스테이지 관리창 설정
            checkout.SetActive(title.text != RepositoryModel.checkout);
            stage.SetActive(title.text == RepositoryModel.checkout);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Branch")
            {
                BranchModel targetBranch = other.GetComponent<BranchModel>();
                if (targetBranch.GetTitle() != GetTitle())
                {
                    mergePossible = true;
                    targetBranch.ApplyMergeEndMaterial();

                    mergeStartBranch = GetTitle();
                    mergeEndBranch = targetBranch.GetTitle();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Branch")
            {
                BranchModel targetBranch = other.GetComponent<BranchModel>();
                if (targetBranch.GetTitle() != GetTitle())
                {
                    mergePossible = false;
                    targetBranch.LoadOriginalMaterial();
                }
            }
        }
    }
}