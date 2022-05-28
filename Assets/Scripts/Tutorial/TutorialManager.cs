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
        [SerializeField] RepositoryModel repositoryModel;


        private string mission;

        public void SetCheckString(string check)
        {
            mission = check;
        }

        public void CommandEnd()
        {
            if (mission == "git remote add")
            {
                if (input.text.Contains("git remote add"))
                    DoNextTutorial();
                return;
            }

            if (input.text == mission)
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

                case "git remote add":
                    GitFunction.TutorialCommit();
                    repositoryModel.RefreshViewModels();
                    break;

                default:
                    break;
            }
        }
    }
}