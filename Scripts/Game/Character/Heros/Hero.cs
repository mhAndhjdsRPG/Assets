using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InputManager))]
public class Hero : ICharacter
{
    
   
    public override CharacterType CharacterType
    {
        get
        {
            return CharacterType.Hero;
        }
    }

    public override string Name
    {
        get
        {
            return "";
        }
    }

    protected override void InitAttackInfoDic()
    {
        foreach (var state in ani.GetBehaviours<PlayerAttackState>())
        {
            attackInfoDic.Add(state.attackInfo.Name, state.attackInfo);
        }
    }
}
