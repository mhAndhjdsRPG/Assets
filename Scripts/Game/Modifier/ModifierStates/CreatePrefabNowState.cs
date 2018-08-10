using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// 不完善，应该接入对象池，部位的获取方式也有待确定
/// </summary>
public class CreatePrefabStateNow : IModifierState
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

        createGo = (GameObject)GameObject.Instantiate(Resources.Load(preFabPath));

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
