// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using Boxophobic.StyledGUI;

namespace TheVisualEngine
{
#if UNITY_EDITOR
    [HelpURL("https://docs.google.com/document/d/145JOVlJ1tE-WODW45YoJ6Ixg23mFc56EnB_8Tbwloz8/edit#heading=h.q4fstlrr3cw4")]
    [ExecuteInEditMode]
    [AddComponentMenu("BOXOPHOBIC/The Visual Engine/TVE Terrain")]
#endif
    public class TVETerrain : StyledMonoBehaviour
    {
        [StyledBanner(0.890f, 0.745f, 0.309f, "Terrain")]
        public bool styledBanner;

#if !THE_VISUAL_ENGINE_TERRAIN
        [StyledMessage("Error", "The Terrain Shader Module is required for the terrain component to work!", 5, 10)]
        public bool styledMessage = true;
#endif
        [Tooltip("Sync the terrain data with the material in editor if the terrain is modified by external tools.")]
        public TVETerrainRefresh terrainRefresh = TVETerrainRefresh.Always;
        [Tooltip("Sets the terrain bounds multiplier used to avoid patches culling when using vertex offset elements.")]
        public float terrainBounds = 1;
        [Tooltip("Override the terrain layer maps and settings without modifying the actual terrain layer.")]
        public TVETerrainSettings terrainSettings = new TVETerrainSettings();

        [HideInInspector]
        public Terrain terrain;
        [HideInInspector]
        public Renderer meshRenderer;
        [HideInInspector]
        public Material terrainMaterial;
        [HideInInspector]
        public MaterialPropertyBlock terrainPropertyBlock;

        [StyledSpace(5)]
        public bool styledSpace1;

        void OnEnable()
        {
            InitializeTerrain();
            UpdateTerrainSettings();
        }

