using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Magic)), ExecuteInEditMode]
public class MagicEditor : Editor
{
    Magic magic;

    private void OnEnable()
    {
        magic = target as Magic;
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (magic.magicType == MagicType.lockPos)
        {
            magic.targetPos = EditorGUILayout.Vector3Field("targetPos", magic.targetPos);
        }

        if (magic.magicType == MagicType.lockTrans)
        {
            magic.targetPos = EditorGUILayout.Vector3Field("targetPos", magic.targetPos);

            magic.targetTrans = EditorGUILayout.ObjectField("targetTrans", magic.targetTrans, typeof(UnityEngine.Transform), true) as Transform;
        }


        EditorTools.DrawSpace(2);
        EditorGUILayout.LabelField("=====运行时信息=====");
        EditorGUI.BeginDisabledGroup(true);

        EditorGUILayout.FloatField("CurrentTime", magic.CurrentTime);

        EditorGUILayout.FloatField("TimePercent", magic.TimePercent);

        EditorGUILayout.IntField("InverseSpeedSign", magic.InverseSpeedSign);

        EditorGUI.EndDisabledGroup();



      

    }




}
