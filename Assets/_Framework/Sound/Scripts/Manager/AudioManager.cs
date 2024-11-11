using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource source;
    [SerializeField] private Dictionary<string, AudioClip> audioPool = new Dictionary<string, AudioClip>();

    [HideInInspector] public bool isInit;
    public void Init()
    {
    }

    public void PlayBgm(string key, bool isLoop = true)
    {
        if (!audioPool.ContainsKey(key))
            audioPool.Add(key, ResourceManager.instance.LoadAsset<AudioClip>("Sound/" + key));
        source.clip = audioPool[key];
        source.loop = isLoop;
        source.Play();
    }

    public void PlayOneShot(string key)
    {
        if (!audioPool.ContainsKey(key))
            audioPool.Add(key, ResourceManager.instance.LoadAsset<AudioClip>("Sound/" + key));
        source.PlayOneShot(audioPool[key]);
    }

    public void StopBgm()
    {
        source.Stop();
    }
}