using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SampleImplementProvider : StateImplementionProvider
{
    public SampleImplementProvider(ICharacter owner) : base(owner) { }


    public override GameObject CreateGo(string prefabPath, EffectAttachType attachType)
    {
        GameObject createdGo = (GameObject)GameObject.Instantiate(Resources.Load(prefabPath));

        Transform parent = null;

        Vector3 localPos = Vector3.zero;

        switch (attachType)
        {
            case EffectAttachType.Follow_Origin:
                parent = owner.origin;
                localPos = Vector3.zero + Vector3.up * 5;
                break;
            case EffectAttachType.Follow_Overhead:
                parent = owner.head;
                localPos = Vector3.zero + Vector3.up * 60;
                break;
            case EffectAttachType.World_Origin:
                parent = null;
                localPos = Vector3.zero;
                break;
            case EffectAttachType.Follow_Chest:
                parent = owner.chest;
                localPos = Vector3.zero;
                break;
            default:
                throw new System.NotImplementedException("没有实现该功能");
        }

        createdGo.transform.SetParent(parent);
        createdGo.transform.localPosition = localPos;

        return createdGo;
    }


    float invulnerableActiveCount;

    public override void SetInvulnerableActive(bool isActive)
    {
        if (isActive)
        {
            invulnerableActiveCount++;
            owner.notGetHurt = true;
        }
        else
        {
            invulnerableActiveCount--;
            if (invulnerableActiveCount == 0)
            {
                owner.notGetHurt = false;
            }
        }
    }

    public override void DestroyGo(GameObject gameObject)
    {
        GameObject.Destroy(gameObject);
    }

    public override void ChangeHp(float value)
    {
        owner.HP += value;
    }

    public override void ChangeAglAddValue(float value)
    {
        owner.AddAgl += value;
    }

    public override Action<string, string, bool> OnSetIconInfoActive { get; set; }
    public override void SetIconInfoActive(string iconPath, string description, bool isActive)
    {
        OnSetIconInfoActive?.Invoke(iconPath,description,isActive);
    }

}

