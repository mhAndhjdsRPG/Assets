using UnityEngine;
using System.Collections;

public abstract class IModifierState
{
    private const float NotUpdate = -1f;
    public abstract string Name { get; }
    /// <summary>
    /// 绑定到的Modifier
    /// </summary>
    public Modifier modifier;
    /// <summary>
    /// 持续时间
    /// </summary>
    public float duration;
    /// <summary>
    /// 频率
    /// </summary>
    public float interval = NotUpdate;



    /// <summary>
    /// 结束时间
    /// </summary>
    private float endTime;



    public void Create()
    {
        endTime = Time.time + duration;
        OnCreate();
    }

    protected abstract void OnCreate();


    private float lastWaitTime = 0;
    private bool isDistroyed = false;
    public void Update()
    {
        if (Time.time <= endTime)
        {
            lastWaitTime -= Time.deltaTime;
            if (lastWaitTime <= 0)
            {
                Execute();
                lastWaitTime = interval;
            }
        }
        else if (isDistroyed == false)
        {
            OnDestroy();
            isDistroyed = true;
        }
    }

    protected abstract void Execute();




    public void Destroy()
    {
        OnDestroy();
    }

    protected abstract void OnDestroy();
}
