using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    
    private char Xstatus;
    private char Ystatus;

    private string Path;

    public Status() { }

    public Status(char _xstatus, char _ystatus, string _path)
    {
        SetXstatus(_xstatus);
        SetYstatus(_ystatus);
        SetPath(_path);
    }


    public void SetXstatus(char c)
    {
        this.Xstatus = c;
    }

    public void SetYstatus(char c)
    {
        this.Ystatus = c;
    }

    public void SetPath(string s)
    {
        this.Path = s;
    }

    public char GetXstatus()
    {
        return this.Xstatus;
    }

    public char GetYstatus()
    {
        return this.Ystatus;
    }

    public string GetPath()
    {
        return this.Path;
    }


}
