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
        // 커밋 오브젝트 프리팹
        [SerializeField] GameObject commit;

        // 갓 커밋의 시작 트랜스폼
        [SerializeField] Transform addTransform;
        [SerializeField] Transform modifyTransform;
        [SerializeField] Transform deleteTransform;

        [SerializeField] Transform start;

        // 커밋 종류별 개수
        private int addCount;
        private int modifyCount;
        private int deleteCount;

        // 커밋 개수
        private int count;

        // Y축 간격
        [SerializeField] float distance;

        // 브랜치 이름
        [SerializeField] TextMeshPro title;

        private void Start()
        {
            addCount = 0;
            modifyCount = 0;
            deleteCount = 0;
            
            count = 0;
        }

        // 커밋 오브젝트 생성
        public void MakeCommit(CommitData data)
        {
            GameObject newObject = Instantiate(commit);
            CommitModel newCommit = newObject.GetComponent<CommitModel>();
            newObject.transform.parent = transform;

            //switch ((CommitCategory)data.type)
            //{
            //    case CommitCategory.Add:
            //        BuildCommit(addTransform, ref addCount, CommitCategory.Add);
            //        break;

            //    case CommitCategory.Modify:
            //        BuildCommit(modifyTransform, ref modifyCount, CommitCategory.Modify);
            //        break;

            //    case CommitCategory.Delete:
            //        BuildCommit(deleteTransform, ref deleteCount, CommitCategory.Delete);
            //        break;

            //    case CommitCategory.Finish:
            //        BuildCommit(finishTransform, ref finishCount, CommitCategory.Finish);
            //        break;

            //    default:
            //        break;
            //}

            // 모델 좌표 설정 및 정보 적용
            newObject.transform.position = start.position;
            newObject.transform.Translate(0f, count * distance, 0f);
            newCommit.SetInformation(data.message, data.author, data.date);
            count++;
        }

        // 종류에 따른 커밋 오브젝트 생성
        //private void BuildCommit(Transform start, ref int count, CommitCategory type)
        //{
        //    newObject.transform.position = start.position;
        //    newObject.transform.Translate(0f, count * distance, 0f);
        //    newCommit.ApplyMaterial(type);
        //    count++;
        //}

        // 브랜치 이름 적용
        public void ApplyTitle(string _title)
        {
            title.text = _title;
        }
    }
}