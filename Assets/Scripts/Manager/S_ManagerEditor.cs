using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(S_SceneManager))]
public class S_ManagerEditor : Editor
{
    [SerializeField]
    private S_SceneManager sceneManager;
    public string sceneToStart;
    public enum DisplayCategory
    {
        Assets_Scene, Kiki_Scene, Alexis_Scene, Corentin_Scene, MAIN_VerticalSlice, Killian_Scene, Tom_Scene, Playtest_Scene, Maxime_Scene, Kevin_Scene
    }


    public DisplayCategory categoryToDisplay;

    public override void OnInspectorGUI()
    {

        categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Scene à lancer", categoryToDisplay);
        sceneToStart = categoryToDisplay.ToString();

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }
}