        void Update()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (Selection.Contains(gameObject) || terrainRefresh == TVETerrainRefresh.Always)
                {
                    UpdateTerrainSettings();
                    UpdateOverrideNames();
                }
            }
#endif
        }


        void InitializeTerrain()
        {
            terrain = GetComponent<Terrain>();
            meshRenderer = GetComponent<Renderer>();

            //if (terrain.materialTemplate != null)
            //{
            //    if (terrain.materialTemplate.shader.name.Contains("Error"))
            //    {
            //        terrain.materialTemplate.shader = Shader.Find("BOXOPHOBIC/The Visual Engine/Landscape/Terrain Standard Lit");
            //    }
            //}
        }

        public void UpdateTerrainSettings()
        {
            if (terrainPropertyBlock == null)
            {
                terrainPropertyBlock = new MaterialPropertyBlock();
            }

            bool validTerrain = terrain != null && terrain.terrainData != null && terrain.materialTemplate != null;
            bool validRenderer = meshRenderer != null && meshRenderer.sharedMaterial != null;

            if (validTerrain)
            {
                if (terrain.terrainData.holesTexture != null)
                {
                    terrainPropertyBlock.SetTexture("_TerrainHolesTex", terrain.terrainData.holesTexture);
                }

                for (int i = 0; i < terrain.terrainData.alphamapTextures.Length; i++)
                {
                    var splat = terrain.terrainData.alphamapTextures[i];
                    var index = i + 1;

                    if (splat != null)
                    {
                        terrainPropertyBlock.SetTexture("_TerrainControlTex" + index, splat);
                    }
                }

                for (int i = 0; i < terrain.terrainData.terrainLayers.Length; i++)
                {
                    var layer = terrain.terrainData.terrainLayers[i];
                    var index = i + 1;

                    if (layer == null)
                    {
                        continue;
                    }

                    CopyLayerSettings(terrainPropertyBlock, layer, index);
                }
            }

            if (validTerrain || validRenderer)
            {
                if (terrainSettings.useCustomTextures)
                {
                    if (terrainSettings.terrainHolesMask != null)
                    {
                        terrainPropertyBlock.SetTexture("_TerrainHolesTex", terrainSettings.terrainHolesMask);
                    }

                    if (terrainSettings.terrainControl01 != null)
                    {
                        terrainPropertyBlock.SetTexture("_TerrainControlTex1", terrainSettings.terrainControl01);
                    }

                    if (terrainSettings.terrainControl02 != null)
                    {
                        terrainPropertyBlock.SetTexture("_TerrainControlTex2", terrainSettings.terrainControl02);
                    }

                    if (terrainSettings.terrainControl03 != null)
                    {
                        terrainPropertyBlock.SetTexture("_TerrainControlTex3", terrainSettings.terrainControl03);
                    }

                    if (terrainSettings.terrainControl04 != null)
                    {
                        terrainPropertyBlock.SetTexture("_TerrainControlTex4", terrainSettings.terrainControl04);
                    }
                }

                // Terrain Layer Overrides
                for (int i = 0; i < terrainSettings.terrainLayers.Count; i++)
                {
                    var overrideElement = terrainSettings.terrainLayers[i];

                    if (!overrideElement.isInitialized)
                    {
                        terrainSettings.terrainLayers[i] = new TVETerrainLayerSettings();
                        terrainSettings.terrainLayers[i].isInitialized = true;
                    }

                    int index;

                    if (terrainSettings.useLayersOrderAsID)
                    {
                        index = i;
                    }
                    else
                    {
                        index = overrideElement.layerID;
                    }

                    terrainPropertyBlock.SetVector("_TerrainColor" + index, overrideElement.layerColor);

                    if (/*terrainLayerSettings.overrideAllLayers && */ overrideElement.useCustomLayer)
                    {
                        if (overrideElement.terrainLayer != null)
                        {
                            CopyLayerSettings(terrainPropertyBlock, overrideElement.terrainLayer, index);
                        }
                    }

                    if (/*terrainLayerSettings.overrideAllTextures && */ overrideElement.useCustomTextures)
                    {
                        if (overrideElement.layerAlbedo != null)
                        {
                            terrainPropertyBlock.SetTexture("_TerrainAlbedoTex" + index, overrideElement.layerAlbedo);
                        }

                        if (overrideElement.layerNormal != null)
                        {
                            terrainPropertyBlock.SetTexture("_TerrainNormalTex" + index, overrideElement.layerNormal);
                        }

                        if (overrideElement.layerShader != null)
                        {
                            terrainPropertyBlock.SetTexture("_TerrainShaderTex" + index, overrideElement.layerShader);
                        }
                    }

                    if (/*terrainLayerSettings.overrideAllSettings &&*/ overrideElement.useCustomSettings)
                    {
                        terrainPropertyBlock.SetVector("_TerrainSpecular" + index, overrideElement.layerSpecular);
                        terrainPropertyBlock.SetVector("_TerrainShaderMin" + index, overrideElement.layerRemapMin);
                        terrainPropertyBlock.SetVector("_TerrainShaderMax" + index, overrideElement.layerRemapMax);
                        terrainPropertyBlock.SetVector("_TerrainParams" + index, new Vector4(0, 0, overrideElement.layerNormalScale, overrideElement.layerSmoothness));
                    }

                    if (/*terrainLayerSettings.overrideAllSettings &&*/ overrideElement.useCustomCoords)
                    {
                        if (overrideElement.layerUVMode == TVEUVMode.Tilling)
                        {
                            terrainPropertyBlock.SetVector("_TerrainCoord" + index, new Vector4(overrideElement.layerUVValue.x, overrideElement.layerUVValue.y, overrideElement.layerUVValue.z, overrideElement.layerUVValue.w));
                        }
                        else
                        {
                            terrainPropertyBlock.SetVector("_TerrainCoord" + index, new Vector4(1 / overrideElement.layerUVValue.x, 1 / overrideElement.layerUVValue.y, overrideElement.layerUVValue.z, overrideElement.layerUVValue.w));
                        }
                    }
                }
            }

            if (validTerrain)
            {
                terrainMaterial = terrain.materialTemplate;

                // Terrain Transform
                terrainPropertyBlock.SetVector("_TerrainPosition", terrain.transform.position);
                terrainPropertyBlock.SetVector("_TerrainSize", terrain.terrainData.size);
                terrainPropertyBlock.SetFloat("_TerrainModelMode", 0);

                terrain.SetSplatMaterialPropertyBlock(terrainPropertyBlock);

                terrain.patchBoundsMultiplier = new Vector3(terrainBounds, terrainBounds, terrainBounds);
            }

            if (validRenderer)
            {
                terrainMaterial = meshRenderer.sharedMaterial;

                terrainPropertyBlock.SetVector("_TerrainPosition", meshRenderer.bounds.center);
                terrainPropertyBlock.SetVector("_TerrainSize", meshRenderer.bounds.size);
                terrainPropertyBlock.SetFloat("_TerrainModelMode", 1);

                meshRenderer.SetPropertyBlock(terrainPropertyBlock);
            }
        }

        void CopyLayerSettings(MaterialPropertyBlock materialPropertyBlock, TerrainLayer layer, int id)
        {
            if (layer.diffuseTexture != null)
            {
                materialPropertyBlock.SetTexture("_TerrainAlbedoTex" + id, layer.diffuseTexture);
            }
            else
            {
                materialPropertyBlock.SetTexture("_TerrainAlbedoTex" + id, Texture2D.whiteTexture);
            }

            if (layer.normalMapTexture != null)
            {
                materialPropertyBlock.SetTexture("_TerrainNormalTex" + id, layer.normalMapTexture);
            }
            else
            {
                materialPropertyBlock.SetTexture("_TerrainNormalTex" + id, Texture2D.normalTexture);
            }

            if (layer.maskMapTexture != null)
            {
                materialPropertyBlock.SetTexture("_TerrainShaderTex" + id, layer.maskMapTexture);
            }
            else
            {
                materialPropertyBlock.SetTexture("_TerrainShaderTex" + id, Texture2D.whiteTexture);
            }

            materialPropertyBlock.SetVector("_TerrainShaderMin" + id, layer.maskMapRemapMin);
            materialPropertyBlock.SetVector("_TerrainShaderMax" + id, layer.maskMapRemapMax);
            materialPropertyBlock.SetVector("_TerrainParams" + id, new Vector4(layer.metallic, 0, layer.normalScale, layer.smoothness));
            materialPropertyBlock.SetVector("_TerrainSpecular" + id, layer.specular);
            materialPropertyBlock.SetVector("_TerrainCoord" + id, new Vector4(1 / layer.tileSize.x, 1 / layer.tileSize.y, layer.tileOffset.x, layer.tileOffset.y));
        }

