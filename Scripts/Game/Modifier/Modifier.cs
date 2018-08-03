using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    public ICharacter owner;
    public string name;
    public EffectAttachType effectAttachType;
    public string effectName;
    public string textureName;
    public bool isPurgable;
    public float duration;
    public ModifierTypeAndValue modifierTypeAndValue;

    private List<IModifierState> modifierStateList = new List<IModifierState>();

    public void OnCreate()
    {
        for (int i = 0; i < modifierStateList.Count; i++)
        {
            modifierStateList[i].OnCreate();
        }
    }

    public void OnDestroy()
    {

    }

    public void OnUpdate()
    {
        for (int i = 0; i < modifierStateList.Count; i++)
        {
            modifierStateList[i].OnUpdate();
        }
    }

    public void AddModifierState(IModifierState modifierState)
    {
        duration = modifierState.duration > duration ? modifierState.duration : duration;
        modifierStateList.Add(modifierState);
    }
}



public struct ModifierTypeAndValue
{
    public static ModifierTypeAndValue defaultModifierTypeAndValue = new ModifierTypeAndValue(ModifierType.Default, 0);


    public ModifierTypeAndValue(ModifierType modifierType, float value)
    {
        this.modifierType = modifierType;
        this.value = value;
    }


    public ModifierType modifierType;
    public float value;



    public static ModifierTypeAndValue operator +(ModifierTypeAndValue modifierTypeAndValue1, ModifierTypeAndValue modifierTypeAndValue2)
    {
        if (modifierTypeAndValue1.modifierType == modifierTypeAndValue2.modifierType)
        {
            return new ModifierTypeAndValue(modifierTypeAndValue1.modifierType, modifierTypeAndValue1.value + modifierTypeAndValue2.value);
        }
        else
        {
            return defaultModifierTypeAndValue;
        }
    }
    public static ModifierTypeAndValue operator -(ModifierTypeAndValue modifierTypeAndValue1, ModifierTypeAndValue modifierTypeAndValue2)
    {
        if (modifierTypeAndValue1.modifierType == modifierTypeAndValue2.modifierType)
        {
            return new ModifierTypeAndValue(modifierTypeAndValue1.modifierType, modifierTypeAndValue1.value - modifierTypeAndValue2.value);
        }
        else
        {
            return defaultModifierTypeAndValue;
        }
    }
    public static ModifierTypeAndValue operator *(ModifierTypeAndValue modifierTypeAndValue1, ModifierTypeAndValue modifierTypeAndValue2)
    {
        if (modifierTypeAndValue1.modifierType == modifierTypeAndValue2.modifierType)
        {
            return new ModifierTypeAndValue(modifierTypeAndValue1.modifierType, modifierTypeAndValue1.value * modifierTypeAndValue2.value);
        }
        else
        {
            return defaultModifierTypeAndValue;
        }
    }
    public static bool operator ==(ModifierTypeAndValue modifierTypeAndValue1, ModifierTypeAndValue modifierTypeAndValue2)
    {
        if (modifierTypeAndValue1.modifierType == modifierTypeAndValue2.modifierType && modifierTypeAndValue1.value * modifierTypeAndValue2.value > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool operator !=(ModifierTypeAndValue modifierTypeAndValue1, ModifierTypeAndValue modifierTypeAndValue2)
    {
        return !(modifierTypeAndValue1 == modifierTypeAndValue2);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}