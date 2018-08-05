using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;

public class ModiferFactory : MonoBehaviour
{
    const string XmlStateHeader = "State_";

    public static ModiferFactory Instance;

    private Dictionary<string, Type> modifierStateTypeDic=new Dictionary<string, Type>();

    private XDocument modiferXml;

    private void Awake()
    {
        Instance = this;
        InitModifierStateDic();
        modiferXml = XDocument.Load(FolderPaths.Modifiers);
    }

    private void Start()
    {
        GetModifier("Test");
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


    public Modifier GetModifier(string modifierName)
    {
        Modifier modifier = new Modifier();

        XElement modifierElement= modiferXml.Root.Element(modifierName);
        if (modifierElement == null)
        {
            throw new SystemException($"xml没有找到{modifierName}");
        }
        var childElements= modifierElement.Elements();

        foreach (XElement element in childElements)
        {
            string elementName = element.Name.ToString();
            if (elementName.Contains(XmlStateHeader))
            {
                string stateName = elementName.Remove(0, XmlStateHeader.Count());
                IModifierState state= CreateModifierState(stateName);
                state.Init(element);
                modifier.AddModifierState(state);
                
            }
        }
        
        return null;
    }


    



    public IModifierState CreateModifierState(string modifierClassName)
    {
        if (modifierStateTypeDic.Keys.Contains(modifierClassName))
        {
            return (IModifierState)Activator.CreateInstance(modifierStateTypeDic[modifierClassName]);
        }
        else throw new NullReferenceException("没有该类型，请检查是否派生自IModifierState");

    }

}
