using UnityEngine;
using System.Collections;
using System;

public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    public static string Name { get { return typeof(T).ToString(); } }

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // 如果为空，先试着从Resource中找到该对象
                instance = ResourcesManager.Instance.Load<T>(FolderPaths.ScriptableObjectSingletons, Name);
                //instance = Resources.Load("ScriptableObjectSingletons/SoundManager") as T;
                if (instance == null)
                {
                    throw new Exception(Name + " As ScriptableObjectSingleton Has No Instance!");
                }
            }
            return instance;
        }
    }
}
