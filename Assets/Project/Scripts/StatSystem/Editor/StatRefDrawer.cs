using Project.Scripts.StatSystem.Stats;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    [CustomPropertyDrawer(typeof(StatRef))]
    public class StatRefDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty statType = property.FindPropertyRelative("statType");
            
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField(label);
            EditorGUILayout.PropertyField(statType, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }
    }
}