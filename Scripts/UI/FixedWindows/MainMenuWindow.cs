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
        buttonsDic["Btn_Achievement"].onClick.AddListener(Btn_AchievementClick);
        imageDic["Image_Shade"].raycastTarget = false;
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


    public void Btn_AchievementClick()
    {
        Debug.Log("Btn_ShopClick");
    }

    public void Btn_StartGameClick()
    {
        Debug.Log("Btn_StartGameClick");
    }

    public void Btn_QuitClick()
    {
        Debug.Log("Btn_QuitClick");
    }

}
