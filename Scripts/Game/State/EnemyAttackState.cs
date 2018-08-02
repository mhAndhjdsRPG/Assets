using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState, IAttackState
{
    public AttackInfo AttackInfo => attackInfo;

    [Range(0, 1)]
    public float moveSpeed;

    [HideInInspector, SerializeField]
    public AttackInfo attackInfo;
    
    public Action<IAttackState> AttackStateExitEvent { get; set; }

    public ICharacter Owner => owner;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        Move(moveSpeed);

    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        owner.weaponManager.StopWeaponCheck();
        AttackStateExitEvent?.Invoke(this);
    }



}
