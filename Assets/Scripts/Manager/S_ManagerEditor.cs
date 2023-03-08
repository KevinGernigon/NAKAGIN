using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
[CustomEditor(typeof(S_SceneManager))]
public class S_ManagerEditor : Editor
{
    private S_SceneManager sceneManager;

    private void OnEnable()
    {
        sceneManager = target as S_SceneManager;
    }

    public enum DisplayCategory
    {
        Corentin_Scene, Kiki_Scene, Alexis_Scene, Assets_Scene, MAIN_VerticalSlice, Killian_Scene, Tom_Scene, Playtest_Scene, Maxime_Scene, Kevin_Scene
    }


    [SerializeField] public DisplayCategory categoryToDisplay;

    public override void OnInspectorGUI()
    {

        categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Scene à lancer", categoryToDisplay);

        sceneManager.sceneToStart = categoryToDisplay.ToString();

        EditorGUILayout.Space();
        
        EditorUtility.SetDirty(sceneManager);
        serializedObject.ApplyModifiedProperties();
    }
}*/
