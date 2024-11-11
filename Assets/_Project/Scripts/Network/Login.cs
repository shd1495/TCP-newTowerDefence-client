using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;


[System.Serializable]
public class Login
{
    public string username;
    
}

[System.Serializable]
[API("/login", eRequestType.GET)]
public class RequestLogin : Request<RequestLogin>
{
    public string username;
    public string password;

    public RequestLogin(params object[] param) : base(param)
    {

    }
}