using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasIntro : CanvasBase<CanvasIntro>
{
    IEnumerator Start()
    {
        Application.targetFrameRate = 60;
        if (!GameManager.isInstance || !GameManager.instance.isLoadDontDestroy)
        {
            yield return SceneManager.LoadSceneAsync("DontDestroy", LoadSceneMode.Additive);
            GameManager.instance.isLoadDontDestroy = true;
        }
        yield return new WaitForSeconds(2);
        yield return SceneManager.LoadSceneAsync("Main");

    }
}