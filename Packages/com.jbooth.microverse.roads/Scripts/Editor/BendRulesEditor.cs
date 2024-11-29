using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JBooth.MicroVerseCore
{
    [CustomEditor(typeof(BendRules))]
    public class BendRulesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BendRules rules = (target as BendRules);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mode"));
            serializedObject.ApplyModifiedProperties();
            if (rules.mode != BendRules.Mode.None)
            { 
                EditorGUILayout.PropertyField(serializedObject.FindProperty("spawnRules"));
                if (rules.mode != BendRules.Mode.Bend)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("placeRules"));
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
