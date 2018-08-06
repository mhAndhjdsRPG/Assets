using UnityEngine;
using System.Collections;

public struct ModifierEffectInfo
{
    public ModifierEffectInfo(string name, GameObject gameObject)
    {
        this.name = name;
        this.gameObject = gameObject;
    }
    public string name;
    public GameObject gameObject;
    
}
