using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ChangeAglState : IModifierState
{

    public float addAgl;

    public override string Name
    {
        get
        {
            return typeof(ChangeAglState).ToString();
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        owner.AddAgl += addAgl;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        owner.AddAgl -= addAgl;
    }

    public override void InitDataWithXml(XElement element)
    {
        base.InitDataWithXml(element);
        addAgl = element.Attribute("addAgl").Value.ParseToFloat();
    }


}
