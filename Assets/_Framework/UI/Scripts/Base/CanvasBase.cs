using Ironcow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if USE_AUTO_CACHING
public class CanvasBase<T> : MonoSingleton<T> where T : CanvasBase<T>
#else
public class CanvasBase : MonoBehaviour
#endif
{
    [SerializeField] protected List<Transform> parents;

    protected override void Awake()
    {
        base.Awake();
        if (parents.Count > 0) UIManager.SetParents(parents);
    }
}
