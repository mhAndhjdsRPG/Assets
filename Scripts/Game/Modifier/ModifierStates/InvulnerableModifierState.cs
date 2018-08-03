using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulnerableModifierState : IModifierState
{
    public override string Name
    {
        get
        {
            return "InvulnerableModifierState";
        }
    }

    protected override void OnStart()
    {
        
    }

    protected override void OnDestroy()
    {
        throw new System.NotImplementedException();
    }


}
