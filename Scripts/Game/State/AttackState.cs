using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Linq;

public class AttackState : State
{

    [Range(0, 1)]
    public float moveSpeed;

    [SerializeField]
    private bool haveModifier;
    public bool HaveModifier => haveModifier;

    [HideInInspector, SerializeField]
    public AttackInfo attackInfo;

    #region 跳转相关参数

    string needTriggerNameAfterCanJump;

    bool isStoreTrigger;

    #endregion


    public Action<AttackState> AttackStateExitEvent { get; set; }
    public ICharacter Owner => owner;
    public AttackInfo AttackInfo => attackInfo;
   
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
        AttackStateExitEvent?.Invoke(this);
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
            needTriggerNameAfterCanJump = string.Empty;
        }

    }

    protected override void Init(Animator animator)
    {
        base.Init(animator);
        if (haveModifier)
        {
            
        }
    }


}