#if UNITY_EDITOR
        void UpdateOverrideNames()
        {
            // Terrain Layer Overrides
            for (int i = 0; i < terrainSettings.terrainLayers.Count; i++)
            {
                var overrideElement = terrainSettings.terrainLayers[i];
                
                int index;

                if (terrainSettings.useLayersOrderAsID)
                {
                    index = i;
                }
                else
                {
                    index = overrideElement.layerID - 1;
                }

                bool validTerrain = terrain != null && terrain.terrainData != null;

                if (validTerrain && terrain.terrainData.terrainLayers != null && terrain.terrainData.terrainLayers.Length > index && terrain.terrainData.terrainLayers[index] != null)
                {
                    overrideElement.name = terrain.terrainData.terrainLayers[index].name;
                }
                else
                {
                    overrideElement.name = "Layer " + overrideElement.layerID + " (Missing)";
                }

                if (/*terrainLayerSettings.overrideAllLayers &&*/ overrideElement.useCustomLayer)
                {
                    if (overrideElement.terrainLayer != null)
                    {
                        overrideElement.name = overrideElement.terrainLayer.name;
                    }
                }
            }
        }

        //void UpdateOverrideDebugData()
        //{
        //    var perLayerMaps = 3;

        //    if (terrainShaderSettings.shaderMaps == TVETerrainMapsMode.Packed)
        //    {
        //        perLayerMaps = 2;
        //    }

        //    var sampleCountList = new List<int>();

        //    for (int i = 0; i < (int)terrainShaderSettings.shaderLayers; i++)
        //    {
        //        sampleCountList.Add(1);
        //    }

        //    // Terrain Shader Overrides
        //    for (int i = 0; i < terrainShaderSettings.shaderLayerOverrides.Count; i++)
        //    {
        //        var overrideElement = terrainShaderSettings.shaderLayerOverrides[i];
        //        var overrideID = overrideElement.layerID - 1;

        //        if (terrain.terrainData.terrainLayers != null && terrain.terrainData.terrainLayers.Length > overrideID && terrain.terrainData.terrainLayers[overrideID] != null)
        //        {
        //            if (terrainShaderSettings.useAllFeatureOverrides)
        //            {
        //                if (overrideElement.textureCoords == TVETextureCoordsMode.Planar)
        //                {
        //                    if (overrideElement.textureSample == TVETextureSampleMode.Stochastic)
        //                    {
        //                        sampleCountList[overrideID] = 3;
        //                    }
        //                    else
        //                    {
        //                        sampleCountList[overrideID] = 1;
        //                    }
        //                }

        //                if (overrideElement.textureCoords == TVETextureCoordsMode.Triplanar)
        //                {
        //                    if (overrideElement.textureSample == TVETextureSampleMode.Stochastic)
        //                    {
        //                        sampleCountList[overrideID] = 9;
        //                    }
        //                    else
        //                    {
        //                        sampleCountList[overrideID] = 3;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    var sampleCount = 0;

        //    for (int i = 0; i < sampleCountList.Count; i++)
        //    {
        //        sampleCount += sampleCountList[i];
        //    }

        //    terrainShaderSettings.debugData = "<size=10>Layers: " + (int)terrainShaderSettings.shaderLayers + " supported | Maps: " + (int)terrainShaderSettings.shaderLayers * perLayerMaps + " textures | Samples: " + sampleCount * perLayerMaps + " texture samples</size>";
        //}

        void OnValidate()
        {
            InitializeTerrain();
        }
#endif
    }
}


