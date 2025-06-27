using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    /// <summary>
    /// Custom inspector for <see cref="StatComponent"/>.
    /// Provides a button to reset all stats in the inspector.
    /// </summary>
    [CustomEditor(typeof(StatComponent))]
    public class StatComponentCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            StatComponent statComponent = (StatComponent)target;

            EditorGUILayout.BeginVertical();
            GUILayout.Space(5);

            if (GUILayout.Button("Reset Stats", GUILayout.Height(25))) // Set a fixed height for the button
            {
                statComponent.ResetStats();
            }

            GUILayout.Space(5);
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();
        }
    }
}