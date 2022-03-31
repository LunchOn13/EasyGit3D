using System.Collections;
using System;
using System.Text;
using UnityEngine;

public class testEngine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IslandEngine tmp;
        tmp = new IslandEngine();

        tmp.StartEngine();

        tmp.WriteInput("git branch --all");

        StringBuilder ttt = tmp.ReadOutput();

        while (ttt == null)
        {
            ttt = tmp.ReadOutput();
        }
        UnityEngine.Debug.Log("Done");
        UnityEngine.Debug.Log(ttt.ToString());

    }

    // Update is called once per frame
    void Update()
    {

    }
}
