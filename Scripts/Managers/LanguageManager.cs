using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using UnityEngine.UI;

public class LanguageManager : Singleton<LanguageManager>
{
    /// <summary>
    /// 语言管理初始化
    /// </summary>
    public LanguageManager()
    {
        LoadLanguageToCache();
        CurLanguageType = (LanguageType)PlayerPrefs.GetInt(PlayerPrefsKeys.Setting_LanguageType);
    }

    /// <summary>
    /// Hash字典缓存
    /// </summary>
    private Dictionary<string, Hashtable> languageHashDic = new Dictionary<string, Hashtable>();
    /// <summary>
    /// 文本组建绑定Key的字典
    /// </summary>
    private Dictionary<Text, string> textBindKeyDic = new Dictionary<Text, string>();
    /// <summary>
    /// 字体字典
    /// </summary>
    private Dictionary<string, Font> fontDic = new Dictionary<string, Font>();


    private LanguageType curLanguageType;
    /// <summary>
    /// 当前语言类型
    /// </summary>
    public LanguageType CurLanguageType
    {
        get
        {
            return curLanguageType;
        }
        set
        {
            if (curLanguageType != value)
            {
                curLanguageType = value;
                UpdateAllText();
            }
        }
    }



    /// <summary>
    /// 绑定文本组建与字符串键
    /// </summary>
    /// <param name="text"></param>
    /// <param name="key"></param>
    public void BindKeyToText(Text text, string key)
    {
        //Debug.Log(CurLanguageType.ToString());
        //如果有当前语言类型对应语言种类的键
        if (languageHashDic.ContainsKey(CurLanguageType.ToString()))
        {
            //如果有对应key的字符串
            if (languageHashDic[CurLanguageType.ToString()].ContainsKey(key))
            {
                //如果该text已经被绑定
                if (textBindKeyDic.ContainsKey(text))
                {
                    textBindKeyDic[text] = key;
                }
                else
                {
                    textBindKeyDic.Add(text, key);
                }
                text.font = fontDic[CurLanguageType.ToString()];
                text.text = languageHashDic[CurLanguageType.ToString()][key].ToString();
            }
            else
            {
                throw new Exception("LanguageDic Not Contain This Key : " + key);
            }
        }
        else
        {
            throw new Exception("Language Xml Init Error");
        }
    }

    /// <summary>
    /// 解除文本的绑定
    /// </summary>
    /// <param name="text"></param>
    public void UnbindText(Text text)
    {
        if (textBindKeyDic.ContainsKey(text))
        {
            textBindKeyDic.Remove(text);
        }
        else
        {
            Debug.Log("text Not In textBindKeyDic");
        }
    }

    /// <summary>
    /// 资源释放
    /// </summary>
    public override void ReleaseCache()
    {
        base.ReleaseCache();
        languageHashDic.Clear();
        textBindKeyDic.Clear();
    }

    /// <summary>
    /// 更新所有Text组建
    /// </summary>
    private void UpdateAllText()
    {
        foreach (Text t in textBindKeyDic.Keys)
        {
            string key = textBindKeyDic[t];
            if (languageHashDic.ContainsKey(CurLanguageType.ToString()))
            {
                if (languageHashDic[CurLanguageType.ToString()].ContainsKey(key))
                {
                    t.text = languageHashDic[CurLanguageType.ToString()][textBindKeyDic[t]].ToString();
                    if (fontDic.ContainsKey(CurLanguageType.ToString()))
                    {
                        t.font = fontDic[CurLanguageType.ToString()];
                    }
                }
                else
                {
                    throw new Exception("LanguageDic Not Contain This Key : " + key);
                }
            }
            else
            {
                throw new Exception("Language Xml Init Error");
            }
        }
    }

    /// <summary>
    /// 加载Xml文件将信息存入对应哈希表（哈希表可从字典中获得）
    /// </summary>
    private void LoadLanguageToCache()
    {
        XmlDocument xmlDoc = new XmlDocument();
        string filePath = Application.dataPath + "/Resources/Xmls/Language/Language.xml";
        if (File.Exists(filePath))
        {
            xmlDoc.Load(filePath);
            XmlNode root = xmlDoc.SelectSingleNode("Language");
            XmlNodeList languageTypeNodes = root.ChildNodes;
            for (int i = 0; i < languageTypeNodes.Count; i++)
            {
                //如果字体字典中没有语言键
                if (!fontDic.ContainsKey(languageTypeNodes[i].Name))
                {
                    string fontName = (languageTypeNodes[i] as XmlElement).GetAttribute("font");
                    Font font = ResourcesManager.Instance.Load<Font>(FolderPaths.Fonts, fontName);
                    fontDic.Add(languageTypeNodes[i].Name, font);
                }
                else
                {
                    Debug.Log("The Font Has Been Loaded");
                }
                //Debug.Log(languageTypeNodes[i].Name);//English Chinese ...
                if (!languageHashDic.ContainsKey(languageTypeNodes[i].Name))
                {
                    languageHashDic.Add(languageTypeNodes[i].Name, new Hashtable());
                    XmlNodeList stringNodes = languageTypeNodes[i].SelectNodes("string");
                    for (int j = 0; j < stringNodes.Count; j++)
                    {
                        XmlElement xe = stringNodes[j] as XmlElement;
                        //Debug.Log(xe.FirstChild.Value + xe.GetAttribute("name"));
                        Hashtable hash = languageHashDic[languageTypeNodes[i].Name];
                        hash.Add(xe.GetAttribute("name"), xe.FirstChild.Value);
                    }
                }
                else
                {
                    Debug.Log("The Language Has Been Loaded");
                }
            }
        }
        else
        {
            Debug.Log("The File Dont Exists");
        }
    }

}