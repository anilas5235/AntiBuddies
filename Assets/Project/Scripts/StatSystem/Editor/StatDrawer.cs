using System.Collections.Generic;
using Project.Scripts.StatSystem.Stats;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    /// <summary>
    /// Custom property drawer for the <see cref="Stat"/> class.
    /// Displays a foldout with editable and read-only fields for stat properties.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [CustomPropertyDrawer(typeof(Stat))]
    public class StatDrawer : PropertyDrawer
    {
        private static readonly Dictionary<string, bool> FoldoutStates = new();

        private const int Padding = 2;

        private static readonly float Spacing =
            EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty statType = property.FindPropertyRelative("statType");
            SerializedProperty statValue = property.FindPropertyRelative("statValue");
            SerializedProperty clampedValue = property.FindPropertyRelative("clampedValue");
            SerializedProperty percentMultiplier = property.FindPropertyRelative("percentMultiplier");
            SerializedProperty maxValue = property.FindPropertyRelative("maxValue");
            SerializedProperty minValue = property.FindPropertyRelative("minValue");
            SerializedProperty baseStatValue = property.FindPropertyRelative("baseStatValue");
            SerializedProperty tempStatBonus = property.FindPropertyRelative("tempStatBonus");

            string propertyPath = property.propertyPath; // Unique identifier for the property
            FoldoutStates.TryAdd(propertyPath, false);

            // Calculate the total height of the property
            float totalHeight = GetPropertyHeight(property, label);
            GUI.Box(new Rect(position.x, position.y, position.width, totalHeight), GUIContent.none); // Draw the box

            position.y += Padding;
            position.x += Padding; // Add padding
            position.width -= Padding * 2; // Adjust width for padding
            position.height = EditorGUIUtility.singleLineHeight;

            // Draw foldout, statType, and clampedValue on the same line
            Rect foldoutRect = new(position.x, position.y, position.width * 0.5f, position.height);
            Rect statTypeRect = new(position.x + position.width * 0.52f, position.y, position.width * 0.23f,
                position.height);
            Rect clampedValueRect = new(position.x + position.width * 0.76f, position.y, position.width * 0.22f,
                position.height);

            FoldoutStates[propertyPath] = EditorGUI.Foldout(foldoutRect, FoldoutStates[propertyPath],
                statType.objectReferenceValue ? statType.objectReferenceValue.name : "Unknown", true);

            EditorGUI.PropertyField(statTypeRect, statType, GUIContent.none);

            GUI.enabled = false; // Disable editing for clampedValue
            EditorGUI.PropertyField(clampedValueRect, clampedValue, GUIContent.none);
            GUI.enabled = true; // Re-enable editing for other fields

            if (FoldoutStates[propertyPath])
            {
                EditorGUI.indentLevel++;
                position.y += Spacing;

                GUI.enabled = false;
                EditorGUI.PropertyField(position, statValue);
                GUI.enabled = true;

                position.y += Spacing;

                EditorGUI.BeginChangeCheck(); // Start tracking changes
                EditorGUI.PropertyField(position, percentMultiplier, new GUIContent("% Multiplier"));
                position.y += Spacing;

                EditorGUI.PropertyField(position, maxValue);
                position.y += Spacing;

                EditorGUI.PropertyField(position, minValue);
                position.y += Spacing;

                EditorGUI.PropertyField(position, baseStatValue);
                position.y += Spacing;

                EditorGUI.PropertyField(position, tempStatBonus);

                position.y += Spacing;
                EditorGUI.indentLevel--;
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            string propertyPath = property.propertyPath;
            bool isFoldout = FoldoutStates.ContainsKey(propertyPath) && FoldoutStates[propertyPath];
            float height = EditorGUIUtility.singleLineHeight; // For the foldout and clampedValue
            if (isFoldout)
            {
                height += Spacing * 7; // For the remaining fields
            }

            return height + Padding * 2;
        }
    }
}