// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using Boxophobic.StyledGUI;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Rendering;

namespace TheVisualEngine
{
    [HelpURL("https://docs.google.com/document/d/145JOVlJ1tE-WODW45YoJ6Ixg23mFc56EnB_8Tbwloz8/edit#heading=h.hbq3w8ae720x")]
    [ExecuteInEditMode]
    [AddComponentMenu("BOXOPHOBIC/The Visual Engine/TVE Manager")]
    public class TVEManager : StyledMonoBehaviour
    {
        public static TVEManager Instance;

        [StyledBanner(0.890f, 0.745f, 0.309f, "The Visual Engine")]
        public bool styledBanner;

        [StyledCategory("Quick Settings", 5, 10)]
        public bool quickCat;

        [Tooltip("Controls the global wind power.")]
        [StyledRangeOptions("Motion Control", 0, 1, new string[] { "None", "Windy", "Strong" })]
        public float motionControl = 0.5f;

        [Space(10)]
        [Tooltip("Use the Seasons slider to control the element properties when the element is set to Seasons mode.")]
        [StyledRangeOptions("Season Control", 0, 4, new string[] { "Winter", "Spring", "Summer", "Autumn", "Winter" })]
        public float seasonControl = 2f;

        [StyledCategory("Main Settings")]
        public bool lightCat;

        [StyledMessage("Error", "Main Camera not found! Make sure you have a main camera with Main Camera tag in your scene! Particle elements updating will be skipped without it. Enter play mode to update the status!", 0, 10)]
        public bool styledCameraMessaage = false;

        [Tooltip("Sets the main camera used for scene rendering.")]
        public Camera mainCamera;

        [Tooltip("Sets the main light used as the sun in the scene.")]
        public Light mainLight;

        [Tooltip("Sets the main direction from a gameobject.")]
        public GameObject mainWind;

        [StyledCategory("Global Settings")]
        public bool defaultCat;

        public TVEGlobalCoatData globalCoatData = new();
        public TVEGlobalPaintData globalPaintData = new();
        public TVEGlobalAtmoData globalAtmoData = new();
        public TVEGlobalFadeData globalFadeData = new();
        public TVEGlobalGlowData globalGlowData = new();
        public TVEGlobalFormData globalFormData = new();
        public TVEGlobalLandData globalLandData = new();

        [StyledCategory("Element Settings")]
        public bool rendererCat;

#if UNITY_EDITOR
        [StyledMessage("Info", "Realtime Sorting is not supported for elements with GPU Instanceing enabled!", 0, 10)]
        public bool styledSortingMessaage = true;
#endif

        [Tooltip("Controls the elements visibility in scene and game view.")]
        public TVEElementsVisibility elementVisibility = TVEElementsVisibility.HiddenAtRuntime;
        [HideInInspector]
        public TVEElementsVisibility elementVisibilityOld = TVEElementsVisibility.HiddenAtRuntime;
        [Tooltip("Controls the elements sorting by element position. Always on in edit mode.")]
        public TVEElementsSorting elementSorting = TVEElementsSorting.SortInEditMode;

        [Space(10)]
        public TVETextureSize renderBaseResolution = TVETextureSize._512;
        public float renderBaseDistance = 400f;
        public TVETextureSize renderNearResolution = TVETextureSize._512;
        public float renderNearDistance = 40f;

        [Range(0, 1)]
        public float renderFadeTransition = 0.5f;

#if !THE_VISUAL_ENGINE_DEBUG
        [System.NonSerialized]
#endif
        [Space(10)]
        public List<TVERenderData> renderDataSet = new List<TVERenderData>();

#if !THE_VISUAL_ENGINE_DEBUG
        [System.NonSerialized]
#endif
        public List<TVEElement> renderElements = new List<TVEElement>();

#if !THE_VISUAL_ENGINE_DEBUG
        [System.NonSerialized]
#endif
        public List<TVEInstanced> renderInstances = new List<TVEInstanced>();

        [System.NonSerialized]
        public Vector3 focusPosition;
        Vector3 focusRotation;

