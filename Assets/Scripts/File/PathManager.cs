using Ookii.Dialogs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 파일 경로 탐색 및 저장과 불러오기를 가능하게 함
/// </summary>
public class PathManager : MonoBehaviour
{
    VistaFolderBrowserDialog OpenDialog;

    // 파일 정보 저장 경로
    //private string repositorySavePath;

    public static bool openRepositoryPossible = false;
    public static string repositoryPath = "";

    [SerializeField] GameObject warningText;
    [SerializeField] GameObject startButton;

    private void Start()
    {
        //repositorySavePath = UnityEngine.Application.persistentDataPath + "\\repository.text";
        OpenDialog = new VistaFolderBrowserDialog();
        openRepositoryPossible = false;
    }

    // 경로 정보 불러오기
    private bool LoadPath(string load, ref string path)
    {
        if (File.Exists(load))
        {
            path = File.ReadAllText(load);
            return true;
        }
        return false;
    }

    // 경로 정보 저장하기
    private void SavePath(string save, string path)
    {
        File.WriteAllText(save, path);
    }

    public void FileSearch(ref string path)
    {
        if (openRepositoryPossible) return;

        if (OpenDialog.ShowDialog() == DialogResult.OK)
        {
            if (OpenDialog.SelectedPath != null)
            {
                path = OpenDialog.SelectedPath;
                if (!Directory.Exists(path + "\\.git"))
                    warningText.SetActive(true);
                else
                {
                    openRepositoryPossible = true;
                    warningText.SetActive(false);
                    //SavePath(save, path);
                }
            }
        }
    }

    // 튜토리얼용 폴더 지정
    public void SetGitDirectory()
    {
        if (OpenDialog.ShowDialog() == DialogResult.OK)
        {
            if (OpenDialog.SelectedPath != null)
                repositoryPath = OpenDialog.SelectedPath;
        }

        CMDworker.engine.StartEngine();
    }

    // 깃 경로 탐색
    public void RepositorySearch()
    {
        FileSearch(ref repositoryPath);
    }

    public void SetOpenRepositoryPossible(bool flag)
    {
        openRepositoryPossible = flag;
    }

    public void CheckRepositoryLoading()
    {
        if (!openRepositoryPossible) return;
        startButton.SetActive(false);
    }
}