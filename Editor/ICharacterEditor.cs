using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ICharacter),true)]
[ExecuteInEditMode]
public class ICharacterEditor : Editor{

    ICharacter character;


    float maxHp;
    float hp;
    float atk;
    float agl;
    float hard;
    float dodgeCoolDown;
    bool canDodge;

    bool showInfo=true;


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        showInfo = EditorGUILayout.Foldout(showInfo,"ShowInfo");
        if (showInfo)
        {
            DrawInfo();
        }
    }


    protected void DrawInfo()
    {

        character = target as ICharacter;

        EditorGUILayout.LabelField("Name",character.Name);

        maxHp = EditorGUILayout.FloatField("maxHp", character.MaxHP);
        if (maxHp != character.MaxHP)
        {
            character.MaxHP = maxHp;
        }

        hp = EditorGUILayout.FloatField("Hp", character.HP);
        if (hp != character.HP)
        {
            character.HP = hp;
        }

        atk = EditorGUILayout.FloatField("Atk", character.BaseATK);
        if (atk != character.BaseATK)
        {
            character.BaseATK = atk;
        }

        agl = EditorGUILayout.FloatField("Agl", character.TotalAGL);
        if (agl != character.TotalAGL)
        {
            character.TotalAGL = agl;
        }

        hard = EditorGUILayout.FloatField("Hard", character.Hard);
        if (hard != character.Hard)
        {
            character.Hard = hard;
        }

        canDodge=EditorGUILayout.Toggle("CanDodge", character.CanDodge);
        if (canDodge != character.CanDodge)
        {
            character.CanDodge = canDodge;
        }

       

    }

}