        MaterialPropertyBlock propertyBlock;

        Matrix4x4 projectionMatrix;
        Matrix4x4 modelViewMatrix = new Matrix4x4
        (
            new Vector4(1f, 0f, 0f, 0f),
            new Vector4(0f, 0f, -1f, 0f),
            new Vector4(0f, -1f, 0f, 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );

        void OnEnable()
        {
#if UNITY_EDITOR
            if (gameObject.GetComponent<MeshRenderer>() == null)
            {
                gameObject.AddComponent<MeshRenderer>();
                gameObject.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<Material>("Internal Arrow");
                gameObject.GetComponent<MeshRenderer>().hideFlags = HideFlags.HideInInspector;
            }

            if (gameObject.GetComponent<MeshFilter>() == null)
            {
                gameObject.AddComponent<MeshFilter>();
                gameObject.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Internal Mesh Arrow");
                gameObject.GetComponent<MeshFilter>().hideFlags = HideFlags.HideInInspector;
            }
#endif

            EnableManager();

            InitElementsRendering();

            SortElementObjects();
            SetElementsVisibility();
        }

        void OnDisable()
        {
            DisableManager();
        }

        void OnDestroy()
        {
            DisableManager();
        }

        void Start()
        {
            // Disable Arrow in play mode
            if (Application.isPlaying == true)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }

            if (mainLight == null)
            {
                SetGlobalLightingMainLight();
            }
        }

        void Update()
        {
            if (mainWind == null)
            {
                mainWind = gameObject;
            }

            gameObject.transform.eulerAngles = new Vector3(0, mainWind.transform.eulerAngles.y, 0);

            SetGlobalShaderProperties();
        }


        void LateUpdate()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            if (mainCamera != null)
            {
                focusPosition = mainCamera.transform.position;
                focusRotation = mainCamera.transform.eulerAngles;
            }

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (SceneView.lastActiveSceneView != null)
                {
                    focusPosition = SceneView.lastActiveSceneView.camera.transform.position;
                    focusRotation = SceneView.lastActiveSceneView.camera.transform.eulerAngles;
                }
            }
#endif

            Shader.SetGlobalFloat("TVE_MainCameraSpeedValue", Vector3.Magnitude(focusPosition) * 0.1f + (focusRotation.x + focusRotation.y) * 0.001f);

            if (propertyBlock == null)
            {
                propertyBlock = new MaterialPropertyBlock();
            }

            if (elementSorting == TVEElementsSorting.SortAtRuntime || sortDirty)
            {
                SortElementObjects();
            }

            if (elementVisibilityOld != elementVisibility)
            {
                SetElementsVisibility();

                elementVisibilityOld = elementVisibility;
            }

