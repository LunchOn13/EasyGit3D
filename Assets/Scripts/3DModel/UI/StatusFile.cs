using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성,
    /// 기능 : 스테이지 패널 내 파일 관리
    /// </summary>
    public class StatusFile : MonoBehaviour
    {
        private string path;

        [SerializeField] Text pathText;

        public void SetPathText(string _path)
        {
            path = _path;
            pathText.text = _path;
        }

        public void RestoreFile()
        {
            GitFunction.RestoreFile(path);
            StageManager.DeleteStatusModel(path);
            Destroy(gameObject);
        }
    }
}