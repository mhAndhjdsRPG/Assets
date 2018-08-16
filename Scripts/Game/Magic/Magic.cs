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
public class Magic : MonoBehaviour
{
    [Space(5)]
    public float stayTime;
    public bool run;

    [Space(5)]
    public AnimationCurve xoffsetCurve;
    public float xScale;

    [Space(5)]
    public AnimationCurve yoffsetCureve;
    public float yScale;

    [Space(5)]
    public MagicType magicType;


    /// <summary>
    /// stayTime类型专用
    /// </summary>
    [HideInInspector]
    public AnimationCurve speedCurve;
    [HideInInspector]
    public float speedScale;

    /// <summary>
    /// 锁定专用
    /// </summary>
    [Tooltip("与目标点的距离1为在原点，0为到达目标点"), HideInInspector]
    public AnimationCurve distanceFromTargetCurve;
    [HideInInspector]
    public Transform targetTrans;
    private Vector3 targetPos;







    #region 运行时参数，不需要修改

    public float CurrentTime { get; private set; }
    public float TimePercent { get; private set; }

    float xOffset;
    float yOffset;

    Transform originTrans;

    public float TotalLenth
    {
        get
        {
            transform.LookAt(targetPos);
            return Vector3.Distance(originTrans.position, targetPos);
        }
    }

    #endregion

    private void Update()
    {
        if (!run)
        {
            return;
        }


        if (originTrans == null)
        {
            originTrans = new GameObject(transform.name + "Origin").transform;
            originTrans.position = transform.position;
        }

        CurrentTime += Time.deltaTime;

        if (CurrentTime > stayTime)
        {
            return;
        }

        TimePercent = CurrentTime / stayTime;

        xOffset = xoffsetCurve.Evaluate(TimePercent) * xScale;
        yOffset = yoffsetCureve.Evaluate(TimePercent) * yScale;


        switch (magicType)
        {
            case MagicType.stayTime:
                float speed = speedCurve.Evaluate(TimePercent)*speedScale;
                transform.position = GetPosWithZSpeed(xOffset, yOffset, speed);
                break;
            case MagicType.lockPos:
                MoveWithTargetPos();
                break;
            case MagicType.lockTrans:
                targetPos = targetTrans.position;
                MoveWithTargetPos();
                break;
            default:
                break;
        }

    }

    void MoveWithTargetPos()
    {
        float distanceToTarget = distanceFromTargetCurve.Evaluate(TimePercent);
        transform.position = GetPosWithZDistanceToTarget(xOffset, yOffset, distanceToTarget);
    }



    Vector3 GetPosWithZSpeed(float xOffset, float yOffset, float speed)
    {
        float zOffset = Vector3.Dot(transform.position - originTrans.position, originTrans.forward) + speed * Time.deltaTime;
        
        return originTrans.position + xOffset * originTrans.up + yOffset * originTrans.right + zOffset * originTrans.forward;
    }

    Vector3 GetPosWithZDistanceToTarget(float xOffset, float yOffset, float distanceToTarget)
    {
        return originTrans.position + xOffset * originTrans.right + yOffset * originTrans.up + (1 - distanceToTarget) * TotalLenth * originTrans.forward;
    }


    private void OnDrawGizmos()
    {

        if (originTrans != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPos, 2);
        }
    }
}
