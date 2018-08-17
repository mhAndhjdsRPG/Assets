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
    public AnimationCurve scaleCurve;
    public float scaleCurveScale;

    [Space(5)]
    public MagicType magicType;




    /// <summary>
    /// 锁定专用
    /// </summary>
    [HideInInspector]
    public Transform targetTrans;
    [HideInInspector]
    public Vector3 targetPos;
    [HideInInspector]
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
       
        TimePercent = CurrentTime / stayTime;

        float xOffset = xoffsetCurve.Evaluate(TimePercent) * xScale;
        float yOffset = yoffsetCureve.Evaluate(TimePercent) * yScale;
        float currentScale = scaleCurve.Evaluate(TimePercent) * scaleCurveScale;

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

        float zOffset = GetZOffset(speed);

        if (magicType == MagicType.lockTrans)
        {
            targetPos = targetTrans.position;
            originTrans.LookAt(targetPos);
        }

        transform.position = GetPos(xOffset, yOffset, zOffset);

        ManageRotation(xOffset, yOffset, zOffset);

        transform.localScale = new Vector3(currentScale, currentScale, currentScale);

        behavior?.OnMagicUpdate();
    }

    void ManageRotation(float xOffset, float yOffset, float zOffset)
    {
        Vector3 currentPos = new Vector3(xOffset, yOffset, zOffset);
        Vector3 normalizedForward = currentPos - oldPos;
        Vector3 targetForward = normalizedForward.x * originTrans.right + normalizedForward.y * originTrans.up + normalizedForward.z * originTrans.forward;
        transform.forward = Vector3.Lerp(transform.forward, targetForward, rotationLerpSpeed);

        oldPos = currentPos;
    }


    float GetZOffset(float speed)
    {
        return  Vector3.Dot(transform.position - originTrans.position, originTrans.forward) + speed * Time.deltaTime;
    }


    Vector3 GetPos(float xOffset,float yOffset,float zOffset)
    {
        return originTrans.position + xOffset * originTrans.right + yOffset * originTrans.up + zOffset * originTrans.forward;
    }

    public void Begin()
    {
        run = true;
        if (originTrans == null)
        {
            originTrans = new GameObject(transform.name + "Origin").transform;
            originTrans.position = transform.position;
            behavior?.OnStart();
            InverseSpeedSign = 1;
        }
    }


    public void Stop()
    {
        run = false;
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
