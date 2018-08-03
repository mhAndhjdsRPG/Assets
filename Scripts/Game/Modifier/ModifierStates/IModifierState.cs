using UnityEngine;
using System.Collections;

public abstract class IModifierState
{
    private const float NotUpdate = -1f;
    public abstract string Name { get; }

    public float duration = NotUpdate;
    public float interval;


    public abstract void OnCreate();
    public abstract void OnUpdate();
    public abstract void OnDestroy();
}
