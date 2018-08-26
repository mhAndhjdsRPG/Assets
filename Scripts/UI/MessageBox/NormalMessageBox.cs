using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NormalMessageBox : MessageBox
{

    public override string Name
    {
        get
        {
            return "NormalMessageBox";
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        buttonsDic["Btn_Close"].onClick.AddListener(CloseThisWindow);

        LanguageManager.Instance.BindKeyToText(textDic["Text_Enter"], "enter");
        LanguageManager.Instance.BindKeyToText(textDic["Text_Cancel"], "cancel");
    }

    private void OnDestroy()
    {
        LanguageManager.Instance.UnbindText(textDic["Text_Enter"]);
        LanguageManager.Instance.UnbindText(textDic["Text_Cancel"]);
        LanguageManager.Instance.UnbindText(textDic["Text_Tip"]);
    }


    public void CloseThisWindow()
    {
        UIManager.Instance.CloseWindow(Name);
    }


    /// <summary>
    /// 设置标签
    /// </summary>
    /// <param name="stringKey"></param>
    public void SetTip(string stringKey)
    {
        LanguageManager.Instance.BindKeyToText(textDic["Text_Tip"], stringKey);
    }


}
