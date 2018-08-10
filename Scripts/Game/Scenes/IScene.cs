using UnityEngine;
using System.Collections;

public abstract class IScene
{
    public abstract string Name { get; }

    public abstract void OnSceneEnter();

    public abstract void OnSceneExit();

    public abstract void OnSceneStartLoad();
}
