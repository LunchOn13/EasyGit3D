using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CMDworker : MonoBehaviour
{
    public static IslandEngine engine = new IslandEngine();
    public static parsing pa = new parsing();

    // test를 위해 넣은 임시 변수
    // 삭제하고 error, output class 에 is reading 변수를 넣어야함
    private static bool isReading = false;

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
        //UnityEngine.Debug.Log("output");
        if(engine.output.IsReadable())
        {
            UnityEngine.Debug.Log("lineofoutput " + engine.output.LineofOutput());
            InputManager.OutputControl(engine.output.OutputLines());
        }


        if(engine.error.IsReadable() && isReading == false)
        {
            isReading = true;
            UnityEngine.Debug.Log("lineofError " + engine.error.LineofError());
            //UnityEngine.Debug.Log("error is " + engine.error.ErrorLines());
            InputManager.OutputControl(engine.error.ErrorLines());
            engine.error.ErrorCheck();
            isReading = false;
        }

    }

    public static void startParseLog()
    {
        string specialInput = "git log -20 --topo-order --branches --pretty=\"{%n\\\"CommitHash\\\" : \\\"%h\\\",%n\\\"Author\\\" : \\\"%an\\\",%n\\\"authorDate\\\" : \\\"%ai\\\",%n\\\"Message\\\" : \\\"%s\\\",%n\\\"ref\\\" : \\\"%D\\\"%n,\\\"ParentHash\\\" : \\\"%p\\\"%n},\"";
        //UnityEngine.Debug.Log(specialInput);
        engine.WriteInput(specialInput);

        //UnityEngine.Debug.Log(engine.output.IsReadable());
        while(!engine.output.IsReadable())
        {
            
        }
        //UnityEngine.Debug.Log(engine.output.OutputLines());
        pa.parseLog(engine.output.OutputLines());
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
