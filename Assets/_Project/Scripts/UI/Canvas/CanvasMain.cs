using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasMain : CanvasBase<CanvasMain>
{
    protected override void Awake()
    {
        if (!GameManager.instance.isLoadDontDestroy)
        {
            SceneManager.LoadScene("Intro");
            return;
        }
        base.Awake();
        UIManager.Show<UIMain>();
    }
}