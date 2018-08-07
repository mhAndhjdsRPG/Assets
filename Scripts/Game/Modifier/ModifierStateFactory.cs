using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;


public class ModifierStateFactory
{
    
    private Dictionary<string, Type> modifierStateTypeDic=new Dictionary<string, Type>();

    public ModifierStateFactory()
    {
        InitModifierStateDic();
    }

    

    #region 初始化 modifierStateTypeDic

    void InitModifierStateDic()
    {
        Type[] modifierStates = FindAllSubClass(typeof(IModifierState));
        foreach (Type stateType in modifierStates)
        {
            modifierStateTypeDic.Add(stateType.Name, stateType);
        }
    }

    
    Type[] FindAllSubClass(Type baseType)
    {
        var subTypesQuery = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where IsSubClass(t, baseType)
                            select t;

         return subTypesQuery.ToArray();
    }
    

    bool IsSubClass(Type checkType, Type baseType)
    {
        Type checkBaseType = checkType.BaseType;
        
        while (checkBaseType != null)
        {
            if (checkBaseType.Equals(baseType))
            {
                return true;
            }
            checkBaseType = checkBaseType.BaseType;
        }

        return false;
    }

    #endregion

    
    public IModifierState CreateModifierState(string modifierClassName)
    {
        if (modifierStateTypeDic.Keys.Contains(modifierClassName))
        {
            return (IModifierState)Activator.CreateInstance(modifierStateTypeDic[modifierClassName]);
        }
        else throw new NullReferenceException("没有该类型，请检查是否派生自IModifierState");

    }

}
