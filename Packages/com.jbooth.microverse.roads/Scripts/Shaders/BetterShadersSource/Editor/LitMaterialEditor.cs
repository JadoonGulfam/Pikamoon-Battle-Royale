//////////////////////////////////////////////////////
// MicroVerse
// Copyright (c) Jason Booth
//////////////////////////////////////////////////////

// This is the material editor for the resulting shader. Basically it
// calls into the editor stub for all the various editors. 

using UnityEngine;
using UnityEditor;
using JBooth.MicroVerseCore;

namespace JBooth.Road
{
    public class LitMaterialEditor : ShaderGUI
    {

        LitBaseStub stub = null;



        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {

            var mat = materialEditor.target as Material;

            if (stub == null)
            {
                stub = new LitBaseStub(this, mat);
            }


            EditorGUI.BeginChangeCheck();
            stub.DoLitGUI(materialEditor, props);

            stub.DoWetness(materialEditor, props);
            stub.DoPuddles(materialEditor, props);
            stub.DoSnow(materialEditor, props);

            if (UnityEngine.Rendering.SupportedRenderingFeatures.active.editableMaterialRenderQueue)
                materialEditor.RenderQueueField();
            materialEditor.EnableInstancingField();
            materialEditor.DoubleSidedGIField();
            if (EditorGUI.EndChangeCheck())
            {
                var rs = Object.FindObjectsOfType<RoadSystem>();
                foreach (var r in rs)
                {
                    if (r.templateMaterial != null && r.templateMaterial == mat)
                    {
                        r.UpdateMaterialOverrides();
                    }
                }

            }
        }
    }
}
