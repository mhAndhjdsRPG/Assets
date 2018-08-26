using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        buttonsDic["Btn_InputHorizontal-"].onClick.AddListener(Btn_InputHorizontal0Click);
        buttonsDic["Btn_InputHorizontal+"].onClick.AddListener(Btn_InputHorizontal1Click);
        buttonsDic["Btn_InputVertical-"].onClick.AddListener(Btn_InputVertical0Click);
        buttonsDic["Btn_InputVertical+"].onClick.AddListener(Btn_InputVertical1Click);
        buttonsDic["Btn_InputAttack"].onClick.AddListener(Btn_InputAttackClick);
        buttonsDic["Btn_InputDodge"].onClick.AddListener(Btn_InputDodgeClick);
        buttonsDic["Btn_InputSwitch"].onClick.AddListener(Btn_InputSwitchClick);
        buttonsDic["Btn_InputTakeOrDrop"].onClick.AddListener(Btn_InputTakeOrDropClick);


        LanguageManager.Instance.BindKeyToText(textDic["Text_InputSetting"], "inputsetting");

        LanguageManager.Instance.BindKeyToText(textDic["Text_Horizontal"], "inputsetting_horzontal");
        LanguageManager.Instance.BindKeyToText(textDic["Text_Vertical"], "inputsetting_vertical");
        LanguageManager.Instance.BindKeyToText(textDic["Text_Attack"], "inputsetting_attack");
        LanguageManager.Instance.BindKeyToText(textDic["Text_Dodge"], "inputsetting_dodge");
        LanguageManager.Instance.BindKeyToText(textDic["Text_Switch"], "inputsetting_switch");
        LanguageManager.Instance.BindKeyToText(textDic["Text_TakeOrDrop"], "inputsetting_takeordrop");
        LanguageManager.Instance.BindKeyToText(textDic["Text_MouseReversal"], "inputsetting_mousereversal");
        LanguageManager.Instance.BindKeyToText(textDic["Text_MouseSensitivity"], "inputsetting_mousesensitivity");


        sliderDic["Slider_SFXVolume"].value = PlayerPrefs.GetFloat(PlayerPrefsKeys.Setting_SFXVolume);
        sliderDic["Slider_MusicVolume"].value = PlayerPrefs.GetFloat(PlayerPrefsKeys.Setting_MusicVolume);
        dropdownDic["Dropdown_Language"].value = PlayerPrefs.GetInt(PlayerPrefsKeys.Setting_LanguageType);
    }

    private void OnDestroy()
    {
        LanguageManager.Instance.UnbindText(textDic["Text_InputSetting"]);

        LanguageManager.Instance.UnbindText(textDic["Text_Horizontal"]);
        LanguageManager.Instance.UnbindText(textDic["Text_Vertical"]);
        LanguageManager.Instance.UnbindText(textDic["Text_Attack"]);
        LanguageManager.Instance.UnbindText(textDic["Text_Dodge"]);
        LanguageManager.Instance.UnbindText(textDic["Text_Switch"]);
        LanguageManager.Instance.UnbindText(textDic["Text_TakeOrDrop"]);
        LanguageManager.Instance.UnbindText(textDic["Text_MouseReversal"]);
        LanguageManager.Instance.UnbindText(textDic["Text_MouseSensitivity"]);
    }


    private void Btn_SaveClick()
    {
        NormalMessageBox normalMessageBox = (NormalMessageBox)UIManager.Instance.CreateOrShowWindow(WindowName.NormalMessageBox, transform);
        normalMessageBox.SetTip("setting_savetip");
        normalMessageBox.buttonsDic["Btn_Enter"].onClick.AddListener(SaveMsgBoxBtn_EnterClick);
        normalMessageBox.buttonsDic["Btn_Cancel"].onClick.AddListener(normalMessageBox.CloseThisWindow);
    }

    public void SaveMsgBoxBtn_EnterClick()
    {
        //设置语言
        LanguageManager.Instance.CurLanguageType = (LanguageType)(dropdownDic["Dropdown_Language"].value);
        PlayerPrefs.SetInt(PlayerPrefsKeys.Setting_LanguageType, dropdownDic["Dropdown_Language"].value);

        //设置背景音乐音量
        PlayerPrefs.SetFloat(PlayerPrefsKeys.Setting_MusicVolume, sliderDic["Slider_MusicVolume"].value);
        SoundManager.Instance.MusicVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.Setting_MusicVolume);

        //设置音效音量
        PlayerPrefs.SetFloat(PlayerPrefsKeys.Setting_SFXVolume, sliderDic["Slider_SFXVolume"].value);
        SoundManager.Instance.SFXVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.Setting_SFXVolume);


        UIManager.Instance.CloseWindow(WindowName.NormalMessageBox);
    }


    private void Btn_BackClick()
    {
        UIManager.Instance.HideWindow(Name);
    }




    #region SetInputButtonActions
    private void Btn_InputHorizontal0Click()
    {
        Btn_SetInputText("Text_InputHorizontal-");
    }
    private void Btn_InputHorizontal1Click()
    {
        Btn_SetInputText("Text_InputHorizontal+");
    }
    private void Btn_InputVertical0Click()
    {
        Btn_SetInputText("Text_InputVertical-");
    }
    private void Btn_InputVertical1Click()
    {
        Btn_SetInputText("Text_InputVertical+");
    }
    private void Btn_InputAttackClick()
    {
        Btn_SetInputText("Text_InputAttack");
    }
    private void Btn_InputDodgeClick()
    {
        Btn_SetInputText("Text_InputDodge");
    }
    private void Btn_InputTakeOrDropClick()
    {
        Btn_SetInputText("Text_InputTakeOrDrop");
    }
    private void Btn_InputSwitchClick()
    {
        Btn_SetInputText("Text_InputSwitch");
    }


    private void Btn_SetInputText(string textName)
    {
        StartCoroutine(BindKeyCode(textDic[textName]));
    }
    private IEnumerator BindKeyCode(Text text)
    {
        KeyCode bindKeyCode = KeyCode.None;
        LanguageManager.Instance.BindKeyToText(text, "inputsetting_readinput");
        while (true)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keycode))
                    {
                        bindKeyCode = keycode;
                        text.text = bindKeyCode.ToString();
                        LanguageManager.Instance.UnbindText(text);
                        break;
                    }
                }
            }
            if (bindKeyCode != KeyCode.None)
            {
                break;
            }
            yield return null;
        }
    }
    #endregion




}
