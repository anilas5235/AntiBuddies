using Project.Scripts.StatSystem.Stats;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    //[CustomPropertyDrawer(typeof(StatRef))]
    public class StatRefDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty statDependency = property.FindPropertyRelative("statDependency");
            
            
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label);
            EditorGUILayout.PropertyField(statDependency, GUIContent.none);
            EditorGUILayout.EndHorizontal();
            ChildContent(position, property);
            EditorGUILayout.EndVertical();
        }

        protected virtual void ChildContent(Rect position, SerializedProperty property)
        {
        }
    }
}