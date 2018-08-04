using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWindow : FixedWindow
{
    public override string Name
    {
        get
        {
            return "MainMenuWindow";
        }
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        buttonsDic["Btn_StartGame"].onClick.AddListener(Btn_StartGameClick);
        buttonsDic["Btn_Quit"].onClick.AddListener(Btn_QuitClick);
        buttonsDic["Btn_Setting"].onClick.AddListener(Btn_SettingClick);
    }



    private enum DivideType
    {
        Horizontal,
        Vertical,
    }


    private void Btn_SettingClick()
    {
        UIManager.Instance.CreateOrShowWindow(WindowName.SettingWindow, UIManager.Instance.Canvas);
    }

    private void Btn_ShopClick()
    {

    }

    private void Btn_StartGameClick()
    {

    }

    private void Btn_QuitClick()
    {
        Application.Quit();
    }

}
