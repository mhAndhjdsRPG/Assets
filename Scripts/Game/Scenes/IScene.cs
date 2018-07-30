using UnityEngine;
using System.Collections;

public abstract class IScene
{
    public abstract string Name { get; }

    public abstract void Update();

    public abstract void OnSceneEnter();

    public abstract void OnSceneExit();
}
