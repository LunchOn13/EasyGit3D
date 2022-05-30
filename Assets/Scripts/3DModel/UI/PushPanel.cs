using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성,
    /// 기능 : 커밋 메시지 작성 및 푸시 패널
    /// </summary>
    public class PushPanel : MonoBehaviour
    {
        [SerializeField] Text commit;
        [SerializeField] RepositoryModel repository;

        public void TutorialCommitAndPushFiles()
        {
            GitFunction.Commit(commit.text);
            while (!CMDworker.engine.output.IsReadable())
            { }

            GitFunction.TutorialPush();
            while (!CMDworker.engine.output.IsReadable())
            { }

            repository.RefreshViewModels();
            gameObject.SetActive(false);
        }

        public void CommitAndPushFiles()
        {
            GitFunction.Commit(commit.text);
            while (!CMDworker.engine.output.IsReadable())
            { }

            GitFunction.Push();
            while (!CMDworker.engine.output.IsReadable())
            { }

            repository.RefreshViewModels();
            gameObject.SetActive(false);
        }
    }
}