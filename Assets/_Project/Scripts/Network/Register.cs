using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;


[System.Serializable]
public class Register
{
    public string username;

}

[System.Serializable]
[API("/register", eRequestType.GET)]
public class RequestRegister : Request<RequestRegister>
{
    public string username;
    public string password;

    public RequestRegister(params object[] param) : base(param)
    {

    }
}