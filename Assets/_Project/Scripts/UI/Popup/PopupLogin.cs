using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using TMPro;
using Ironcow.WebSocketPacket;

public class PopupLogin : UIBase
{
    [SerializeField] private TMP_InputField inputId;
    [SerializeField] private TMP_InputField inputPw;

    public override void Opened(object[] param)
    {
        inputId.text = PlayerPrefs.GetString("lastId");
        inputPw.text = PlayerPrefs.GetString("lastPw");
    }

    public override void HideDirect()
    {
        UIManager.Hide<PopupLogin>();
    }

    public void OnClickLogin()
    {
        GamePacket gamePacket = new GamePacket();
        gamePacket.LoginRequest = new C2SLoginRequest() { Id = inputId.text, Password = inputPw.text };
        SocketManager.instance.Send(gamePacket);
    }

    public void OnLoginResult(bool isSuccess)
    {
        GameManager.instance.isLogin = isSuccess;
        UIManager.Get<UIMain>().SetLogin(isSuccess);
        if (isSuccess)
        {
            PlayerPrefs.SetString("lastId", inputId.text);
            PlayerPrefs.SetString("lastPw", inputPw.text);
        }
        HideDirect();
    }
}