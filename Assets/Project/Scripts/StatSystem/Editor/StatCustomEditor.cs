using Project.Scripts.StatSystem.Stats;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    [CustomPropertyDrawer(typeof(Stat))]
    public class StatCustomEditor : PropertyDrawer
    {
        private bool foldout;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty statType = property.FindPropertyRelative("statType");
            SerializedProperty statValue = property.FindPropertyRelative("statValue");
            SerializedProperty clampedValue = property.FindPropertyRelative("clampedValue");
            SerializedProperty maxValue = property.FindPropertyRelative("maxValue");
            SerializedProperty minValue = property.FindPropertyRelative("minValue");

            EditorGUILayout.LabelField(new GUIContent(statType.name, "Stat Type"), EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(clampedValue);

            foldout = EditorGUILayout.Foldout(foldout, "Stat Properties");
            if (foldout)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.PropertyField(statValue);
                EditorGUILayout.PropertyField(maxValue);
                EditorGUILayout.PropertyField(minValue);
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndVertical();
        }
    }
}