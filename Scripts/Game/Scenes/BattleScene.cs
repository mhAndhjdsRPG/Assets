using UnityEngine;
using System.Collections;

public class BattleScene : IScene
{
    public RoomController roomController;
    public override string Name
    {
        get
        {
            return "BattleScene";
        }
    }

    public static BattleScene instance;
    public static BattleScene Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BattleScene();
            }
            return instance;
        }
    }

    public override void OnSceneEnter()
    {

    }

    public override void OnSceneExit()
    {

    }

    public override void OnSceneStartLoad()
    {
        
    }

    
}
