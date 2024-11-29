using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBooth.MicroVerseCore
{
    [ExecuteInEditMode]
    public class RoadMaterialOverride : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public Texture2D maskTexture;

        // Never referenced
        public static void ClearCache()
        {
            //instances.Clear();
        }

        public void OnEnable()
        {
            if (meshRenderer == null)
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }
            if (meshRenderer != null)
            {
                RoadSystem rs = GetComponentInParent<RoadSystem>();
                if (rs != null && rs.templateMaterial != null)
                {
                    Override(rs.templateMaterial);
                }
            }
        }

        public void Override(Material m)
        {
            RoadSystem rs = GetComponentInParent<RoadSystem>();
            if (meshRenderer != null && maskTexture != null)
            {
                Material inst = null;
                if (rs.materialInstances.ContainsKey(maskTexture))
                {
                    inst = rs.materialInstances[maskTexture];
                }
                else
                {
                    inst = new Material(m);
                    rs.materialInstances.Add(maskTexture, inst);
                }
                meshRenderer.sharedMaterial = inst;
                inst.CopyPropertiesFromMaterial(m);
                inst.SetTexture("_Mask", maskTexture);
                inst.hideFlags = HideFlags.DontSave;
            }
        }

    }

}