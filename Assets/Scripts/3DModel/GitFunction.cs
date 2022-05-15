using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성
    /// 기능 : 깃 프로세스와 UI/UX 조작 연결
    /// </summary>
    public class GitFunction : MonoBehaviour
    {
        // 체크아웃 관련 UI
        [SerializeField] GameObject checkoutButton;
        [SerializeField] Text checkoutText;

        private void Start()
        {
            checkoutButton.SetActive(false);
            checkoutText.text = RepositoryModel.checkout;
        }

        // git checkout 실행
        public void Checkout()
        {
            InputManager.OutputControl("git checkout " + RepositoryModel.focus + "\n");
            CMDworker.input("git checkout " + RepositoryModel.focus);
        }
    }
}