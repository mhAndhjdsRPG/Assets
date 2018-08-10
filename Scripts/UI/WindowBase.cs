using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public abstract class WindowBase : MonoBehaviour
{
    /// <summary>
    /// 窗口类型
    /// </summary>
    public WindowType windowType = WindowType.Undefine;

    /// <summary>
    /// 窗口背景Image组建
    /// </summary>
    protected Image windowBackground;


    #region ISelectable
    public List<Selectable> selectableList = new List<Selectable>();
    /// <summary>
    /// 按钮组建的列表
    /// </summary>
    public Dictionary<string, Button> buttonsDic = new Dictionary<string, Button>();
    /// <summary>
    /// 输入框字典
    /// </summary>
    public Dictionary<string, InputField> inputFieldDic = new Dictionary<string, InputField>();
    /// <summary>
    /// 滑动条
    /// </summary>
    public Dictionary<string, Slider> sliderDic = new Dictionary<string, Slider>();
    /// <summary>
    /// 拉选框
    /// </summary>
    public Dictionary<string, Dropdown> dropdownDic = new Dictionary<string, Dropdown>();
    /// <summary>
    /// 勾选框
    /// </summary>
    public Dictionary<string, Toggle> toggleDic = new Dictionary<string, Toggle>();
    #endregion

    #region Normal
    /// <summary>
    /// 文本字典
    /// </summary>
    public Dictionary<string, Text> textDic = new Dictionary<string, Text>();
    /// <summary>
    /// 图片字典
    /// </summary>
    public Dictionary<string, Image> imageDic = new Dictionary<string, Image>();
    #endregion

    /// <summary>
    /// 名字
    /// </summary>
    public abstract string Name { get; }


    //Use this for before initialization
    private void Awake()
    {
        OnAwake();
    }
    // Use this for initialization
    private void Start()
    {
        OnStart();
    }
    // Update is called once per frame
    private void Update()
    {
        OnUpdate();
    }


    /// <summary>
    /// 在Awake中调用的方法
    /// </summary>
    protected virtual void OnAwake()
    {
        Button[] buttons = transform.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null && !buttonsDic.ContainsKey(buttons[i].name))
            {
                buttonsDic.Add(buttons[i].name, buttons[i]);
                selectableList.Add(buttons[i]);
            }
        }
        InputField[] inputFields = transform.GetComponentsInChildren<InputField>();
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i] != null && !inputFieldDic.ContainsKey(inputFields[i].name))
            {
                inputFieldDic.Add(inputFields[i].name, inputFields[i]);
                selectableList.Add(inputFields[i]);
            }
        }
        Slider[] sliders = transform.GetComponentsInChildren<Slider>();
        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i] != null && !sliderDic.ContainsKey(sliders[i].name))
            {
                sliderDic.Add(sliders[i].name, sliders[i]);
                selectableList.Add(sliders[i]);
            }
        }
        Dropdown[] dropdowns = transform.GetComponentsInChildren<Dropdown>();
        for (int i = 0; i < dropdowns.Length; i++)
        {
            if (dropdowns[i] != null && !dropdownDic.ContainsKey(dropdowns[i].name))
            {
                dropdownDic.Add(dropdowns[i].name, dropdowns[i]);
                selectableList.Add(dropdowns[i]);
            }
        }
        Toggle[] toggles = transform.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i] != null && !toggleDic.ContainsKey(toggles[i].name))
            {
                toggleDic.Add(toggles[i].name, toggles[i]);
                selectableList.Add(toggles[i]);
            }
        }

        Text[] texts = transform.GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i] != null && !textDic.ContainsKey(texts[i].name))
            {
                textDic.Add(texts[i].name, texts[i]);
            }
        }
        Image[] images = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null && !imageDic.ContainsKey(images[i].name))
            {
                imageDic.Add(images[i].name, images[i]);
            }
        }
        //给窗口背景Image组建赋值
        windowBackground = transform.GetComponent<Image>();
    }
    /// <summary>
    /// 在Start中调用的方法
    /// </summary>
    protected virtual void OnStart() { }
    /// <summary>
    /// 在update中调用的方法
    /// </summary>
    protected virtual void OnUpdate() { }


}
