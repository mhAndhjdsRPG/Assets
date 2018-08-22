using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 继承monobehavior的单例
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject instanceGameObj = GameObject.Find(typeof(T).ToString());
                if (instanceGameObj == null)
                {
                    instanceGameObj = new GameObject(typeof(T).ToString());
                    instance = instanceGameObj.AddComponent<T>();
                }
                else
                {
                    if (instanceGameObj.GetComponent<T>() != null)
                    {
                        instance = instanceGameObj.GetComponent<T>();
                    }
                    else
                    {
                        instance = instanceGameObj.AddComponent<T>();
                    }
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
        OnAwake();
    }

    protected abstract void OnAwake();


    private void Start()
    {
        OnStart();
    }

    protected abstract void OnStart();

    private void Update()
    {
        OnUpdate();
    }

    protected abstract void OnUpdate();

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }

    protected abstract void OnFixedUpdate();

    private void LateUpdate()
    {
        OnLateUpdate();
    }

    protected abstract void OnLateUpdate();


}