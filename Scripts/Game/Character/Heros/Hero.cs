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

   

}
