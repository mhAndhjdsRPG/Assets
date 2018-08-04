using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class RangeChecker {

    protected const int DefualtLayer = int.MinValue;
    protected const Transform DefualtTrans = null;

    public int checkLayerMask;
    public Transform startTr;


    public RangeChecker()
    {

    }
    
    protected int currentCheckLayerMask;
    protected Transform currentStartTr;
    
    
    public RangeCheckType checkType;

    public abstract void DrawCheckArea();

    public List<Transform> Check(Transform transform = DefualtTrans, int layerMask = DefualtLayer)
    {
        //int currentCheckLayerMask = layerMask == DefualtLayer ? this.checkLayerMask : layerMask;
        //Transform currentStartTr = transform == DefualtTrans ? this.startTr : transform;

        return Check();
    }
    protected abstract List<Transform> Check();
}
