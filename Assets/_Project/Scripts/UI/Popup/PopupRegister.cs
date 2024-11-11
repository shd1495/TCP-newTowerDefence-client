using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using TMPro;
using Ironcow.WebSocketPacket;

public class PopupRegister : UIBase
{
    [SerializeField] private TMP_InputField inputId;
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPw;
    [SerializeField] private TMP_InputField inputPwRe;

    public override void Opened(object[] param)
    {
        
    }

    public override void HideDirect()
    {
        UIManager.Hide<PopupRegister>();
    }

    public void OnClickSignUp()
    {
        GamePacket gamePacket = new GamePacket();
        gamePacket.RegisterRequest = new C2SRegisterRequest() { Id = inputId.text, Email = inputEmail.text, Password = inputPw.text };
        SocketManager.instance.Send(gamePacket);
    }


    public void OnRegisterResult(bool isSuccess)
    {
        HideDirect();
    }
}