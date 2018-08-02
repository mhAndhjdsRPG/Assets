using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : ICharacter
{
    public override CharacterType CharacterType => CharacterType.Monster;


    public override string Name => "monster";

   
}
