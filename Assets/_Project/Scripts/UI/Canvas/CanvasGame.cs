using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;

public class CanvasGame : CanvasBase<CanvasGame>
{
    public GameObjects player1;
    public GameObjects player2;
    public Camera mainCamera;
    public Camera p1Camera;
    public Camera p2Camera;

    protected override void Awake()
    {
        base.Awake();
        UIManager.Show<UIGame>();
        GameManager.instance.OnGameStart();
    }
}