//Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;

namespace TheVisualEngine
{
    [DisallowMultipleComponent]
    [CustomEditor(typeof(TVETerrain))]
    public class TVEInspectorTerrain : Editor
    {
        string excludeProp = "m_Script";
        //OOTerrain targetScript;

        //void OnEnable()
        //{
        //    targetScript = (OOTerrain)target;
        //}

        public override void OnInspectorGUI()
        {
            DrawInspector();
        }

        void DrawInspector()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, excludeProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}


