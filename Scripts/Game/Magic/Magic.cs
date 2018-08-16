using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MagicType
{
    stayTime,
    lockPos,
    lockTrans
}

//[RequireComponent(typeof(Rigidbody))]
public sealed class Magic : MonoBehaviour
{
    [Header("=====伤害信息=====")]
    public float loseHp;
    public float loseHard;

    [Header("=====生成物信息=====")]
    public GameObject startGo;
    public GameObject arriveGo;
    public GameObject destroyGo;
    public GameObject colliderGo;


    [Header("=====运动信息=====")]
    [SerializeField]
    private bool run;
    public bool destroyInTheEnd;
    public float stayTime;
    public int throughCount;
    public LayerMask collideLayer;
    [Range(0,1)]
    public float rotationLerpSpeed;

    [Space(5)]
    public AnimationCurve xoffsetCurve;
    public float xScale;

    [Space(5)]
    public AnimationCurve yoffsetCureve;
    public float yScale;

    [Space(5)]
    public AnimationCurve speedCurve;
    public float speedScale;

    [Space(5)]
    public MagicType magicType;




    /// <summary>
    /// 锁定专用
    /// </summary>
    [HideInInspector]
    public Transform targetTrans;
    [HideInInspector]
    public Vector3 targetPos;

    public float zDistanceToInverse;



    #region 运行时参数，不需要修改
    public int InverseSpeedSign { get; private set; }
    public float CurrentTime { get; private set; }
    public float TimePercent { get; private set; }

    Transform originTrans;
    Vector3 oldPos;

    public float TotalLenth
    {
        get
        {
            return Vector3.Distance(originTrans.position, targetPos);
        }
    }

    #endregion


    MagicBehavior behavior;

    private void Start()
    {
        behavior = GetComponent<MagicBehavior>();
    }


    private void Update()
    {
        if (!run)
        {
            return;
        }


        if (originTrans == null)
        {
            Begin();
        }

        CurrentTime += Time.deltaTime;

        if (CurrentTime > stayTime)
        {
            if (destroyInTheEnd)
            {
                DestroyMagic();
            }
            else
            {
                return;
            }
        }

        oldPos = transform.position;

        TimePercent = CurrentTime / stayTime;

        float xOffset = xoffsetCurve.Evaluate(TimePercent) * xScale;
        float yOffset = yoffsetCureve.Evaluate(TimePercent) * yScale;

        if (magicType == MagicType.lockTrans)
        {
            targetPos = targetTrans.position;
            originTrans.LookAt(targetPos);
        }

        if (magicType != MagicType.stayTime)
        {

            float zDistance = Vector3.Dot(targetPos - transform.position, originTrans.forward);
            
            int newInverseSign = (int)Mathf.Sign(zDistance);

            if (newInverseSign == -1 * InverseSpeedSign && Mathf.Abs(zDistance) > zDistanceToInverse)
            {
                InverseSpeedSign = newInverseSign;
            }
        }
        

        float speed = speedCurve.Evaluate(TimePercent) * speedScale*InverseSpeedSign;

        transform.position = GetPos(xOffset, yOffset, speed);

        transform.forward = Vector3.Lerp(transform.forward, transform.position - oldPos,rotationLerpSpeed);

        behavior?.OnMagicUpdate();
    }


    Vector3 GetPos(float xOffset,float yOffset,float speed)
    {
        float zOffset =Vector3.Dot(transform.position-originTrans.position,originTrans.forward) + speed * Time.deltaTime;

        return originTrans.position + xOffset * originTrans.right + yOffset * originTrans.up + zOffset * originTrans.forward;
    }

    public void Begin()
    {
        run = true;
        originTrans = new GameObject(transform.name + "Origin").transform;
        originTrans.position = transform.position;
        behavior?.OnStart();
        InverseSpeedSign = 1;
    }

    private void OnTriggerEnter(Collider collider)
    {
        int colliderLayer= 1 << collider.gameObject.layer;

        if ((collideLayer & colliderLayer) != 0)
        {
            throughCount--;
            behavior?.OnTriger(collider);

            if (throughCount > 1)
            {
                CreateGo(colliderGo);
            }
            else
            {
                DestroyMagic();
            }

        }


    }

    void CreateGo(GameObject gameObject)
    {
        if (gameObject != null)
        {
            Instantiate(gameObject, transform.position, Quaternion.identity);

        }
    }

    void DestroyMagic()
    {
        CreateGo(destroyGo);
        behavior?.OnMagicDestroy();
        Destroy(originTrans.gameObject);
        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {

        if (originTrans != null && magicType != MagicType.stayTime)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPos, 2);
        }
    }
}
