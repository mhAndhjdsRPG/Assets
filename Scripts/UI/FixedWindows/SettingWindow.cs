using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingWindow : FixedWindow
{
    public override string Name
    {
        get
        {
            return "SettingWindow";
        }
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        
        for (int i = 0; i < buttonsList.Count; i++)
        {
            switch (buttonsList[i].name)
            {
                case "Btn_Exit":
                    buttonsList[i].onClick.AddListener(Btn_ExitClick);
                    break;
                case "Btn_Save":
                    buttonsList[i].onClick.AddListener(Btn_SaveClick);
                    break;
               
            }
        }
    }
    private void Btn_ExitClick()
    {

    }
    private void Btn_SaveClick()
    {

    }

}
