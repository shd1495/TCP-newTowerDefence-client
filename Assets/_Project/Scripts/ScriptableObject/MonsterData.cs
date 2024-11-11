using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/MonsterData")]
public class MonsterDataSO : BaseDataSO
{
    public int maxHp;
    public int hpPerLv;
    public float spd;
    public int def;
    public int defPerLv; 
    public int atk;
    public int atkPerLv;
}