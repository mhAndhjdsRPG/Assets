using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SectorChecker : RangeChecker
{
    public float angle, radius;

    public SectorChecker(float radius, float angle)
    {
        checkType = RangeCheckType.Sector;
        this.radius = radius;
        this.angle = angle;
    }

    public SectorChecker(float radius, float angle, int checkLayerMask):this(radius,angle)
    {
        this.checkLayerMask = checkLayerMask;
    }

    public SectorChecker(float radius, float angle, int checkLayerMask, Transform startTrans) : this(radius, angle, checkLayerMask) 
    {
        startTr = startTrans;
    }


    protected override List<Transform> Check()
    {
       
        Collider[] colliders = Physics.OverlapSphere(currentStartTr.position, radius, currentCheckLayerMask);

        List<Transform> beHitedTrans = new List<Transform>();

        foreach (Collider col in colliders)
        {

            float angleBetween = Vector3.Angle(currentStartTr.forward, col.transform.position - currentStartTr.position);

            if (angleBetween < angle / 2)
            {
                beHitedTrans.Add(col.transform);
            }
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

        Vector3 dirLeft = Quaternion.AngleAxis(-angle / 2, currentStartTr.up) * currentStartTr.forward;
        Vector3 dirRight = Quaternion.AngleAxis(angle / 2, currentStartTr.up) * currentStartTr.forward;
        Vector3 dirTop = Quaternion.AngleAxis(angle / 2, currentStartTr.right) * currentStartTr.forward;
        Vector3 dirBottom = Quaternion.AngleAxis(-angle / 2, currentStartTr.right) * currentStartTr.forward;

        Gizmos.DrawLine(currentStartTr.position, currentStartTr.position + dirLeft * radius);
        Gizmos.DrawLine(currentStartTr.position, currentStartTr.position + dirRight * radius);
        Gizmos.DrawLine(currentStartTr.position, currentStartTr.position + dirTop * radius);
        Gizmos.DrawLine(currentStartTr.position, currentStartTr.position + dirBottom * radius);
        Gizmos.DrawWireSphere(currentStartTr.position, radius);
    }
}
