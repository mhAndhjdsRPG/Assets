using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatWindow : FixedWindow
{
    Hero hero;
  
    public override string Name
    {
        get
        {
            return "CombatWindow";
        }
    }

    protected override void OnAwake()
    {
        
    }

    private void Btn_0Click()
    {

    }
    private void Btn_1Click()
    {

    }
    private void Btn_2Click()
    {

    }
    private void ShowHp(float curhp,float maxhp)
    {
        imageDic["Image_HpBar"].fillAmount = curhp / maxhp;
    }
    private void ShowMonsterHp(float curmonsterhp,float maxmonsterhp)
    {
        imageDic["Image_MonsterHpBar"].fillAmount = curmonsterhp / maxmonsterhp;
    }
}
