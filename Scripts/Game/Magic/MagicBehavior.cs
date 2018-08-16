using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBehavior : MonoBehaviour
{
    public virtual void OnStart() { }

    public virtual void OnTriger(Collider collider) { }

    public virtual void OnArrive() { }

    public virtual void OnMagicDestroy() { }

    public virtual void OnMagicUpdate() { }


}
