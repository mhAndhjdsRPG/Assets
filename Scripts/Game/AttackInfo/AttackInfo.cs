using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class AttackInfo
{
    public AttackInfo(string name, ICharacter owner, RangeChecker rangeChecker, float coolDown, string inputStr)
    {
        Name = name;
        Owner = owner;
        RangeChecker = rangeChecker;
        CoolDown = coolDown;
        InputStr = inputStr;
    }

    [SerializeField]
    private string name;
    [SerializeField]
    private ICharacter owner;
    [SerializeField]
    private RangeChecker rangeChecker;
    [SerializeField]
    private bool canUse = true;
    [SerializeField]
    private float coolDown = 0f;
    [SerializeField]
    private string inputStr = "";
    [SerializeField]
    private float damageRate = 1f;
    [SerializeField]
    private float decreaseHard = 0;

    public float CoolDown
    {
        get
        {
            return coolDown;
        }

        set
        {
            coolDown = value;
        }
    }

    public RangeChecker RangeChecker
    {
        get
        {
            return rangeChecker;
        }

        set
        {
            rangeChecker = value;
        }
    }

    public ICharacter Owner
    {
        get
        {
            return owner;
        }

        set
        {
            owner = value;
        }
    }

    public string InputStr
    {
        get
        {
            return inputStr;
        }

        set
        {
            inputStr = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public bool CanUse
    {
        get
        {
            return canUse;
        }

        set
        {
            canUse = value;
        }
    }

    public float DamageRate
    {
        get
        {
            return damageRate;
        }

        set
        {
            damageRate = value;
        }
    }

    public float DecreaseHard
    {
        get
        {
            return decreaseHard;
        }

        set
        {
            decreaseHard = value;
        }
    }
}
