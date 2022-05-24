using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 브랜치 및 커밋 전체 관리 및 모델링
    /// </summary>
    public class RepositoryModel : MonoBehaviour
    {
        [SerializeField] GameObject line;
        [SerializeField] GameObject branch;

        // 마스터 브랜치 트랜스폼
        [SerializeField] Transform master;

        // 현재 브랜치와 매핑되는 카메라 위치
        private ConcurrentDictionary<string, Transform> cameras;
        
        // 체크아웃 및 현재 포커스 브랜치
        public static string checkout;
        public static string focus;

        // 브랜치 각도 간격
        private float angle;

        // 라인 길이
        private float distance;

        // 브랜치들의 중심이 되는 메인
        private BranchData main;

        private List<BranchData> develop;

        [SerializeField] GameObject checkoutButton;
        [SerializeField] Text checkoutText;
        [SerializeField] GameObject stagePanel;

        private Dictionary<string, commit> commitDictionary;
        private RepositoryData repositoryData;
        private List<GameObject> allModelList;

        // 스테이터스 데이터
        private List<Status> statusList;
 
        private void Start()
        {
            develop = new List<BranchData>();
            cameras = new ConcurrentDictionary<string, Transform>();
            repositoryData = new RepositoryData();
            allModelList = new List<GameObject>();
        }

        public void CheckoutBranch()
        {
            // 변경사항 있으면 체크아웃 불가
            if (statusList.Count > 0) return;

            StartCoroutine(Checkout());
        }

        public IEnumerator Checkout()
        {
            GitFunction.Checkout(focus);
            while (!CMDworker.engine.output.IsReadable())
                yield return null;
            RefreshViewModels();
        }

        public void RefreshViewModels()
        {
            ClearAllModels();
            GetStatusData();
            GetRepositoryData();
        }

        public void ClearAllModels()
        {
            for (int i = 0; i < allModelList.Count; i++)
                Destroy(allModelList[i]);
            allModelList.Clear();
        }

        // 레포지토리 데이터 불러옴
        public void GetRepositoryData()
        {
            CMDworker.startParseLog();
            commitDictionary = CMDworker.pa.branches;

            repositoryData.branches = new List<BranchData>();

            string branchName = "";
            bool meetMain = false;
            BranchData currentBranch = null;

            foreach (string key in commitDictionary.Keys)
            {
                // REF 이름이 존재
                if (commitDictionary[key].theRef[0] != "")
                {
                    bool checkout = false;
                    string lastBranchName = branchName;

                    // 브랜치 이름 및 체크아웃 확인
                    foreach (string current in commitDictionary[key].theRef)
                    {
                        if (current.Contains("origin/") && !current.Contains("HEAD"))
                        {
                            branchName = current.Trim();
                            branchName = branchName.Remove(0, 7);
                        }

                        if (!current.Contains("origin/") && !current.Contains("HEAD"))
                            branchName = current.Trim();

                        if (current.Contains("HEAD ->"))
                        {
                            branchName = current.Trim().Remove(0, 8);
                            checkout = true;
                        }
                    }

                    if (checkout) repositoryData.checkout = branchName;

                    if (branchName != lastBranchName)
                    {
                        if (currentBranch != null)
                            repositoryData.branches.Add(currentBranch);

                        currentBranch = new BranchData(branchName);

                        if (meetMain && checkout)
                        {
                            repositoryData.branches.Add(currentBranch);
                            break;
                        }

                        // MAIN이 마지막이라고 가정해야 할 수 있음..
                        if (branchName == "main")
                        {
                            meetMain = true;
                            if (repositoryData.checkout != null)
                            {
                                repositoryData.branches.Add(currentBranch);
                                break;
                            }
                        }
                    }
                }

                currentBranch.commits.Add(new CommitData(commitDictionary[key].message, commitDictionary[key].author, commitDictionary[key].time.ToString()));
            }

            BuildRepository();
        }

        // 변경사항 정보 불러옴
        public void GetStatusData()
        {
            CMDworker.startParseStatus();
            statusList = CMDworker.pa.StatusList;
        }

        // 레포지토리 모델링
        private void BuildRepository()
        {
            // 현재 체크아웃 브랜치
            checkout = repositoryData.checkout;
            checkoutText.text = checkout;

            develop.Clear();

            // 브랜치 분류
            foreach (BranchData branch in repositoryData.branches)
            {
                // 메인 브랜치
                if (branch.title == "main")
                    main = branch;
                // 기타 브랜치
                else
                    develop.Add(branch);
            }

            angle = 180 / (develop.Count + 1);
            distance = line.GetComponent<Line>().line.localScale.y * 2;

            MadeBranch(main).transform.position = master.position;
            LocateAllBranch();

            Camera.main.transform.position = cameras[checkout].position;
            Camera.main.transform.rotation = cameras[checkout].rotation;
        }

        // 브랜치 개별 모델링
        public GameObject MadeBranch(BranchData data)
        {
            GameObject newObject = Instantiate(branch);
            BranchModel newBranch = newObject.GetComponent<BranchModel>();

            // 브랜치 정보 적용
            newBranch.ApplyTitle(data.title);

            for (int i = 0; i < data.commits.Count; i++)
                newBranch.MakeCommit(data.commits[i]);

            // 현재 체크아웃 브랜치면 스테이터스 모델도 생성
            if(data.title == checkout)
            {
                for (int i = 0; i < statusList.Count; i++)
                    newBranch.MakeStatusModel(statusList[i]);
            }

            newBranch.LoadCheckout(checkoutButton);
            newBranch.LoadStage(stagePanel);
            newBranch.SaveOriginalMaterial();
            newBranch.SetRepository(this);

            // 드래그 적용
            newObject.GetComponent<DragBranch>().Initialize();

            // 카메라 위치 적용
            cameras[data.title] = newBranch.cameraTransform;

            allModelList.Add(newObject);
            allModelList.Add(newObject.GetComponent<DragBranch>().GetDragObject());

            return newObject;
        }
        
        // 브랜치 배치 설정
        public void LocateAllBranch()
        {
            for(int i = 1; i <= develop.Count; i++)
            {
                // 라인 생성
                GameObject newObject = Instantiate(line);

                newObject.transform.position = master.position;
                newObject.transform.Rotate(0f, -angle * i, 0f);

                allModelList.Add(newObject);

                // 브랜치 생성
                GameObject newBranch = MadeBranch(develop[i - 1]);

                float positionX = master.position.x + distance * Mathf.Cos(angle * i * Mathf.Deg2Rad);
                float positionY = master.position.y;
                float positionZ = master.position.z + distance * Mathf.Sin(angle * i * Mathf.Deg2Rad);

                newBranch.transform.position = new Vector3(positionX, positionY, positionZ);
            }
        }
    }
}