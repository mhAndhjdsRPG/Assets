using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour
{
    public ICharacter owner;
    [Header("====== Pos ======")]
    public WeaponSlot backWeaponSlot;
    public WeaponSlot rightHandWeaponSlot;
    public WeaponSlot leftHandWeaponSlot;
    public WeaponSlot rightFeetWeaponSlot;
    public WeaponSlot leftFeetWeaponSlot;

    Animator ani;


    List<WeaponSlot> beginCheckWeaponSlots;
   
    private void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool(rightHandWeaponSlot.CurrentWeapon.Type.ToString(), true);

        beginCheckWeaponSlots = new List<WeaponSlot>();
    }


    public bool CanChangeWeapon
    {
        get { return backWeaponSlot.IsDefaultWeapon && rightHandWeaponSlot.IsDefaultWeapon; }
    }


    public void ChangeWeapon()
    {
        IWeapon weapon = backWeaponSlot.CurrentWeapon;
        backWeaponSlot.CurrentWeapon = rightHandWeaponSlot.CurrentWeapon;
        rightHandWeaponSlot.CurrentWeapon = weapon;

        SetCurrentWeaponAniParam();
    }


    public void PickUpWeapon()
    {
        if (rightHandWeaponSlot.CanArmWeapon)
        {
            IWeapon weapon = FindNearestWeapon();
            rightHandWeaponSlot.CurrentWeapon = weapon;
        }

        SetCurrentWeaponAniParam();
    }


    public void ThrowWeapon()
    {
        rightHandWeaponSlot.ThrowWeapon();

        SetCurrentWeaponAniParam();
    }


    

    IWeapon FindNearestWeapon()
    {
        Collider[] weapons = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("NoOwnerWeapon"));
        float minDistance = float.MaxValue;
        IWeapon resultWeapn = null;
        foreach (Collider weaponCol in weapons)
        {
            var currentDistance = Vector3.Distance(weaponCol.transform.position, transform.position);
            if (minDistance > currentDistance)
            {
                minDistance = currentDistance;
                resultWeapn = weaponCol.GetComponent<IWeapon>();
            }
        }

        return resultWeapn;
    }



    #region CheckAttack

    public void BeginWeaponCheck(string slotPosName)
    {
         WeaponSlot slot =GetWeaponSlotBySlotPosName(slotPosName);
        slot.BeginCheck();
        beginCheckWeaponSlots.AddIfNotContains(slot);
    }

    

    public void StopWeaponCheck()
    {
        foreach (var slot in beginCheckWeaponSlots)
        {
            slot.StopCheck();
        }
        beginCheckWeaponSlots =new List<WeaponSlot>();

        (owner.state as PlayerAttackState)?.SetAnimCanJumpToOthers();
    }


    WeaponSlot GetWeaponSlotBySlotPosName(string name)
    {
        var slotPosEnum = (SlotPos)Enum.Parse(typeof(SlotPos), name);
        return GetWeaponSlotBySlotPosEnum(slotPosEnum);
    }

    WeaponSlot GetWeaponSlotBySlotPosEnum(SlotPos slotPosEnum)
    {
        WeaponSlot slot = null;

        switch (slotPosEnum)
        {
            case SlotPos.leftHand:
                slot = leftHandWeaponSlot;
                break;
            case SlotPos.rightHand:
                slot = rightHandWeaponSlot;
                break;
            case SlotPos.leftFoot:
                slot = leftFeetWeaponSlot;
                break;
            case SlotPos.rightFoot:
                slot = rightFeetWeaponSlot;
                break;
            case SlotPos.back:
                slot = backWeaponSlot;
                break;
            default:
                throw new Exception("参数错误");
        }

        return slot;
    }


    #endregion

    #region SetAnimationParam

    void SetCurrentWeaponAniParam()
    {
        FalseAllAniWeaponBool();
        ani.SetBool(rightHandWeaponSlot.CurrentWeapon.Type.ToString(), true);
    }

    void FalseAllAniWeaponBool()
    {
        var parms = ani.parameters;
        foreach (var parm in parms)
        {
            if (parm.type == AnimatorControllerParameterType.Bool)
            {
                SetFalseIfItsWeaponBool(parm);
            }
        }
    }

    void SetFalseIfItsWeaponBool(AnimatorControllerParameter parm)
    {
        foreach (var weaponTypeName in Enum.GetNames(typeof(WeaponType)))
        {
            if (parm.name == weaponTypeName)
            {
                ani.SetBool(parm.name, false);
            }
        }
    }




    #endregion
}




