﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Linq;
using Google.Protobuf.Collections;
using Gamedef;

public class SocketRoleScenes : SocketBase
{

    //public void InitSceneData()
    //{
    //    //注册消息回调函数
    //    _netWorker.RegisterMessage<RoleSceneData>(roleEnterScene);
    //    _netWorker.RegisterMessage<SceneRolesData>(sceneRolesData);
    //    _netWorker.RegisterMessage<RoleLeaveScene>(roleLeaveScene);
    //    _netWorker.RegisterMessage<RoleMoveToPositionACK>(roleMovePosition);
    //    _netWorker.RegisterMessage<RoleOffline>(roleOffline);
    //}


    ////发送进入场景消息
    //public void  EnterScene()
    //{
    //    RoleEnterScene enterscene = new RoleEnterScene();

    //    _netWorker.SendMessage(enterscene);
    //}

    ////发送角色位置移动消息
    //public void MoveToPosition(Vector3Data _fromPosition , Vector3Data _toPosition)
    //{
    //    RoleMoveToPositionREQ movePosition = new RoleMoveToPositionREQ();
    //    movePosition.FromPos = _fromPosition;
    //    movePosition.ToPos = _toPosition;

    //    _netWorker.SendMessage(movePosition);
    //}

    ////角色切换场景
    //public void ChangeSence(ScenePosition position)
    //{
    //    RoleChangeScene rcs = new RoleChangeScene();
    //    rcs.SceneData = position;

    //    _netWorker.SendMessage(rcs);
    //}

    ////定义回调消息处理函数
    //Action<object> roleEnterScene = new Action<object>(RoleEnterSceneAck);
    //Action<object> sceneRolesData = new Action<object>(SceneRolesDataAck);
    //Action<object> roleLeaveScene = new Action<object>(RoleLeaveSceneAck);
    //Action<object> roleMovePosition = new Action<object>(RoleMoveToPositionAck);
    //Action<object> roleOffline = new Action<object>(OffLine);

    ////接收服务器发回的角色进入场景消息
    //public static void RoleEnterSceneAck(object data)
    //{
    //    RoleSceneData roleSceneData = new RoleSceneData();
    //    roleSceneData.MergeFrom((RoleSceneData)data);

    //    if (roleSceneData.RoleID == _playerInfor._RoleID)
    //        return;

    //    //如果是其他玩家，保留其他玩家信息（注意重复创建）
    //    _playerInfor.SetIfCreateOtherPlayer(true);

    //    _playerInfor._otherPlayer.MergeFrom(roleSceneData);

    //}

    ////接收场景中其他玩家的信息
    //public static void SceneRolesDataAck(object data)
    //{
    //    SceneRolesData sceneRoles = new SceneRolesData();
    //    sceneRoles.MergeFrom((SceneRolesData)data);


    //    if (sceneRoles.CalculateSize() < 0)
    //        return;
        
    //    _playerInfor._othersPlayer.MergeFrom(sceneRoles);
    //}


    ////当有人离开场景
    //public static void RoleLeaveSceneAck(object data)
    //{
    //    RoleLeaveScene roleleave = new RoleLeaveScene();
    //    roleleave.MergeFrom((RoleLeaveScene)data);

    //    if (_playerInfor._RoleID == roleleave.RoleID)          //如果是自己，这里不做处理
    //        return;

    //    //是别人时，需要做的处理
    //}


    ////接收角色移动结果消息
    //public static void RoleMoveToPositionAck(object data)
    //{
    //    RoleMoveToPositionACK moveResult = new RoleMoveToPositionACK();
    //    moveResult.MergeFrom((RoleMoveToPositionACK)data);

    //    //对结果做处理
    //}

    ////同步角色位置信息

    //public static void MoveNotice(object data)
    //{
    //    RoleMoveNotice roleMove = new RoleMoveNotice();
    //    roleMove.MergeFrom((RoleMoveNotice)data);

    //    if (roleMove.RoleID == _playerInfor._RoleID)
    //        return;


    //    //同步其他人的位置信息

    //}

    //public static void OffLine(object data)
    //{
    //    RoleOffline offline = new RoleOffline();
    //    offline.MergeFrom((RoleOffline)data);

        
    //}


}
 