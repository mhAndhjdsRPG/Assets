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

    private float beginTime;

    private float nextExecuteTime;

    public  void Start()
    {
        beginTime = Time.time;
        endTime = Time.time + duration;
        nextExecuteTime = beginTime + interval;
        OnStart();
    }

    protected abstract void OnStart();
    
    public void Update()
    {
        if (Time.time <= endTime)
        {
            ExecuteIfNeed();
        }
        else 
        {
            
        }
    }

    void ExecuteIfNeed()
    {
        if (Time.time > nextExecuteTime)
        {
            Execute();
            nextExecuteTime = Time.time + interval;
        }
    }


    protected abstract void Execute();


    public void Destroy()
    {
        OnDestroy();
    }

    protected abstract void OnDestroy();
}
