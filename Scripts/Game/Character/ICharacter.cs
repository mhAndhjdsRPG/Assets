using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager),typeof(WeaponManager))]
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

    [HideInInspector]
    public Animator ani;
    

    #region 角色属性
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
            maxHP = value;
            OnMaxHPChange?.Invoke(HP, maxHP);
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
            hp = value;
            OnHPChange?.Invoke(hp, MaxHP);
        }
    }


    public OnFloatChange OnATKChange;
    [SerializeField, HideInInspector]
    private float atk;
    public float ATK
    {
        get
        {
            return atk;
        }

        set
        {
            OnATKChange?.Invoke(atk, value);
            atk = value;
        }
    }

    public OnFloatChange OnAGLChange;
    [SerializeField, HideInInspector]
    private float agl;
    public float AGL
    {
        get
        {
            return agl;
        }
        set
        {
            OnAGLChange?.Invoke(agl, value);
            agl = value;
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
            OnHardChange?.Invoke(hard, value);
            hard = value;
        }
    }

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


    /// <summary>
    /// 名字访问器
    /// </summary>
    public abstract string Name
    {
        get;
    }

   
    #endregion

    
    public Dictionary<string, AttackInfo> skillInfoDic = new Dictionary<string, AttackInfo>();



    protected virtual void Awake()
    {
        ani = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        //需要在start中调用GetBehavior保证动画对象正确初始化
        foreach (AttackState state in ani.GetBehaviours<AttackState>())
        {
            skillInfoDic.Add(state.attackInfo.Name, state.attackInfo);
            print(skillInfoDic[state.attackInfo.Name].Name);
        }
    }


    protected virtual void Update()
    {
        
    }


   
}
