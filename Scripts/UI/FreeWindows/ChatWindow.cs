using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;

public class ChatWindow : FreeWindow {

    public override string Name
    {
        get
        {
            return "ChatWindow";
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        buttonsDic["Btn_Send"].onClick.AddListener(SendBtnClick);

    }

  
   
    private void SendBtnClick()
    {
        ChatNetController.Instance.SendBtnClick(inputFieldDic["InputField_Send"].text);
    }


    private void Update()
    {
        textDic["Text_ShowMessage"].text = ChatNetController.Instance.reveStr;
    }
}
