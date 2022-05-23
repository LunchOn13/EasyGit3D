using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성,
    /// 기능 : GIT ADD 관련 패널
    /// </summary>
    public class AddPanel : MonoBehaviour
    {
        private string path;

        // 애니메이션 관련 변수
        private Vector2 raise;
        private float disX, disY;
        private Vector2 size;
        private Vector3 point;

        private RectTransform rect;
        private ConcurrentDictionary<string, StatusModel> statusModelDictionary;

        [SerializeField] Text pathText;
        [SerializeField] float duration;
        [SerializeField] Button addButton;
        [SerializeField] Button addAllButton;

        // 스테이지 추가 파일 형식
        [SerializeField] GameObject statusFile;
        [SerializeField] GameObject stageList;

        private void Start()
        {
            statusModelDictionary = new ConcurrentDictionary<string, StatusModel>();

            rect = GetComponent<RectTransform>();
            size = rect.sizeDelta;
            point = transform.position;

            HidePanel();
        }

        public void SetPathText(string _path)
        {
            path = _path;
            pathText.text = _path;
        }

        public void AddStatusModel(StatusModel statusModel)
        {
            statusModelDictionary[statusModel.GetPath()] = statusModel;
        }

        public void HighlightAllStatusModels()
        {
            foreach (StatusModel statusModel in statusModelDictionary.Values)
                statusModel.HighlightModel();
        }

        public void AddStageFile(StatusModel statusModel)
        {
            // 이미 스테이지에 올라가있는지 확인
            if(StageManager.AddStatusModel(statusModel))
            {
                GameObject newStageFile = Instantiate(statusFile);
                newStageFile.transform.parent = stageList.transform;
                newStageFile.GetComponent<StatusFile>().SetPathText(statusModel.GetPath());
            }
        }

        // 스테이지에 파일 추가
        public void AddFile()
        {
            AddStageFile(statusModelDictionary[path]);
            GitFunction.AddFile(path);
            statusModelDictionary[path].HighlightModel();
            HidePanel();
        }

        // 스테이지에 모든 파일 추가
        public void AddAllFiles()
        {
            foreach (StatusModel statusModel in statusModelDictionary.Values)
                AddStageFile(statusModel);
            GitFunction.AddAllFiles();
            HidePanel();
        }

        public void HidePanel()
        {
            StopAllCoroutines();

            addButton.enabled = false;
            addAllButton.enabled = false;

            rect.sizeDelta = Vector2.zero;
        }

        public void ShowPanel(Vector3 start, string _path)
        {
            StopAllCoroutines();

            // 초기화
            rect.sizeDelta = Vector2.zero;
            transform.position = start;

            // 커밋 정보 적용
            pathText.text = _path;
            path = _path;

            raise.x = size.x / duration * Time.deltaTime;
            raise.y = size.y / duration * Time.deltaTime;

            disX = (point.x - start.x) / duration * Time.deltaTime;
            disY = (point.y - start.y) / duration * Time.deltaTime;

            StartCoroutine(ShowAnimation());
        }

        private IEnumerator ShowAnimation()
        {
            while (rect.sizeDelta.x < size.x)
            {
                transform.Translate(disX, disY, 0);
                rect.sizeDelta += raise;

                yield return null;
            }

            transform.position = point;
            rect.sizeDelta = size;

            addButton.enabled = true;
            addAllButton.enabled = true;

            yield return null;
        }
    }
}