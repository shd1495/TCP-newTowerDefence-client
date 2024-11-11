using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;

public class PopupMatching : UIBase
{
    [SerializeField] private Transform progress;

    public override void Opened(object[] param)
    {
        StartCoroutine(SpinProgress());
    }

    IEnumerator SpinProgress()
    {
        while(true)
        {
            progress.transform.localEulerAngles = new Vector3(0, 0, progress.transform.localEulerAngles.z - 1);
            yield return null;
        }
    }

    public override void HideDirect()
    {
        UIManager.Hide<PopupMatching>();
    }
}