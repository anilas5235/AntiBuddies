using Project.Scripts.StatSystem.Stats;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    [CustomPropertyDrawer(typeof(Stat))]
    public class StatDrawer : PropertyDrawer
    {
        private bool foldout;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty statType = property.FindPropertyRelative("statType");
            SerializedProperty statValue = property.FindPropertyRelative("statValue");
            SerializedProperty clampedValue = property.FindPropertyRelative("clampedValue");
            SerializedProperty maxValue = property.FindPropertyRelative("maxValue");
            SerializedProperty minValue = property.FindPropertyRelative("minValue");
            SerializedProperty baseStatValue = property.FindPropertyRelative("baseStatValue");
            SerializedProperty tempStatBonus = property.FindPropertyRelative("tempStatBonus");

            EditorGUILayout.BeginVertical("box");
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(new GUIContent(statType.objectReferenceValue.name, "Stat Type"),
                        EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(clampedValue, GUIContent.none);
                }
                EditorGUILayout.EndHorizontal();

                foldout = EditorGUILayout.Foldout(foldout, "Details");
                if (foldout)
                {
                    EditorGUILayout.BeginVertical("box");
                    {
                        EditorGUILayout.PropertyField(statValue);
                        EditorGUILayout.PropertyField(maxValue);
                        EditorGUILayout.PropertyField(minValue);
                        EditorGUILayout.PropertyField(baseStatValue);
                        EditorGUILayout.PropertyField(tempStatBonus);
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}