            SubmitRenderBuffers();
            ExecuteRenderBuffers(true);
            ExecuteRenderBuffers(false);

#if UNITY_EDITOR
            if (Selection.Contains(gameObject))
            {
                //UpdateDebugData();

                if (elementSorting == TVEElementsSorting.SortAtRuntime)
                {
                    styledSortingMessaage = true;
                }
                else
                {
                    styledSortingMessaage = false;
                }

                if (mainCamera == null)
                {
                    styledCameraMessaage = true;
                }
                else
                {
                    styledCameraMessaage = false;
                }
            }
#endif
        }

        void EnableManager()
        {
            if (TVEManager.Instance != null && TVEManager.Instance != this)
            {
                TVEManager.Instance.gameObject.SetActive(false);
            }

            Instance = this;
            Instance.name = "The Visual Engine";

            Shader.SetGlobalFloat("TVE_ManagerActive", 1);
            Shader.SetGlobalFloat("TVE_IsEnabled", 1);
        }

        void DisableManager()
        {
            Instance = null;

            //globalVolume.DisableVolumeRendering();

            Shader.SetGlobalFloat("TVE_ManagerActive", 0);
            Shader.SetGlobalFloat("TVE_IsEnabled", 0);

            DestroyRenderBuffers();
            DisableElementsRendering();
        }

        void SetGlobalShaderProperties()
        {
            Shader.SetGlobalFloat("TVE_RenderBaseFadeValue", (1 - 0.75f) * 0.999f);
            Shader.SetGlobalFloat("TVE_RenderNearFadeValue", (1 - renderFadeTransition) * 0.999f);

            if (mainLight != null)
            {
                var mainLightColor = mainLight.color.linear;
                var mainLightIntensity = mainLight.intensity;
                var mainLightValue = new Color(mainLightIntensity, mainLightIntensity, mainLightIntensity).linear;
                var mainLightParams = new Color(mainLightColor.r, mainLightColor.g, mainLightColor.b, mainLightValue.r);

                Shader.SetGlobalVector("TVE_MainLightParams", mainLightParams);
                Shader.SetGlobalVector("TVE_MainLightDirection", Vector4.Normalize(-mainLight.transform.forward));
            }
            else
            {
                var mainLightParams = new Vector4(1, 1, 1, 1);

                Shader.SetGlobalVector("TVE_MainLightParams", mainLightParams);
                Shader.SetGlobalVector("TVE_MainLightDirection", new Vector4(0, 1, 0, 0));
            }

            float seasonLerp = 0;

            if (seasonControl >= 0 && seasonControl < 1)
            {
                seasonLerp = seasonControl;
                Shader.SetGlobalVector("TVE_SeasonOption", new Vector4(1, 0, 0, 0));
            }
            else if (seasonControl >= 1 && seasonControl < 2)
            {
                seasonLerp = seasonControl - 1.0f;
                Shader.SetGlobalVector("TVE_SeasonOption", new Vector4(0, 1, 0, 0));
            }
            else if (seasonControl >= 2 && seasonControl < 3)
            {
                seasonLerp = seasonControl - 2.0f;
                Shader.SetGlobalVector("TVE_SeasonOption", new Vector4(0, 0, 1, 0));
            }
            else if (seasonControl >= 3 && seasonControl <= 4)
            {
                seasonLerp = seasonControl - 3.0f;
                Shader.SetGlobalVector("TVE_SeasonOption", new Vector4(0, 0, 0, 1));
            }

            var smoothLerp = Mathf.SmoothStep(0, 1, seasonLerp);
            Shader.SetGlobalFloat("TVE_SeasonLerp", smoothLerp);

            if (QualitySettings.activeColorSpace == ColorSpace.Linear)
            {
                var color = globalPaintData.tintingColor.linear;
                var alpha = globalPaintData.tintingIntensity;
                Shader.SetGlobalVector("TVE_PaintParams", new Vector4(color.r, color.g, color.b, alpha));
            }
            else
            {
                var color = globalPaintData.tintingColor;
                var alpha = globalPaintData.tintingIntensity;
                Shader.SetGlobalVector("TVE_PaintParams", new Vector4(color.r, color.g, color.b, alpha));
            }

            if (QualitySettings.activeColorSpace == ColorSpace.Linear)
            {
                var color = globalGlowData.emissiveColor.linear;
                var alpha = globalGlowData.subsurfaceIntensity;
                Shader.SetGlobalVector("TVE_GlowParams", new Vector4(color.r, color.g, color.b, alpha));
            }
            else
            {
                var color = globalGlowData.emissiveColor;
                var alpha = globalGlowData.subsurfaceIntensity;
                Shader.SetGlobalVector("TVE_GlowParams", new Vector4(color.r, color.g, color.b, alpha));
            }

            var coatParams = new Color(globalCoatData.terrainIntensity, globalCoatData.detailIntensity, globalCoatData.layerIntensity, 0);
            Shader.SetGlobalVector("TVE_CoatParams", coatParams);

            var atmoParams = new Color(globalAtmoData.drynessIntensity, globalAtmoData.wetnessIntensity, globalAtmoData.overlayIntensity, globalAtmoData.raindropsIntensity);
            Shader.SetGlobalVector("TVE_AtmoParams", atmoParams);

            var fadeParams = new Color(0, 0, 0, globalFadeData.cutoutIntensity);
            Shader.SetGlobalVector("TVE_FadeParams", fadeParams);

            var formParams = new Vector4(0.5f, 0.5f, globalFormData.heightConform, globalFormData.sizeFadeValue);
            Shader.SetGlobalVector("TVE_FormParams", formParams);

            var landParams = new Vector4(0.5f, 0.5f, globalLandData.heightOffset, 0);
            Shader.SetGlobalVector("TVE_LandParams", landParams);

            var motionDirection = mainWind.transform.forward;

            var materialManagerActive = Shader.GetGlobalFloat("TVE_MaterialManagerActive");
            var motionControlEditor = Shader.GetGlobalFloat("TVE_MotionControlEditor");

            if (materialManagerActive == 0)
            {
                Shader.SetGlobalFloat("TVE_MotionIntensityValue", motionControl);
                Shader.SetGlobalVector("TVE_MotionParams", new Vector4(motionDirection.x * 0.5f + 0.5f, motionDirection.z * 0.5f + 0.5f, motionControl, 0.0f));
            }
            else
            {
                Shader.SetGlobalFloat("TVE_MotionIntensityValue", motionControlEditor);
                Shader.SetGlobalVector("TVE_MotionParams", new Vector4(motionDirection.x * 0.5f + 0.5f, motionDirection.z * 0.5f + 0.5f, motionControlEditor, 0.0f));
            }

            Shader.SetGlobalVector("TVE_WindParams", new Vector4(0.5f, 0.5f, 0.0f, 0.0f));
            Shader.SetGlobalVector("TVE_PushParams", new Vector4(0.5f, 0.5f, 0.0f, 0.0f));

        }

        public void InitElementsRendering()
        {
            renderDataSet = new List<TVERenderData>();
            renderElements = new List<TVEElement>();
            renderInstances = new List<TVEInstanced>();
        }

        public void DisableElementsRendering()
        {
            for (int e = 0; e < renderElements.Count; e++)
            {
                var element = renderElements[e];

                if (element != null)
                {
                    element.isActive = false;
                }
            }

            for (int e = 0; e < renderInstances.Count; e++)
            {
                var instances = renderInstances[e];

                for (int p = 0; p < instances.elements.Count; p++)
                {
                    if (instances.elements[p] != null)
                    {
                        instances.elements[p].isActive = false;
                    }
                }
            }

            renderDataSet = new List<TVERenderData>();
            renderElements = new List<TVEElement>();
            renderInstances = new List<TVEInstanced>();
        }

        public void CreateRenderData(string renderName)
        {
            var renderData = new TVERenderData();

            if (!renderData.isInitialized)
            {
                renderData.renderMode = TVEBool.On;
                renderData.renderName = "Custom";
                renderData.textureType = TVETextureRange.HDR;
                renderData.textureArray = TVEBool.On;
                renderData.isRendering = true;

                renderData.isInitialized = true;
            }

            var globalName = "TVE_" + renderName;

            renderData.renderName = renderName;
            renderData.texBaseName = globalName + "BaseTex";
            renderData.texNearName = globalName + "NearTex";
            renderData.texParams = globalName + "Params";
            renderData.texLayers = globalName + "Layers";

            renderData.renderDataID = renderName.GetHashCode();
            renderData.bufferSize = -1;

            renderDataSet.Add(renderData);

            Shader.SetGlobalFloat(globalName + "Active", 1);
        }

        public void CreateRenderBuffer(TVERenderData renderData)
        {
            if (renderData.renderTexBase != null)
            {
                renderData.renderTexBase.Release();
            }

            if (renderData.renderTexNear != null)
            {
                renderData.renderTexNear.Release();
            }

            if (renderData.commandBuffers != null)
            {
                for (int b = 0; b < renderData.commandBuffers.Length; b++)
                {
                    renderData.commandBuffers[b].Clear();
                }
            }

            renderData.bufferUsage = new float[9];

            // Might need initialization to avoid issues in the shaders
            for (int i = 0; i < renderData.bufferUsage.Length; i++)
            {
                renderData.bufferUsage[i] = 0f;
            }

            Shader.SetGlobalFloatArray(renderData.texLayers, renderData.bufferUsage);

            if (renderData.renderMode != TVEBool.Off && renderData.bufferSize > -1)
            {
                //int baseSize = Mathf.Max(Mathf.RoundToInt((int)baseResolution * renderScale), 32);
                //int nearSize = Mathf.Max(Mathf.RoundToInt((int)nearResolution * renderScale), 32);

                int baseSize = (int)renderBaseResolution;
                int nearSize = (int)renderNearResolution;

                RenderTextureFormat RTFormat = RenderTextureFormat.Default;

                if (renderData.textureType == TVETextureRange.HDR)
                {
                    RTFormat = RenderTextureFormat.ARGBHalf;
                }

                renderData.renderTexBase = new RenderTexture(baseSize, baseSize, 0, RTFormat, 0);
                renderData.renderTexNear = new RenderTexture(nearSize, nearSize, 0, RTFormat, 0);

                TextureDimension RTDimention = TextureDimension.Tex2D;

                if (renderData.textureArray == TVEBool.On)
                {
                    RTDimention = TextureDimension.Tex2DArray;
                }

                renderData.renderTexBase.dimension = RTDimention;
                renderData.renderTexBase.volumeDepth = renderData.bufferSize + 1;
                renderData.renderTexBase.name = renderData.texBaseName;
                renderData.renderTexBase.wrapMode = TextureWrapMode.Clamp;
                renderData.renderTexBase.useMipMap = false;

                renderData.renderTexNear.dimension = RTDimention;
                renderData.renderTexNear.volumeDepth = renderData.bufferSize + 1;
                renderData.renderTexNear.name = renderData.texNearName;
                renderData.renderTexNear.wrapMode = TextureWrapMode.Clamp;
                renderData.renderTexNear.useMipMap = false;

                renderData.commandBuffers = new CommandBuffer[renderData.bufferSize + 1];

                for (int b = 0; b < renderData.commandBuffers.Length; b++)
                {
                    renderData.commandBuffers[b] = new CommandBuffer();
                    renderData.commandBuffers[b].name = "The Visual Engine/" + renderData.renderName;
                }

                Shader.SetGlobalTexture(renderData.texBaseName, renderData.renderTexBase);
                Shader.SetGlobalTexture(renderData.texNearName, renderData.renderTexNear);
                //Shader.SetGlobalInt(renderData.texLayersMax, renderData.bufferSize + 1);
            }
            else
            {
                if (renderData.textureArray == TVEBool.On)
                {
                    Shader.SetGlobalTexture(renderData.texBaseName, Resources.Load<Texture2DArray>("Internal ArrayTex"));
                    Shader.SetGlobalTexture(renderData.texNearName, Resources.Load<Texture2DArray>("Internal ArrayTex"));
                }
                else
                {
                    Shader.SetGlobalTexture(renderData.texBaseName, Texture2D.whiteTexture);
                }

                //Shader.SetGlobalInt(renderData.texLayersMax, 1);
            }
        }

        void SubmitRenderBuffers()
        {
            for (int d = 0; d < renderDataSet.Count; d++)
            {
                var renderData = renderDataSet[d];

                if (renderData == null || renderData.commandBuffers == null || renderData.renderMode == TVEBool.Off || !renderData.isRendering)
                {
                    continue;
                }

                var bufferParams = Shader.GetGlobalVector(renderData.texParams);

                for (int b = 0; b < renderData.commandBuffers.Length; b++)
                {
                    renderData.commandBuffers[b].Clear();
                    renderData.commandBuffers[b].ClearRenderTarget(true, true, bufferParams);
                    renderData.bufferUsage[b] = 0;

                    for (int e = 0; e < renderElements.Count; e++)
                    {
                        var element = renderElements[e];

                        if (element.isActive == false || element == null)
                        {
                            renderElements.RemoveAt(e);

                            continue;
                        }

                        if (renderData.renderDataID == element.renderDataID)
                        {
                            if (element.renderLayers[b] == 1)
                            {
                                // Optimization when particle elements are not used
                                if (element.elementMesh == null)
                                {
                                    Camera.SetupCurrent(mainCamera);
                                }

                                propertyBlock.SetVector("_ElementParams", element.elementParams);
                                element.elementRenderer.SetPropertyBlock(propertyBlock);

                                renderData.commandBuffers[b].DrawRenderer(element.elementRenderer, element.elementMaterial, 0, element.renderPass);
                                renderData.bufferUsage[b] = 1;
                            }
                        }

                    }

                    for (int e = 0; e < renderInstances.Count; e++)
                    {
                        var instances = renderInstances[e];

                        if (!instances.material.enableInstancing)
                        {
                            continue;
                        }

                        if (instances.elements.Count == 0)
                        {
                            continue;
                        }

                        if (renderData.renderDataID == instances.renderDataID)
                        {
                            if (instances.renderLayers[b] == 1)
                            {
                                for (int p = 0; p < instances.elements.Count; p++)
                                {
                                    if (instances.elements[p].isActive == false || instances.elements[p] == null || instances.renderers[p] == null)
                                    {
                                        instances.elements.RemoveAt(p);
                                        instances.renderers.RemoveAt(p);

                                        continue;
                                    }
                                }

                                var elementsCount = instances.elements.Count;

                                if (elementsCount == 0)
                                {
                                    continue;
                                }

                                if (instances.matrices == null || instances.matrices.Length != elementsCount)
                                {
                                    instances.matrices = new Matrix4x4[elementsCount];
                                    instances.parameters = new Vector4[elementsCount];
                                }

                                for (int p = 0; p < elementsCount; p++)
                                {
                                    instances.matrices[p] = instances.renderers[p].localToWorldMatrix;
                                    instances.parameters[p] = instances.elements[p].elementParams;
                                }

                                if (instances.propertyBlockCount != elementsCount)
                                {
                                    if (instances.propertyBlock == null)
                                    {
                                        instances.propertyBlock = new MaterialPropertyBlock();
                                    }
                                    else
                                    {
                                        instances.propertyBlock.Clear();
                                    }

                                    instances.propertyBlockCount = elementsCount;
                                }

                                instances.propertyBlock.SetVectorArray("_ElementParams", instances.parameters);

                                renderData.commandBuffers[b].DrawMeshInstanced(instances.mesh, 0, instances.material, instances.renderPass, instances.matrices, elementsCount, instances.propertyBlock);
                                renderData.bufferUsage[b] = 1;
                            }
                        }
                    }
                }

                Shader.SetGlobalFloatArray(renderData.texLayers, renderData.bufferUsage);

                //for (int u = 0; u < renderData.bufferUsage.Length; u++)
                //{
                //    Debug.Log(renderData.texLayers + " Index: " + u + " Usage: " + renderData.bufferUsage[u]);
                //}
            }
        }

        void ExecuteRenderBuffers(bool isBase)
        {
            GL.PushMatrix();
            RenderTexture currentRenderTexture = RenderTexture.active;

            var position = focusPosition;
            var scale = new Vector3(renderBaseDistance * 2f, 100000, renderBaseDistance * 2f);

            if (!isBase)
            {
                scale = new Vector3(renderNearDistance * 2f, 100000, renderNearDistance * 2f);
            }

            //var resolution = Mathf.Max(32, renderBaseSize);
            var resolution = (int)renderBaseResolution;

            if (!isBase)
            {
                //resolution = Mathf.Max(32, renderNearSize);
                resolution = (int)renderNearResolution;
            }

            var gridX = scale.x / resolution;
            var gridZ = scale.z / resolution;
            var posX = Mathf.Round(position.x / gridX) * gridX;
            var posZ = Mathf.Round(position.z / gridZ) * gridZ;

            position = new Vector3(posX, position.y, posZ);

            var x = 1 / scale.x;
            var y = 1 / scale.z;
            var z = x * position.x - 0.5f;
            var w = y * position.z - 0.5f;
            var coords = new Vector4(x, y, -z, -w);

            if (isBase)
            {
                Shader.SetGlobalVector("TVE_RenderBaseCoords", coords);
                Shader.SetGlobalVector("TVE_RenderBasePositionR", new Vector4(position.x, position.y, position.z, renderBaseDistance));
            }
            else
            {
                Shader.SetGlobalVector("TVE_RenderNearCoords", coords);
                Shader.SetGlobalVector("TVE_RenderNearPositionR", new Vector4(position.x, position.y, position.z, renderNearDistance));
            }


            //Shader.SetGlobalVector(renderData.volPosition, position);
            //Shader.SetGlobalVector(renderData.volScale, scale);

            //renderData.internalPosition = position;
            //renderData.internalScale = scale;

            //if (renderData.renderMode == OORenderDataMode.ScreenSpaceProjection)
            //{
            //    if (mainCamera != null)
            //    {
            //        projectionMatrix = mainCamera.projectionMatrix;
            //    }
            //}
            //else
            {
                GL.modelview = modelViewMatrix;

                projectionMatrix = Matrix4x4.Ortho(-scale.x / 2 + position.x,
                                                    scale.x / 2 + position.x,
                                                    scale.z / 2 - position.z,
                                                   -scale.z / 2 - position.z,
                                                   -scale.y / 2 + position.y,
                                                    scale.y / 2 + position.y);
            }

            GL.LoadProjectionMatrix(projectionMatrix);

            for (int d = 0; d < renderDataSet.Count; d++)
            {
                var renderData = renderDataSet[d];

                if (renderData == null || renderData.commandBuffers == null || renderData.renderMode == TVEBool.Off || !renderData.isRendering)
                {
                    continue;
                }

                for (int b = 0; b < renderData.commandBuffers.Length; b++)
                {
                    if (isBase)
                    {
                        Graphics.SetRenderTarget(renderData.renderTexBase, 0, CubemapFace.Unknown, b);
                    }
                    else
                    {
                        Graphics.SetRenderTarget(renderData.renderTexNear, 0, CubemapFace.Unknown, b);
                    }

                    Graphics.ExecuteCommandBuffer(renderData.commandBuffers[b]);
                }
            }

            RenderTexture.active = currentRenderTexture;
            GL.PopMatrix();
        }

        void DestroyRenderBuffers()
        {
            for (int d = 0; d < renderDataSet.Count; d++)
            {
                var renderData = renderDataSet[d];

                if (renderData == null)
                {
                    continue;
                }

                if (renderData.renderTexBase != null)
                {
                    renderData.renderTexBase.Release();
                }

                if (renderData.renderTexNear != null)
                {
                    renderData.renderTexNear.Release();
                }

                if (renderData.commandBuffers != null)
                {
                    for (int b = 0; b < renderData.commandBuffers.Length; b++)
                    {
                        renderData.commandBuffers[b].Clear();
                        renderData.bufferUsage[b] = 0;
                    }
                }

                // Reset buffer usage because it is stored globally and is transfered to next scene
                Shader.SetGlobalFloatArray(renderData.texLayers, renderData.bufferUsage);
            }
        }

        bool sortDirty = false;
        public void MarkSortDirty() => sortDirty = true;

        public void SortElementObjects()
        {
            renderElements.Sort((e1, e2) =>
            {
                if (e1 == null && e2 == null) return 0;
                if (e1 == null) return -1;
                if (e2 == null) return 1;
                return e1.transform.position.y.CompareTo(e2.transform.position.y);
            });
            sortDirty = false;
            //for (int i = 0; i < elementObjects.Count - 1; i++) { 
            //  for (int j = 0; j < elementObjects.Count - 1; j++) {
            //    var prevElement = elementObjects[j];
            //    var nextElement = elementObjects[j + 1];

            //    if (prevElement != null && nextElement != null) {
            //      if (prevElement.gameObject.transform.position.y > nextElement.gameObject.transform.position.y) {
            //        var next = elementObjects[j + 1];
            //        elementObjects[j + 1] = elementObjects[j];
            //        elementObjects[j] = next;
            //      }
            //    }
            //  }
            //}
        }

        void SetElementsVisibility()
        {
            if (elementVisibility == TVEElementsVisibility.AlwaysHidden)
            {
                DisableElementsVisibility();
            }
            else if (elementVisibility == TVEElementsVisibility.AlwaysVisible)
            {
                EnableElementsVisibility();
            }
            else if (elementVisibility == TVEElementsVisibility.HiddenAtRuntime)
            {
                if (Application.isPlaying)
                {
                    DisableElementsVisibility();
                }
                else
                {
                    EnableElementsVisibility();
                }
            }
        }

        void EnableElementsVisibility()
        {
            for (int i = 0; i < renderElements.Count; i++)
            {
                var element = renderElements[i];

                if (element != null && element.customVisibility == TVEElementVisibility.UseGlobalSettings)
                {
                    element.elementRenderer.enabled = true;
                }
            }
        }

        void DisableElementsVisibility()
        {
            for (int i = 0; i < renderElements.Count; i++)
            {
                var element = renderElements[i];

                if (element != null && element.customVisibility == TVEElementVisibility.UseGlobalSettings)
                {
                    element.elementRenderer.enabled = false;
                }
            }
        }

        void SetGlobalLightingMainLight()
        {
#if UNITY_2023_1_OR_NEWER
            var allLights = FindObjectsByType<Light>(FindObjectsSortMode.None);
#else
            var allLights = FindObjectsOfType<Light>();
#endif

            var intensity = 0.0f;

            for (int i = 0; i < allLights.Length; i++)
            {
                if (allLights[i].type == LightType.Directional)
                {
                    if (allLights[i].intensity > intensity)
                    {
                        mainLight = allLights[i];
                    }
                }
            }
        }

