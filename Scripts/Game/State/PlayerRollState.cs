using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    Vector3 rollDir;
    Vector2 blendParam;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);

        rollDir = (input.Horizontal * owner.transform.right + input.Vertical * owner.transform.forward).normalized;
        blendParam = BuildBlendParam(rollDir);

        owner.attackCalculator.canGetHurt = false;
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        Roll();
        
    }



    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        owner.attackCalculator.canGetHurt = true;
    }


    void Roll()
    {
        characterController.SimpleMove(rollDir * owner.AGL);
        ani.SetFloat("horizontal", blendParam.x);
        ani.SetFloat("vertical", blendParam.y);
    }

    Vector2 BuildBlendParam(Vector3 rollDir)
    {
        if (rollDir == Vector3.zero)
        {
            return Vector2.zero;
        }

        if (Mathf.Abs(rollDir.x) > Mathf.Abs(rollDir.z))
        {
            return new Vector2(Mathf.Sign(rollDir.x), rollDir.z/ rollDir.x);
        }
        else
        {
            return new Vector2(rollDir.x / rollDir.z, Mathf.Sign(rollDir.z));
        }
    }
    
}
