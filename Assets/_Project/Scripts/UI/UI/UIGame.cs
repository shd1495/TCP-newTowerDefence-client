using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using TMPro;

public class UIGame : UIBase
{
    [SerializeField] private TMP_Text topScore;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text gold;
    [SerializeField] private TMP_Text level;
    [SerializeField] private GameObject p1Victory;
    [SerializeField] private GameObject p1Defeat;
    [SerializeField] private GameObject p2Victory;
    [SerializeField] private GameObject p2Defeat;

    [SerializeField] private HpGauge homeHpGauge1;

    [SerializeField] private HpGauge homeHpGauge2;
    public RectTransform hpParent;

    public override void Opened(object[] param)
    {
        homeHpGauge1.Init(100);
        homeHpGauge2.Init(100);
        if (param.Length > 0)
        {
            topScore.text = param[0].ToString();
        }
        if (param.Length > 1)
        {
            score.text = param[1].ToString();
        }
        if (param.Length > 2)
        {
            gold.text = param[2].ToString();
        }
        if (param.Length > 3)
        {
            level.text = param[3].ToString();
        }
        p1Victory.SetActive(false);
        p1Defeat.SetActive(false);
        p2Victory.SetActive(false);
        p2Defeat.SetActive(false);
        AudioManager.instance.PlayBgm("bgm", true);
    }

    public void SetGoldText(int gold)
    {
        this.gold.text = gold.ToString();
    }

    public void SetTopScoreText(int score)
    {
        this.topScore.text = score.ToString();
    }

    public void SetScoreText(int score)
    {
        this.score.text = score.ToString();
    }

    public void SetLevelText(int level)
    {
        this.level.text = level.ToString();
    }

    public override void HideDirect()
    {
        UIManager.Hide<UIGame>();
    }

    public void InitHpGauge(int hp)
    {
        homeHpGauge1.Init(hp);
        homeHpGauge2.Init(hp);
    }

    public void SetHpGauge1(int damage)
    {
        homeHpGauge1.SetProgress(damage);
    }

    public void SetHpGauge2(int damage)
    {
        homeHpGauge2.SetProgress(damage);
    }

    public void SetHpGaugeTarget(ePlayer player, Transform target)
    {
        if (player == ePlayer.me)
            homeHpGauge1.SetTraget(target, player);
        else
            homeHpGauge2.SetTraget(target, player);
    }

    public void SetHpGauge2Target(Transform target)
    {
    }

    public void OnClickAddTower()
    {
        // 초기 세팅 TowerCost 반영
        if (GameManager.instance.gold >= GameManager.instance.initialGameState.TowerCost)
        {
            GameManager.instance.gold -= GameManager.instance.initialGameState.TowerCost;
            var tower = GameManager.instance.AddRandomTower();
            GamePacket packet = new GamePacket();
            packet.TowerPurchaseRequest = new C2STowerPurchaseRequest() { X = tower.transform.localPosition.x, Y = tower.transform.localPosition.y };
            SocketManager.instance.Send(packet);
        }
    }

    public void SetWinner(bool isWin)
    {
        AudioManager.instance.PlayOneShot(isWin ? "win" : "lose");
        p1Victory.SetActive(isWin);
        p1Defeat.SetActive(!isWin);
        p2Victory.SetActive(!isWin);
        p2Defeat.SetActive(isWin);
        GameManager.instance.OnGameEnd();
    }

    public TMP_Text log;
    public void Log(string log)
    {
        this.log.text = log;
    }
}