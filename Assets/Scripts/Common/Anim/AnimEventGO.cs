using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
[LuaCallCSharp]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimEventCallback))]
public class AnimatorEventGOLua : MonoBehaviour
{
    public static AnimatorEventGOLua AddToGameObject(Transform transform)
    {
        if (transform.GetComponent<Animator>() == null)
        {
            Logger.LogError("{0} 没有找到对应的 Animator组件", transform.gameObject.name);
            return null;
        }

        return transform.gameObject.AddComponent<AnimatorEventGOLua>();
    }

    public static AnimatorEventGOLua AddEventToGameObject(GameObject gameObject)
    {
        if (gameObject.GetComponent<Animator>() == null)
        {
            Logger.LogError("{0} 没有找到对应的 Animator组件", gameObject.name);
            return null;
        }

        return gameObject.AddComponent<AnimatorEventGOLua>();
    }

    public static AnimatorEventGOLua AddEventToGameObject(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour.gameObject.GetComponent<Animator>() == null)
        {
            Logger.LogError("{0} 没有找到对应的 Animator组件", monoBehaviour.gameObject.name);
            return null;
        }

        return monoBehaviour.gameObject.AddComponent<AnimatorEventGOLua>();
    }

    private Animator _animator;
    private Dictionary<string, AnimationClip> _dictionaryAnimationClip = new Dictionary<string, AnimationClip>();
    private AnimEventCallback _animEventCallback;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _animEventCallback = GetComponent<AnimEventCallback>();
        if (_animEventCallback == null)
        {
            _animEventCallback = gameObject.AddComponent<AnimEventCallback>();
        }

        if (_animator == null)
        {
            _animator = gameObject.AddComponent<Animator>();
        }

        foreach (var animationClip in _animator.runtimeAnimatorController.animationClips)
        {
            _dictionaryAnimationClip[animationClip.name] = animationClip;
        }
    }


    /// <summary>
    /// 动画中添加回调
    /// </summary>
    /// <param name="aniName">需要添加回调动画的名字</param>
    /// <param name="action">回调方法</param>
    /// <param name="time">回调调用的时间</param>
    public void AddEvent(string aniName, Action action, float time)
    {
        AnimationClip animationClip;
        if (_dictionaryAnimationClip.TryGetValue(aniName, out animationClip))
        {
            var animationEvent = new AnimationEvent();
            animationEvent.functionName = "EventCallback";
            animationEvent.stringParameter = aniName + action.GetHashCode().ToString();
            animationEvent.time = time;
            animationClip.AddEvent(animationEvent);
            _animEventCallback.AddAction(animationEvent.stringParameter, action);
        }
        else
        {
            Logger.LogError("没有找到对应的 动画 {0}", aniName);
        }
    }

    /// <summary>
    /// 添加动画开始回调
    /// </summary>
    /// <param name="aniName">需要添加回调动画的名字</param>
    /// <param name="action">回调方法</param>
    public void AddEventStart(string aniName, Action action)
    {
        AddEvent(aniName, action, 0);
    }

    /// <summary>
    /// 添加动画结束回调
    /// </summary>
    /// <param name="aniName">需要添加回调动画的名字</param>
    /// <param name="action">回调方法</param>
    public void AddEventEnd(string aniName, Action action)
    {
        AnimationClip animationClip;
        if (_dictionaryAnimationClip.TryGetValue(aniName, out animationClip))
            AddEvent(aniName, action, animationClip.length);
        else
            Logger.LogError("没有找到对应的 动画 {0}", aniName);
    }
}

[Hotfix]
[LuaCallCSharp]
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(AnimEventCallback))]
public class AnimationEventGOLua : MonoBehaviour
{
    public static AnimationEventGOLua AddToGameObject(Transform transform)
    {
        if (transform.GetComponent<Animation>() == null)
        {
            Logger.LogError("{0} 没有找到对应的 Animation 组件", transform.gameObject.name);
            return null;
        }

        return transform.gameObject.AddComponent<AnimationEventGOLua>();
    }

    public static AnimationEventGOLua AddEventToGameObject(GameObject gameObject)
    {
        if (gameObject.GetComponent<Animation>() == null)
        {
            Logger.LogError("{0} 没有找到对应的 Animation 组件", gameObject.name);
            return null;
        }

        return gameObject.AddComponent<AnimationEventGOLua>();
    }

    public static AnimationEventGOLua AddEventToGameObject(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour.gameObject.GetComponent<Animation>() == null)
        {
            Logger.LogError("{0} 没有找到对应的 Animation 组件", monoBehaviour.gameObject.name);
            return null;
        }

        return monoBehaviour.gameObject.AddComponent<AnimationEventGOLua>();
    }


    private AnimationClip _animationClip;
    private AnimEventCallback _animEventCallback;

    private void Awake()
    {
        _animationClip = gameObject.GetComponent<Animation>().clip;
        _animEventCallback = GetComponent<AnimEventCallback>();
        if (_animEventCallback == null)
        {
            _animEventCallback = gameObject.AddComponent<AnimEventCallback>();
        }
    }

    /// <summary>
    /// 动画中添加回调
    /// </summary>
    /// <param name="aniName">需要添加回调动画的名字</param>
    /// <param name="action">回调方法</param>
    /// <param name="time">回调调用的时间</param>
    public void AddEvent(string aniName, Action action, float time)
    {
        var animationEvent = new AnimationEvent();
        animationEvent.functionName = "EventCallback";
        animationEvent.stringParameter = aniName + action.GetHashCode().ToString();
        animationEvent.time = time;
        _animationClip.AddEvent(animationEvent);
        // _dictionaryAction[animationEvent.stringParameter] = action;
        _animEventCallback.AddAction(animationEvent.stringParameter, action);
    }

    /// <summary>
    /// 添加动画开始回调
    /// </summary>
    /// <param name="aniName">需要添加回调动画的名字</param>
    /// <param name="action">回调方法</param>
    public void AddEventStart(string aniName, Action action)
    {
        AddEvent(aniName, action, 0);
    }

    /// <summary>
    /// 添加动画结束回调
    /// </summary>
    /// <param name="aniName">需要添加回调动画的名字</param>
    /// <param name="action">回调方法</param>
    public void AddEventEnd(string aniName, Action action)
    {
        AddEvent(aniName, action, _animationClip.length);
    }
}

class AnimEventCallback : MonoBehaviour
{
    private Dictionary<string, Action> _dictionaryAction =
        new Dictionary<string, Action>();

    public void AddAction(string name, Action action)
    {
        _dictionaryAction[name] = action;
    }

    private void EventCallback(string parameter)
    {
        Action action;
        if (_dictionaryAction.TryGetValue(parameter, out action))
        {
            action.Invoke();
        }
        else
        {
            Logger.LogError("没有找到对应的 回调 {0}", parameter);
        }
    }
}