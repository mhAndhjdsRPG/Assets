using UnityEngine;
using System.Xml.Linq;


public class ModifierManager : MonoBehaviour {

    public static ModifierManager Instance;

    const string XmlStateBeginStr = "State";

    private ModifierStateFactory factory;

    private ModifierStatePool pool;

    private XDocument modiferXml;


    private void Awake()
    {
        factory = new ModifierStateFactory();
        pool = new ModifierStatePool();
        modiferXml = XDocument.Load(FolderPaths.Modifiers);

        Instance = this;

    }


    public Modifier GetModifier(string modifierName,ICharacter affectedCharacter)
    {
        Modifier modifier = new Modifier(affectedCharacter);

        XElement modifierElement = modiferXml.Root.Element(modifierName);

        if (modifierElement == null)
        {
            throw new System.SystemException($"xml没有找到{modifierName}");
        }

        XElement modiferStates = modifierElement.Element(XmlStateBeginStr);

        var childElements = modiferStates.Elements();

        foreach (XElement element in childElements)
        {
            modifier.AddModifierState(GetModiferState(element, affectedCharacter));
        }

        return modifier;
    }


    IModifierState GetModiferState(XElement element,ICharacter affectedCharacter)
    {
        string stateName = element.Name.ToString();

        IModifierState state;

        if (pool.Have(stateName))
        {
            state=pool.GetModifierState(stateName);
            print("get from pool");
        }
        else
        {
            state=factory.CreateModifierState(stateName);
            print("get from factory");
        }

        state.InitDataWithXml(element);

        return state;
    }


    public void StoreModifierState(IModifierState state)
    {
        print("Store");
        pool.StoreModifierState(state);
    }



}
