using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    [CustomPropertyDrawer(typeof(StatDefaultData.DefaultStat))]
    public class DefaultStatDrawer : PropertyDrawer
    {
        private const int LabelWidth = 70;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty statType = property.FindPropertyRelative("statType");
            SerializedProperty value = property.FindPropertyRelative("value");

            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField(statType.displayName, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(statType, new GUIContent());
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(value.displayName, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(value, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }
    }
}
