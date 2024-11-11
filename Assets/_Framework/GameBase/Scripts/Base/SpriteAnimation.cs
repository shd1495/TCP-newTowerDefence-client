using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using UnityEngine.UI;
using System;

[Serializable]
public class AnimationSpriteGroup
{
    public string key;
    public List<Sprite> sprites;
}

public class SpriteAnimation :
#if USE_AUTO_CACHING
    MonoAutoCaching
#else
    MonoBehaviour
#endif
{
    [SerializeField] private List<AnimationSpriteGroup> animations;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveToFrame = 20;
    private float nowFrame;
    private int nowIndex;
    private AnimationSpriteGroup nowAnimation;

    private void Start()
    {
        var sprites = ResourceManager.instance.LoadAssets<Sprite>("Images/" + name);
        foreach (var ani in animations)
        {
            ani.sprites = sprites.FindAll(obj => obj.name.Contains(ani.key));
        }
        ChangeAnimation(animations[0].key);
    }

    private void Update()
    {
        if (nowAnimation == null) return;
        nowFrame++;
        if(nowFrame > moveToFrame)
        {
            nowFrame = 0;
            nowIndex = Util.Next(nowIndex, 0, nowAnimation.sprites.Count, false);
            spriteRenderer.sprite = nowAnimation.sprites[nowIndex];
        }
    }

    public void ChangeAnimation(string key)
    {
        nowAnimation = animations.Find(obj => obj.key == key);
        if (nowAnimation != null)
        {
            nowFrame = 0;
            nowIndex = 0;
            spriteRenderer.sprite = nowAnimation.sprites[nowIndex];
        }
    }
}