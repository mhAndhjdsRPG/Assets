using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : State
{
    Vector3 rollDir;
    Vector2 blendParam;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);

        rollDir = (input.Horizontal * owner.transform.right + input.Vertical * owner.transform.forward).normalized;
        blendParam = BuildBlendParam();

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
        characterController.SimpleMove(rollDir * owner.TotalAGL);
        ani.SetFloat("Horizontal", blendParam.x);
        ani.SetFloat("Vertical", blendParam.y);
    }

    Vector2 BuildBlendParam()
    {
        Vector2 blendParam = new Vector2(input.horizontal,input.vertical).normalized;

        if (blendParam == Vector2.zero)
        {
            return Vector2.zero;
        }

        float longerSide= Mathf.Abs(blendParam.x) > Mathf.Abs(blendParam.y) ? Mathf.Abs(blendParam.x) : Mathf.Abs(blendParam.y);

        //blendParam = blendParam * (1 / longerSide);
        blendParam = blendParam / longerSide;

        return blendParam;
    }

}
