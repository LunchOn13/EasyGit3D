using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성,
    /// 기능 : 튜토리얼 진행 관리
    /// </summary>
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] InputField input;

        [SerializeField] GameObject remote;
        [SerializeField] GameObject status1;
        [SerializeField] GameObject status3;
        [SerializeField] GameObject push;
        [SerializeField] GameObject pushPanel;
        [SerializeField] GameObject status4;
        [SerializeField] GameObject branch2;
        [SerializeField] GameObject status5;

        [SerializeField] RepositoryModel repositoryModel;

        private string mission;

        public void SetCheckString(string check)
        {
            mission = check;
        }

        public void CommandEnd()
        {
            if (mission == "git remote add origin")
            {
                if (input.text.Contains("git remote add origin"))
                    DoNextTutorial();
                return;
            }

            if (input.text == mission)
                DoNextTutorial();
        }

        public void CheckCurrentTutorial(string current)
        {
            if (current == mission)
                DoNextTutorial();
        }

        public void WaitPush()
        {
            if (mission != "git push") return;
            DoNextTutorial();
        }

        private void DoNextTutorial()
        {
            switch (mission)
            {
                case "git init":
                    PathManager.openRepositoryPossible = true;
                    remote.SetActive(true);
                    break;

                case "git remote add origin":
                    GitFunction.TutorialCommit();
                    repositoryModel.RefreshViewModels();
                    status1.SetActive(true);
                    break;

                case "git add":
                    status3.SetActive(true);
                    break;

                case "git commit":
                    push.SetActive(true);
                    break;

                case "git push":
                    status4.SetActive(true);
                    break;

                case "git branch1":
                    branch2.SetActive(true);
                    break;

                case "git branch2":
                    status5.SetActive(true);
                    break;

                default:
                    break;
            }
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}