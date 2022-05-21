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

        public void CommitAndPushFiles()
        {
            StartCoroutine(CommitAndPush());
        }

        private IEnumerator CommitAndPush()
        {
            GitFunction.Commit(commit.text);
            yield return new WaitUntil(() => CMDworker.engine.output.IsReadable() == true);

            Debug.Log("PUSH");

            GitFunction.Push();
            yield return null;
        }
    }
}