#if UNITY_EDITOR
        //void UpdateDebugData()
        //{
        //    for (int i = 0; i < renderDataSet.Count; i++)
        //    {
        //        var renderData = renderDataSet[i];

        //        float memory = 0;
        //        float pixels = 0;

        //        if (renderData.renderMode != OOBoolMode.Off && renderData.bufferSize > -1)
        //        {
        //            memory = renderData.textureBaseRT.height * renderData.textureBaseRT.width * (renderData.bufferSize + 1) * 0.00762939453125f / 1000f;
        //            pixels = (renderData.textureBaseRT.width / renderData.internalScale.x + renderData.textureBaseRT.height / renderData.internalScale.z) / 2;
        //        }

        //        string debug = "<size=10>Memory: " + memory.ToString("F2") + " mb | Resolution: " + pixels.ToString("F2") + " pix/unit </size>";

        //        if (renderData.renderName == "TVE_Colors")
        //        {
        //            renderColors.debugData = debug;
        //        }

        //        if (renderData.renderName == "TVE_Extras")
        //        {
        //            renderExtras.debugData = debug;
        //        }

        //        if (renderData.renderName == "TVE_Motion")
        //        {
        //            renderMotion.debugData = debug;
        //        }

        //        if (renderData.renderName == "TVE_Vertex")
        //        {
        //            renderVertex.debugData = debug;
        //        }
        //    }
        //}

        void OnDrawGizmosSelected()
        {
            if (mainCamera != null)
            {
                Handles.color = new Color(0.0f, 0.0f, 0.0f, 0.2f);
                Handles.DrawWireDisc(mainCamera.transform.position, Vector3.up, renderBaseDistance);
                Handles.DrawWireDisc(mainCamera.transform.position, Vector3.up, renderNearDistance);
            }
        }

        void OnDrawGizmos()
        {
            if (mainCamera != null)
            {
                Handles.color = new Color(0.0f, 0.0f, 0.0f, 0.1f);
                Handles.DrawWireDisc(mainCamera.transform.position, Vector3.up, renderBaseDistance);
                Handles.DrawWireDisc(mainCamera.transform.position, Vector3.up, renderNearDistance);
            }
        }

        void OnValidate()
        {
            for (int d = 0; d < renderDataSet.Count; d++)
            {
                var renderData = renderDataSet[d];

                if (renderData == null)
                {
                    continue;
                }

                CreateRenderBuffer(renderData);
            }
        }
#endif
    }
}