using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    [CustomEditor(typeof(StatComponent))]
    public class StatComponentCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            StatComponent statComponent = (StatComponent)target;

            EditorGUILayout.BeginVertical(); // Begin vertical layout for stability
            GUILayout.Space(5); // Add spacing to avoid jittering

            if (GUILayout.Button("Reset Stats", GUILayout.Height(25))) // Set a fixed height for the button
            {
                statComponent.ResetStats();
            }

            GUILayout.Space(5); // Add spacing to separate the button from other elements
            EditorGUILayout.EndVertical(); // End vertical layout

            base.OnInspectorGUI();
        }
    }
}
