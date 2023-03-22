using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(S_SceneManager))]
public class S_ManagerEditor : Editor
{
    private S_SceneManager sceneManager;
    private SerializedProperty sp_categoryToDisplay => serializedObject.FindProperty("categoryToDisplay");


    private void OnEnable()
    {
        sceneManager = target as S_SceneManager;
    }





    public override void OnInspectorGUI()
    {



        sp_categoryToDisplay.intValue = EditorGUILayout.Popup("Scene lancé :",sp_categoryToDisplay.intValue, GetEnumStrings());

        sceneManager.sceneToStart = ((DisplayCategory)sp_categoryToDisplay.intValue).ToString();


        EditorGUILayout.Space();
        
        EditorUtility.SetDirty(sceneManager);
        serializedObject.ApplyModifiedProperties();
    }

    private string[] GetEnumStrings()
    {
        int length = DisplayCategory.GetNames(typeof(DisplayCategory)).Length;
        string[] myStrings = new string[length];

        for (int i = 0; i < length; i++)
        {
            myStrings[i] = ((DisplayCategory)i).ToString();
        }

        return myStrings;
    }
}
