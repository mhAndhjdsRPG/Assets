using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : State
{

    public Action<AttackState> attackStateExitEvent;

    public string[] canUseAttackInfoNames;

    [Range(0, 1)]
    public float moveSpeed;

    [HideInInspector, SerializeField]
    public AttackInfo attackInfo;


    string needTriggerNameAfterCanJump;

    bool isStoreTrigger;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        isStoreTrigger = true;
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        Move(moveSpeed);


        if (isStoreTrigger)
        {
            var names = GetTriggeringNames();

            if (names.Count != 0)
            {
                needTriggerNameAfterCanJump = names[0];
            }
        }
        else
        {
            SetAnimTriggerParam();
        }

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        owner.weaponManager.StopWeaponCheck();
        attackStateExitEvent?.Invoke(this);
    }


    public void SetAnimCanJumpToOthers()
    {
        isStoreTrigger = false;
        SetJumpTriggerAfterJumpEvent();
    }

    void SetJumpTriggerAfterJumpEvent()
    {
        if (!string.IsNullOrEmpty(needTriggerNameAfterCanJump))
        {
            ani.SetTrigger(needTriggerNameAfterCanJump);
        }

    }




}
