using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public sealed class InvulnerableModifierState : IModifierState
{
    public override string Name
    {
        get
        {
            return typeof(InvulnerableModifierState).ToString();
        }
    }

    protected override void OnStart()
    {
        owner.NotGetHurt = true;
    }

    protected override void OnDestroy()
    {
        owner.NotGetHurt = false;
    }

    
}
