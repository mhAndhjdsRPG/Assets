using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// 当前操作窗体
    /// </summary>
    private WindowBase currentWindow;
    /// <summary>
    /// 画布组件
    /// </summary>
    private Transform canvas;
    public Transform Canvas
    {
        get
        {
            GameObject canvasAndEventSystem = GameObject.Find("CanvasAndEventSystem");
            if (canvasAndEventSystem == null)
            {
                canvasAndEventSystem = GameObject.Find("CanvasAndEventSystem(Clone)");
                if (canvasAndEventSystem == null)
                {
                    GameObject cAERes = ResourcesManager.Instance.Load<GameObject>(FolderPaths.UI, "CanvasAndEventSystem");
                    canvas = GameObject.Instantiate(cAERes).transform.Find("Canvas");
                    GameObject.DontDestroyOnLoad(cAERes);
                }
            }
            else
            {
                canvas = canvasAndEventSystem.transform.Find("Canvas");
            }
            return canvas;
        }
    }
    public WindowBase CurrentWindow
    {
        get
        {
            return currentWindow;
        }
        set
        {
            //冻结上一个窗口
            if (currentWindow != null)
            {
                FreezeWindow(currentWindow.Name);
            }
            //置顶新当前
            BumpCurrentWindowToTop(value);
            ThawWindow(value.Name);
            currentWindow = value;
        }
    }

    /// <summary>
    /// 窗口字典
    /// </summary>
    private Dictionary<string, WindowBase> windowsDic = new Dictionary<string, WindowBase>();




    /// <summary>
    /// 创建或展示窗口
    /// </summary>
    public WindowBase CreateOrShowWindow(string windowName, Transform parent)
    {
        WindowBase retWindowBase = null;
        if (windowsDic.ContainsKey(windowName))
        {
            windowsDic[windowName].gameObject.SetActive(true);
            if (windowsDic[windowName].transform.parent != parent)
            {
                windowsDic[windowName].transform.SetParent(parent);
            }
            retWindowBase = windowsDic[windowName];
        }
        else
        {
            GameObject windowGameObjectRes = ResourcesManager.Instance.Load<GameObject>(FolderPaths.Windows, windowName);
            if (windowGameObjectRes != null)
            {
                GameObject windowObj = GameObject.Instantiate(windowGameObjectRes, parent);
                WindowBase windowBase = windowObj.GetComponent<WindowBase>();
                if (windowBase != null)
                {
                    windowsDic.Add(windowName, windowBase);
                    retWindowBase = windowBase;
                }
                else
                {
                    Debug.Log("Without WindowBase On " + windowName);
                }
            }
            else
            {
                Debug.Log("Cant Find Window Resources :" + windowName);
            }
        }
        if (retWindowBase != null)
        {
            CurrentWindow = retWindowBase;
        }
        return retWindowBase;
    }

    /// <summary>
    /// 隐藏窗体
    /// </summary>
    /// <param name="windowName"></param>
    public void HideWindow(string windowName)
    {
        if (windowsDic.ContainsKey(windowName))
        {
            if (windowsDic[windowName].gameObject.activeSelf)
            {
                windowsDic[windowName].gameObject.SetActive(false);
                //若隐藏的是当前窗口，则激活最顶端的窗口
                if (windowsDic[windowName] == CurrentWindow)
                {
                    CurrentWindow = GetLastIndexWindow(Canvas);
                }
            }
        }
        else
        {
            Debug.Log("Not Found This Window :" + windowName);
        }
    }

    /// <summary>
    /// 关闭窗体
    /// </summary>
    public void CloseWindow(string windowName)
    {
        if (windowsDic.ContainsKey(windowName))
        {
            if (windowsDic[windowName].GetComponentsInChildren<WindowBase>().Length > 1)
            {
                Debug.Log("Please Delete Children Firstly");
            }
            else
            {
                WindowBase deleteWindow = windowsDic[windowName];
                windowsDic.Remove(windowName);
                GameObject.Destroy(deleteWindow.gameObject);
                CurrentWindow = Canvas.GetChild(Canvas.childCount - 1).GetComponent<WindowBase>();
            }
        }
        else
        {
            Debug.Log("Not Found This Window :" + windowName);
        }
    }




    /// <summary>
    /// 冻结窗口
    /// </summary>
    /// <param name="windowName"></param>
    private void FreezeWindow(string windowName)
    {
        if (windowsDic.ContainsKey(windowName))
        {
            List<Selectable> selectableList = windowsDic[windowName].selectableList;
            for (int i = 0; i < selectableList.Count; i++)
            {
                selectableList[i].interactable = false;
            }
        }
        else
        {
            Debug.Log("Not Found This Window :" + windowName);
        }
    }

    /// <summary>
    /// 解冻窗口
    /// </summary>
    /// <param name="windowName"></param>
    private void ThawWindow(string windowName)
    {
        if (windowsDic.ContainsKey(windowName))
        {
            List<Selectable> selectableList = windowsDic[windowName].selectableList;
            for (int i = 0; i < selectableList.Count; i++)
            {
                selectableList[i].interactable = true;
            }
        }
        else
        {
            Debug.Log("Not Found This Window :" + windowName);
        }
    }

    /// <summary>
    /// 置顶Window
    /// </summary>
    /// <param name="window"></param>
    private void BumpCurrentWindowToTop(WindowBase window)
    {
        window.transform.SetAsLastSibling();
        if (window.transform.parent.name != "Canvas")
        {
            if (window.transform.parent.GetSiblingIndex() != window.transform.parent.parent.childCount - 1)
            {
                WindowBase parentWindow = window.transform.GetComponentInParent<WindowBase>();
                if (parentWindow != null)
                {
                    BumpCurrentWindowToTop(parentWindow);
                }
                else
                {
                    throw new System.Exception("Not Found WindowBase On " + window.Name + " Parent");
                }
            }
        }

    }



    #region Tools
    /// <summary>
    /// 获得顶端的窗体
    /// </summary>
    /// <param name="startTr"></param>
    /// <returns></returns>
    private WindowBase GetTopWindow(Transform startTr)
    {
        if (startTr.childCount > 0)
        {
            WindowBase lastWindow = GetLastIndexWindow(startTr);
            if (lastWindow != null)
            {
                return GetTopWindow(startTr);
            }
        }
        return startTr.GetComponent<WindowBase>();
    }
    /// <summary>
    /// GetTopWindow使用的小方法
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    private WindowBase GetLastIndexWindow(Transform parent)
    {
        WindowBase window = null;
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            window = parent.GetChild(i).GetComponent<WindowBase>();
            if (window != null)
            {
                return window;
            }
        }
        return window;
    }
    #endregion

}
