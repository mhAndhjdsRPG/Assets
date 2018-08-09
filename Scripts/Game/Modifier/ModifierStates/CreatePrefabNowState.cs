using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// 不完善，应该接入对象池，部位的获取方式也有待确定
/// </summary>
public class CreatePrefabStateNow:IModifierState
{
    public string preFabPath;

    public EffectAttachType attachType;

    private GameObject createGo;

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

        preFabPath = element.Attribute("preFabPath").Value;

        attachType = element.Attribute("attachType").Value.ParseToEnum<EffectAttachType>();
    }

    protected override void OnStart()
    {
        base.OnStart();

        createGo =(GameObject)GameObject.Instantiate(Resources.Load(preFabPath));

        Transform parent=null;

        Vector3 localPos=Vector3.zero;

        switch (attachType)
        {
            case EffectAttachType.Follow_Origin:
                break;
            case EffectAttachType.Follow_Overhead:
                parent = owner.head;
                localPos= Vector3.zero + Vector3.up*60;
                break;
            case EffectAttachType.Follow_Chest:
                break;
            case EffectAttachType.Follow_Head:
                break;
            case EffectAttachType.Start_At_CustomOrigin:
                break;
            case EffectAttachType.World_Origin:
                break;
            default:
                break;
        }

        createGo.transform.SetParent(parent);
        createGo.transform.localPosition = localPos;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameObject.Destroy(createGo);
        createGo = null;
    }

}
