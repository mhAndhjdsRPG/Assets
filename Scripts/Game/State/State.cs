using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : StateMachineBehaviour
{
    [Tooltip("如果不是敌人则不用添加")]
    public string[] canUseAttackInfoNames;

    public string stateName;

    public CharacterType characterType;

    protected InputManager input;

    protected Animator ani;

    protected CharacterController characterController;

    protected ICharacter owner;

    bool isInit = false;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        InitIfNeed(animator);

        owner.state = this;
    }



    void InitIfNeed(Animator animator)
    {
        if (isInit == false)
        {
            Init(animator);
            isInit = true;
        }
    }

    protected virtual void Init(Animator animator)
    {
        ani = animator;
        owner = ani.GetComponent<ICharacter>();
        if (owner.CharacterType != characterType)
        {
            throw new System.Exception(stateName + "初始化失败，请检查挂载对象的characterType是否匹配");
        }
        input = owner.input;
        characterController = owner.GetComponent<CharacterController>();
    }

    protected void MoveAndSetAnimParam()
    {
        ani.SetFloat("Horizontal", input.Horizontal);
        ani.SetFloat("Vertical", input.Vertical);
        Move();
    }


    protected virtual void Move(float modifySpeedRate = 1)
    {
        Vector3 moveDir = (input.Horizontal * owner.transform.right + input.Vertical * owner.transform.forward).normalized;
        moveDir *= SquareToCycleLenth(input.Horizontal, input.Vertical);
        characterController.SimpleMove(moveDir * owner.TotalAGL * modifySpeedRate);
    }




    protected virtual void Freeze()
    {
        ani.SetFloat("Horizontal", 0);
        ani.SetFloat("Vertical", 0);
        characterController.SimpleMove(Vector3.zero);
    }


    /// <summary>
    /// 键盘需要用这个将xy的输入转化为0到1的长度
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <returns></returns>
    public float SquareToCycleLenth(float X, float Y)
    {
        float powerLenth = X * X + Y * Y - (X * X * Y * Y);
        return Mathf.Sqrt(powerLenth);
    }

    protected List<string> GetTriggeringNames()
    {
        List<string> triggerNames=new List<string>();

        var names = input.GetPressingButtonNames();

        foreach (var name in names)
        {
            if (IsTrigger(name))
            {
                triggerNames.Add(name);
            }
        }

        return triggerNames;
    }


    protected void SetAnimTriggerParam()
    {
        foreach (var triggerName in GetTriggeringNames())
        {
            ani.SetTrigger(triggerName);
        } 
    }

    bool IsTrigger(string name)
    {
        foreach (var param in ani.parameters)
        {
            if (param.name == name && param.type == AnimatorControllerParameterType.Trigger)
            {
                return true;
            }
        }

        return false;
    }
}
