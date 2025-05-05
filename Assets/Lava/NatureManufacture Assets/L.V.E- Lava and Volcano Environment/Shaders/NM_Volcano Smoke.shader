Shader "NatureManufacture/HDRP/Lit/Lava/Volcano Heated Smoke"
{
    Properties
    {
        Color_a3063e2cb473472aaed8dd09bb0a1785("Particle Color (RGB) Alpha(A)", Color) = (1, 1, 1, 1)
        [NoScaleOffset]_ParticleMask("Particle Mask (A)", 2D) = "white" {}
        _ParticleMaskTilingOffset("Particle Mask Tiling and Offset", Vector) = (1, 1, 0, 0)
        [Normal][NoScaleOffset]_ParticleNormal("Particle Normal", 2D) = "bump" {}
        Distortion_Power("Distortion Power", Range(0, 1)) = 0.01
        _Distortion_Blur("Distortion Blur", Float) = 0.2
        _NormalTilingOffset("Distortion Tiling and Offset", Vector) = (1, 1, 0, 0)
        _Alpha_Threshold("Alpha Threshold", Float) = 0.01
        [HideInInspector]_EmissionColor("Color", Color) = (1, 1, 1, 1)
        [HideInInspector]_RenderQueueType("Float", Float) = 4
        [HideInInspector][ToggleUI]_AddPrecomputedVelocity("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_DepthOffsetEnable("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_ConservativeDepthOffsetEnable("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_TransparentWritingMotionVec("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_AlphaCutoffEnable("Boolean", Float) = 1
        [HideInInspector]_TransparentSortPriority("_TransparentSortPriority", Float) = 0
        [HideInInspector][ToggleUI]_UseShadowThreshold("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_DoubleSidedEnable("Boolean", Float) = 0
        [HideInInspector][Enum(Flip, 0, Mirror, 1, None, 2)]_DoubleSidedNormalMode("Float", Float) = 2
        [HideInInspector]_DoubleSidedConstants("Vector4", Vector) = (1, 1, -1, 0)
        [HideInInspector][Enum(Auto, 0, On, 1, Off, 2)]_DoubleSidedGIMode("Float", Float) = 0
        [HideInInspector][ToggleUI]_TransparentDepthPrepassEnable("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_TransparentDepthPostpassEnable("Boolean", Float) = 0
        [HideInInspector]_SurfaceType("Float", Float) = 1
        [HideInInspector]_BlendMode("Float", Float) = 0
        [HideInInspector]_SrcBlend("Float", Float) = 1
        [HideInInspector]_DstBlend("Float", Float) = 0
        [HideInInspector]_AlphaSrcBlend("Float", Float) = 1
        [HideInInspector]_AlphaDstBlend("Float", Float) = 0
        [HideInInspector][ToggleUI]_ZWrite("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_TransparentZWrite("Boolean", Float) = 0
        [HideInInspector]_CullMode("Float", Float) = 2
        [HideInInspector][ToggleUI]_EnableFogOnTransparent("Boolean", Float) = 1
        [HideInInspector]_CullModeForward("Float", Float) = 2
        [HideInInspector][Enum(Front, 1, Back, 2)]_TransparentCullMode("Float", Float) = 2
        [HideInInspector][Enum(UnityEditor.Rendering.HighDefinition.OpaqueCullMode)]_OpaqueCullMode("Float", Float) = 2
        [HideInInspector]_ZTestDepthEqualForOpaque("Float", Int) = 4
        [HideInInspector][Enum(UnityEngine.Rendering.CompareFunction)]_ZTestTransparent("Float", Float) = 4
        [HideInInspector][ToggleUI]_TransparentBackfaceEnable("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_EnableBlendModePreserveSpecularLighting("Boolean", Float) = 0
        [HideInInspector]_StencilRef("Float", Int) = 0
        [HideInInspector]_StencilWriteMask("Float", Int) = 6
        [HideInInspector]_StencilRefDepth("Float", Int) = 1
        [HideInInspector]_StencilWriteMaskDepth("Float", Int) = 9
        [HideInInspector]_StencilRefMV("Float", Int) = 33
        [HideInInspector]_StencilWriteMaskMV("Float", Int) = 43
        [HideInInspector]_StencilRefDistortionVec("Float", Int) = 4
        [HideInInspector]_StencilWriteMaskDistortionVec("Float", Int) = 4
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="HDRenderPipeline"
            "RenderType"="HDUnlitShader"
            "Queue"="Transparent+0"
            "DisableBatching"="False"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="HDUnlitSubTarget"
        }
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }
        
            // Render State
            Cull [_CullMode]
        ZWrite On
        ColorMask 0
        ZClip [_ZClip]
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma instancing_options renderinglayer
        #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            struct CustomInterpolators
        {
        };
        #define USE_CUSTOMINTERP_SUBSTRUCT
        
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
            #define HAVE_MESH_MODIFICATION
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_SHADOWS
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct AttributesMesh
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 texCoord0;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float4 VertexColor;
        };
        struct PackedVaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float4 color : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        
            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            ZERO_INITIALIZE(PackedVaryingsMeshToPS, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.color.xyzw = input.color;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.color = input.color.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        
        VertexDescription GetVertexDescription(AttributesMesh input, float3 timeParameters
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time parameters with used one (This is required to correctly handle motion vectors for vertex animation based on time)
        
            // evaluate vertex graph
        #ifdef HAVE_VFX_MODIFICATION
            GraphProperties properties;
            ZERO_INITIALIZE(GraphProperties, properties);
        
            // Fetch the vertex graph properties for the particle instance.
            GetElementVertexProperties(element, properties);
        
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs, properties);
        #else
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
        #endif
            return vertexDescription;
        
        }
        
        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters
        #ifdef USE_CUSTOMINTERP_SUBSTRUCT
            #ifdef TESSELLATION_ON
            , inout VaryingsMeshToDS varyings
            #else
            , inout VaryingsMeshToPS varyings
            #endif
        #endif
        #ifdef HAVE_VFX_MODIFICATION
                , AttributesElement element
        #endif
            )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, timeParameters
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
        
            // copy graph output to the results
            input.positionOS = vertexDescription.Position;
            input.normalOS = vertexDescription.Normal;
            input.tangentOS.xyz = vertexDescription.Tangent;
        
            
        
            return input;
        }
        
        #if defined(_ADD_CUSTOM_VELOCITY) // For shader graph custom velocity
        // Return precomputed Velocity in object space
        float3 GetCustomVelocity(AttributesMesh input
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, _TimeParameters.xyz
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
            return vertexDescription.CustomVelocity;
        }
        #endif
        
        FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);
        
            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position
        
            output.texCoord0 =                  input.texCoord0;
            output.color =                      input.color;
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
        
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            // splice point to copy custom interpolator fields from varyings to frag inputs
            
        
            return output;
        }
        
        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
        #if defined(HAVE_VFX_MODIFICATION) && defined(UNITY_INSTANCING_ENABLED)
            unity_InstanceID = input.instanceID;
        #endif
            VaryingsMeshToPS unpacked = UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
        #else
        #endif
        
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassDepthOnly.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "META"
            Tags
            {
                "LightMode" = "META"
            }
        
            // Render State
            Cull Off
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma instancing_options renderinglayer
        #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        
            // Keywords
            #pragma shader_feature _ EDITOR_VISUALIZATION
        #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_POSITIONPREDISPLACEMENT_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD1
            #define VARYINGS_NEED_TEXCOORD2
            #define VARYINGS_NEED_TEXCOORD3
            #define VARYINGS_NEED_COLOR
        
            #define HAVE_MESH_MODIFICATION
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
            #define FRAG_INPUTS_USE_TEXCOORD1
            #define FRAG_INPUTS_USE_TEXCOORD2
            #define FRAG_INPUTS_USE_TEXCOORD3
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_LIGHT_TRANSPORT
        #define RAYTRACING_SHADER_GRAPH_DEFAULT
        #define SCENEPICKINGPASS 1
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/PickingSpaceTransforms.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct AttributesMesh
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
             float4 uv2 : TEXCOORD2;
             float4 uv3 : TEXCOORD3;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float3 positionRWS;
             float3 positionPredisplacementRWS;
             float4 texCoord0;
             float4 texCoord1;
             float4 texCoord2;
             float4 texCoord3;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct VertexDescriptionInputs
        {
        };
        struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        struct PackedVaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float4 texCoord1 : INTERP1;
             float4 texCoord2 : INTERP2;
             float4 texCoord3 : INTERP3;
             float4 color : INTERP4;
             float3 positionRWS : INTERP5;
             float3 positionPredisplacementRWS : INTERP6;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        
            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            ZERO_INITIALIZE(PackedVaryingsMeshToPS, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.texCoord1.xyzw = input.texCoord1;
            output.texCoord2.xyzw = input.texCoord2;
            output.texCoord3.xyzw = input.texCoord3;
            output.color.xyzw = input.color;
            output.positionRWS.xyz = input.positionRWS;
            output.positionPredisplacementRWS.xyz = input.positionPredisplacementRWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.texCoord1 = input.texCoord1.xyzw;
            output.texCoord2 = input.texCoord2.xyzw;
            output.texCoord3 = input.texCoord3.xyzw;
            output.color = input.color.xyzw;
            output.positionRWS = input.positionRWS.xyz;
            output.positionPredisplacementRWS = input.positionPredisplacementRWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            struct VertexDescription
        {
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            return description;
        }
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
        
            return output;
        }
        
        VertexDescription GetVertexDescription(AttributesMesh input, float3 timeParameters
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time parameters with used one (This is required to correctly handle motion vectors for vertex animation based on time)
        
            // evaluate vertex graph
        #ifdef HAVE_VFX_MODIFICATION
            GraphProperties properties;
            ZERO_INITIALIZE(GraphProperties, properties);
        
            // Fetch the vertex graph properties for the particle instance.
            GetElementVertexProperties(element, properties);
        
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs, properties);
        #else
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
        #endif
            return vertexDescription;
        
        }
        
        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters
        #ifdef USE_CUSTOMINTERP_SUBSTRUCT
            #ifdef TESSELLATION_ON
            , inout VaryingsMeshToDS varyings
            #else
            , inout VaryingsMeshToPS varyings
            #endif
        #endif
        #ifdef HAVE_VFX_MODIFICATION
                , AttributesElement element
        #endif
            )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, timeParameters
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
        
            // copy graph output to the results
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorVertMeshCustomInterpolation' */
        
            return input;
        }
        
        #if defined(_ADD_CUSTOM_VELOCITY) // For shader graph custom velocity
        // Return precomputed Velocity in object space
        float3 GetCustomVelocity(AttributesMesh input
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, _TimeParameters.xyz
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
            return vertexDescription.CustomVelocity;
        }
        #endif
        
        FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);
        
            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position
        
            output.positionRWS =                input.positionRWS;
            output.positionPixel =              input.positionCS.xy; // NOTE: this is not actually in clip space, it is the VPOS pixel coordinate value
            output.positionPredisplacementRWS = input.positionPredisplacementRWS;
            output.texCoord0 =                  input.texCoord0;
            output.texCoord1 =                  input.texCoord1;
            output.texCoord2 =                  input.texCoord2;
            output.texCoord3 =                  input.texCoord3;
            output.color =                      input.color;
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
        
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            // splice point to copy custom interpolator fields from varyings to frag inputs
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorVaryingsToFragInputs' */
        
            return output;
        }
        
        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
        #if defined(HAVE_VFX_MODIFICATION) && defined(UNITY_INSTANCING_ENABLED)
            unity_InstanceID = input.instanceID;
        #endif
            VaryingsMeshToPS unpacked = UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorCopyToSDI' */
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassLightTransport.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "ScenePickingPass"
            Tags
            {
                "LightMode" = "Picking"
            }
        
            // Render State
            Cull [_CullMode]
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma editor_sync_compilation
        #pragma instancing_options renderinglayer
        #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            struct CustomInterpolators
        {
        };
        #define USE_CUSTOMINTERP_SUBSTRUCT
        
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
            #define HAVE_MESH_MODIFICATION
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_DEPTH_ONLY
        #define SCENEPICKINGPASS 1
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/PickingSpaceTransforms.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct AttributesMesh
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        struct PackedVaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 tangentWS : INTERP0;
             float4 texCoord0 : INTERP1;
             float4 color : INTERP2;
             float3 normalWS : INTERP3;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        
            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            ZERO_INITIALIZE(PackedVaryingsMeshToPS, output);
            output.positionCS = input.positionCS;
            output.tangentWS.xyzw = input.tangentWS;
            output.texCoord0.xyzw = input.texCoord0;
            output.color.xyzw = input.color;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.tangentWS = input.tangentWS.xyzw;
            output.texCoord0 = input.texCoord0.xyzw;
            output.color = input.color.xyzw;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        
        VertexDescription GetVertexDescription(AttributesMesh input, float3 timeParameters
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time parameters with used one (This is required to correctly handle motion vectors for vertex animation based on time)
        
            // evaluate vertex graph
        #ifdef HAVE_VFX_MODIFICATION
            GraphProperties properties;
            ZERO_INITIALIZE(GraphProperties, properties);
        
            // Fetch the vertex graph properties for the particle instance.
            GetElementVertexProperties(element, properties);
        
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs, properties);
        #else
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
        #endif
            return vertexDescription;
        
        }
        
        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters
        #ifdef USE_CUSTOMINTERP_SUBSTRUCT
            #ifdef TESSELLATION_ON
            , inout VaryingsMeshToDS varyings
            #else
            , inout VaryingsMeshToPS varyings
            #endif
        #endif
        #ifdef HAVE_VFX_MODIFICATION
                , AttributesElement element
        #endif
            )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, timeParameters
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
        
            // copy graph output to the results
            input.positionOS = vertexDescription.Position;
            input.normalOS = vertexDescription.Normal;
            input.tangentOS.xyz = vertexDescription.Tangent;
        
            
        
            return input;
        }
        
        #if defined(_ADD_CUSTOM_VELOCITY) // For shader graph custom velocity
        // Return precomputed Velocity in object space
        float3 GetCustomVelocity(AttributesMesh input
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, _TimeParameters.xyz
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
            return vertexDescription.CustomVelocity;
        }
        #endif
        
        FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);
        
            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position
        
            output.positionPixel =              input.positionCS.xy; // NOTE: this is not actually in clip space, it is the VPOS pixel coordinate value
            output.tangentToWorld =             BuildTangentToWorld(input.tangentWS, input.normalWS);
            output.texCoord0 =                  input.texCoord0;
            output.color =                      input.color;
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
        
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            // splice point to copy custom interpolator fields from varyings to frag inputs
            
        
            return output;
        }
        
        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
        #if defined(HAVE_VFX_MODIFICATION) && defined(UNITY_INSTANCING_ENABLED)
            unity_InstanceID = input.instanceID;
        #endif
            VaryingsMeshToPS unpacked = UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassDepthOnly.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "SceneSelectionPass"
            Tags
            {
                "LightMode" = "SceneSelectionPass"
            }
        
            // Render State
            Cull Off
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma editor_sync_compilation
        #pragma instancing_options renderinglayer
        #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            struct CustomInterpolators
        {
        };
        #define USE_CUSTOMINTERP_SUBSTRUCT
        
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
            #define HAVE_MESH_MODIFICATION
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_DEPTH_ONLY
        #define RAYTRACING_SHADER_GRAPH_DEFAULT
        #define SCENESELECTIONPASS 1
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/PickingSpaceTransforms.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct AttributesMesh
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 texCoord0;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        struct PackedVaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float4 color : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        
            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            ZERO_INITIALIZE(PackedVaryingsMeshToPS, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.color.xyzw = input.color;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.color = input.color.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        
        VertexDescription GetVertexDescription(AttributesMesh input, float3 timeParameters
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time parameters with used one (This is required to correctly handle motion vectors for vertex animation based on time)
        
            // evaluate vertex graph
        #ifdef HAVE_VFX_MODIFICATION
            GraphProperties properties;
            ZERO_INITIALIZE(GraphProperties, properties);
        
            // Fetch the vertex graph properties for the particle instance.
            GetElementVertexProperties(element, properties);
        
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs, properties);
        #else
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
        #endif
            return vertexDescription;
        
        }
        
        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters
        #ifdef USE_CUSTOMINTERP_SUBSTRUCT
            #ifdef TESSELLATION_ON
            , inout VaryingsMeshToDS varyings
            #else
            , inout VaryingsMeshToPS varyings
            #endif
        #endif
        #ifdef HAVE_VFX_MODIFICATION
                , AttributesElement element
        #endif
            )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, timeParameters
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
        
            // copy graph output to the results
            input.positionOS = vertexDescription.Position;
            input.normalOS = vertexDescription.Normal;
            input.tangentOS.xyz = vertexDescription.Tangent;
        
            
        
            return input;
        }
        
        #if defined(_ADD_CUSTOM_VELOCITY) // For shader graph custom velocity
        // Return precomputed Velocity in object space
        float3 GetCustomVelocity(AttributesMesh input
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, _TimeParameters.xyz
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
            return vertexDescription.CustomVelocity;
        }
        #endif
        
        FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);
        
            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position
        
            output.positionPixel =              input.positionCS.xy; // NOTE: this is not actually in clip space, it is the VPOS pixel coordinate value
            output.texCoord0 =                  input.texCoord0;
            output.color =                      input.color;
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
        
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            // splice point to copy custom interpolator fields from varyings to frag inputs
            
        
            return output;
        }
        
        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
        #if defined(HAVE_VFX_MODIFICATION) && defined(UNITY_INSTANCING_ENABLED)
            unity_InstanceID = input.instanceID;
        #endif
            VaryingsMeshToPS unpacked = UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassDepthOnly.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "MotionVectors"
            Tags
            {
                "LightMode" = "MotionVectors"
            }
        
            // Render State
            Cull [_CullMode]
        ZWrite On
        Stencil
        {
        WriteMask [_StencilWriteMaskMV]
        Ref [_StencilRefMV]
        CompFront Always
        PassFront Replace
        CompBack Always
        PassBack Replace
        }
        AlphaToMask [_AlphaCutoffEnable]
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma instancing_options renderinglayer
        #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma multi_compile_fragment _ WRITE_MSAA_DEPTH
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            struct CustomInterpolators
        {
        };
        #define USE_CUSTOMINTERP_SUBSTRUCT
        
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
            #define HAVE_MESH_MODIFICATION
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_MOTION_VECTORS
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct AttributesMesh
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float3 positionRWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        struct PackedVaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 tangentWS : INTERP0;
             float4 texCoord0 : INTERP1;
             float4 color : INTERP2;
             float3 positionRWS : INTERP3;
             float3 normalWS : INTERP4;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        
            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            ZERO_INITIALIZE(PackedVaryingsMeshToPS, output);
            output.positionCS = input.positionCS;
            output.tangentWS.xyzw = input.tangentWS;
            output.texCoord0.xyzw = input.texCoord0;
            output.color.xyzw = input.color;
            output.positionRWS.xyz = input.positionRWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.tangentWS = input.tangentWS.xyzw;
            output.texCoord0 = input.texCoord0.xyzw;
            output.color = input.color.xyzw;
            output.positionRWS = input.positionRWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        
        VertexDescription GetVertexDescription(AttributesMesh input, float3 timeParameters
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time parameters with used one (This is required to correctly handle motion vectors for vertex animation based on time)
        
            // evaluate vertex graph
        #ifdef HAVE_VFX_MODIFICATION
            GraphProperties properties;
            ZERO_INITIALIZE(GraphProperties, properties);
        
            // Fetch the vertex graph properties for the particle instance.
            GetElementVertexProperties(element, properties);
        
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs, properties);
        #else
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
        #endif
            return vertexDescription;
        
        }
        
        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters
        #ifdef USE_CUSTOMINTERP_SUBSTRUCT
            #ifdef TESSELLATION_ON
            , inout VaryingsMeshToDS varyings
            #else
            , inout VaryingsMeshToPS varyings
            #endif
        #endif
        #ifdef HAVE_VFX_MODIFICATION
                , AttributesElement element
        #endif
            )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, timeParameters
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
        
            // copy graph output to the results
            input.positionOS = vertexDescription.Position;
            input.normalOS = vertexDescription.Normal;
            input.tangentOS.xyz = vertexDescription.Tangent;
        
            
        
            return input;
        }
        
        #if defined(_ADD_CUSTOM_VELOCITY) // For shader graph custom velocity
        // Return precomputed Velocity in object space
        float3 GetCustomVelocity(AttributesMesh input
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, _TimeParameters.xyz
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
            return vertexDescription.CustomVelocity;
        }
        #endif
        
        FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);
        
            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position
        
            output.positionRWS =                input.positionRWS;
            output.positionPixel =              input.positionCS.xy; // NOTE: this is not actually in clip space, it is the VPOS pixel coordinate value
            output.tangentToWorld =             BuildTangentToWorld(input.tangentWS, input.normalWS);
            output.texCoord0 =                  input.texCoord0;
            output.color =                      input.color;
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
        
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            // splice point to copy custom interpolator fields from varyings to frag inputs
            
        
            return output;
        }
        
        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
        #if defined(HAVE_VFX_MODIFICATION) && defined(UNITY_INSTANCING_ENABLED)
            unity_InstanceID = input.instanceID;
        #endif
            VaryingsMeshToPS unpacked = UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassMotionVectors.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "DepthForwardOnly"
            Tags
            {
                "LightMode" = "DepthForwardOnly"
            }
        
            // Render State
            Cull [_CullMode]
        ZWrite On
        Stencil
        {
        WriteMask [_StencilWriteMaskDepth]
        Ref [_StencilRefDepth]
        CompFront Always
        PassFront Replace
        CompBack Always
        PassBack Replace
        }
        AlphaToMask [_AlphaCutoffEnable]
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma instancing_options renderinglayer
        #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma multi_compile_fragment _ WRITE_MSAA_DEPTH
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            struct CustomInterpolators
        {
        };
        #define USE_CUSTOMINTERP_SUBSTRUCT
        
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
            #define HAVE_MESH_MODIFICATION
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_DEPTH_ONLY
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct AttributesMesh
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        struct PackedVaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 tangentWS : INTERP0;
             float4 texCoord0 : INTERP1;
             float4 color : INTERP2;
             float3 normalWS : INTERP3;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        
            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            ZERO_INITIALIZE(PackedVaryingsMeshToPS, output);
            output.positionCS = input.positionCS;
            output.tangentWS.xyzw = input.tangentWS;
            output.texCoord0.xyzw = input.texCoord0;
            output.color.xyzw = input.color;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.tangentWS = input.tangentWS.xyzw;
            output.texCoord0 = input.texCoord0.xyzw;
            output.color = input.color.xyzw;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        
        VertexDescription GetVertexDescription(AttributesMesh input, float3 timeParameters
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time parameters with used one (This is required to correctly handle motion vectors for vertex animation based on time)
        
            // evaluate vertex graph
        #ifdef HAVE_VFX_MODIFICATION
            GraphProperties properties;
            ZERO_INITIALIZE(GraphProperties, properties);
        
            // Fetch the vertex graph properties for the particle instance.
            GetElementVertexProperties(element, properties);
        
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs, properties);
        #else
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
        #endif
            return vertexDescription;
        
        }
        
        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters
        #ifdef USE_CUSTOMINTERP_SUBSTRUCT
            #ifdef TESSELLATION_ON
            , inout VaryingsMeshToDS varyings
            #else
            , inout VaryingsMeshToPS varyings
            #endif
        #endif
        #ifdef HAVE_VFX_MODIFICATION
                , AttributesElement element
        #endif
            )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, timeParameters
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
        
            // copy graph output to the results
            input.positionOS = vertexDescription.Position;
            input.normalOS = vertexDescription.Normal;
            input.tangentOS.xyz = vertexDescription.Tangent;
        
            
        
            return input;
        }
        
        #if defined(_ADD_CUSTOM_VELOCITY) // For shader graph custom velocity
        // Return precomputed Velocity in object space
        float3 GetCustomVelocity(AttributesMesh input
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, _TimeParameters.xyz
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
            return vertexDescription.CustomVelocity;
        }
        #endif
        
        FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);
        
            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position
        
            output.positionPixel =              input.positionCS.xy; // NOTE: this is not actually in clip space, it is the VPOS pixel coordinate value
            output.tangentToWorld =             BuildTangentToWorld(input.tangentWS, input.normalWS);
            output.texCoord0 =                  input.texCoord0;
            output.color =                      input.color;
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
        
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            // splice point to copy custom interpolator fields from varyings to frag inputs
            
        
            return output;
        }
        
        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
        #if defined(HAVE_VFX_MODIFICATION) && defined(UNITY_INSTANCING_ENABLED)
            unity_InstanceID = input.instanceID;
        #endif
            VaryingsMeshToPS unpacked = UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassDepthOnly.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "ForwardOnly"
            Tags
            {
                "LightMode" = "ForwardOnly"
            }
        
            // Render State
            Cull [_CullModeForward]
        Blend [_SrcBlend] [_DstBlend], [_AlphaSrcBlend] [_AlphaDstBlend]
        Blend 1 SrcAlpha OneMinusSrcAlpha
        ZTest [_ZTestDepthEqualForOpaque]
        ZWrite [_ZWrite]
        ColorMask [_ColorMaskTransparentVelOne] 1
        ColorMask [_ColorMaskTransparentVelTwo] 2
        Stencil
        {
        WriteMask [_StencilWriteMask]
        Ref [_StencilRef]
        CompFront Always
        PassFront Replace
        CompBack Always
        PassBack Replace
        }
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma instancing_options renderinglayer
        #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
        #pragma multi_compile _ DEBUG_DISPLAY
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            struct CustomInterpolators
        {
        };
        #define USE_CUSTOMINTERP_SUBSTRUCT
        
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
            #define HAVE_MESH_MODIFICATION
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_FORWARD_UNLIT
        #define RAYTRACING_SHADER_GRAPH_DEFAULT
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct AttributesMesh
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float3 positionRWS;
             float4 texCoord0;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        struct PackedVaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float4 color : INTERP1;
             float3 positionRWS : INTERP2;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        
            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            ZERO_INITIALIZE(PackedVaryingsMeshToPS, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.color.xyzw = input.color;
            output.positionRWS.xyz = input.positionRWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.color = input.color.xyzw;
            output.positionRWS = input.positionRWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
            float4 VTPackedFeedback;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            {
                surface.VTPackedFeedback = float4(1.0f,1.0f,1.0f,1.0f);
            }
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        
        VertexDescription GetVertexDescription(AttributesMesh input, float3 timeParameters
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time parameters with used one (This is required to correctly handle motion vectors for vertex animation based on time)
        
            // evaluate vertex graph
        #ifdef HAVE_VFX_MODIFICATION
            GraphProperties properties;
            ZERO_INITIALIZE(GraphProperties, properties);
        
            // Fetch the vertex graph properties for the particle instance.
            GetElementVertexProperties(element, properties);
        
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs, properties);
        #else
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
        #endif
            return vertexDescription;
        
        }
        
        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters
        #ifdef USE_CUSTOMINTERP_SUBSTRUCT
            #ifdef TESSELLATION_ON
            , inout VaryingsMeshToDS varyings
            #else
            , inout VaryingsMeshToPS varyings
            #endif
        #endif
        #ifdef HAVE_VFX_MODIFICATION
                , AttributesElement element
        #endif
            )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, timeParameters
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
        
            // copy graph output to the results
            input.positionOS = vertexDescription.Position;
            input.normalOS = vertexDescription.Normal;
            input.tangentOS.xyz = vertexDescription.Tangent;
        
            
        
            return input;
        }
        
        #if defined(_ADD_CUSTOM_VELOCITY) // For shader graph custom velocity
        // Return precomputed Velocity in object space
        float3 GetCustomVelocity(AttributesMesh input
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, _TimeParameters.xyz
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
            return vertexDescription.CustomVelocity;
        }
        #endif
        
        FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);
        
            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position
        
            output.positionRWS =                input.positionRWS;
            output.positionPixel =              input.positionCS.xy; // NOTE: this is not actually in clip space, it is the VPOS pixel coordinate value
            output.texCoord0 =                  input.texCoord0;
            output.color =                      input.color;
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
        
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            // splice point to copy custom interpolator fields from varyings to frag inputs
            
        
            return output;
        }
        
        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
        #if defined(HAVE_VFX_MODIFICATION) && defined(UNITY_INSTANCING_ENABLED)
            unity_InstanceID = input.instanceID;
        #endif
            VaryingsMeshToPS unpacked = UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                builtinData.vtPackedFeedback = surfaceDescription.VTPackedFeedback;
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassForwardUnlit.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "FullScreenDebug"
            Tags
            {
                "LightMode" = "FullScreenDebug"
            }
        
            // Render State
            Cull [_CullMode]
        ZTest LEqual
        ZWrite Off
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma instancing_options renderinglayer
        #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            struct CustomInterpolators
        {
        };
        #define USE_CUSTOMINTERP_SUBSTRUCT
        
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
            #define HAVE_MESH_MODIFICATION
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_FULL_SCREEN_DEBUG
        #define RAYTRACING_SHADER_GRAPH_DEFAULT
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct AttributesMesh
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 texCoord0;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        struct PackedVaryingsMeshToPS
        {
            SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float4 color : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        
            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            ZERO_INITIALIZE(PackedVaryingsMeshToPS, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.color.xyzw = input.color;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.color = input.color.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        
        VertexDescription GetVertexDescription(AttributesMesh input, float3 timeParameters
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time parameters with used one (This is required to correctly handle motion vectors for vertex animation based on time)
        
            // evaluate vertex graph
        #ifdef HAVE_VFX_MODIFICATION
            GraphProperties properties;
            ZERO_INITIALIZE(GraphProperties, properties);
        
            // Fetch the vertex graph properties for the particle instance.
            GetElementVertexProperties(element, properties);
        
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs, properties);
        #else
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);
        #endif
            return vertexDescription;
        
        }
        
        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters
        #ifdef USE_CUSTOMINTERP_SUBSTRUCT
            #ifdef TESSELLATION_ON
            , inout VaryingsMeshToDS varyings
            #else
            , inout VaryingsMeshToPS varyings
            #endif
        #endif
        #ifdef HAVE_VFX_MODIFICATION
                , AttributesElement element
        #endif
            )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, timeParameters
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
        
            // copy graph output to the results
            input.positionOS = vertexDescription.Position;
            input.normalOS = vertexDescription.Normal;
            input.tangentOS.xyz = vertexDescription.Tangent;
        
            
        
            return input;
        }
        
        #if defined(_ADD_CUSTOM_VELOCITY) // For shader graph custom velocity
        // Return precomputed Velocity in object space
        float3 GetCustomVelocity(AttributesMesh input
        #ifdef HAVE_VFX_MODIFICATION
            , AttributesElement element
        #endif
        )
        {
            VertexDescription vertexDescription = GetVertexDescription(input, _TimeParameters.xyz
        #ifdef HAVE_VFX_MODIFICATION
                , element
        #endif
            );
            return vertexDescription.CustomVelocity;
        }
        #endif
        
        FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);
        
            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position
        
            output.positionPixel =              input.positionCS.xy; // NOTE: this is not actually in clip space, it is the VPOS pixel coordinate value
            output.texCoord0 =                  input.texCoord0;
            output.color =                      input.color;
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
        
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            // splice point to copy custom interpolator fields from varyings to frag inputs
            
        
            return output;
        }
        
        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
        #if defined(HAVE_VFX_MODIFICATION) && defined(UNITY_INSTANCING_ENABLED)
            unity_InstanceID = input.instanceID;
        #endif
            VaryingsMeshToPS unpacked = UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassFullScreenDebug.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="HDRenderPipeline"
            "RenderType"="HDUnlitShader"
            "Queue"="Transparent+0"
            "DisableBatching"="False"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="HDUnlitSubTarget"
        }
        Pass
        {
            Name "IndirectDXR"
            Tags
            {
                "LightMode" = "IndirectDXR"
            }
        
            // Render State
            // RenderState: <None>
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 5.0
        #pragma raytracing surface_shader
        #pragma only_renderers d3d11 xboxseries ps5
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
        #pragma multi_compile _ DEBUG_DISPLAY
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_RAYTRACING_INDIRECT
        #define SHADOW_LOW
        #define RAYTRACING_SHADER_GRAPH_RAYTRACED
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracingLightLoop.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingIntersection.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/UnlitRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RayTracingCommon.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        
            //Interpolator Packs: <None>
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            // GraphVertex: <None>
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorCopyToSDI' */
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassRaytracingIndirect.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "VisibilityDXR"
            Tags
            {
                "LightMode" = "VisibilityDXR"
            }
        
            // Render State
            // RenderState: <None>
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 5.0
        #pragma raytracing surface_shader
        #pragma only_renderers d3d11 xboxseries ps5
        
            // Keywords
            #pragma multi_compile _ TRANSPARENT_COLOR_SHADOW
        #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_RAYTRACING_VISIBILITY
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracingLightLoop.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingIntersection.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/UnlitRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RayTracingCommon.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        
            //Interpolator Packs: <None>
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            // GraphVertex: <None>
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorCopyToSDI' */
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassRaytracingVisibility.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "ForwardDXR"
            Tags
            {
                "LightMode" = "ForwardDXR"
            }
        
            // Render State
            // RenderState: <None>
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 5.0
        #pragma raytracing surface_shader
        #pragma only_renderers d3d11 xboxseries ps5
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
        #pragma multi_compile _ DEBUG_DISPLAY
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_RAYTRACING_FORWARD
        #define SHADOW_LOW
        #define RAYTRACING_SHADER_GRAPH_RAYTRACED
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracingLightLoop.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingIntersection.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/UnlitRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RayTracingCommon.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        
            //Interpolator Packs: <None>
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            // GraphVertex: <None>
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorCopyToSDI' */
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassRaytracingForward.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "GBufferDXR"
            Tags
            {
                "LightMode" = "GBufferDXR"
            }
        
            // Render State
            // RenderState: <None>
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 5.0
        #pragma raytracing surface_shader
        #pragma only_renderers d3d11 xboxseries ps5
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
        #pragma multi_compile _ DEBUG_DISPLAY
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_RAYTRACING_GBUFFER
        #define SHADOW_LOW
        #define RAYTRACING_SHADER_GRAPH_RAYTRACED
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracingLightLoop.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/Deferred/RaytracingIntersectonGBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/NormalBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/StandardLit/StandardLit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/UnlitRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RayTracingCommon.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        
            //Interpolator Packs: <None>
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            // GraphVertex: <None>
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorCopyToSDI' */
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassRaytracingGBuffer.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
        Pass
        {
            Name "DebugDXR"
            Tags
            {
                "LightMode" = "DebugDXR"
            }
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 5.0
        #pragma raytracing surface_shader
        #pragma only_renderers d3d11 xboxseries ps5
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingIntersection.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RayTracingCommon.hlsl"
        	// GraphIncludes: <None>
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassRayTracingDebug.hlsl"
        
            ENDHLSL
        }
        Pass
        {
            Name "PathTracingDXR"
            Tags
            {
                "LightMode" = "PathTracingDXR"
            }
        
            // Render State
            // RenderState: <None>
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 5.0
        #pragma raytracing surface_shader
        #pragma only_renderers d3d11 xboxseries ps5
        
            // Keywords
            #pragma shader_feature_local _ _ALPHATEST_ON
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local_fragment _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>
        
            // For custom interpolators to inject a substruct definition before FragInputs definition,
            // allowing for FragInputs to capture CI's intended for ShaderGraph's SDI.
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
        
            // TODO: Merge FragInputsVFX substruct with CustomInterpolators.
        	#ifdef HAVE_VFX_MODIFICATION
        	struct FragInputsVFX
            {
                /* WARNING: $splice Could not find named fragment 'FragInputsVFX' */
            };
            #endif
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl" // Required by Tessellation.hlsl
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Tessellation.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            // Always include Shader Graph version
            // Always include last to avoid double macros
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl" // Need to be here for Gradient struct definition
        
            // --------------------------------------------------
            // Defines
        
            // Attribute
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
        
        
            //Strip down the FragInputs.hlsl (on graphics), so we can only optimize the interpolators we use.
            //if by accident something requests contents of FragInputs.hlsl, it will be caught as a compiler error
            //Frag inputs stripping is only enabled when FRAG_INPUTS_ENABLE_STRIPPING is set
            #if !defined(SHADER_STAGE_RAY_TRACING) && SHADERPASS != SHADERPASS_RAYTRACING_GBUFFER && SHADERPASS != SHADERPASS_FULL_SCREEN_DEBUG
            #define FRAG_INPUTS_ENABLE_STRIPPING
            #endif
            #define FRAG_INPUTS_USE_TEXCOORD0
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
        
        
        
            #define SHADERPASS SHADERPASS_PATH_TRACING
        #define REQUIRE_OPAQUE_TEXTURE
        
        
            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif
        
            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end
        
            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif
        
            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT
        
        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && (SHADERPASS == SHADERPASS_FORWARD_UNLIT || SHADERPASS == SHADERPASS_PATH_TRACING)
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it
        
            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif
        
            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif
        
            // Define _DEFERRED_CAPABLE_MATERIAL for shader capable to run in deferred pass
            #if defined(SHADER_LIT) && !defined(_SURFACE_TYPE_TRANSPARENT)
                #define _DEFERRED_CAPABLE_MATERIAL
            #endif
        
            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif
        
            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 Color_a3063e2cb473472aaed8dd09bb0a1785;
        float4 _ParticleMask_TexelSize;
        float4 _ParticleMaskTilingOffset;
        float4 _ParticleNormal_TexelSize;
        float Distortion_Power;
        float4 _NormalTilingOffset;
        float _Distortion_Blur;
        float _Alpha_Threshold;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ParticleMask);
        SAMPLER(sampler_ParticleMask);
        TEXTURE2D(_ParticleNormal);
        SAMPLER(sampler_ParticleNormal);
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracing.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/ShaderVariablesRaytracingLightLoop.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RaytracingIntersection.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/Raytracing/Shaders/RayTracingCommon.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        	// GraphIncludes: <None>
        
            // --------------------------------------------------
            // Structs and Packing
        
            struct SurfaceDescriptionInputs
        {
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 VertexColor;
        };
        
            //Interpolator Packs: <None>
        
            // --------------------------------------------------
            // Graph
        
        
            // Graph Functions
            
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_NormalStrength_float(float3 In, float Strength, out float3 Out)
        {
            Out = float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        float3 Unity_HDRP_SampleSceneColor_float(float2 uv, float lod, float exposureMultiplier)
        {
            exposureMultiplier = 1.0;
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT) && (SHADERPASS != SHADERPASS_PATH_TRACING) && (SHADERPASS != SHADERPASS_RAYTRACING_VISIBILITY) && (SHADERPASS != SHADERPASS_RAYTRACING_FORWARD)
            return SampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            #if defined(REQUIRE_OPAQUE_TEXTURE) && defined(CUSTOM_PASS_SAMPLING_HLSL) && defined(SHADERPASS) && (SHADERPASS == SHADERPASS_DRAWPROCEDURAL || SHADERPASS == SHADERPASS_BLIT)
            return CustomPassSampleCameraColor(uv, lod) * exposureMultiplier;
            #endif
            return float3(0.0, 0.0, 0.0);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
            // Graph Vertex
            // GraphVertex: <None>
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4 = Color_a3063e2cb473472aaed8dd09bb0a1785;
            float _Split_8b14302553ddc3879748aad6158293a9_R_1_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[0];
            float _Split_8b14302553ddc3879748aad6158293a9_G_2_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[1];
            float _Split_8b14302553ddc3879748aad6158293a9_B_3_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[2];
            float _Split_8b14302553ddc3879748aad6158293a9_A_4_Float = _Property_ac5736a7a8714b54908cfe4a0d156286_Out_0_Vector4[3];
            float4 _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4;
            float3 _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3;
            float2 _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2;
            Unity_Combine_float(_Split_8b14302553ddc3879748aad6158293a9_R_1_Float, _Split_8b14302553ddc3879748aad6158293a9_G_2_Float, _Split_8b14302553ddc3879748aad6158293a9_B_3_Float, 0, _Combine_8492f7d7ab0f418381806b721bed62d6_RGBA_4_Vector4, _Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _Combine_8492f7d7ab0f418381806b721bed62d6_RG_6_Vector2);
            float4 _ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            UnityTexture2D _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleNormal);
            float4 _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4 = _NormalTilingOffset;
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[0];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[1];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[2];
            float _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float = _Property_269571a8a5154482b825042058b5c3b3_Out_0_Vector4[3];
            float4 _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4;
            float3 _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3;
            float2 _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_B_3_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_A_4_Float, 0, 0, _Combine_845cb1e758f7a887bcd17694c573d134_RGBA_4_Vector4, _Combine_845cb1e758f7a887bcd17694c573d134_RGB_5_Vector3, _Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2);
            float4 _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4;
            float3 _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3;
            float2 _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2;
            Unity_Combine_float(_Split_8cbf1573be6f5782bb185dfcee24d55b_R_1_Float, _Split_8cbf1573be6f5782bb185dfcee24d55b_G_2_Float, 0, 0, _Combine_ccad6919c709958e92daa536f3084a22_RGBA_4_Vector4, _Combine_ccad6919c709958e92daa536f3084a22_RGB_5_Vector3, _Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2);
            float4 _UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_ccad6919c709958e92daa536f3084a22_RG_6_Vector2, (_UV_1ef8497bd7d05986b5a077de31e42520_Out_0_Vector4.xy), _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2);
            float2 _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2;
            Unity_Add_float2(_Combine_845cb1e758f7a887bcd17694c573d134_RG_6_Vector2, _Multiply_7b7c92dba37a958a955dde2b060c4e7d_Out_2_Vector2, _Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2);
            float4 _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.tex, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.samplerstate, _Property_69b7f815b05e9080a80aa1406ac34a33_Out_0_Texture2D.GetTransformedUV(_Add_7ad38bda511b1d848b8cdff1293db07a_Out_2_Vector2) );
            _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4);
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_R_4_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.r;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_G_5_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.g;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_B_6_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.b;
            float _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_A_7_Float = _SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.a;
            float _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float = Distortion_Power;
            float3 _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3;
            Unity_NormalStrength_float((_SampleTexture2D_981cf8a05e29f0809d6fb0ec9d5188f4_RGBA_0_Vector4.xyz), _Property_e9478b395fb69180a8f07c76b1fc22fe_Out_0_Float, _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3);
            float3 _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3;
            Unity_Add_float3((_ScreenPosition_937829c9b1a6d685baa02d92309ac38b_Out_0_Vector4.xyz), _NormalStrength_b32f75d09f123287be599808ec1d904e_Out_2_Vector3, _Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3);
            float _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float = _Distortion_Blur;
            float3 _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3 = Unity_HDRP_SampleSceneColor_float((float4(_Add_a4a1d3e0c22bfe89afcd8394f4b37438_Out_2_Vector3, 1.0)).xy, _Property_1adf7d93622546cabba833096a7a2e31_Out_0_Float, 1.0);
            float3 _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            Unity_Multiply_float3_float3(_Combine_8492f7d7ab0f418381806b721bed62d6_RGB_5_Vector3, _HDSceneColor_eb3cb5fab80b258ca675e45409b62833_Output_2_Vector3, _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3);
            UnityTexture2D _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D = UnityBuildTexture2DStructNoScale(_ParticleMask);
            float4 _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4 = _ParticleMaskTilingOffset;
            float _Split_09316789d5be448aba41ce9a8a79e989_R_1_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[0];
            float _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[1];
            float _Split_09316789d5be448aba41ce9a8a79e989_B_3_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[2];
            float _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float = _Property_d1ddfebd1c71f38784ca2d8fae4912f9_Out_0_Vector4[3];
            float4 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4;
            float3 _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3;
            float2 _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_B_3_Float, _Split_09316789d5be448aba41ce9a8a79e989_A_4_Float, 0, 0, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGBA_4_Vector4, _Combine_b73d37b980d957808efc77b8e5c6eeec_RGB_5_Vector3, _Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2);
            float4 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4;
            float3 _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3;
            float2 _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2;
            Unity_Combine_float(_Split_09316789d5be448aba41ce9a8a79e989_R_1_Float, _Split_09316789d5be448aba41ce9a8a79e989_G_2_Float, 0, 0, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGBA_4_Vector4, _Combine_e5516dc3c548c486a4ac584bc51b3893_RGB_5_Vector3, _Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2);
            float4 _UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4 = IN.uv0;
            float2 _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2;
            Unity_Multiply_float2_float2(_Combine_e5516dc3c548c486a4ac584bc51b3893_RG_6_Vector2, (_UV_c1acb6bd7e0ad482a5d924e1e6aa52e5_Out_0_Vector4.xy), _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2);
            float2 _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2;
            Unity_Add_float2(_Combine_b73d37b980d957808efc77b8e5c6eeec_RG_6_Vector2, _Multiply_6835e7e10ef4cb848e2e1ca777876cb5_Out_2_Vector2, _Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2);
            float4 _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.tex, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.samplerstate, _Property_e96052cb5280048fb7045df6517dadfe_Out_0_Texture2D.GetTransformedUV(_Add_9ac539502ae37687b2e506d5849f543a_Out_2_Vector2) );
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_R_4_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.r;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_G_5_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.g;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_B_6_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.b;
            float _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float = _SampleTexture2D_bc686c6640e38c83928df8f381a07990_RGBA_0_Vector4.a;
            float _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float;
            Unity_Multiply_float_float(_Split_8b14302553ddc3879748aad6158293a9_A_4_Float, _SampleTexture2D_bc686c6640e38c83928df8f381a07990_A_7_Float, _Multiply_da551cc95d964f8086754940028076ab_Out_2_Float);
            float4 _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4;
            Unity_Clamp_float4(IN.VertexColor, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4);
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_R_1_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[0];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_G_2_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[1];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_B_3_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[2];
            float _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float = _Clamp_693ebb0e102d42ebad9c7953fb331620_Out_3_Vector4[3];
            float _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_da551cc95d964f8086754940028076ab_Out_2_Float, _Split_d51cf25ee33c4aabb624110dc48b44aa_A_4_Float, _Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float);
            float _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            Unity_Clamp_float(_Multiply_caf3d325ad6b4d11b4f7f106f61ed9d2_Out_2_Float, 0, 1, _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float);
            float _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float = _Alpha_Threshold;
            surface.BaseColor = _Multiply_d025dda6148a4d589c5688cb9cfb1ac3_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = _Clamp_78dcb113a6184987be354edbe2ff600c_Out_3_Float;
            surface.AlphaClipThreshold = _Property_1c0f9f3a35b54d8b9a2176ece6ce9d95_Out_0_Float;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
            #ifdef HAVE_VFX_MODIFICATION
            #define VFX_SRP_ATTRIBUTES AttributesMesh
            #define VaryingsMeshType VaryingsMeshToPS
            #define VFX_SRP_VARYINGS VaryingsMeshType
            #define VFX_SRP_SURFACE_INPUTS FragInputs
            #endif
            SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
        
        #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x < 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #else
            output.PixelPosition = float2(input.positionPixel.x, (_ProjectionParams.x > 0) ? (_ScreenParams.y - input.positionPixel.y) : input.positionPixel.y);
        #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 =                                        input.texCoord0;
            output.VertexColor =                                input.color;
        
            // splice point to copy frag inputs custom interpolator pack into the SDI
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorCopyToSDI' */
        
            return output;
        }
        
            // --------------------------------------------------
            // Build Surface Data (Specific Material)
        
        void ApplyDecalToSurfaceDataNoNormal(DecalSurfaceData decalSurfaceData, inout SurfaceData surfaceData);
        
        void ApplyDecalAndGetNormal(FragInputs fragInputs, PositionInputs posInput, SurfaceDescription surfaceDescription,
            inout SurfaceData surfaceData)
        {
            float3 doubleSidedConstants = GetDoubleSidedConstants();
        
        #ifdef DECAL_NORMAL_BLENDING
            // SG nodes don't ouptut surface gradients, so if decals require surf grad blending, we have to convert
            // the normal to gradient before applying the decal. We then have to resolve the gradient back to world space
            float3 normalTS;
        
        
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, fragInputs.tangentToWorld[2], normalTS);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        
            GetNormalWS_SG(fragInputs, normalTS, surfaceData.normalWS, doubleSidedConstants);
        #else
            // normal delivered to master node
        
            #if HAVE_DECALS
            if (_EnableDecals)
            {
                float alpha = 1.0;
                alpha = surfaceDescription.Alpha;
        
                // Both uses and modifies 'surfaceData.normalWS'.
                DecalSurfaceData decalSurfaceData = GetDecalSurfaceData(posInput, fragInputs, alpha);
                ApplyDecalToSurfaceNormal(decalSurfaceData, surfaceData.normalWS.xyz);
                ApplyDecalToSurfaceDataNoNormal(decalSurfaceData, surfaceData);
            }
            #endif
        #endif
        }
        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);
        
            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;
        
            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif
        
            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif
        
            #ifdef _ENABLE_SHADOW_MATTE
        
                #if (SHADERPASS == SHADERPASS_FORWARD_UNLIT) || (SHADERPASS == SHADERPASS_RAYTRACING_GBUFFER) || (SHADERPASS == SHADERPASS_RAYTRACING_INDIRECT) || (SHADERPASS == SHADERPASS_RAYTRACING_FORWARD)
        
                    HDShadowContext shadowContext = InitShadowContext();
        
                    // Evaluate the shadow, the normal is guaranteed if shadow matte is enabled on this shader.
                    float3 shadow3;
                    ShadowLoopMin(shadowContext, posInput, normalize(fragInputs.tangentToWorld[2]), asuint(_ShadowMatteFilter), GetMeshRenderingLightLayer(), shadow3);
        
                    // Compute the average value in the fourth channel
                    float4 shadow = float4(shadow3, dot(shadow3, float3(1.0/3.0, 1.0/3.0, 1.0/3.0)));
        
                    float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                    float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);
        
                    // Keep the nested lerp
                    // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                    // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                    #ifdef _SURFACE_TYPE_TRANSPARENT
                        surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb), surfaceDescription.Alpha);
                    #else
                        surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow.rgb);
                    #endif
                    localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
        
                    surfaceDescription.Alpha = localAlpha;
        
                #elif SHADERPASS == SHADERPASS_PATH_TRACING
        
                    surfaceData.normalWS = fragInputs.tangentToWorld[2];
                    surfaceData.shadowTint = surfaceDescription.ShadowTint.rgba;
        
                #endif
        
            #endif // _ENABLE_SHADOW_MATTE
        }
        
            // --------------------------------------------------
            // Get Surface And BuiltinData
        
            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif
        
                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif
        
                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT
        
                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
        
                #if defined(HAVE_VFX_MODIFICATION)
                GraphProperties properties;
                ZERO_INITIALIZE(GraphProperties, properties);
        
                GetElementPixelProperties(fragInputs, properties);
        
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs, properties);
                #else
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);
                #endif
        
                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif
        
                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif
        
                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif
        
                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD1
                    float4 lightmapTexCoord1 = fragInputs.texCoord1;
                #else
                    float4 lightmapTexCoord1 = float4(0,0,0,0);
                #endif
        
                #ifdef FRAG_INPUTS_USE_TEXCOORD2
                    float4 lightmapTexCoord2 = fragInputs.texCoord2;
                #else
                    float4 lightmapTexCoord2 = float4(0,0,0,0);
                #endif
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], lightmapTexCoord1, lightmapTexCoord2, builtinData);
        
                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);
        
                ZERO_BUILTIN_INITIALIZE(builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;
        
                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif
        
                #endif // SHADER_UNLIT
        
                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif
        
                // override sampleBakedGI - not used by Unlit
        
                builtinData.emissiveColor = surfaceDescription.Emission;
        
                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                #ifdef UNITY_VIRTUAL_TEXTURING
                #endif
        
                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif
        
                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif
        
                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif
        
                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassPathTracing.hlsl"
        
            // --------------------------------------------------
            // Visual Effect Vertex Invocations
        
        	#ifdef HAVE_VFX_MODIFICATION
        	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/VisualEffectVertex.hlsl"
        	#endif
        
            ENDHLSL
        }
    }
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    CustomEditorForRenderPipeline "Rendering.HighDefinition.HDUnlitGUI" "UnityEngine.Rendering.HighDefinition.HDRenderPipelineAsset"
    FallBack "Hidden/Shader Graph/FallbackError"
}