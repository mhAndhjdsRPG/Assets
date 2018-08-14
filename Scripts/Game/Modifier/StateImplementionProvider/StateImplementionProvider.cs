using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class StateImplementionProvider
{

    public ICharacter owner;

    public StateImplementionProvider(ICharacter owner)
    {
        this.owner = owner;
    }


    public abstract void SetInvulnerableActive(bool isActive);

    public abstract GameObject CreateGo(string prefabPath, EffectAttachType attachType);


    public abstract void DestroyGo(GameObject gameObject);

    public abstract void ChangeHp(float value);

    public abstract void ChangeAglAddValue(float value);


    public abstract Action<string,string,bool> OnSetIconInfoActive { get; set; }
    public abstract void SetIconInfoActive(string iconPath, string description,bool isActive);

}

