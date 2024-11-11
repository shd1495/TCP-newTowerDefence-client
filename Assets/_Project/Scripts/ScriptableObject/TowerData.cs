using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;

[CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerData")]
public class TowerDataSO : BaseDataSO
{
    public int power;
    public int powerPerLv;
    public int range;
    public float cooldown;
    public int duration;
    public int cost;
    public float extra;
    public float extraPerLv;
}