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

        public void Pull()
        {
            InputManager.OutputControl("git pull" + "\n");
            CMDworker.input("git pull");
            while(!CMDworker.engine.output.IsReadable())
            { }
        }

        public static void Checkout(string branch)
        {
            InputManager.OutputControl("git checkout " + branch + "\n");
            CMDworker.input("git checkout " + branch);
        }

        public static void AddFile(string path)
        {
            InputManager.OutputControl("git add " + path + "\n");
            CMDworker.input("git add " + path);
        }

        public static void AddAllFiles()
        {
            InputManager.OutputControl("git add ." + "\n");
            CMDworker.input("git add .");
        }

        public static void RestoreFile(string path)
        {
            InputManager.OutputControl("git restore --staged " + path + "\n");
            CMDworker.input("git restore --staged " + path);
        }

        public static void RestoreAllFiles()
        {
            InputManager.OutputControl("git restore --staged ." + "\n");
            CMDworker.input("git restore --staged .");
        }

        public static void Commit(string message)
        {
            InputManager.OutputControl("git commit -m \"" + message + "\"\n");
            CMDworker.input("git commit -m \"" + message + "\"");
        }

        public static void Push()
        {
            InputManager.OutputControl("git push" + "\n");
            CMDworker.input("git push");
        }

        public static void Merge(string start)
        {
            InputManager.OutputControl("git merge " + start + "\n");
            CMDworker.input("git merge " + start);
        }

        public static void MakeBranch(string newBranch)
        {
            InputManager.OutputControl("git branch " + newBranch + "\n");
            CMDworker.input("git branch " + newBranch);
            while (!CMDworker.engine.output.IsReadable())
            { }

            Checkout(newBranch);
            while (!CMDworker.engine.output.IsReadable())
            { }

            TutorialCommit();
        }

        public static void TutorialCommit()
        {
            InputManager.OutputControl("git commit -m \"First commit\" --allow-empty\n");
            CMDworker.input("git commit -m \"First commit\" --allow-empty");
            while (!CMDworker.engine.output.IsReadable())
            { }
        }

        public static void TutorialPush()
        {
            InputManager.OutputControl("git push -u origin main" + "\n");
            CMDworker.input("git push -u origin main");
        }

        public void MakeSampleFile()
        {
            InputManager.OutputControl("touch sample.txt\n");
            CMDworker.input("touch sample.txt");
            while (!CMDworker.engine.output.IsReadable())
            { }
        }
    }
}