using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using TMPro;

public class HpGauge : MonoAutoCaching
{
    [SerializeField] private TMP_Text progress;
    [SerializeField] private Image gauge;

    private Transform target;
    private ePlayer player;
    private int max;
    private RectTransform rt { get => transform as RectTransform; }
    public void Init(int max)
    {
        this.max = max;
        SetProgress(max);
    }

    public void SetProgress(int val)
    {
        val = Mathf.Max(0, val);
        progress.text = string.Format("{0}/{1}", val, max);
        gauge.fillAmount = (float)val / (float)max;
        /*if(val <= 0)
        {
            GameManager.instance.OnGameEnd();
        }*/
    }

    public void SetTraget(Transform target, ePlayer player)
    {
        this.target = target;
        this.player = player;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            var pos = RectTransformUtility.WorldToScreenPoint(player == ePlayer.me ? CanvasGame.instance.p1Camera : CanvasGame.instance.p2Camera, target.position + new Vector3(0, 100f));

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Get<UIGame>().hpParent, pos, player == ePlayer.me ? CanvasGame.instance.mainCamera : CanvasGame.instance.mainCamera, out Vector2 localPoint))
            {
                rt.anchoredPosition3D = new Vector3(localPoint.x, (localPoint.y + (player == ePlayer.me ? (Screen.height / 2) : 0)), 0);
                if (player == ePlayer.another)
                {
                    //UIManager.Get<UIGame>().Log(pos.ToString() + ", " + localPoint.ToString() + ", " + rt.anchoredPosition3D.ToString());
                }
            }
            else
            {
                rt.anchorMax = Vector2.zero; rt.anchorMin = Vector2.zero;
                rt.anchoredPosition3D = new Vector3(pos.x, (pos.y + (player == ePlayer.me ? 540f + Mathf.Max(UIManager.Get<UIGame>().hpParent.rect.height - 1080f, 0) : 0)), 0);
                if (player == ePlayer.another)
                {
                    //UIManager.Get<UIGame>().Log(pos.ToString() + ", " + localPoint.ToString() + ", " + rt.anchoredPosition3D.ToString());
                }
            }
        }
    }
}