using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soul : IMonster
{
    public ICharacter player;

   
    public override string Name
    {
        get
        {
            return "Soul";
        }
    }
    
}
