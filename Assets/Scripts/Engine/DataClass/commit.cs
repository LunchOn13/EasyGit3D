using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

public class commit
{
    public string hash;
    public string author;
    public string message;
    public string[] parentHash;
    public string[] theRef;
    public DateTime time;

    public commit()
    {

    }
    public commit(JToken _hash, JToken _author, JToken _dateString, JToken _message, string _ref, JToken _parentHash)
    {
        this.hash = _hash.ToString();
        this.author = _author.ToString();
        this.time = DateTime.Parse(_dateString.ToString());
        this.message = _message.ToString();
        this.parentHash = _parentHash.ToString().Split(' ');
        this.theRef = _ref.ToString().Split(',');
    }

 
}
