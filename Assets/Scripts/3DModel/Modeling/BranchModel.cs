﻿using System.Collections;
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
        // 커밋 오브젝트 프리팹
        [SerializeField] GameObject commit;

        // 갓 커밋의 시작 트랜스폼
        [SerializeField] Transform addTransform;
        [SerializeField] Transform modifyTransform;
        [SerializeField] Transform deleteTransform;
        [SerializeField] Transform finishTransform;

        // 커밋 종류별 개수
        private int addCount;
        private int modifyCount;
        private int deleteCount;
        private int finishCount;

        // Y축 간격
        [SerializeField] float distance;

        // 브랜치 이름
        [SerializeField] TextMeshPro title;

        // [임시] 더미 데이터
        [SerializeField] CommitData[] dummy;

        private void Start()
        {
            addCount = 0;
            modifyCount = 0;
            deleteCount = 0;
            finishCount = 0;
        }

        // 커밋 오브젝트 생성
        public void MakeCommit(CommitCategory category)
        {
            switch (category)
            {
                case CommitCategory.Add:
                    BuildCommit(addTransform, ref addCount, CommitCategory.Add);
                    break;

                case CommitCategory.Modify:
                    BuildCommit(modifyTransform, ref modifyCount, CommitCategory.Modify);
                    break;

                case CommitCategory.Delete:
                    BuildCommit(deleteTransform, ref deleteCount, CommitCategory.Delete);
                    break;

                case CommitCategory.Finish:
                    BuildCommit(finishTransform, ref finishCount, CommitCategory.Finish);
                    break;

                default:
                    break;
            }
        }

        // 종류에 따른 커밋 오브젝트 생성
        private void BuildCommit(Transform start, ref int count, CommitCategory type)
        {
            GameObject newObject = Instantiate(commit);
            CommitModel newCommit = newObject.GetComponent<CommitModel>();

            newObject.transform.parent = transform;

            newObject.transform.position = start.position;
            newObject.transform.Translate(0f, count * distance, 0f);
            newCommit.ApplyMaterial(type);
            count++;
        }

        // 브랜치 이름 적용
        public void ApplyTitle(string _title)
        {
            title.text = _title;
        }
    }
}