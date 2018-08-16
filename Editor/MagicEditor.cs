using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Magic)),ExecuteInEditMode]
public class MagicEditor : Editor
{
    Magic magic;
    SerializedProperty speedCurve;
    SerializedProperty distanceFromTargetCurve;
    SerializedProperty lockTrans;

    private void OnEnable()
    {
        magic = target as Magic;
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        switch (magic.magicType)
        {
            case MagicType.stayTime:
                DrawStayTime();
                break;
            case MagicType.lockPos:
                DrawLockPos();
                break;
            case MagicType.lockTrans:
                DrawLockTrans();
                break;
            default:
                break;
        }

        EditorTools.DrawSpace(2);
        EditorGUILayout.LabelField("=====运行时信息=====");
        EditorGUI.BeginDisabledGroup(true);

        EditorGUILayout.FloatField("CurrentTime", magic.CurrentTime);

        EditorGUILayout.FloatField("TimePercent", magic.TimePercent);

        EditorGUI.EndDisabledGroup();

    }


    void DrawStayTime()
    {
        serializedObject.Update();

        speedCurve = serializedObject.FindProperty("speedCurve");

        EditorGUILayout.PropertyField(speedCurve);

        serializedObject.ApplyModifiedProperties();

        magic.speedScale=EditorGUILayout.FloatField("SpeedScale",magic.speedScale);
    }

    void DrawLockPos()
    {
        serializedObject.Update();

        distanceFromTargetCurve = serializedObject.FindProperty("distanceFromTargetCurve");

        EditorGUILayout.PropertyField(distanceFromTargetCurve);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawLockTrans()
    {
        serializedObject.Update();

        lockTrans = serializedObject.FindProperty("targetTrans");

        EditorGUILayout.PropertyField(lockTrans);

        serializedObject.ApplyModifiedProperties();

        DrawLockPos();

    }


}
