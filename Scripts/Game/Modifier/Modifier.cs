using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Modifier
{
    #region 非初始化字段
    public ICharacter owner;
    public float duration;
    #endregion

    #region 初始化字段
    public string name;
    public bool isPurgable;
    public ModifierTypeAndValue modifierTypeAndValue;
    private List<IModifierState> modifierStateList = new List<IModifierState>();
    #endregion

    #region 事件
    public Action OnUpdate;
    public Action OnStart;
    #endregion

    #region Tools
    /// <summary>
    /// 给工厂用的组装方法
    /// </summary>
    /// <param name="modifierState"></param>
    public void AddModifierState(IModifierState modifierState)
    {
        modifierState.InitWithModifier(this);

        //更新总体duration
        duration = modifierState.duration > duration ? modifierState.duration : duration;
        modifierStateList.Add(modifierState);
    }
    /// <summary>
    /// State通知Modifier自己被移除
    /// </summary>
    /// <param name="state"></param>
    public void RemoveAndStoreState(IModifierState state)
    {
        modifierStateList.RemoveIfContains(state);
        ModifierManager.Instance.StoreModifierState(state);

        if (modifierStateList.Count == 0)
        {
            owner.UpdateModifier -= Update;
        }
    }
    #endregion


    public Modifier(ICharacter affectedCharacter)
    {
        owner = affectedCharacter;
    }


    public void Start()
    {
        OnStart?.Invoke();
        owner.UpdateModifier += Update;
    }

    public void Update()
    {
        OnUpdate?.Invoke();
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