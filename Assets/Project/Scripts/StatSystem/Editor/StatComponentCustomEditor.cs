using UnityEditor;
using UnityEngine;

namespace Project.Scripts.StatSystem.Editor
{
    [CustomEditor(typeof(StatComponent))]
    public class StatComponentCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            StatComponent statComponent = (StatComponent)target;
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Live Stats"))
                {
                    statComponent.CheckLiveStats();
                    EditorUtility.SetDirty(statComponent);
                    Repaint();
                }

                GUILayout.Space(10);

                if (GUILayout.Button("Reset Stats"))
                {
                    statComponent.ResetLiveStats();
                }

                GUILayout.Space(10);

                if (GUILayout.Button("Force Stats to Update"))
                {
                    statComponent.ForceStatsToUpdate();
                }
            }
            GUILayout.EndHorizontal();
            base.OnInspectorGUI();
        }
    }
}
