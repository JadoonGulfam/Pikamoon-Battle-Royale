using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Mathematics;
using UnityEngine.Splines;
using UnityEditor.Splines;
using System;

namespace JBooth.MicroVerseCore
{
    [CustomEditor(typeof(SplineRelativeTransform))]
    public class SplineRelativeTransformEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                var srt = (target as SplineRelativeTransform);
                if (srt != null)
                {
                    srt.enabled = false;
                    srt.enabled = true;
                }

            }
        }

        
    }
}
