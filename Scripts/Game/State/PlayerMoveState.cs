using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState :PlayerState {

    public bool canMove;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (canMove)
        {
            MoveAndSetAnimParam();
        }
        else
        {
            Freeze();
        }

        SetAnimTriggerParam();
       

    }

    

   

}
