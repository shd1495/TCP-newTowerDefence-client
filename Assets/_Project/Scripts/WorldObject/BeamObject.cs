using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using System.Threading;

public class BeamObject : MonoAutoCaching
{
    [SerializeField] private Transform spriteRenderer;
    Monster targetMonster;
    bool isLive = true;

    public BeamObject SetTimer()
    {
        StartCoroutine(AutoDestroy());
        return this;
    }

    public BeamObject SetTarget(Monster monster)
    {
        targetMonster = monster;
        StartCoroutine(OnRotate());
        return this;
    }

    IEnumerator OnRotate()
    {
        while (isLive && targetMonster != null)
        {
            var normal = (transform.position - targetMonster.transform.position).normalized;
            var degree = Mathf.Atan2(normal.y, normal.x) * 180 / Mathf.PI;
            transform.eulerAngles = new Vector3(0, 0, degree);
            var dist = Vector3.Distance(transform.position, targetMonster.transform.position);
            transform.localScale = new Vector3(dist / 2, 50, 1);
            yield return null;
        }
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1);
        isLive = false;
        Destroy(gameObject);
    }
}