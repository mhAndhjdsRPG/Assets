using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using System;

/// <summary>
/// 不完善，应该接入对象池，部位的获取方式也有待确定
/// </summary>
public class CreatePrefabStateNow : IModifierState
{
    public string prefabPath;

    public EffectAttachType attachType;

    private GameObject createdGo;

    private Func<string, EffectAttachType, GameObject> CreateGo;

    private Action<GameObject> DestroyGo;

    public override string Name
    {
        get
        {
            return typeof(CreatePrefabStateNow).ToString();
        }
    }


    public override void InitDataWithXml(XElement element)
    {
        base.InitDataWithXml(element);

        prefabPath = element.Attribute("preFabPath").Value;

        attachType = element.Attribute("attachType").Value.ParseToEnum<EffectAttachType>();
    }

    public override void InitWithModifier(Modifier modifier)
    {
        base.InitWithModifier(modifier);

        CreateGo = owner.stateImplemention.CreateGo;
        DestroyGo = owner.stateImplemention.DestroyGo;
    }


    protected override void ManageSelfStart()
    {
        base.ManageSelfStart();

        createdGo = CreateGo(prefabPath, attachType);
    }

    protected override void ManageSelfDestroy()
    {
        base.ManageSelfDestroy();
        DestroyGo(createdGo);
        createdGo = null;
    }

}
