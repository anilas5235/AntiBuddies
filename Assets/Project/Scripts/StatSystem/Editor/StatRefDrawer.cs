using Project.Scripts.StatSystem.Stats;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    /// <summary>
    /// Custom property drawer for <see cref="StatRef"/>.
    /// Displays stat type and efficiency fields in the inspector.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [CustomPropertyDrawer(typeof(StatRef))]
    public class StatRefDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty statType = property.FindPropertyRelative("statDependency.statType");
            SerializedProperty useEfficiency = property.FindPropertyRelative("statDependency.useEfficiency");

            float lineHeight = EditorGUIUtility.singleLineHeight;
            float spacing = EditorGUIUtility.standardVerticalSpacing;

            position.x -= 10; // Add padding
            position.width -= 10;

            // Draw label
            Rect currentRect = new(position.x, position.y, position.width, lineHeight);
            EditorGUI.LabelField(currentRect,
                label.text + "(" + (statType.objectReferenceValue ? statType.objectReferenceValue.name : "Unknown") +
                ")", EditorStyles.boldLabel);

            // Draw statType field
            currentRect.y += lineHeight + spacing;
            EditorGUI.PropertyField(currentRect, statType, new GUIContent("Stat Type"));

            // Draw useEfficiency field
            currentRect.y += lineHeight + spacing;
            EditorGUI.PropertyField(currentRect, useEfficiency, new GUIContent("Use Efficiency"));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Calculate height for two fields and spacing
            return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 3;
        }
    }
}