using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System;

public sealed class ContinuouslyChangeHpState : IModifierState
{
    public float changeHpPerTime;

    private Action<float> ChangeHp;

    public override string Name
    {
        get
        {
            return typeof(ContinuouslyChangeHpState).ToString();
        }
    }

    public override void InitWithModifier(Modifier modifier)
    {
        base.InitWithModifier(modifier);
        ChangeHp = owner.stateImplemention.ChangeHp;
    }

    public override void InitDataWithXml(XElement element)
    {
        base.InitDataWithXml(element);
        changeHpPerTime = element.Attribute("loseHpPerTime").Value.ParseToFloat();
    }

    protected override void Execute()
    {
        base.Execute();
        ChangeHp(changeHpPerTime);
    }
    
}
