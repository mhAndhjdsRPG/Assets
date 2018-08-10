using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorTools 
{
    public static void DrawSpace(int spaceCout)
    {
        for (int i = 0; i < spaceCout; i++)
        {
            EditorGUILayout.Space();
        }
    }
	
}
