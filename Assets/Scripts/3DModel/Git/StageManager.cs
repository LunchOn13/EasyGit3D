using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 스테이지된 파일 관리
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        [SerializeField] Transform stageList;
        [SerializeField] GameObject pushPanel;

        private static ConcurrentDictionary<string, StatusModel> stageModelDictionary;

        private void Start()
        {
            stageModelDictionary = new ConcurrentDictionary<string, StatusModel>();
            pushPanel.SetActive(false);
        }

        public void OpenPushPanel()
        {
            // 스테이지 파일이 있으면 표시
            if (stageList.childCount > 0)
                pushPanel.SetActive(true);
        }

        public void ClearStageList()
        {
            stageModelDictionary.Clear();

            // 스테이지 패널 리스트 초기화
            Transform[] childList = stageList.GetComponentsInChildren<Transform>();
            for (int i = 1; i < childList.Length; i++)
                Destroy(childList[i].gameObject);
        }

        public void DeleteAllStageModels()
        {
            if (stageList.childCount == 0)
                return;

            GitFunction.RestoreAllFiles();

            // 스테이지 패널 리스트 초기화
            Transform[] childList = stageList.GetComponentsInChildren<Transform>();
            for (int i = 1; i < childList.Length; i++)
                Destroy(childList[i].gameObject);

            foreach (string path in stageModelDictionary.Keys)
                DeleteStatusModel(path);
        }

        // 해당 파일이 존재하는지 확인
        public static bool CheckStatusModel(string path)
        {
            return stageModelDictionary.ContainsKey(path);
        }

        public static bool AddStatusModel(StatusModel statusModel)
        {
            if (!stageModelDictionary.ContainsKey(statusModel.GetPath()))
            {
                stageModelDictionary[statusModel.GetPath()] = statusModel;
                return true;
            }
            else return false;
        }

        public static void DeleteStatusModel(string path)
        {
            StatusModel deletedStatusModel;
            if (stageModelDictionary.TryRemove(path, out deletedStatusModel))
                deletedStatusModel.ReturnBaseModel();
        }
    }
}