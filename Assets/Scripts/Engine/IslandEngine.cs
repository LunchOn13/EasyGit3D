using System.Diagnostics;
using System.IO;
using System;
using System.Text;


public class IslandEngine
{
    public Output output = new Output();
    public Error error = new Error();
    public string inputCommand = "";

    private readonly Process bash = new Process();
    private readonly ProcessStartInfo bashInfo = new ProcessStartInfo();

    private StreamWriter writer;

    /// <summary>
    /// 환경변수에서 git 경로를 가져와서 실행시킨다.
    /// </summary>
    /// <returns>
    /// string인 컴퓨터 환경변수상의 git 경로를 가져온다.
    /// </returns>
    public string FindGitPath()
    {
        string a = Environment.GetEnvironmentVariable("path");
        string gitPath = "";

        foreach (string e in a.Split(';'))
        {
            if (e.Contains("Git\\cmd") || e.Contains("git\\cmd"))
            {
                // 여러개가 나오는 경우는 아직 모른다
                string[] contents = e.Split('\\');
                foreach (string k in contents)
                {
                    gitPath += k;
                    gitPath += "\\\\";
                    if (k.ToLower() == "git")
                        break;
                }
                gitPath += "bin\\\\";
                gitPath += "bash.exe";

            }
        }
        return gitPath;
    }
    /// <summary>
    /// 배쉬 위치가 예상한 곳이 아니라면 실행할 깃 배쉬의 위치를 입력
    /// </summary>
    /// <param name="s"></param>
    public void SetFileName(string s)
    {
        bashInfo.FileName = s;
        bash.StartInfo = bashInfo;
    }

    /// <summary>
    /// git bash를 실행시켜서 조작할 directory 경로를 설정
    /// </summary>
    /// <param name="s">경로값</param>
    public void SetWorkingDirectory(string s)
    {
        bashInfo.WorkingDirectory = s;
        bash.StartInfo = bashInfo;
    }

    /// <summary>
    /// 기본적인 시작 설정값
    /// </summary>
    private void SetInfo()
    {
        bashInfo.FileName = FindGitPath();
        bashInfo.UseShellExecute = false;
        bashInfo.CreateNoWindow = true;

        bashInfo.WorkingDirectory = PathManager.repositoryPath;

        bashInfo.RedirectStandardOutput = true;
        bashInfo.RedirectStandardInput = true;
        bashInfo.RedirectStandardError = true;

        bash.StartInfo = bashInfo;

        bash.OutputDataReceived += OutputHandler;
        bash.ErrorDataReceived += ErrorHandler;
        //bash.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);
    }

    /// <summary>
    /// 깃 배쉬를 실행시키고, output과 error를 읽기 시작
    /// </summary>
    public void StartEngine()
    {
        SetInfo();
        bash.Start();
        
        bash.BeginOutputReadLine();

        // 에러 읽기 ( Progress 읽기 )
        bash.BeginErrorReadLine();
        UnityEngine.Debug.Log("start error read line");

        WriteInput("pwd");
        while (!output.IsReadable())
        {

        }
    }


    /// <summary>
    /// 깃 배쉬에서 실행시킬 명령어를 넣고 실행한다
    /// </summary>
    /// <param name="input"> 깃 배쉬에 입력할 명령어를 그대로 넣을 것</param>
    public void WriteInput(string input)
    {
        output.Clear();
        error.Clear();
        writer = bash.StandardInput;
        inputCommand = input;
        writer.WriteLine(input);
        writer.Flush();

        // 항상 종료 시점에 제대로 끝났는지 확인하기 위한 용도
        writer.WriteLine("echo EOF");
        writer.Flush();
    }

    /// <summary>
    /// 깃 배쉬 종료한다
    /// </summary>
    public void StopEngine()
    {
        writer.Close();
        bash.Close();
    }
    /**
     * 
     * 
     *  git push 했을 때 나오는 값들임 파싱할때 필요할거같아서 기록함
 Enumerating objects: 27, done.
Counting objects: 100% (27/27), done.
Delta compression using up to 12 threads
Compressing objects: 100% (17/17), done.
Writing objects: 100% (17/17), 4.00 KiB | 4.00 MiB/s, done.
Total 17 (delta 10), reused 0 (delta 0), pack-reused 0
remote: Resolving deltas: 100% (10/10), completed with 9 local objects.
To github.com:LunchOn13/Swimming_on_git.git
   d874e529..211ce95a  LJW -> LJW
     */

    /// <summary>
    /// 깃 배쉬의 output을 가져와서 무엇을 할 것인가~
    /// </summary>
    /// <param name="sendingProcess"></param>
    /// <param name="outLine"></param>
    private void OutputHandler(object sendingProcess,
            DataReceivedEventArgs outLine)
    {
        // Collect the sort command output.
        if (!String.IsNullOrEmpty(outLine.Data))
        {
            //UnityEngine.Debug.Log(outLine.Data);
            if (outLine.Data == "EOF")
            {
                UnityEngine.Debug.Log("set isReadable !!!");
                output.setIsOutput();
            }
            else
                output.AppendLine(outLine.Data);
        }
    }

    /// <summary>
    /// --progress 옵션을 넣어줘야만 한다.
    /// 에러 핸들러인데
    /// git 에서 진행상황 보여줄때 errorstream에다가 적어서
    /// 표시해준다 그래서 이걸로 봐야함
    /// git clone --progress 깃주소
    /// 이런 형식으로 적어야함
    /// 
    ///  /// https://docs.microsoft.com/ko-kr/dotnet/api/system.diagnostics.process.errordatareceived?view=net-6.0
    /// 참조
    /// </summary>
    /// <param name=""></param>
    private void ErrorHandler(object sendingProcess, DataReceivedEventArgs errLine)
    {
        if (!String.IsNullOrEmpty(errLine.Data))
        {
            error.AppendLine(errLine.Data);
        }
    }

    /// <summary>
    /// 현재 저장소의 파일상 위치를 return한다
    /// </summary>
    public string ShowWorkingDirectory()
    {
        return bashInfo.WorkingDirectory;
    }
}