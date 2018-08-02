using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerAttackState))]
[ExecuteInEditMode]
public class FsmAttackInfoEditor : Editor
{

    PlayerAttackState state;

    

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        state = target as PlayerAttackState;

        DrawAttackInfo();

        serializedObject.ApplyModifiedProperties();
    }


    void DrawAttackInfo()
    {
        GUILayout.Space(5);
        GUILayout.Label("====== AttackInfo =======");

        var info = state.attackInfo;

        var name=EditorGUILayout.TextField("name", info.Name);
        if (name != info.Name)
        {
            info.Name = name;
        }

        var inputStr = EditorGUILayout.TextField("inputStr", info.InputStr);
        if (inputStr != info.InputStr)
        {
            info.InputStr = inputStr;
        }

        var coolDown = EditorGUILayout.FloatField("coolDown", info.CoolDown);
        if (coolDown != info.CoolDown)
        {
            info.CoolDown = coolDown;
        }

        var damageRate = EditorGUILayout.FloatField("damageRate", info.DamageRate);
        if (damageRate != info.DamageRate)
        {
            info.DamageRate = damageRate;
        }



        DrawRangeChecker();
    }





    RangeCheckType checkType = RangeCheckType.Sector;
    int layer;
    bool isValueChanged;
    bool isSwitchChanged;
    void DrawRangeChecker()
    {
        if (state.attackInfo.RangeChecker != null)
        {
            checkType = state.attackInfo.RangeChecker.checkType;
            layer = state.attackInfo.RangeChecker.checkLayerMask;
        }
        else
        {
            checkType = RangeCheckType.NoCheker;
            layer = 0;
        }

        var newCheckType = (RangeCheckType)EditorGUILayout.EnumPopup("CheckType", checkType);

        if (newCheckType != checkType)
        {
            checkType = newCheckType;
            isSwitchChanged = true;
        }
        else
        {
            isSwitchChanged = false;
        }

        switch (checkType)
        {
            case RangeCheckType.Cycle:
                CreateCycleCheck();
                break;
            case RangeCheckType.BoxCast:
                CreateBoxCheker();
                break;
            case RangeCheckType.Sector:
                CreateSectorChecker();
                break;
            case RangeCheckType.NoCheker:
              state.attackInfo.RangeChecker = null;
                break;
            default:
                break;
        }

    }


    float cycleRadius;
    void CreateCycleCheck()
    {
        if (state.attackInfo.RangeChecker is CycleChecker)
        {
            cycleRadius = (state.attackInfo.RangeChecker as CycleChecker).radius;
        }
        else
        {
            cycleRadius = 0;
        }

        EditorGUI.BeginChangeCheck();

        layer = EditorGUILayout.LayerField("Layer", layer);
        cycleRadius = EditorGUILayout.FloatField("radius", cycleRadius);

        isValueChanged = EditorGUI.EndChangeCheck();
        if (NeedCreateCheker)
        {
            CycleChecker checker = new CycleChecker(cycleRadius, layer);
            state.attackInfo.RangeChecker = checker;
        }

    }

    float width, distance;
    void CreateBoxCheker()
    {

        if(state.attackInfo.RangeChecker is BoxCastChecker)
        {
           width = (state.attackInfo.RangeChecker as BoxCastChecker).width;
            distance = (state.attackInfo.RangeChecker as BoxCastChecker).distance;
        }
        else
        {
            width = 0;
            distance = 0;
        }

        EditorGUI.BeginChangeCheck();

        layer = EditorGUILayout.LayerField("Layer", layer);
        width = EditorGUILayout.FloatField("width", width);
        distance = EditorGUILayout.FloatField("distance", distance);

        isValueChanged = EditorGUI.EndChangeCheck();
        if (NeedCreateCheker)
        {
            BoxCastChecker checker = new BoxCastChecker(width, distance, layer);
            state.attackInfo.RangeChecker = checker;
        }

       
    }

    float sectorAngle, sectorRadius;
    void CreateSectorChecker()
    {
        if (state.attackInfo.RangeChecker is SectorChecker)
        {
           sectorAngle = (state.attackInfo.RangeChecker as SectorChecker).angle;
           sectorRadius = (state.attackInfo.RangeChecker as SectorChecker).radius;
        }
        else
        {
            sectorAngle = 0;
            sectorRadius = 0;
        }
        
        EditorGUI.BeginChangeCheck();

        layer = EditorGUILayout.LayerField("Layer", layer);
        sectorAngle = EditorGUILayout.FloatField("angle",sectorAngle);
        sectorRadius = EditorGUILayout.FloatField("radius", sectorRadius);

        isValueChanged = EditorGUI.EndChangeCheck();
        if (NeedCreateCheker)
        {
            SectorChecker checker = new SectorChecker(sectorRadius, sectorRadius, layer);
            state.attackInfo.RangeChecker = checker;
        }
        
    }

    bool NeedCreateCheker
    {
        get { return state.attackInfo.RangeChecker == null || isSwitchChanged || isValueChanged; }
    }

  
}



