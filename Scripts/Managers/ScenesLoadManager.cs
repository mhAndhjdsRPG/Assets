using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoadManager : Singleton<ScenesLoadManager>
{
    private Dictionary<Type, IScene> sceneDic = new Dictionary<Type, IScene>();

    public IScene curScene = null;

    public IScene GetScene<T>() where T : IScene, new()
    {
        if (!sceneDic.ContainsKey(typeof(T)))
        {
            sceneDic.Add(typeof(T), new T());
        }
        return sceneDic[typeof(T)];
    }

    public void LoadScene(IScene nextScene)
    {
        curScene.OnSceneExit();
        curScene = nextScene;
        nextScene.OnSceneStartLoad();
        SceneManager.LoadScene(nextScene.Name);
        nextScene.OnSceneEnter();
    }

    public void LoadSceneSync(IScene nextScene)
    {
        curScene.OnSceneExit();
        curScene = nextScene;
        LoadingWindow.nextSceneName = nextScene.Name;
        LoadingWindow.OnLoadCompletion = nextScene.OnSceneEnter;
        UIManager.Instance.CreateOrShowWindow(WindowName.LoadingWindow, null);
        nextScene.OnSceneStartLoad();
    }
}
