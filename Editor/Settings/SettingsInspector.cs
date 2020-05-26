﻿using UnityEngine;
using UnityEditor;

namespace BgTools.Settings
{
    [CustomEditor(typeof(SettingsHolder))]
    public class SettingsInspector : Editor
    {
        private bool watchingForChanges;

        [MenuItem("Tools/BG Tools/Settings", false, 2)]
        public static void ShowConfiguration()
        {
            ScriptableObject settings = CreateInstance<SettingsHolder>();
            settings.name = "BG Tools - Settings";
            Selection.activeObject = settings;
        }

        private void OnEnable()
        {
            watchingForChanges = EditorPrefs.GetBool("BGTools.PlayerPrefsEditor.WatchingForChanges", false);
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginVertical();

            EditorGUI.BeginChangeCheck();

            GUILayout.Label("PlayerPrefs Editor", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent("Monitor PlayerPref changes", "Detect PlayerPref changes in the system and refresh the view automaticlly"));
            watchingForChanges = GUILayout.Toggle(watchingForChanges, string.Empty);
            GUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                if (watchingForChanges)
                {
                    EditorPrefs.SetBool("BGTools.PlayerPrefsEditor.WatchingForChanges", watchingForChanges);
                }
                else
                {
                    EditorPrefs.DeleteKey("BGTools.PlayerPrefsEditor.WatchingForChanges");
                }
            }

            GUILayout.EndVertical();
        }
    }

    // Need to be connected with inspector
    public class SettingsHolder : ScriptableObject
    { }
}