using System;

public sealed class InvulnerableModifierState : IModifierState
{

    Action<bool> SetInvulnerableActive;


    public override string Name
    {
        get
        {
            return typeof(InvulnerableModifierState).ToString();
        }
    }

    protected override void ManageSelfStart()
    {
        SetInvulnerableActive(true);
    }

    protected override void ManageSelfDestroy()
    {
        SetInvulnerableActive(false);
    }
    
    public override void InitWithModifier(Modifier modifier)
    {
        base.InitWithModifier(modifier);
        SetInvulnerableActive = owner.stateImplemention.SetInvulnerableActive;
    }


}
