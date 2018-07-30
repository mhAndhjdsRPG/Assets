using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHero :ICharacter {

    public IMagic magic;

    public override CharacterType CharacterType
    {
        get
        {
            return CharacterType.Hero;
        }
    }

    public override string Name => "TestHero";

}
