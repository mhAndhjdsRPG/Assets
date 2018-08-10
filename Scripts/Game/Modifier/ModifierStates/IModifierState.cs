using UnityEngine;
using System.Xml.Linq;


public abstract class IModifierState
{
    private const float NotUpdate = float.MaxValue;

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


    private float lastWaitTime;

    public void Start()
    {
        lastWaitTime = interval;
        OnStart();
    }

    public void Update()
    {
        
        ExecuteIfNeed();

        duration -= Time.deltaTime;
        if (duration<0)
        {
            Destroy();
        }
    }

    void ExecuteIfNeed()
    {
        if (interval == NotUpdate)
        {
            return;
        }

        lastWaitTime -= Time.deltaTime;
        if (lastWaitTime <= 0)
        {
            OnExecute();
            lastWaitTime = interval;
        }
    }


    public virtual void InitDataWithXml(XElement element)
    {
        duration = element.Attribute("duration").Value.ParseToFloat();

        XAttribute intervalAttribute = element.Attribute("interval");

        if (intervalAttribute == null)
        {
            interval = NotUpdate;
        }
        else
        {
            interval = intervalAttribute.Value.ParseToFloat();
        }

    }


    public virtual void InitWithModifier(Modifier modifier)
    {
        this.modifier = modifier;
        owner = modifier.owner;
        modifier.OnStart += Start;
        modifier.OnUpdate += Update;
    }

    public void Destroy()
    {
        OnDestroy();

        modifier.OnStart -= Start;
        modifier.OnUpdate -= Update;
        modifier.RemoveAndStoreState(this);
    }


    protected virtual void OnStart() { }
    protected virtual void OnExecute() { }
    protected virtual void OnDestroy() { }



}
