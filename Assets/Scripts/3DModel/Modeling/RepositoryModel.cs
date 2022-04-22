using System.Collections;
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

        // 브랜치 각도 간격
        private float angle;

        // 라인 길이
        private float distance;

        // 브랜치들의 중심이 되는 메인
        private BranchData main;

        private List<BranchData> develop;

        // [임시] 더미 데이터
        public RepositoryData repositoryDummy;

        private void Start()
        {
            develop = new List<BranchData>();

            // [임시] 불러오기 시험
            repositoryDummy = JsonUtility.FromJson<RepositoryData>((Resources.Load("TestData") as TextAsset).ToString());

            // 브랜치 분류
            foreach (BranchData branch in repositoryDummy.branches)
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
        }

        // 브랜치 개별 모델링
        public GameObject MadeBranch(BranchData data)
        {
            GameObject newObject = Instantiate(branch);
            BranchModel newBranch = newObject.GetComponent<BranchModel>();

            // 브랜치 정보 적용
            newBranch.ApplyTitle(data.title);
            for (int i = 0; i < data.commits.Length; i++)
                newBranch.MakeCommit(data.commits[i]);

            return newObject;
        }
        
        // 브랜치 배치 설정
        public void LocateAllBranch()
        {
            for(int i = 1; i <= develop.Count; i++)
            {
                // 라인 생성
                GameObject newObject = Instantiate(line);
                Line newLine = newObject.GetComponent<Line>();

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