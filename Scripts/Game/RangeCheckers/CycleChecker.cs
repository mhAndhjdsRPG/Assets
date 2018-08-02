using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CycleChecker : RangeChecker
{
    public float radius;

    public CycleChecker(float radius)
    {
        checkType = RangeCheckType.Cycle;
        this.radius = radius;
    }

    public CycleChecker(float radius, int checkLayerMask):this(radius)
    {
        this.checkLayerMask = checkLayerMask;
    }

    public CycleChecker(float radius, int checkLayerMask, Transform startTrans) : this(radius, checkLayerMask)
    {
        startTr = startTrans;
    }
    

    protected override List<Transform> Check()
    {
        
        Collider[] colliders = Physics.OverlapSphere(currentStartTr.position, radius,currentCheckLayerMask);

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


        Gizmos.DrawWireSphere(currentStartTr.position, radius);

    }
}
