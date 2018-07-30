using UnityEngine;
using System.Collections;
using System;

public abstract class IWeapon : MonoBehaviour
{
    /// <summary>
    /// 拥有着
    /// </summary>
    public ICharacter owner = null;

    #region 武器属性
    [SerializeField]
    private int atk = 0;
    public int ATK
    {
        get
        {
            return atk;
        }

        set
        {
            atk = value;
        }
    }

    float Length
    {
        get { return GetComponent<BoxCollider>().size.y; }
    }

   
    float subTenacity;
    public float SubTenacity
    {
        get
        {
            return subTenacity;
        }

        set
        {
            subTenacity = value;
        }
    }


    #endregion


    /// <summary>
    /// 播放音效
    /// </summary>
    public abstract void PlaySoundEffects();
    /// <summary>
    /// 攻击时候调用的函数
    /// </summary>
    public abstract void OnAttack();

    public abstract WeaponType Type { get; }

    public void GetThisWeapon(ICharacter owner)
    {
        this.owner = owner;
    }

    public void BeginCheck()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = true; ;
    }

    public void StopCheck()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
    }

}
