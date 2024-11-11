using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PopupServerSetting : UIBase
{
    [SerializeField] private TMP_InputField inputIp;
    [SerializeField] private TMP_InputField inputPort;

    UnityAction callback;

    public override void Opened(object[] param)
    {
        callback = (UnityAction)param[0];
        inputIp.text = PlayerPrefs.GetString("ip", SocketManager.instance.ip);
        inputPort.text = PlayerPrefs.GetString("port", SocketManager.instance.port.ToString());
    }

    public override void HideDirect()
    {
        callback.Invoke();
        UIManager.Hide<PopupServerSetting>();
    }

    public void OnClickSetting()
    {
        NetworkManager.instance.Init(inputIp.text, inputPort.text);
        SocketManager.instance.Init(inputIp.text, int.Parse(inputPort.text)).Connect();
        PlayerPrefs.SetString("ip", inputIp.text);
        PlayerPrefs.SetString("port", inputPort.text);
        HideDirect();
    }
}