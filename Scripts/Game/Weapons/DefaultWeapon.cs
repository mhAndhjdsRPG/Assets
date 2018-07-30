using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : IWeapon
{
    public override WeaponType Type => WeaponType.arm;

    public override void OnAttack()
    {
        
    }

    public override void PlaySoundEffects()
    {
        
    }
}
