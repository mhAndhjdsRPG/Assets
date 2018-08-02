using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxCastChecker : RangeChecker
{
    public float width, distance;


    public BoxCastChecker(float width, float distance)
    {
        checkType = RangeCheckType.BoxCast;
        this.width = width;
        this.distance = distance;
    }

    public BoxCastChecker(float width, float distance, int checkLayerMask):this(width,distance)
    {
        this.checkLayerMask = checkLayerMask;
    }

    public BoxCastChecker(float width, float distance, int checkLayerMask, Transform startTrans) : this(width,distance,checkLayerMask)
    {
        this.startTr = startTrans;
    }
    
    protected override List<Transform> Check()
    {
        Quaternion rotation = currentStartTr.rotation;
        Collider[] colliders = Physics.OverlapBox(currentStartTr.position + currentStartTr.forward * (distance / 2), new Vector3(width / 2, width / 2, distance / 2), rotation,currentCheckLayerMask);
        List<Transform> beHitedTrans = new List<Transform>();
        foreach (Collider col in colliders)
        {
            beHitedTrans.Add(col.transform);
        }
        if (beHitedTrans == null)
        {
            beHitedTrans = new List<Transform>();
        }
        return beHitedTrans;
    }


    public override void DrawCheckArea()
    {
        Gizmos.color = Color.red;

        Vector3 rightHightPoint = currentStartTr.position + currentStartTr.right * width / 2 + currentStartTr.up * width / 2;
        Vector3 rightLowPoint = currentStartTr.position + currentStartTr.right * width / 2 - currentStartTr.up * width / 2;
        Vector3 leftHightPoint = currentStartTr.position - currentStartTr.right * width / 2 + currentStartTr.up * width / 2;
        Vector3 leftLowPoint = currentStartTr.position - currentStartTr.right * width / 2 - currentStartTr.up * width / 2;

        Vector3 farRightHightPoint = currentStartTr.position + currentStartTr.right * width / 2 + currentStartTr.up * width / 2 + currentStartTr.forward * distance;
        Vector3 farRightLowPoint = currentStartTr.position + currentStartTr.right * width / 2 - currentStartTr.up * width / 2 + currentStartTr.forward * distance;
        Vector3 farLeftHightPoint = currentStartTr.position - currentStartTr.right * width / 2 + currentStartTr.up * width / 2 + currentStartTr.forward * distance;
        Vector3 farLeftLowPoint = currentStartTr.position - currentStartTr.right * width / 2 - currentStartTr.up * width / 2 + currentStartTr.forward * distance;

        Gizmos.DrawLine(rightHightPoint, leftHightPoint);
        Gizmos.DrawLine(leftHightPoint, leftLowPoint);
        Gizmos.DrawLine(leftLowPoint, rightLowPoint);
        Gizmos.DrawLine(rightLowPoint, rightHightPoint);


        Gizmos.DrawLine(farRightHightPoint, farLeftHightPoint);
        Gizmos.DrawLine(farLeftHightPoint, farLeftLowPoint);
        Gizmos.DrawLine(farLeftLowPoint, farRightLowPoint);
        Gizmos.DrawLine(farRightLowPoint, farRightHightPoint);


        Gizmos.DrawLine(rightHightPoint, farRightHightPoint);
        Gizmos.DrawLine(rightLowPoint, farRightLowPoint);

        Gizmos.DrawLine(leftHightPoint, farLeftHightPoint);
        Gizmos.DrawLine(leftLowPoint, farLeftLowPoint);
    }
}
