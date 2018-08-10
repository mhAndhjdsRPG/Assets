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
    float hard;
    float dodgeCoolDown;
    bool canDodge;

    bool showInfo=true;

    private void OnEnable()
    {
        character = target as ICharacter;
    }

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

        DrawAtk();

        DrawAgl();

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


    void DrawAtk()
    {
        EditorTools.DrawSpace(2);
        EditorGUILayout.LabelField("====== Atk ======");
        var atk = EditorGUILayout.FloatField("BaseAtk", character.BaseATK);
        if (atk != character.BaseATK)
        {
            character.BaseATK = atk;
        }

        var addAtk = EditorGUILayout.FloatField("AddAtk", character.AddAtk);
        if (addAtk != character.AddAtk)
        {
            character.AddAtk = addAtk;
        }

        var multipleAtk = EditorGUILayout.FloatField("multipleAtk", character.MultipleAtk);
        if (multipleAtk != character.MultipleAtk)
        {
            character.MultipleAtk = multipleAtk;
        }


        EditorGUI.BeginDisabledGroup(true);

        EditorGUILayout.FloatField("TotalAtk", character.TotalAtk);
       
        EditorGUI.EndDisabledGroup();
    }


    void DrawAgl()
    {
        EditorTools.DrawSpace(2);
        EditorGUILayout.LabelField("====== Agl ======");
        var agl = EditorGUILayout.FloatField("BaseAgl", character.BaseAgl);
        if (agl != character.BaseAgl)
        {
            character.BaseAgl = agl;
        }

        var addAgl = EditorGUILayout.FloatField("AddAgl", character.AddAgl);
        if (addAgl != character.AddAgl)
        {
            character.AddAgl = addAgl;
        }

        var multipleAgl = EditorGUILayout.FloatField("multipleAgl", character.MultipleAgl);
        if (multipleAgl != character.MultipleAgl)
        {
            character.MultipleAgl = multipleAgl;
        }


        EditorGUI.BeginDisabledGroup(true);

        EditorGUILayout.FloatField("TotalAgl", character.TotalAGL);

        EditorGUI.EndDisabledGroup();
    }

}
