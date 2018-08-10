using UnityEngine;
using System.Collections;

public class MainScene : IScene
{
    public override string Name
    {
        get
        {
            return "MainScene";
        }
    }

    public override void OnSceneEnter()
    {
        UIManager.Instance.CreateOrShowWindow(WindowName.MainMenuWindow, UIManager.Instance.Canvas);
    }

    public override void OnSceneExit()
    {
        
    }

    public override void OnSceneStartLoad()
    {
        
    }

}
