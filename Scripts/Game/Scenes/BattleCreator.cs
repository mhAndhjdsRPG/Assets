using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCreator : MonoBehaviour {

    public RoomController roomController;

    // Use this for initialization
    void Awake () {
        roomController = InitRoomController();
        roomController.PillarWeight = 60;
        roomController.ChestWeight = 10;
        roomController.CreateRoomEnvironment(7, 11);
        roomController.PutBuildingInRoom();
        (ScenesLoadManager.Instance.curScene as BattleScene).roomController = roomController;
    }
	
	// Update is called once per frame
	void Update () {
		
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
