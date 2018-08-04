using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatWindow : FixedWindow
{
    public Hero hero;
    public IMonster monster;

    public override string Name
    {
        get
        {
            return "CombatWindow";
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        hero.OnHPChange += ShowHp;
        monster.OnHPChange += ShowMonsterHp;
        
    }

   
    private void ShowHp(float curhp, float maxhp)
    {
        imageDic["Image_HpBar"].fillAmount = curhp / maxhp;
    }
    private void ShowMonsterHp(float curmonsterhp, float maxmonsterhp)
    {
        imageDic["Image_MonsterHpBar"].fillAmount = curmonsterhp / maxmonsterhp;
    }
}
