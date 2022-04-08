using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CMDworker : MonoBehaviour
{
    public static IslandEngine engine = new IslandEngine();

    // Start is called before the first frame update
    void Start()
    {
        engine.StartEngine();

        //engine.ReadOutput();
    }

    /// <summary>
    /// 많은 개선이 필요할 것으로 보임
    /// </summary>
    /// <param name="s"></param>
    public static void input(string s)
    {
        engine.WriteInput(s);
    }

    public static void output()
    {
        if(engine.output.IsReadable())
        {
            UnityEngine.Debug.Log("lineofoutput " + engine.output.LineofOutput());
            UnityEngine.Debug.Log("output is " + engine.output.OutputLines());
        }


        if(engine.error.IsReadable())
        {
            UnityEngine.Debug.Log("lineofError " + engine.error.LineofError());
            UnityEngine.Debug.Log("error is " + engine.error.ErrorLines());
        }

    }

    public void error(string errorLine)
    {
        UnityEngine.Debug.Log(errorLine);
        InputManager.OutputControl(errorLine);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
