using UnityEngine;
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




    private float endTime;
    private float lastWaitTime=0;

    public  void Start()
    {
        endTime = Time.time + duration;
        OnStart();
    }
    
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
        modifier.RemoveAndStoreState(this);
    }

    protected virtual void OnStart() { }
    protected virtual void OnExecute() { }
    protected virtual void OnDestroy() { }

    
    public virtual void Init(XElement element)
    {
        duration = element.Attribute("duration").Value.ParseToFloat();
        interval = element.Attribute("interval").Value.ParseToFloat();
       
    }
    
}
