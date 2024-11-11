
using Ironcow;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInfo
{
    public static UserInfo Instance { get => DataManager.instance.userInfo; set => DataManager.instance.userInfo = value; }
}