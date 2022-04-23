using System;
using System.Text;

public class Error
{

    private StringBuilder error;

    private Boolean isError = false;
    private int lineCount = 0;

    // 실제 에러인 경우
    // TODO::체크해봐야한다.
    private Boolean RealError = false;

    private int wholeTask = 0;
    private int nowTask = 0;
    private int progress = 0;


    public Error()
    {
        this.error = new StringBuilder();
        this.isError = false;
        this.lineCount = 0;
        this.wholeTask = 0;
        this.nowTask = 0;
        this.RealError = false;
    }

    /// <summary>
    /// error가 존재하는지 확인한다.
    /// </summary>
    /// <returns></returns>
    public Boolean IsReadable()
    {
        return this.isError;
    }

    public int LineofError()
    {
        return this.lineCount;
    }

    public int EntireTask()
    {
        return this.wholeTask;
    }

    public int NowTask()
    {
        return this.nowTask;
    }

    public Boolean IsRealError()
    {
        return this.RealError;
    }

    public void ErrorCheck()
    {
        this.isError = false;
    }

    /// <summary>
    /// error를 입력하고 저장하며, 문자열 개수를 증가시키고, 읽을 수 있게 flag 설정한다.
    /// </summary>
    /// <param name="s"></param>
    public void AppendLine(string s)
    {
        // TODO:: 실제 에러인지 체크하는 로직이 필요하다.
        // TODO:: Task 확인하는 것도 필요하다.


        this.isError = true;
        if (s.Contains("fatal"))
        {
            // fatal error
            this.error.Clear();
            this.error.AppendLine(s);
            this.RealError = true;
        }
        else if (s.Contains("Total"))
        {
            // 전체 작업 파싱
            String[] splited = s.Split(' ');
            this.wholeTask = Convert.ToInt32(splited[2]);
        }
        else if (s.Contains("%"))
        {
            String[] splited = s.Split(' ');
            int target = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i].Contains("%"))
                    target = i;
            }

            // percentage 파싱
            string percentage = splited[target];
            percentage = percentage.Remove(percentage.Length - 1);
            this.progress = Convert.ToInt32(percentage);

            // 현재 완료된 작업 파싱
            string now = splited[target + 1];
            now = now.Split('/')[0];
            now = now.Remove(0, 1);
            this.nowTask = Convert.ToInt32(now);
        }

        this.error.AppendLine(s);
        this.lineCount++;

        /* 
         * ErrorLines() 로 가져오고보면
         * ErrorHandler에서는 바로바로 한줄씩 가져와서 로그 찍는데
         * Update()에서 돌리는 경우에는 몇줄당 하나씩 가져온다
         * 
         * 즉, 여기 함수내부는 Handler에서 가져올때마다 정상 실행되니
         * 여기서 다 처리해서 변수로 읽을 수 있게 하면 될것으로 보인다.
         * 
         * 어차피 엄청 빠른 것들은 1프레임보다 더 빠르게 진행속도가 올라갈듯?
         * 
         */

    }

    /// <summary>
    /// IsReadable을 확인하고 호출할 것
    /// </summary>
    /// <returns></returns>
    public string ErrorLines()
    {
        UnityEngine.Debug.Log("isError is " + this.isError);
        if (this.isError)
        {
            string s = this.error.ToString();
            error.Clear();
            return s;
        }
        else
            return "null";
    }

    /// <summary>
    /// 명령어 입력 전에 꼭 실행시켜서 초기화 할것
    /// </summary>
    public void Clear()
    {
        this.error.Clear();
        this.lineCount = 0;
        this.isError = false;
        this.wholeTask = 0;
        this.nowTask = 0;
        this.RealError = false;
    }

}
