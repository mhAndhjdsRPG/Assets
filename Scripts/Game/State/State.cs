using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : StateMachineBehaviour
{

    public string stateName;

    public ICharacter owner;

    protected InputManager input;

    protected Animator ani;

    protected CharacterController characterController;

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
        input = owner.input;
        characterController = owner.GetComponent<CharacterController>();
    }

    protected  void MoveAndSetAnimParam()
    {
        ani.SetFloat("horizontal", input.Horizontal);
        ani.SetFloat("vertical", input.Vertical);
        Move();
    }


    protected virtual void Move(float modifySpeedRate=1)
    {
        Vector3 moveDir = (input.Horizontal * owner.transform.right + input.Vertical * owner.transform.forward).normalized;
        moveDir *= SquareToCycleLenth(input.Horizontal, input.Vertical);
        characterController.SimpleMove(moveDir * owner.AGL*modifySpeedRate);
    }


    

    protected virtual void Freeze()
    {
        ani.SetFloat("horizontal", 0);
        ani.SetFloat("vertical", 0);
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

    protected void SetAnimTriggerParam()
    {
        var names = GetTriggeringNames();
        foreach (string name in names)
        {
            ani.SetTrigger(name);
        }
    }

    protected List<string> GetTriggeringNames()
    {
        List<string> names = new List<string>();

        if (input.inputBoolDic["fire1"])
        {
            names.Add("fire1");
        }

        if (input.inputBoolDic["fire2"])
        {
            names.Add("fire2");
        }

        if (input.inputBoolDic["fire3"])
        {
            names.Add("fire3");
        }

        if (input.inputBoolDic["fire4"])
        {
            names.Add("fire4");
        }

        if (input.inputBoolDic["roll"])
        {
            names.Add("roll");
        }

        if (input.inputBoolDic["switch"])
        {
            names.Add("switch");
        }

        return names;

    }





}
