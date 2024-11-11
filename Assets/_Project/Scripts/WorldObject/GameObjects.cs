using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;

public class GameObjects : MonoSingleton<GameObjects>
{
    public Transform startPosition;
    public Transform endPosition;
    public Transform roadParent;
    public Transform house;
}