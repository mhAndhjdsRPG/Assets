using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public sealed class ContinuouslyChangeHpState : IModifierState
{
    public float changeHpPerTime;


    public override string Name
    {
        get
        {
            return typeof(ContinuouslyChangeHpState).ToString();
        }
    }


    protected override void OnExecute()
    {
        base.OnExecute();
        owner.HP += changeHpPerTime;
    }


    public override void InitDataWithXml(XElement element)
    {
        base.InitDataWithXml(element);
        changeHpPerTime=element.Attribute("loseHpPerTime").Value.ParseToFloat();
    }
}
