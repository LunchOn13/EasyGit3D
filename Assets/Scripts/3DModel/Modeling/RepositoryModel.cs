﻿using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

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

        // 체크아웃 버튼
        [SerializeField] GameObject checkoutButton;

        // 스테이지 패널
        [SerializeField] GameObject stagePanel;

        // 레포지토리 데이터
        private Dictionary<string, commit> commitDictionary;
        private RepositoryData repositoryData;

        private void Start()
        {
            develop = new List<BranchData>();
            cameras = new ConcurrentDictionary<string, Transform>();
            repositoryData = new RepositoryData();
        }

        // 레포지토리 데이터 불러옴
        public void GetRepositoryData()
        {
            CMDworker.startParseLog();
            commitDictionary = CMDworker.pa.branches;

            repositoryData.branches = new List<BranchData>();

            string branchName = "";
            string lastParent = "";
            BranchData currentBranch = null;

            foreach (string key in commitDictionary.Keys)
            {
                // MAIN이 마지막이라고 가정해야 할 수 있음..
                if(branchName == "main")
                {
                    if (key == lastParent)
                    {
                        repositoryData.branches.Add(currentBranch);
                        break;
                    }
                }

                string currentParent = commitDictionary[key].parentHash[0];

                // REF 이름이 존재
                if (commitDictionary[key].theRef[0] != "")
                {
                    // MERGE 이후니 패스
                    if (branchName == "main" && commitDictionary[key].parentHash.Length > 1)
                        continue;

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
                            checkout = true;
                    }

                    if (checkout) repositoryData.checkout = branchName;

                    currentParent = commitDictionary[key].parentHash[0];

                    if (branchName != lastBranchName)
                    { 
                        if(currentBranch != null)
                            repositoryData.branches.Add(currentBranch);

                        currentBranch = new BranchData(branchName);
                        lastParent = currentParent;
                    }
                }

                currentBranch.commits.Add(new CommitData(commitDictionary[key].message, commitDictionary[key].author, commitDictionary[key].time.ToString()));
            }

            BuildRepository();
        }

        // 레포지토리 모델링
        private void BuildRepository()
        {
            // 현재 체크아웃 브랜치
            checkout = repositoryData.checkout;

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

            // 카메라 현재 체크아웃 브랜치에 고정
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

            // 체크아웃 버튼 참조
            newBranch.LoadCheckout(checkoutButton);

            // 스테이지 패널 참조
            newBranch.LoadStage(stagePanel);

            // 드래그 적용
            newObject.GetComponent<DragBranch>().Initialize();

            // 카메라 위치 적용
            cameras[data.title] = newBranch.cameraTransform;

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