using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public sealed class ContinuouslyLoseHpState : IModifierState
{
    public float loseHpPerTime;


    public override string Name
    {
        get
        {
            return typeof(ContinuouslyLoseHpState).ToString();
        }
    }


    protected override void OnExecute()
    {
        base.OnExecute();
        owner.HP -= loseHpPerTime;
    }


    public override void Init(XElement element)
    {
        base.Init(element);
        loseHpPerTime=element.Attribute("loseHpPerTime").Value.ParseToFloat();
    }
}
