using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IAttackState {
    Action<IAttackState> AttackStateExitEvent { get; set; }

    ICharacter Owner { get;}

    AttackInfo AttackInfo { get; }

}
