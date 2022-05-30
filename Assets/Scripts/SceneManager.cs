using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성,
/// 기능 : 씬 이동 관리
/// </summary>
public class SceneManager : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}