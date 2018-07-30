using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    
    public SlotPos slotPos;

    [SerializeField]
    private IWeapon defaultWeapon;

    [HideInInspector]
    public ICharacter owner;
    

    [SerializeField]
    private IWeapon currentWeapon;
    public IWeapon CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }

        set
        {
            if (value.Type==WeaponType.arm)
            {
                currentWeapon = defaultWeapon;
            }
            else
            {
                currentWeapon = value;
            }
            currentWeapon.transform.SetParent(transform);
            currentWeapon.transform.localRotation = Quaternion.identity;
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void Awake()
    {
        owner = GetComponentInParent<ICharacter>();

        if (defaultWeapon == null)
        {
            throw new System.Exception("没有默认武器");
        }

        if (owner == null)
        {
            throw new System.Exception("没有设置owner");
        }

        defaultWeapon.owner = owner;

        defaultWeapon.GetComponent<Collider>().isTrigger = true;
        defaultWeapon.GetComponent<Collider>().enabled =false;

        if (currentWeapon != null)
        {
            currentWeapon.GetComponent<Collider>().isTrigger = true;
            currentWeapon.GetComponent<Collider>().enabled = false;
            currentWeapon.owner = owner;
        }
        else
        {
            currentWeapon = defaultWeapon;
        }
       
    }

    public bool IsDefaultWeapon
    {
        get { return currentWeapon == defaultWeapon; }

    }

    public void ThrowWeapon()
    {
        if (!IsDefaultWeapon)
        {
            IWeapon throwWeapon = currentWeapon;
            currentWeapon = defaultWeapon;
            throwWeapon.transform.SetParent(null);
            throwWeapon.GetComponent<Rigidbody>().isKinematic = false;
        }
    }


    public bool CanArmWeapon
    {
        get { return IsDefaultWeapon; }
    }

    

    public void BeginCheck()
    {
        currentWeapon.BeginCheck();
    }

    public void StopCheck()
    {
        currentWeapon.StopCheck();
    }

    public void SetCheckStatus(bool isBeginCheck)
    {
        if (isBeginCheck)
        {
            BeginCheck();
        }
        else
        {
            StopCheck();
        }
    }
    
}
