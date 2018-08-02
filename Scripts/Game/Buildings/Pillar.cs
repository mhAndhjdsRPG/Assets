using UnityEngine;
using System.Collections;

public class Pillar : IBuilding
{
    public override string Name
    {
        get
        {
            return "Pillar";
        }
    }

    public override float Weight
    {
        get
        {
            return CannotWalkThrough;
        }
    }

    protected override void InitAttributes()
    {
        HP = Random.Range(30, 60);
    }

    protected override void OnAwake() { }

    protected override void OnFixedUpdate() { }

    protected override void OnStart() { }

    protected override void OnUpdate() { }

    protected override void OnBroken()
    {

    }
}
