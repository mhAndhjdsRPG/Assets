using System.Collections;
using System;
using System.Xml.Linq;
using UnityEngine;

public class ChangeAglState : IModifierState
{

    public float addAgl;

    private Action<float> ChangeAgl;

    public override string Name
    {
        get
        {
            return typeof(ChangeAglState).ToString();
        }
    }

    protected override void ManageSelfStart()
    {
        base.ManageSelfStart();
        ChangeAgl(addAgl);
    }

    protected override void ManageSelfDestroy()
    {
        base.ManageSelfDestroy();
        ChangeAgl(-addAgl);
    }

    public override void InitDataWithXml(XElement element)
    {
        base.InitDataWithXml(element);
        addAgl = element.Attribute("addAgl").Value.ParseToFloat();
    }


    public override void InitWithModifier(Modifier modifier)
    {
        base.InitWithModifier(modifier);
        ChangeAgl = owner.stateImplemention.ChangeAglAddValue;
    }

}
