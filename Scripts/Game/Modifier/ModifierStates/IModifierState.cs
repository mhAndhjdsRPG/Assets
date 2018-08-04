using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public abstract class IModifierState
{

    private const float NotUpdate = -1f;
    public abstract string Name { get; }
    /// <summary>
    /// 绑定到的Modifier
    /// </summary>
    public Modifier modifier;
    /// <summary>
    /// 被影响者
    /// </summary>
    public ICharacter owner;
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
    private float lastWaitTime=0;

    public  void Start()
    {
        endTime = Time.time + duration;
        OnStart();
    }

<<<<<<< HEAD
    
    
=======
    protected abstract void OnCreate();



    private float lastWaitTime = 0;
    private bool isDistroyed = false;
>>>>>>> Master
    public void Update()
    {
        if (Time.time <= endTime)
        {
            ExecuteIfNeed();
        }
        else 
        {
            Destroy();
        }
    }

    void ExecuteIfNeed()
    {
        lastWaitTime -= Time.time;
        if (lastWaitTime<=0)
        {
            OnExecute();
            lastWaitTime = interval;
        }
    }
    

    public void Destroy()
    {
        OnDestroy();
        modifier.EndThisState(this);
    }

    protected virtual void OnStart() { }
    protected virtual void OnExecute() { }
    protected virtual void OnDestroy() { }

    //在该方法中解析
    public abstract void Init(XElement element);
}
