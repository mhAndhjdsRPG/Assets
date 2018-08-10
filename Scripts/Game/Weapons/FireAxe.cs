using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAxe : IWeapon {
    public override WeaponType Type
    {
        get
        {
            return WeaponType.Axe;
        }
    }

    public override void OnAttack()
    {
    }

    public override void PlaySoundEffects()
    {
    }


}
