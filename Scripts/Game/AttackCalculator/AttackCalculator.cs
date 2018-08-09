using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(ICharacter))]
public class AttackCalculator : MonoBehaviour
{

    public bool canGetHurt;
    ICharacter owner;

    List<ICharacter> attackersInThisRound;

    private void Awake()
    {
        attackersInThisRound = new List<ICharacter>();
        owner = GetComponent<ICharacter>();
    }


    private void OnTriggerEnter(Collider col)
    {
        if (!canGetHurt)
        {
            return;
        }
        
        string layerName = LayerMask.LayerToName(col.gameObject.layer);


        
        if (layerName.Contains("Weapon"))
        {
            AttackedByNearAttack(col);
        }
        else if(layerName.Contains("Magic"))
        {
            AttackedByMagic(col);
        }
    }



    void AttackedByNearAttack(Collider weaponCol)
    {
        IWeapon weapon = weaponCol.GetComponent<IWeapon>();

        AttackState state = weapon.owner.state as AttackState;
        
        if (attackersInThisRound.Contains(weapon.owner))
        {
            return;
        }
        else
        {
            state.AttackStateExitEvent += ReceiveAttackAfterAttackStateExit;
            attackersInThisRound.Add(state.Owner);
        }

        CalculateNearAttack(weapon.owner, weapon,state.AttackInfo);
        SetGetHitAniParm();
    }



    void AttackedByMagic(Collider magicCol)
    {
        IMagic magic = magicCol.GetComponent<IMagic>();
        CalculateMagicAttack(magic, magic.owner, magic.info, magic.weapon);
        SetGetHitAniParm();
    }

   
    
    public void AttackedByRangeAttack(AttackInfo info, IWeapon weapon)
    {
        if (canGetHurt)
        {
            owner.HP -= (info.Owner.BaseATK + weapon.ATK) * info.DamageRate;
        }
    }


    public void AttackedByRangeAttack(AttackInfo skillInfo)
    {
        if (canGetHurt)
        {
            owner.HP -= skillInfo.Owner.BaseATK * skillInfo.DamageRate;
        }

    }



    private void CalculateNearAttack(ICharacter attacker, IWeapon weapon, AttackInfo skillInfo)
    {
        owner.HP -= (attacker.BaseATK + weapon.ATK) * skillInfo.DamageRate;
    }

    private void CalculateMagicAttack(IMagic magic, ICharacter attacker, AttackInfo skillInfo, IWeapon weapon)
    {
        owner.HP -= (attacker.BaseATK + magic.atk + (weapon == null ? 0 : weapon.ATK)) * skillInfo.DamageRate;
    }



    void SetGetHitAniParm()
    {
        owner.ani.SetTrigger("Injured");
    }


    void ReceiveAttackAfterAttackStateExit(AttackState state)
    {
        attackersInThisRound.Remove(state.Owner);
        state.AttackStateExitEvent -= ReceiveAttackAfterAttackStateExit;
    }



}
