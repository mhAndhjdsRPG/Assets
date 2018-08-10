using System;
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
        buttonsDic["Btn_Back"].onClick.AddListener(Btn_BackClick);
        buttonsDic["Btn_Save"].onClick.AddListener(Btn_SaveClick);
    }

    private void Btn_SaveClick()
    {
        UIManager.Instance.HideWindow(Name);
    }

    private void Btn_BackClick()
    {
        UIManager.Instance.HideWindow(Name);
    }

}
