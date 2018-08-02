using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyState : State
{
    public string[] canUseAttackInfoNames;


    [Range(0, 1)]
    public float moveSpeed;

    [HideInInspector, SerializeField]
    public AttackInfo attackInfo;

    public Action<EnemyState> attackStateExitEvent;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        Move(moveSpeed);
        
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        owner.weaponManager.StopWeaponCheck();
        attackStateExitEvent?.Invoke(this);
    }




}
