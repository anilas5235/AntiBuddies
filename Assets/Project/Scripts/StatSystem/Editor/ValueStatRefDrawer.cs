using Project.Scripts.StatSystem.Stats;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    /// <summary>
    /// Custom property drawer for <see cref="ValueStatRef"/>.
    /// Displays base value, current value, transform type, and stat references in the inspector.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [CustomPropertyDrawer(typeof(ValueStatRef))]
    public class ValueStatRefDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty baseValue = property.FindPropertyRelative("baseValue");
            SerializedProperty currValue = property.FindPropertyRelative("currValue");
            SerializedProperty positiveTransform = property.FindPropertyRelative("positiveTransform");
            SerializedProperty statRefs = property.FindPropertyRelative("statRefs");

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            GUI.enabled = false;
            EditorGUILayout.PropertyField(currValue, new GUIContent("Current Value"));
            GUI.enabled = true;

            EditorGUILayout.PropertyField(baseValue, new GUIContent("Base Value"));
            EditorGUILayout.PropertyField(positiveTransform, new GUIContent("Positive Transform"));

            // Ensure the StatRef list is drawn correctly
            EditorGUILayout.PropertyField(statRefs, new GUIContent("Stat References"), true);

            EditorGUILayout.EndVertical();
        }
    }
}