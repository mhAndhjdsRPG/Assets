using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof(WeaponManager))]
public abstract class ICharacter : MonoBehaviour
{
    /// <summary>
    /// float类型变化的委托
    /// </summary>
    /// <param name="beforeValue"></param>
    /// <param name="afterValue"></param>
    public delegate void OnFloatChange(float beforeValue, float afterValue);
    /// <summary>
    /// 角色类型
    /// </summary>
    public abstract CharacterType CharacterType { get; }

    public InputManager input;

    public WeaponManager weaponManager;

    public State state;

    public AttackCalculator attackCalculator;

    [Header("===== 部位 =====")]
    public Transform head;
    public Transform origin;
    public Transform chest;


    [HideInInspector]
    public Animator ani;
    
    #region 角色属性

    #region Hp

    public bool notGetHurt;


    public StateImplementionProvider stateImplemention;

    public OnFloatChange OnMaxHPChange;
    [SerializeField, HideInInspector]
    private float maxHP;
    public float MaxHP
    {
        get
        {
            return maxHP;
        }

        set
        {
            OnMaxHPChange?.Invoke(HP, maxHP);
            maxHP = value;
        }
    }



    public OnFloatChange OnHPChange;
    [SerializeField, HideInInspector]
    private float hp;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            if (notGetHurt && value < hp)
            {
                return;
            }
            else
            {
                OnHPChange?.Invoke(hp, value);
                hp = value;
            }
        }
    }

    #endregion


    #region Atk

    [SerializeField, HideInInspector]
    private float baseAtk;
    public float BaseATK
    {
        get { return baseAtk; }

        set
        {
            baseAtk = value;
            TotalAtk = CalculateTotalValue(BaseATK, AddAtk, MultipleAtk);
        }
    }


    [SerializeField, HideInInspector]
    private float addAtk;
    public float AddAtk
    {
        get { return addAtk; }

        set
        {
            addAtk = value;
            TotalAtk = CalculateTotalValue(BaseATK, AddAtk, MultipleAtk);
        }
    }

    [SerializeField, HideInInspector]
    private float multipleAtk;
    public float MultipleAtk
    {
        get { return multipleAtk; }

        set
        {
            multipleAtk = value;
            TotalAtk = CalculateTotalValue(BaseATK, AddAtk, MultipleAtk);
        }
    }


    public OnFloatChange OnATKChange;
    [SerializeField, HideInInspector]
    private float totalAtk;
    public float TotalAtk
    {
        get { return totalAtk; }

        private set
        {
            OnAGLChange?.Invoke(totalAtk, value);
            totalAtk = value;
        }
    }


    #endregion


    #region Agl

    [SerializeField, HideInInspector]
    private float baseAgl;
    public float BaseAgl
    {
        get { return baseAgl; }
        set
        {
            baseAgl = value;
            TotalAGL = CalculateTotalValue(BaseAgl, AddAgl, MultipleAgl);
        }
    }


    [SerializeField, HideInInspector]
    private float addAgl;
    public float AddAgl
    {
        get { return addAgl; }

        set
        {
            addAgl = value;
            TotalAGL = CalculateTotalValue(BaseAgl, AddAgl, MultipleAgl);
        }
    }


    [SerializeField, HideInInspector]
    private float multipleAgl;
    public float MultipleAgl
    {
        get { return multipleAgl; }
        set
        {
            multipleAgl = value;
            TotalAGL = CalculateTotalValue(BaseAgl, AddAgl, MultipleAgl);
        }
    }



    public OnFloatChange OnAGLChange;
    [SerializeField, HideInInspector]
    private float totalAgl;
    public float TotalAGL
    {
        get
        {
            return totalAgl;
        }
        private set
        {
            OnAGLChange?.Invoke(totalAgl, value);
            totalAgl = value;
        }
    }

    #endregion

    #region Hard

    [HideInInspector]
    public float recoverHardPerTime;

    [HideInInspector]
    public float waitSeoncondForRecoverHard;

    [SerializeField, HideInInspector]
    private float maxHard;
    public float MaxHard
    {
        get { return maxHard; }
        set
        {
            float currenHardPercent =maxHard==0?0:hard / maxHard;
            maxHard = value;
            Hard = currenHardPercent * maxHard;
        }
    }

    public OnFloatChange OnHardChange;
    [SerializeField, HideInInspector]
    private float hard;
    public float Hard
    {
        get
        {
            return hard;
        }

        set
        {
            value = value > MaxHard ? MaxHard : value;
            OnHardChange?.Invoke(hard, value);
            hard = value;
            if (hard <= 0&&maxHard>0)
            {
                ani.SetTrigger("Injured");
                hard = MaxHard;
            }
        }
    }
   
    #endregion


    #region Dodge




    /// <summary>
    /// 闪避冷却
    /// </summary>
    [SerializeField, HideInInspector]
    private float dodgeCoolDown;
    /// <summary>
    /// 闪避冷却
    /// </summary>
    public float DodgeCoolDown
    {
        get
        {
            return dodgeCoolDown;
        }
        set
        {
            dodgeCoolDown = value;
        }
    }
    private bool canDodge = true;
    /// <summary>
    /// 能否闪避的布尔值
    /// </summary>
    public bool CanDodge
    {
        get { return canDodge; }
        set { canDodge = value; }
    }

    /// <summary>
    /// 冷却开始计时
    /// </summary>
    /// <returns></returns>
    public IEnumerator DodgeCoolDownStart()
    {
        CanDodge = false;
        yield return new WaitForSeconds(DodgeCoolDown);
        CanDodge = true;
    }

    #endregion

    /// <summary>
    /// 名字访问器
    /// </summary>
    public abstract string Name
    {
        get;
    }

    static float CalculateTotalValue(float baseValue, float addValue, float multipleValue)
    {
        return (baseValue + addValue) * multipleValue;
    }



    #endregion

    

    public Dictionary<string, AttackInfo> attackInfoDic = new Dictionary<string, AttackInfo>();

    protected virtual void Awake()
    {
        ani = GetComponent<Animator>();
        stateImplemention = new SampleImplementProvider(this);
    }

    protected virtual void Start()
    {
        InitAttackInfoDic();
        StartCoroutine(RecoverHardIfNeed());
    }


    protected virtual void Update()
    {
        UpdateModifier?.Invoke();
    }

    /// <summary>
    /// init the AttackInfoDic,remember that calling this in Start not Awake
    /// cause the animator's GetBehaviours need init in Awake
    /// </summary>
    private void InitAttackInfoDic()
    {
        foreach (var state in ani.GetBehaviours<AttackState>())
        {
            attackInfoDic.Add(state.attackInfo.Name, state.attackInfo);
        }
    }


    #region modifier相关

    #region 接受modifier

    public void ReceiveModifier(string modifierNameInXml, float waitSecondStart = 0)
    {
        Modifier modifier = ModifierManager.Instance.GetModifier(modifierNameInXml, this);
        StartCoroutine(WaitForStartModifier(modifier, waitSecondStart));

    }


    public void ReceiveModifier(Modifier modifier, float waitSecondStart = 0)
    {
        StartCoroutine(WaitForStartModifier(modifier, waitSecondStart));
    }


    IEnumerator WaitForStartModifier(Modifier modifier, float waitSecond)
    {
        yield return new WaitForSeconds(waitSecond);
        modifier.Start();
    }
    #endregion

    #region Modifier事件

    public Action UpdateModifier;

    #endregion

    #endregion


    IEnumerator RecoverHardIfNeed()
    {
        while (true)
        {
            if (Hard <= MaxHard)
            {
                Hard += recoverHardPerTime;
                yield return new WaitForSeconds(waitSeoncondForRecoverHard);
            }
        }
    }

}





