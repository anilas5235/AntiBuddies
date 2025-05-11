using Project.Scripts.StatSystem.Stats;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    [CustomPropertyDrawer(typeof(ValueStatRef))]
    public class ValueStatRefDrawer : StatRefDrawer
    {
        protected override void ChildContent(Rect position, SerializedProperty property)
        {
            SerializedProperty baseValue = property.FindPropertyRelative("baseValue");
            SerializedProperty currValue = property.FindPropertyRelative("currValue");
            SerializedProperty positiveTransform = property.FindPropertyRelative("positiveTransform");

            GUI.enabled = false;
            EditorGUILayout.PropertyField(currValue);
            GUI.enabled = true;
            EditorGUILayout.PropertyField(baseValue);
            EditorGUILayout.PropertyField(positiveTransform);
        }
    }
}