using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIconInfoState : IModifierState {

    string iconPath;
    string description;
    Action<string, string, bool> SetIconInfoActive;

    public override string Name
    {
        get
        {
            return typeof(SetIconInfoState).ToString();
        }
    }
    public override void InitWithModifier(Modifier modifier)
    {
        base.InitWithModifier(modifier);
        SetIconInfoActive = owner.stateImplemention.SetIconInfoActive;
    }

    protected override void ManageSelfStart()
    {
        base.ManageSelfStart();
        SetIconInfoActive(iconPath, description, true);
    }

    protected override void ManageSelfDestroy()
    {
        base.ManageSelfDestroy();
        SetIconInfoActive(iconPath, description, false);
    }

    
}
