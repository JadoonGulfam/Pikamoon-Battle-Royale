//Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace TheVisualEngine
{
    [DisallowMultipleComponent]
    [CustomEditor(typeof(TVEElement))]
    public class TVEInspectorElement : Editor
    {
        string excludeProps = "m_Script";
        TVEElement targetScript;

        void OnEnable()
        {
            targetScript = (TVEElement)target;
        }

        public override void OnInspectorGUI()
        {
            DrawInspector();

            var elementMaterial = targetScript.elementMaterial;

            if (elementMaterial != null)
            {
                if (!EditorUtility.IsPersistent(elementMaterial))
                {
                    GUILayout.Space(10);
                    EditorGUILayout.HelpBox("Unsaved element materials are only recommended for prototyping! Save the material to be able to use the element in prefabs, enable GPU Instancing support and saved runtime performance!", MessageType.Warning);
                    GUILayout.Space(10);

                    if (GUILayout.Button("Save Element Material"))
                    {
                        var savePath = EditorUtility.SaveFilePanelInProject("Save Material", "My Element", "mat", "Save Element material to disk!");

                        if (savePath != "")
                        {
                            // Add OO Element to be able to find the material on upgrade
                            savePath = savePath.Replace("(TVE Element)", "");
                            savePath = savePath.Replace(".mat", " (TVE Element).mat");

                            AssetDatabase.CreateAsset(elementMaterial, savePath);
                            targetScript.gameObject.GetComponent<Renderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>(savePath);

                            if (Application.isPlaying == false)
                            {
                                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                            }

                            TVEUtils.SetLabel(savePath);

                            AssetDatabase.Refresh();
                        }
                    }
                }
            }

            GUILayout.Space(5);
        }

        void DrawInspector()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, excludeProps);

            serializedObject.ApplyModifiedProperties();
        }
    }
}


