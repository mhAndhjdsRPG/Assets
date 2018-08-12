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

    public override void OnSceneEnter()
    {
        
    }

    public override void OnSceneExit()
    {

    }

    public override void OnSceneStartLoad()
    {
        roomController = InitRoomController();
        roomController.CreateRoomEnvironment(7, 11);
        roomController.PutBuildingInRoom();
    }

    private RoomController InitRoomController()
    {
        GameObject room = GameObject.Find("Room");
        if (room == null)
        {
            room = new GameObject("Room");
        }
        if (room.GetComponent<RoomController>() == null)
        {
            room.AddComponent<RoomController>();
        }
        return room.GetComponent<RoomController>();
    }
}
