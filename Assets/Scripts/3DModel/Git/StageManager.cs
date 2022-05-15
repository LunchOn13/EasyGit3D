using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 스테이지된 파일 관리
    /// </summary>
    public class StageManager : ContentCheck
    {
        [SerializeField] Text stage;
        [SerializeField] GameObject commit;

        private new void Start()
        {
            commit.SetActive(false);
            base.Start();
        }

        // 파일 추가
        public void AddFile(string file)
        {
            // 입력한 파일 생성
            GameObject newFile = Instantiate(stage.gameObject);
            newFile.GetComponent<Text>().text = file;

            newFile.transform.parent = GetContent();
            Check();
        }

        // 파일 제거
        public void DeleteFile(string file)
        {
            // 해당 파일 찾기
            foreach (Text current in GetContent().GetComponentsInChildren<Text>())
            {
                // 제거
                if (current.text == file)
                {
                    Destroy(current.gameObject);
                    return;
                }
            }

            Check();
        }

        // 커밋창 표시
        public void OpenCommitPanel()
        {
            commit.SetActive(true);
        }
    }
}