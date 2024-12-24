// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/BOXOPHOBIC/The Visual Engine/Helpers/Debug"
{
	Properties
	{
		[StyledBanner(Debug)]_Banner("Banner", Float) = 0
		[StyledEnum(NULL, Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3 Branch 4 Leaves 5 Height 6 Sphere 7 UV0_Y 8, 0, 0)]_MotionTinyMaskMode("Motion 01 Anim Mask", Float) = 4
		[StyledEnum(NULL, Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3 Branch 4 Leaves 5 Height 6 Sphere 7 UV0_Y 8, 0, 0)]_MotionBaseMaskMode("Motion 01 Anim Mask", Float) = 3
		[StyledEnum(NULL, Vertex_R 0 Vertex_G 1 Vertex_B 2 Vertex_A 3 Branch 4 Leaves 5 Height 6 Sphere 7 UV0_Y 8, 0, 0)]_MotionSmallMaskMode("Motion 01 Anim Mask", Float) = 4
		[StyledRemapSlider]_MotionTinyMaskRemap("Motion 01 Anim Mask", Vector) = (0,1,0,0)
		[StyledRemapSlider]_MotionBaseMaskRemap("Motion 01 Anim Mask", Vector) = (0,1,0,0)
		[StyledRemapSlider]_MotionSmallMaskRemap("Motion 01 Anim Mask", Vector) = (0,1,0,0)
		[HideInInspector]_motion_base_proc_mode("_motion_base_proc_mode", Vector) = (0,0,0,0)
		[HideInInspector]_motion_tiny_proc_mode("_motion_tiny_proc_mode", Vector) = (0,0,0,0)
		[HideInInspector]_motion_small_proc_mode("_motion_small_proc_mode", Vector) = (0,0,0,0)
		[HideInInspector]_motion_tiny_vert_mode("_motion_tiny_vert_mode", Vector) = (0,0,0,0)
		[HideInInspector]_motion_small_vert_mode("_motion_small_vert_mode", Vector) = (0,0,0,0)
		[HideInInspector]_motion_base_vert_mode("_motion_base_vert_mode", Vector) = (0,0,0,0)
		[HideInInspector]_motion_small_mask_mode("_motion_small_mask_mode", Float) = 0
		[HideInInspector]_motion_tiny_mask_mode("_motion_tiny_mask_mode", Float) = 0
		_ObjectRadiusValue("Object Radius Value", Range( 0 , 50)) = 1
		[HideInInspector]_motion_base_mask_mode("_motion_base_mask_mode", Float) = 0
		_ObjectHeightValue("Object Height Value", Range( 0 , 50)) = 1
		[HideInInspector]_object_phase_mode("_object_phase_mode", Vector) = (0,0,0,0)
		_IsVertexShader("_IsVertexShader", Float) = 0
		_IsSimpleShader("_IsSimpleShader", Float) = 0
		[HideInInspector]_IsTVEShader("_IsTVEShader", Float) = 0
		_IsStandardShader("_IsStandardShader", Float) = 0
		_IsSubsurfaceShader("_IsSubsurfaceShader", Float) = 0
		_IsImpostorShader("_IsImpostorShader", Float) = 0
		_IsCoreShader("_IsCoreShader", Float) = 0
		[NoScaleOffset]_MainNormalTex("_MainNormalTex", 2D) = "black" {}
		[NoScaleOffset]_EmissiveTex("_EmissiveTex", 2D) = "black" {}
		[NoScaleOffset]_SecondMaskTex("_SecondMaskTex", 2D) = "black" {}
		[NoScaleOffset]_SecondNormalTex("_SecondNormalTex", 2D) = "black" {}
		[NoScaleOffset]_SecondAlbedoTex("_SecondAlbedoTex", 2D) = "black" {}
		[NoScaleOffset]_MainAlbedoTex("_MainAlbedoTex", 2D) = "black" {}
		[NoScaleOffset]_MainMaskTex("_MainMaskTex", 2D) = "black" {}
		_RenderClip("_RenderClip", Float) = 0
		_IsElementShader("_IsElementShader", Float) = 0
		_IsHelperShader("_IsHelperShader", Float) = 0
		_MainAlphaClipValue("_MainAlphaClipValue", Float) = 0
		_DetailMode("_DetailMode", Float) = 0
		_EmissiveCat("_EmissiveCat", Float) = 0
		[HDR]_EmissiveColor("_EmissiveColor", Color) = (0,0,0,0)
		_IsBlanketShader("_IsBlanketShader", Float) = 0
		_IsPolygonalShader("_IsPolygonalShader", Float) = 0
		[Space(10)][StyledVector(9)]_main_coord_value("_main_coord_value", Vector) = (1,1,0,0)
		[Enum(UV 0,0,Baked,1)]_DetailCoordMode("Detail Coord", Float) = 0
		[Space(10)][StyledVector(9)]_SecondUVs("Detail UVs", Vector) = (1,1,0,0)
		[Space(10)][StyledVector(9)]_EmissiveUVs("Emissive UVs", Vector) = (1,1,0,0)
		_IsIdentifier("_IsIdentifier", Float) = 0
		_IsLiteShader("_IsLiteShader", Float) = 0
		_IsCustomShader("_IsCustomShader", Float) = 0
		_MotionNoiseTex("_MotionNoiseTex", 2D) = "gray" {}
		[StyledMessage(Info, Use this shader to debug the original mesh or the converted mesh attributes., 0,0)]_Message("Message", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

		//_TransmissionShadow( "Transmission Shadow", Range( 0, 1 ) ) = 0.5
		//_TransStrength( "Trans Strength", Range( 0, 50 ) ) = 1
		//_TransNormal( "Trans Normal Distortion", Range( 0, 1 ) ) = 0.5
		//_TransScattering( "Trans Scattering", Range( 1, 50 ) ) = 2
		//_TransDirect( "Trans Direct", Range( 0, 1 ) ) = 0.9
		//_TransAmbient( "Trans Ambient", Range( 0, 1 ) ) = 0.1
		//_TransShadow( "Trans Shadow", Range( 0, 1 ) ) = 0.5
		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
		//[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		//[ToggleOff] _GlossyReflections("Reflections", Float) = 1.0
	}

	SubShader
	{
		
		Tags { "RenderType"="Opaque" "Queue"="Geometry" "DisableBatching"="True" }
	LOD 0

		Cull Off
		AlphaToMask Off
		ZWrite On
		ZTest LEqual
		ColorMask RGBA
		
		Blend Off
		

		CGINCLUDE
		#pragma target 5.0

		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}

		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		ENDCG

		
		Pass
		{
			
			Name "ForwardBase"
			Tags { "LightMode"="ForwardBase" }

			Blend One Zero

			CGPROGRAM
			#define ASE_NO_AMBIENT 1
			#define ASE_USING_SAMPLING_MACROS 1

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#ifndef UNITY_PASS_FORWARDBASE
				#define UNITY_PASS_FORWARDBASE
			#endif
			#include "HLSLSupport.cginc"
			#ifndef UNITY_INSTANCED_LOD_FADE
				#define UNITY_INSTANCED_LOD_FADE
			#endif
			#ifndef UNITY_INSTANCED_SH
				#define UNITY_INSTANCED_SH
			#endif
			#ifndef UNITY_INSTANCED_LIGHTMAPSTS
				#define UNITY_INSTANCED_LIGHTMAPSTS
			#endif
			#include "UnityShaderVariables.cginc"
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			#include "AutoLight.cginc"

			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_VERT_TANGENT
			#define ASE_NEEDS_FRAG_WORLD_NORMAL
			#define ASE_NEEDS_FRAG_WORLD_VIEW_DIR
			#pragma shader_feature_local_vertex TVE_MOTION_WIND_ELEMENT
			#pragma shader_feature_local_vertex TVE_MOTION_WIND_OFF TVE_MOTION_WIND_OPTIMIZED TVE_MOTION_WIND_ADVANCED
			#pragma shader_feature_local TVE_LEGACY
			#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
			#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
			#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
			#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
			#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
			#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
			#else//ASE Sampling Macros
			#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
			#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
			#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
			#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
			#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplertex,coord,lod) tex2DArraylod(tex, float4(coord,lod))
			#endif//ASE Sampling Macros
			

			struct appdata {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				#if UNITY_VERSION >= 201810
					UNITY_POSITION(pos);
				#else
					float4 pos : SV_POSITION;
				#endif
				#if defined(LIGHTMAP_ON) || (!defined(LIGHTMAP_ON) && SHADER_TARGET >= 30)
					float4 lmap : TEXCOORD0;
				#endif
				#if !defined(LIGHTMAP_ON) && UNITY_SHOULD_SAMPLE_SH
					half3 sh : TEXCOORD1;
				#endif
				#if defined(UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS) && UNITY_VERSION >= 201810 && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					UNITY_LIGHTING_COORDS(2,3)
				#elif defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if UNITY_VERSION >= 201710
						UNITY_SHADOW_COORDS(2)
					#else
						SHADOW_COORDS(2)
					#endif
				#endif
				#ifdef ASE_FOG
					UNITY_FOG_COORDS(4)
				#endif
				float4 tSpace0 : TEXCOORD5;
				float4 tSpace1 : TEXCOORD6;
				float4 tSpace2 : TEXCOORD7;
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 screenPos : TEXCOORD8;
				#endif
				float4 ase_texcoord9 : TEXCOORD9;
				float4 ase_texcoord10 : TEXCOORD10;
				float4 ase_texcoord11 : TEXCOORD11;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			uniform half _Banner;
			uniform half _Message;
			uniform float _IsSimpleShader;
			uniform float _IsVertexShader;
			uniform half _IsTVEShader;
			uniform half TVE_DEBUG_Type;
			uniform float _IsCoreShader;
			uniform float _IsBlanketShader;
			uniform float _IsImpostorShader;
			uniform float _IsPolygonalShader;
			uniform float _IsLiteShader;
			uniform float _IsStandardShader;
			uniform float _IsSubsurfaceShader;
			uniform float _IsCustomShader;
			uniform float _IsIdentifier;
			uniform half TVE_DEBUG_Index;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_MainAlbedoTex);
			uniform half4 _main_coord_value;
			SamplerState sampler_MainAlbedoTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_MainNormalTex);
			SamplerState sampler_MainNormalTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_MainMaskTex);
			SamplerState sampler_MainMaskTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondAlbedoTex);
			uniform half _DetailCoordMode;
			uniform half4 _SecondUVs;
			SamplerState sampler_SecondAlbedoTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondNormalTex);
			SamplerState sampler_SecondNormalTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondMaskTex);
			SamplerState sampler_SecondMaskTex;
			uniform float _DetailMode;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveTex);
			uniform half4 _EmissiveUVs;
			SamplerState sampler_EmissiveTex;
			uniform float4 _EmissiveColor;
			uniform float _EmissiveCat;
			uniform half TVE_DEBUG_Min;
			uniform half TVE_DEBUG_Max;
			float4 _MainAlbedoTex_TexelSize;
			float4 _MainNormalTex_TexelSize;
			float4 _MainMaskTex_TexelSize;
			float4 _SecondAlbedoTex_TexelSize;
			float4 _SecondMaskTex_TexelSize;
			float4 _EmissiveTex_TexelSize;
			uniform float4 _MainAlbedoTex_ST;
			UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_DEBUG_MipTex);
			SamplerState samplerTVE_DEBUG_MipTex;
			uniform float4 _MainNormalTex_ST;
			uniform float4 _MainMaskTex_ST;
			uniform float4 _SecondAlbedoTex_ST;
			uniform float4 _SecondMaskTex_ST;
			uniform float4 _EmissiveTex_ST;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_Sampler6891741_g126363);
			SamplerState sampler_Linear_Repeat;
			uniform half4 TVE_MotionParams;
			uniform half TVE_IsEnabled;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_MotionNoiseTex);
			uniform half4 TVE_TimeParams;
			uniform half TVE_DEBUG_Layer;
			uniform float TVE_WindLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_WindBaseTex);
			uniform half4 TVE_RenderBaseCoords;
			SamplerState sampler_Linear_Clamp;
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_WindNearTex);
			uniform half4 TVE_RenderNearCoords;
			uniform float4 TVE_RenderNearPositionR;
			uniform half TVE_RenderNearFadeValue;
			uniform half4 TVE_WindParams;
			uniform float TVE_CoatLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_CoatBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_CoatNearTex);
			uniform half4 TVE_CoatParams;
			uniform float TVE_PaintLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_PaintBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_PaintNearTex);
			uniform half4 TVE_PaintParams;
			uniform float TVE_GlowLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_GlowBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_GlowNearTex);
			uniform half4 TVE_GlowParams;
			uniform float TVE_AtmoLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_AtmoBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_AtmoNearTex);
			uniform half4 TVE_AtmoParams;
			uniform float TVE_FormLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_FormBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_FormNearTex);
			uniform half4 TVE_FormParams;
			uniform float TVE_PushLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_PushBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_PushNearTex);
			uniform half4 TVE_PushParams;
			uniform half4 _object_phase_mode;
			uniform half4 _motion_base_vert_mode;
			uniform half _ObjectHeightValue;
			uniform half4 _motion_base_proc_mode;
			uniform half _ObjectRadiusValue;
			uniform half _motion_base_mask_mode;
			uniform half4 _MotionBaseMaskRemap;
			uniform half _MotionBaseMaskMode;
			uniform half4 _motion_small_vert_mode;
			uniform half4 _motion_small_proc_mode;
			uniform half _motion_small_mask_mode;
			uniform half4 _MotionSmallMaskRemap;
			uniform half _MotionSmallMaskMode;
			uniform half4 _motion_tiny_vert_mode;
			uniform half4 _motion_tiny_proc_mode;
			uniform half _motion_tiny_mask_mode;
			uniform half4 _MotionTinyMaskRemap;
			uniform half _MotionTinyMaskMode;
			uniform half TVE_DEBUG_Filter;
			uniform half TVE_DEBUG_Shading;
			uniform half TVE_DEBUG_Clip;
			uniform float _RenderClip;
			uniform float _MainAlphaClipValue;
			uniform float _IsElementShader;
			uniform float _IsHelperShader;


			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			
			float2 DecodeFloatToVector2( float enc )
			{
				float2 result ;
				result.y = enc % 2048;
				result.x = floor(enc / 2048);
				return result / (2048 - 1);
			}
			

			v2f VertexFunction (appdata v  ) {
				UNITY_SETUP_INSTANCE_ID(v);
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o);
				UNITY_TRANSFER_INSTANCE_ID(v,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float Debug_Index464_g126363 = TVE_DEBUG_Index;
				float3 ifLocalVar40_g126364 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126364 = saturate( v.vertex.xyz );
				float3 ifLocalVar40_g126371 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126371 = v.normal;
				float3 ifLocalVar40_g126377 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126377 = v.tangent.xyz;
				float ifLocalVar40_g126369 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126369 = saturate( v.tangent.w );
				float ifLocalVar40_g126413 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126413 = v.ase_color.r;
				float ifLocalVar40_g126414 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126414 = v.ase_color.g;
				float ifLocalVar40_g126415 = 0;
				if( Debug_Index464_g126363 == 7.0 )
				ifLocalVar40_g126415 = v.ase_color.b;
				float ifLocalVar40_g126416 = 0;
				if( Debug_Index464_g126363 == 8.0 )
				ifLocalVar40_g126416 = v.ase_color.a;
				float3 appendResult1147_g126363 = (float3(v.ase_texcoord.x , v.ase_texcoord.y , 0.0));
				float3 ifLocalVar40_g126417 = 0;
				if( Debug_Index464_g126363 == 9.0 )
				ifLocalVar40_g126417 = appendResult1147_g126363;
				float3 appendResult1148_g126363 = (float3(v.texcoord1.xyzw.x , v.texcoord1.xyzw.y , 0.0));
				float3 ifLocalVar40_g126418 = 0;
				if( Debug_Index464_g126363 == 10.0 )
				ifLocalVar40_g126418 = appendResult1148_g126363;
				float3 appendResult1149_g126363 = (float3(v.texcoord2.xyzw.x , v.texcoord2.xyzw.y , 0.0));
				float3 ifLocalVar40_g126440 = 0;
				if( Debug_Index464_g126363 == 11.0 )
				ifLocalVar40_g126440 = appendResult1149_g126363;
				float4 break33_g126438 = _object_phase_mode;
				float temp_output_30_0_g126438 = ( v.ase_color.r * break33_g126438.x );
				float temp_output_29_0_g126438 = ( v.ase_color.g * break33_g126438.y );
				float temp_output_31_0_g126438 = ( v.ase_color.b * break33_g126438.z );
				float temp_output_28_0_g126438 = ( temp_output_30_0_g126438 + temp_output_29_0_g126438 + temp_output_31_0_g126438 + ( v.ase_color.a * break33_g126438.w ) );
				half Motion_PhaseMask1725_g126363 = temp_output_28_0_g126438;
				float3 hsvTorgb260_g126363 = HSVToRGB( float3(Motion_PhaseMask1725_g126363,1.0,1.0) );
				float3 gammaToLinear266_g126363 = GammaToLinearSpace( hsvTorgb260_g126363 );
				float3 ifLocalVar40_g126441 = 0;
				if( Debug_Index464_g126363 == 12.0 )
				ifLocalVar40_g126441 = gammaToLinear266_g126363;
				float4 break1821_g126363 = v.ase_color;
				float4 break33_g127001 = _motion_base_vert_mode;
				float temp_output_30_0_g127001 = ( break1821_g126363.r * break33_g127001.x );
				float temp_output_29_0_g127001 = ( break1821_g126363.g * break33_g127001.y );
				float temp_output_31_0_g127001 = ( break1821_g126363.b * break33_g127001.z );
				float temp_output_28_0_g127001 = ( temp_output_30_0_g127001 + temp_output_29_0_g127001 + temp_output_31_0_g127001 + ( break1821_g126363.a * break33_g127001.w ) );
				float temp_output_1824_0_g126363 = temp_output_28_0_g127001;
				half Bounds_Height1700_g126363 = _ObjectHeightValue;
				half Final_HeightMask1815_g126363 = saturate( ( v.vertex.xyz.y / Bounds_Height1700_g126363 ) );
				float4 break33_g127005 = _motion_base_proc_mode;
				float temp_output_30_0_g127005 = ( Final_HeightMask1815_g126363 * break33_g127005.x );
				half Bounds_Radius1702_g126363 = _ObjectRadiusValue;
				half Final_SphereMask1816_g126363 = saturate( ( length( v.vertex.xyz ) / ( Bounds_Height1700_g126363 * Bounds_Radius1702_g126363 ) ) );
				float temp_output_29_0_g127005 = ( Final_SphereMask1816_g126363 * break33_g127005.y );
				float temp_output_1834_0_g126363 = ( temp_output_30_0_g127005 + temp_output_29_0_g127005 );
				float lerpResult1887_g126363 = lerp( temp_output_1824_0_g126363 , temp_output_1834_0_g126363 , _motion_base_mask_mode);
				float clampResult17_g126998 = clamp( lerpResult1887_g126363 , 0.0001 , 0.9999 );
				float temp_output_7_0_g126999 = _MotionBaseMaskRemap.x;
				float temp_output_10_0_g126999 = ( _MotionBaseMaskRemap.y - temp_output_7_0_g126999 );
				float temp_output_6_0_g127000 = saturate( ( ( clampResult17_g126998 - temp_output_7_0_g126999 ) / ( temp_output_10_0_g126999 + 0.0001 ) ) );
				#ifdef TVE_REGISTER
				float staticSwitch14_g127000 = ( temp_output_6_0_g127000 + ( _MotionBaseMaskMode * 0.0 ) );
				#else
				float staticSwitch14_g127000 = temp_output_6_0_g127000;
				#endif
				half Motion_BaseMask1675_g126363 = staticSwitch14_g127000;
				float ifLocalVar40_g126442 = 0;
				if( Debug_Index464_g126363 == 13.0 )
				ifLocalVar40_g126442 = Motion_BaseMask1675_g126363;
				float4 break1855_g126363 = v.ase_color;
				float4 break33_g127002 = _motion_small_vert_mode;
				float temp_output_30_0_g127002 = ( break1855_g126363.r * break33_g127002.x );
				float temp_output_29_0_g127002 = ( break1855_g126363.g * break33_g127002.y );
				float temp_output_31_0_g127002 = ( break1855_g126363.b * break33_g127002.z );
				float temp_output_28_0_g127002 = ( temp_output_30_0_g127002 + temp_output_29_0_g127002 + temp_output_31_0_g127002 + ( break1855_g126363.a * break33_g127002.w ) );
				float temp_output_1845_0_g126363 = temp_output_28_0_g127002;
				float4 break33_g127006 = _motion_small_proc_mode;
				float temp_output_30_0_g127006 = ( Final_HeightMask1815_g126363 * break33_g127006.x );
				float temp_output_29_0_g127006 = ( Final_SphereMask1816_g126363 * break33_g127006.y );
				float temp_output_1849_0_g126363 = ( temp_output_30_0_g127006 + temp_output_29_0_g127006 );
				float lerpResult1889_g126363 = lerp( temp_output_1845_0_g126363 , temp_output_1849_0_g126363 , _motion_small_mask_mode);
				float enc1882_g126363 = v.ase_texcoord.z;
				float2 localDecodeFloatToVector21882_g126363 = DecodeFloatToVector2( enc1882_g126363 );
				float2 break1878_g126363 = localDecodeFloatToVector21882_g126363;
				half Small_Mask_Legacy1879_g126363 = break1878_g126363.x;
				#ifdef TVE_LEGACY
				float staticSwitch1883_g126363 = Small_Mask_Legacy1879_g126363;
				#else
				float staticSwitch1883_g126363 = lerpResult1889_g126363;
				#endif
				float clampResult17_g127008 = clamp( staticSwitch1883_g126363 , 0.0001 , 0.9999 );
				float temp_output_7_0_g127009 = _MotionSmallMaskRemap.x;
				float temp_output_10_0_g127009 = ( _MotionSmallMaskRemap.y - temp_output_7_0_g127009 );
				float temp_output_6_0_g127010 = saturate( ( ( clampResult17_g127008 - temp_output_7_0_g127009 ) / ( temp_output_10_0_g127009 + 0.0001 ) ) );
				#ifdef TVE_REGISTER
				float staticSwitch14_g127010 = ( temp_output_6_0_g127010 + ( _MotionSmallMaskMode * 0.0 ) );
				#else
				float staticSwitch14_g127010 = temp_output_6_0_g127010;
				#endif
				half Motion_SmallMask1693_g126363 = staticSwitch14_g127010;
				float ifLocalVar40_g126443 = 0;
				if( Debug_Index464_g126363 == 14.0 )
				ifLocalVar40_g126443 = Motion_SmallMask1693_g126363;
				float4 break1867_g126363 = v.ase_color;
				float4 break33_g127003 = _motion_tiny_vert_mode;
				float temp_output_30_0_g127003 = ( break1867_g126363.r * break33_g127003.x );
				float temp_output_29_0_g127003 = ( break1867_g126363.g * break33_g127003.y );
				float temp_output_31_0_g127003 = ( break1867_g126363.b * break33_g127003.z );
				float temp_output_28_0_g127003 = ( temp_output_30_0_g127003 + temp_output_29_0_g127003 + temp_output_31_0_g127003 + ( break1867_g126363.a * break33_g127003.w ) );
				float temp_output_1860_0_g126363 = temp_output_28_0_g127003;
				float4 break33_g127007 = _motion_tiny_proc_mode;
				float temp_output_30_0_g127007 = ( Final_HeightMask1815_g126363 * break33_g127007.x );
				float temp_output_29_0_g127007 = ( Final_SphereMask1816_g126363 * break33_g127007.y );
				float temp_output_1864_0_g126363 = ( temp_output_30_0_g127007 + temp_output_29_0_g127007 );
				float lerpResult1891_g126363 = lerp( temp_output_1860_0_g126363 , temp_output_1864_0_g126363 , _motion_tiny_mask_mode);
				half Tiny_Mask_Legacy1880_g126363 = break1878_g126363.y;
				#ifdef TVE_LEGACY
				float staticSwitch1886_g126363 = Tiny_Mask_Legacy1880_g126363;
				#else
				float staticSwitch1886_g126363 = lerpResult1891_g126363;
				#endif
				float clampResult17_g127011 = clamp( staticSwitch1886_g126363 , 0.0001 , 0.9999 );
				float temp_output_7_0_g127012 = _MotionTinyMaskRemap.x;
				float temp_output_10_0_g127012 = ( _MotionTinyMaskRemap.y - temp_output_7_0_g127012 );
				float temp_output_6_0_g127013 = saturate( ( ( clampResult17_g127011 - temp_output_7_0_g127012 ) / ( temp_output_10_0_g127012 + 0.0001 ) ) );
				#ifdef TVE_REGISTER
				float staticSwitch14_g127013 = ( temp_output_6_0_g127013 + ( _MotionTinyMaskMode * 0.0 ) );
				#else
				float staticSwitch14_g127013 = temp_output_6_0_g127013;
				#endif
				half Motion_TinyMask1717_g126363 = staticSwitch14_g127013;
				float ifLocalVar40_g126444 = 0;
				if( Debug_Index464_g126363 == 15.0 )
				ifLocalVar40_g126444 = Motion_TinyMask1717_g126363;
				float3 appendResult60_g126439 = (float3(v.ase_texcoord3.x , 0.0 , v.ase_texcoord3.y));
				float3 ifLocalVar40_g126445 = 0;
				if( Debug_Index464_g126363 == 16.0 )
				ifLocalVar40_g126445 = appendResult60_g126439;
				float3 vertexToFrag328_g126363 = ( ( ifLocalVar40_g126364 + ifLocalVar40_g126371 + ifLocalVar40_g126377 + ifLocalVar40_g126369 ) + ( ifLocalVar40_g126413 + ifLocalVar40_g126414 + ifLocalVar40_g126415 + ifLocalVar40_g126416 ) + ( ifLocalVar40_g126417 + ifLocalVar40_g126418 + ifLocalVar40_g126440 ) + ( ifLocalVar40_g126441 + ifLocalVar40_g126442 + ifLocalVar40_g126443 + ifLocalVar40_g126444 + ifLocalVar40_g126445 ) );
				o.ase_texcoord11.xyz = vertexToFrag328_g126363;
				
				o.ase_texcoord9 = v.ase_texcoord;
				o.ase_texcoord10 = v.texcoord1.xyzw;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord11.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.vertex.w = 1;
				v.normal = v.normal;
				v.tangent = v.tangent;

				o.pos = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
				o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

				#ifdef DYNAMICLIGHTMAP_ON
				o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				#endif
				#ifdef LIGHTMAP_ON
				o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				#ifndef LIGHTMAP_ON
					#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
						o.sh = 0;
						#ifdef VERTEXLIGHT_ON
						o.sh += Shade4PointLights (
							unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
							unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
							unity_4LightAtten0, worldPos, worldNormal);
						#endif
						o.sh = ShadeSHPerVertex (worldNormal, o.sh);
					#endif
				#endif

				#if UNITY_VERSION >= 201810 && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					UNITY_TRANSFER_LIGHTING(o, v.texcoord1.xy);
				#elif defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if UNITY_VERSION >= 201710
						UNITY_TRANSFER_SHADOW(o, v.texcoord1.xy);
					#else
						TRANSFER_SHADOW(o);
					#endif
				#endif

				#ifdef ASE_FOG
					UNITY_TRANSFER_FOG(o,o.pos);
				#endif
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
					o.screenPos = ComputeScreenPos(o.pos);
				#endif
				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( appdata v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.tangent = v.tangent;
				o.normal = v.normal;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				o.ase_texcoord3 = v.ase_texcoord3;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				appdata o = (appdata) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
				o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				o.ase_texcoord3 = patch[0].ase_texcoord3 * bary.x + patch[1].ase_texcoord3 * bary.y + patch[2].ase_texcoord3 * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			v2f vert ( appdata v )
			{
				return VertexFunction( v );
			}
			#endif

			fixed4 frag (v2f IN , bool ase_vface : SV_IsFrontFace
				#ifdef _DEPTHOFFSET_ON
				, out float outputDepth : SV_Depth
				#endif
				) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(IN);

				#ifdef LOD_FADE_CROSSFADE
					UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
				#endif

				#if defined(_SPECULAR_SETUP)
					SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
				#else
					SurfaceOutputStandard o = (SurfaceOutputStandard)0;
				#endif
				float3 WorldTangent = float3(IN.tSpace0.x,IN.tSpace1.x,IN.tSpace2.x);
				float3 WorldBiTangent = float3(IN.tSpace0.y,IN.tSpace1.y,IN.tSpace2.y);
				float3 WorldNormal = float3(IN.tSpace0.z,IN.tSpace1.z,IN.tSpace2.z);
				float3 worldPos = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
				#else
					half atten = 1;
				#endif
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 ScreenPos = IN.screenPos;
				#endif

				float4 color690_g126363 = IsGammaSpace() ? float4(0.1,0.1,0.1,0) : float4(0.01002283,0.01002283,0.01002283,0);
				float4 Shading_Inactive1492_g126363 = color690_g126363;
				float Debug_Type367_g126363 = TVE_DEBUG_Type;
				float4 color646_g126363 = IsGammaSpace() ? float4(0.9245283,0.7969696,0.4142933,1) : float4(0.8368256,0.5987038,0.1431069,1);
				float4 Output_Converted717_g126363 = color646_g126363;
				float4 ifLocalVar40_g126426 = 0;
				if( Debug_Type367_g126363 == 0.0 )
				ifLocalVar40_g126426 = Output_Converted717_g126363;
				float4 color1529_g126363 = IsGammaSpace() ? float4(0.9254902,0.7960784,0.4156863,1) : float4(0.8387991,0.5972018,0.1441285,1);
				float _IsCoreShader1551_g126363 = _IsCoreShader;
				float4 color1539_g126363 = IsGammaSpace() ? float4(0.6196079,0.7686275,0.1490196,0) : float4(0.3419145,0.5520116,0.01938236,0);
				float _IsBlanketShader1554_g126363 = _IsBlanketShader;
				float4 color1542_g126363 = IsGammaSpace() ? float4(0.9716981,0.3162602,0.4816265,0) : float4(0.9368213,0.08154967,0.1974273,0);
				float _IsImpostorShader1110_g126363 = _IsImpostorShader;
				float4 color1544_g126363 = IsGammaSpace() ? float4(0.3254902,0.6117647,0.8117647,0) : float4(0.08650047,0.3324516,0.6239604,0);
				float _IsPolygonalShader1112_g126363 = _IsPolygonalShader;
				float4 color1649_g126363 = IsGammaSpace() ? float4(0.6,0.6,0.6,0) : float4(0.3185468,0.3185468,0.3185468,0);
				float _IsLiteShader1648_g126363 = _IsLiteShader;
				float4 Output_Scope1531_g126363 = ( ( color1529_g126363 * _IsCoreShader1551_g126363 ) + ( color1539_g126363 * _IsBlanketShader1554_g126363 ) + ( color1542_g126363 * _IsImpostorShader1110_g126363 ) + ( color1544_g126363 * _IsPolygonalShader1112_g126363 ) + ( color1649_g126363 * _IsLiteShader1648_g126363 ) );
				float4 ifLocalVar40_g126428 = 0;
				if( Debug_Type367_g126363 == 2.0 )
				ifLocalVar40_g126428 = Output_Scope1531_g126363;
				float4 color529_g126363 = IsGammaSpace() ? float4(0.62,0.77,0.15,0) : float4(0.3423916,0.5542217,0.01960665,0);
				float _IsVertexShader1158_g126363 = _IsVertexShader;
				float4 color544_g126363 = IsGammaSpace() ? float4(0.3252937,0.6122813,0.8113208,0) : float4(0.08639329,0.3330702,0.6231937,0);
				float _IsSimpleShader359_g126363 = _IsSimpleShader;
				float4 color521_g126363 = IsGammaSpace() ? float4(0.6566009,0.3404236,0.8490566,0) : float4(0.3886527,0.09487338,0.6903409,0);
				float _IsStandardShader344_g126363 = _IsStandardShader;
				float4 color1121_g126363 = IsGammaSpace() ? float4(0.9716981,0.88463,0.1787558,0) : float4(0.9368213,0.7573396,0.02686729,0);
				float _IsSubsurfaceShader548_g126363 = _IsSubsurfaceShader;
				float4 Output_Lighting525_g126363 = ( ( color529_g126363 * _IsVertexShader1158_g126363 ) + ( color544_g126363 * _IsSimpleShader359_g126363 ) + ( color521_g126363 * _IsStandardShader344_g126363 ) + ( color1121_g126363 * _IsSubsurfaceShader548_g126363 ) );
				float4 ifLocalVar40_g126429 = 0;
				if( Debug_Type367_g126363 == 3.0 )
				ifLocalVar40_g126429 = Output_Lighting525_g126363;
				float4 color1559_g126363 = IsGammaSpace() ? float4(0.9245283,0.7969696,0.4142933,1) : float4(0.8368256,0.5987038,0.1431069,1);
				float4 color1563_g126363 = IsGammaSpace() ? float4(0.3053578,0.8867924,0.5362216,0) : float4(0.0759199,0.7615293,0.2491121,0);
				float _IsCustomShader1570_g126363 = _IsCustomShader;
				float4 lerpResult1561_g126363 = lerp( color1559_g126363 , color1563_g126363 , _IsCustomShader1570_g126363);
				float4 Output_Custom1560_g126363 = lerpResult1561_g126363;
				float4 ifLocalVar40_g126430 = 0;
				if( Debug_Type367_g126363 == 4.0 )
				ifLocalVar40_g126430 = Output_Custom1560_g126363;
				float3 hsvTorgb1452_g126363 = HSVToRGB( float3(( _IsIdentifier / 1000.0 ),1.0,1.0) );
				float3 gammaToLinear1453_g126363 = GammaToLinearSpace( hsvTorgb1452_g126363 );
				float4 appendResult1657_g126363 = (float4(gammaToLinear1453_g126363 , 1.0));
				float4 Output_Sharing1355_g126363 = appendResult1657_g126363;
				float4 ifLocalVar40_g126431 = 0;
				if( Debug_Type367_g126363 == 5.0 )
				ifLocalVar40_g126431 = Output_Sharing1355_g126363;
				float Debug_Index464_g126363 = TVE_DEBUG_Index;
				half2 Main_UVs1219_g126363 = ( ( IN.ase_texcoord9.xy * (_main_coord_value).xy ) + (_main_coord_value).zw );
				float4 tex2DNode586_g126363 = SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, Main_UVs1219_g126363 );
				float3 appendResult637_g126363 = (float3(tex2DNode586_g126363.r , tex2DNode586_g126363.g , tex2DNode586_g126363.b));
				float3 ifLocalVar40_g126370 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126370 = appendResult637_g126363;
				float ifLocalVar40_g126374 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126374 = SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, Main_UVs1219_g126363 ).a;
				float4 tex2DNode604_g126363 = SAMPLE_TEXTURE2D( _MainNormalTex, sampler_MainNormalTex, Main_UVs1219_g126363 );
				float3 appendResult876_g126363 = (float3(tex2DNode604_g126363.a , tex2DNode604_g126363.g , 1.0));
				float3 gammaToLinear878_g126363 = GammaToLinearSpace( appendResult876_g126363 );
				float3 ifLocalVar40_g126378 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126378 = gammaToLinear878_g126363;
				float ifLocalVar40_g126366 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126366 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, Main_UVs1219_g126363 ).r;
				float ifLocalVar40_g126384 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126384 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, Main_UVs1219_g126363 ).g;
				float ifLocalVar40_g126375 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126375 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, Main_UVs1219_g126363 ).b;
				float ifLocalVar40_g126365 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126365 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, Main_UVs1219_g126363 ).a;
				float2 appendResult1251_g126363 = (float2(IN.ase_texcoord10.z , IN.ase_texcoord10.w));
				float2 Mesh_DetailCoord1254_g126363 = appendResult1251_g126363;
				float2 lerpResult1231_g126363 = lerp( IN.ase_texcoord9.xy , Mesh_DetailCoord1254_g126363 , _DetailCoordMode);
				half2 Second_UVs1234_g126363 = ( ( lerpResult1231_g126363 * (_SecondUVs).xy ) + (_SecondUVs).zw );
				float4 tex2DNode854_g126363 = SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondAlbedoTex, Second_UVs1234_g126363 );
				float3 appendResult839_g126363 = (float3(tex2DNode854_g126363.r , tex2DNode854_g126363.g , tex2DNode854_g126363.b));
				float3 ifLocalVar40_g126368 = 0;
				if( Debug_Index464_g126363 == 7.0 )
				ifLocalVar40_g126368 = appendResult839_g126363;
				float ifLocalVar40_g126376 = 0;
				if( Debug_Index464_g126363 == 8.0 )
				ifLocalVar40_g126376 = SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondAlbedoTex, Second_UVs1234_g126363 ).a;
				float4 tex2DNode841_g126363 = SAMPLE_TEXTURE2D( _SecondNormalTex, sampler_SecondNormalTex, Second_UVs1234_g126363 );
				float3 appendResult880_g126363 = (float3(tex2DNode841_g126363.a , tex2DNode841_g126363.g , 1.0));
				float3 gammaToLinear879_g126363 = GammaToLinearSpace( appendResult880_g126363 );
				float3 ifLocalVar40_g126383 = 0;
				if( Debug_Index464_g126363 == 8.0 )
				ifLocalVar40_g126383 = gammaToLinear879_g126363;
				float ifLocalVar40_g126379 = 0;
				if( Debug_Index464_g126363 == 10.0 )
				ifLocalVar40_g126379 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs1234_g126363 ).r;
				float ifLocalVar40_g126373 = 0;
				if( Debug_Index464_g126363 == 11.0 )
				ifLocalVar40_g126373 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs1234_g126363 ).g;
				float ifLocalVar40_g126381 = 0;
				if( Debug_Index464_g126363 == 12.0 )
				ifLocalVar40_g126381 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs1234_g126363 ).b;
				float ifLocalVar40_g126382 = 0;
				if( Debug_Index464_g126363 == 13.0 )
				ifLocalVar40_g126382 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs1234_g126363 ).a;
				half2 Emissive_UVs1245_g126363 = ( ( IN.ase_texcoord9.xy * (_EmissiveUVs).xy ) + (_EmissiveUVs).zw );
				float4 tex2DNode858_g126363 = SAMPLE_TEXTURE2D( _EmissiveTex, sampler_EmissiveTex, Emissive_UVs1245_g126363 );
				float ifLocalVar40_g126372 = 0;
				if( Debug_Index464_g126363 == 14.0 )
				ifLocalVar40_g126372 = tex2DNode858_g126363.r;
				float Debug_Min721_g126363 = TVE_DEBUG_Min;
				float temp_output_7_0_g126380 = Debug_Min721_g126363;
				float4 temp_cast_3 = (temp_output_7_0_g126380).xxxx;
				float Debug_Max723_g126363 = TVE_DEBUG_Max;
				float temp_output_10_0_g126380 = ( Debug_Max723_g126363 - temp_output_7_0_g126380 );
				float4 Output_Maps561_g126363 = saturate( ( ( ( float4( ( ( ifLocalVar40_g126370 + ifLocalVar40_g126374 + ifLocalVar40_g126378 ) + ( ifLocalVar40_g126366 + ifLocalVar40_g126384 + ifLocalVar40_g126375 + ifLocalVar40_g126365 ) ) , 0.0 ) + float4( ( ( ( ifLocalVar40_g126368 + ifLocalVar40_g126376 + ifLocalVar40_g126383 ) + ( ifLocalVar40_g126379 + ifLocalVar40_g126373 + ifLocalVar40_g126381 + ifLocalVar40_g126382 ) ) * _DetailMode ) , 0.0 ) + ( ( ifLocalVar40_g126372 * _EmissiveColor ) * _EmissiveCat ) ) - temp_cast_3 ) / ( temp_output_10_0_g126380 + 0.0001 ) ) );
				float4 ifLocalVar40_g126432 = 0;
				if( Debug_Type367_g126363 == 6.0 )
				ifLocalVar40_g126432 = Output_Maps561_g126363;
				float Resolution44_g126401 = max( _MainAlbedoTex_TexelSize.z , _MainAlbedoTex_TexelSize.w );
				float4 color62_g126401 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126401 = 0;
				if( Resolution44_g126401 <= 256.0 )
				ifLocalVar61_g126401 = color62_g126401;
				float4 color55_g126401 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126401 = 0;
				if( Resolution44_g126401 == 512.0 )
				ifLocalVar56_g126401 = color55_g126401;
				float4 color42_g126401 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126401 = 0;
				if( Resolution44_g126401 == 1024.0 )
				ifLocalVar40_g126401 = color42_g126401;
				float4 color48_g126401 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126401 = 0;
				if( Resolution44_g126401 == 2048.0 )
				ifLocalVar47_g126401 = color48_g126401;
				float4 color51_g126401 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126401 = 0;
				if( Resolution44_g126401 >= 4096.0 )
				ifLocalVar52_g126401 = color51_g126401;
				float4 ifLocalVar40_g126387 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126387 = ( ifLocalVar61_g126401 + ifLocalVar56_g126401 + ifLocalVar40_g126401 + ifLocalVar47_g126401 + ifLocalVar52_g126401 );
				float Resolution44_g126400 = max( _MainNormalTex_TexelSize.z , _MainNormalTex_TexelSize.w );
				float4 color62_g126400 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126400 = 0;
				if( Resolution44_g126400 <= 256.0 )
				ifLocalVar61_g126400 = color62_g126400;
				float4 color55_g126400 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126400 = 0;
				if( Resolution44_g126400 == 512.0 )
				ifLocalVar56_g126400 = color55_g126400;
				float4 color42_g126400 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126400 = 0;
				if( Resolution44_g126400 == 1024.0 )
				ifLocalVar40_g126400 = color42_g126400;
				float4 color48_g126400 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126400 = 0;
				if( Resolution44_g126400 == 2048.0 )
				ifLocalVar47_g126400 = color48_g126400;
				float4 color51_g126400 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126400 = 0;
				if( Resolution44_g126400 >= 4096.0 )
				ifLocalVar52_g126400 = color51_g126400;
				float4 ifLocalVar40_g126385 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126385 = ( ifLocalVar61_g126400 + ifLocalVar56_g126400 + ifLocalVar40_g126400 + ifLocalVar47_g126400 + ifLocalVar52_g126400 );
				float Resolution44_g126399 = max( _MainMaskTex_TexelSize.z , _MainMaskTex_TexelSize.w );
				float4 color62_g126399 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126399 = 0;
				if( Resolution44_g126399 <= 256.0 )
				ifLocalVar61_g126399 = color62_g126399;
				float4 color55_g126399 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126399 = 0;
				if( Resolution44_g126399 == 512.0 )
				ifLocalVar56_g126399 = color55_g126399;
				float4 color42_g126399 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126399 = 0;
				if( Resolution44_g126399 == 1024.0 )
				ifLocalVar40_g126399 = color42_g126399;
				float4 color48_g126399 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126399 = 0;
				if( Resolution44_g126399 == 2048.0 )
				ifLocalVar47_g126399 = color48_g126399;
				float4 color51_g126399 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126399 = 0;
				if( Resolution44_g126399 >= 4096.0 )
				ifLocalVar52_g126399 = color51_g126399;
				float4 ifLocalVar40_g126386 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126386 = ( ifLocalVar61_g126399 + ifLocalVar56_g126399 + ifLocalVar40_g126399 + ifLocalVar47_g126399 + ifLocalVar52_g126399 );
				float Resolution44_g126406 = max( _SecondAlbedoTex_TexelSize.z , _SecondAlbedoTex_TexelSize.w );
				float4 color62_g126406 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126406 = 0;
				if( Resolution44_g126406 <= 256.0 )
				ifLocalVar61_g126406 = color62_g126406;
				float4 color55_g126406 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126406 = 0;
				if( Resolution44_g126406 == 512.0 )
				ifLocalVar56_g126406 = color55_g126406;
				float4 color42_g126406 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126406 = 0;
				if( Resolution44_g126406 == 1024.0 )
				ifLocalVar40_g126406 = color42_g126406;
				float4 color48_g126406 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126406 = 0;
				if( Resolution44_g126406 == 2048.0 )
				ifLocalVar47_g126406 = color48_g126406;
				float4 color51_g126406 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126406 = 0;
				if( Resolution44_g126406 >= 4096.0 )
				ifLocalVar52_g126406 = color51_g126406;
				float4 ifLocalVar40_g126393 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126393 = ( ifLocalVar61_g126406 + ifLocalVar56_g126406 + ifLocalVar40_g126406 + ifLocalVar47_g126406 + ifLocalVar52_g126406 );
				float Resolution44_g126405 = max( _SecondMaskTex_TexelSize.z , _SecondMaskTex_TexelSize.w );
				float4 color62_g126405 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126405 = 0;
				if( Resolution44_g126405 <= 256.0 )
				ifLocalVar61_g126405 = color62_g126405;
				float4 color55_g126405 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126405 = 0;
				if( Resolution44_g126405 == 512.0 )
				ifLocalVar56_g126405 = color55_g126405;
				float4 color42_g126405 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126405 = 0;
				if( Resolution44_g126405 == 1024.0 )
				ifLocalVar40_g126405 = color42_g126405;
				float4 color48_g126405 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126405 = 0;
				if( Resolution44_g126405 == 2048.0 )
				ifLocalVar47_g126405 = color48_g126405;
				float4 color51_g126405 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126405 = 0;
				if( Resolution44_g126405 >= 4096.0 )
				ifLocalVar52_g126405 = color51_g126405;
				float4 ifLocalVar40_g126391 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126391 = ( ifLocalVar61_g126405 + ifLocalVar56_g126405 + ifLocalVar40_g126405 + ifLocalVar47_g126405 + ifLocalVar52_g126405 );
				float Resolution44_g126407 = max( _SecondAlbedoTex_TexelSize.z , _SecondAlbedoTex_TexelSize.w );
				float4 color62_g126407 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126407 = 0;
				if( Resolution44_g126407 <= 256.0 )
				ifLocalVar61_g126407 = color62_g126407;
				float4 color55_g126407 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126407 = 0;
				if( Resolution44_g126407 == 512.0 )
				ifLocalVar56_g126407 = color55_g126407;
				float4 color42_g126407 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126407 = 0;
				if( Resolution44_g126407 == 1024.0 )
				ifLocalVar40_g126407 = color42_g126407;
				float4 color48_g126407 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126407 = 0;
				if( Resolution44_g126407 == 2048.0 )
				ifLocalVar47_g126407 = color48_g126407;
				float4 color51_g126407 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126407 = 0;
				if( Resolution44_g126407 >= 4096.0 )
				ifLocalVar52_g126407 = color51_g126407;
				float4 ifLocalVar40_g126392 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126392 = ( ifLocalVar61_g126407 + ifLocalVar56_g126407 + ifLocalVar40_g126407 + ifLocalVar47_g126407 + ifLocalVar52_g126407 );
				float Resolution44_g126404 = max( _EmissiveTex_TexelSize.z , _EmissiveTex_TexelSize.w );
				float4 color62_g126404 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126404 = 0;
				if( Resolution44_g126404 <= 256.0 )
				ifLocalVar61_g126404 = color62_g126404;
				float4 color55_g126404 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126404 = 0;
				if( Resolution44_g126404 == 512.0 )
				ifLocalVar56_g126404 = color55_g126404;
				float4 color42_g126404 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126404 = 0;
				if( Resolution44_g126404 == 1024.0 )
				ifLocalVar40_g126404 = color42_g126404;
				float4 color48_g126404 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126404 = 0;
				if( Resolution44_g126404 == 2048.0 )
				ifLocalVar47_g126404 = color48_g126404;
				float4 color51_g126404 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126404 = 0;
				if( Resolution44_g126404 >= 4096.0 )
				ifLocalVar52_g126404 = color51_g126404;
				float4 ifLocalVar40_g126394 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126394 = ( ifLocalVar61_g126404 + ifLocalVar56_g126404 + ifLocalVar40_g126404 + ifLocalVar47_g126404 + ifLocalVar52_g126404 );
				float4 Output_Resolution737_g126363 = ( ( ifLocalVar40_g126387 + ifLocalVar40_g126385 + ifLocalVar40_g126386 ) + ( ifLocalVar40_g126393 + ifLocalVar40_g126391 + ifLocalVar40_g126392 ) + ifLocalVar40_g126394 );
				float4 ifLocalVar40_g126433 = 0;
				if( Debug_Type367_g126363 == 7.0 )
				ifLocalVar40_g126433 = Output_Resolution737_g126363;
				float2 uv_MainAlbedoTex = IN.ase_texcoord9.xy * _MainAlbedoTex_ST.xy + _MainAlbedoTex_ST.zw;
				float2 UVs72_g126412 = Main_UVs1219_g126363;
				float Resolution44_g126412 = max( _MainAlbedoTex_TexelSize.z , _MainAlbedoTex_TexelSize.w );
				float4 tex2DNode77_g126412 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126412 * ( Resolution44_g126412 / 8.0 ) ) );
				float4 lerpResult78_g126412 = lerp( SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, uv_MainAlbedoTex ) , tex2DNode77_g126412 , tex2DNode77_g126412.a);
				float4 ifLocalVar40_g126390 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126390 = lerpResult78_g126412;
				float2 uv_MainNormalTex = IN.ase_texcoord9.xy * _MainNormalTex_ST.xy + _MainNormalTex_ST.zw;
				float2 UVs72_g126403 = Main_UVs1219_g126363;
				float Resolution44_g126403 = max( _MainNormalTex_TexelSize.z , _MainNormalTex_TexelSize.w );
				float4 tex2DNode77_g126403 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126403 * ( Resolution44_g126403 / 8.0 ) ) );
				float4 lerpResult78_g126403 = lerp( SAMPLE_TEXTURE2D( _MainNormalTex, sampler_MainNormalTex, uv_MainNormalTex ) , tex2DNode77_g126403 , tex2DNode77_g126403.a);
				float4 ifLocalVar40_g126388 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126388 = lerpResult78_g126403;
				float2 uv_MainMaskTex = IN.ase_texcoord9.xy * _MainMaskTex_ST.xy + _MainMaskTex_ST.zw;
				float2 UVs72_g126402 = Main_UVs1219_g126363;
				float Resolution44_g126402 = max( _MainMaskTex_TexelSize.z , _MainMaskTex_TexelSize.w );
				float4 tex2DNode77_g126402 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126402 * ( Resolution44_g126402 / 8.0 ) ) );
				float4 lerpResult78_g126402 = lerp( SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, uv_MainMaskTex ) , tex2DNode77_g126402 , tex2DNode77_g126402.a);
				float4 ifLocalVar40_g126389 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126389 = lerpResult78_g126402;
				float2 uv_SecondAlbedoTex = IN.ase_texcoord9.xy * _SecondAlbedoTex_ST.xy + _SecondAlbedoTex_ST.zw;
				float2 UVs72_g126410 = Second_UVs1234_g126363;
				float Resolution44_g126410 = max( _SecondAlbedoTex_TexelSize.z , _SecondAlbedoTex_TexelSize.w );
				float4 tex2DNode77_g126410 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126410 * ( Resolution44_g126410 / 8.0 ) ) );
				float4 lerpResult78_g126410 = lerp( SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondAlbedoTex, uv_SecondAlbedoTex ) , tex2DNode77_g126410 , tex2DNode77_g126410.a);
				float4 ifLocalVar40_g126397 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126397 = lerpResult78_g126410;
				float2 uv_SecondMaskTex = IN.ase_texcoord9.xy * _SecondMaskTex_ST.xy + _SecondMaskTex_ST.zw;
				float2 UVs72_g126409 = Second_UVs1234_g126363;
				float Resolution44_g126409 = max( _SecondMaskTex_TexelSize.z , _SecondMaskTex_TexelSize.w );
				float4 tex2DNode77_g126409 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126409 * ( Resolution44_g126409 / 8.0 ) ) );
				float4 lerpResult78_g126409 = lerp( SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, uv_SecondMaskTex ) , tex2DNode77_g126409 , tex2DNode77_g126409.a);
				float4 ifLocalVar40_g126395 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126395 = lerpResult78_g126409;
				float2 UVs72_g126411 = Second_UVs1234_g126363;
				float Resolution44_g126411 = max( _SecondAlbedoTex_TexelSize.z , _SecondAlbedoTex_TexelSize.w );
				float4 tex2DNode77_g126411 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126411 * ( Resolution44_g126411 / 8.0 ) ) );
				float4 lerpResult78_g126411 = lerp( SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondAlbedoTex, uv_SecondAlbedoTex ) , tex2DNode77_g126411 , tex2DNode77_g126411.a);
				float4 ifLocalVar40_g126396 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126396 = lerpResult78_g126411;
				float2 uv_EmissiveTex = IN.ase_texcoord9.xy * _EmissiveTex_ST.xy + _EmissiveTex_ST.zw;
				float2 UVs72_g126408 = Emissive_UVs1245_g126363;
				float Resolution44_g126408 = max( _EmissiveTex_TexelSize.z , _EmissiveTex_TexelSize.w );
				float4 tex2DNode77_g126408 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126408 * ( Resolution44_g126408 / 8.0 ) ) );
				float4 lerpResult78_g126408 = lerp( SAMPLE_TEXTURE2D( _EmissiveTex, sampler_EmissiveTex, uv_EmissiveTex ) , tex2DNode77_g126408 , tex2DNode77_g126408.a);
				float4 ifLocalVar40_g126398 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126398 = lerpResult78_g126408;
				float4 Output_MipLevel1284_g126363 = ( ( ifLocalVar40_g126390 + ifLocalVar40_g126388 + ifLocalVar40_g126389 ) + ( ifLocalVar40_g126397 + ifLocalVar40_g126395 + ifLocalVar40_g126396 ) + ifLocalVar40_g126398 );
				float4 ifLocalVar40_g126434 = 0;
				if( Debug_Type367_g126363 == 8.0 )
				ifLocalVar40_g126434 = Output_MipLevel1284_g126363;
				float3 WorldPosition893_g126363 = worldPos;
				half3 Input_PositionWO419_g126446 = WorldPosition893_g126363;
				half Input_MotionTillingRT670_g126446 = ( 4.0 + 0.2 );
				half2 Noise_CoordRT668_g126446 = ( -(Input_PositionWO419_g126446).xz * Input_MotionTillingRT670_g126446 * 0.005 );
				half4 Noise_ParamsRT535_g126446 = SAMPLE_TEXTURE2D( _Sampler6891741_g126363, sampler_Linear_Repeat, Noise_CoordRT668_g126446 );
				half Input_MotionValue629_g126446 = 1.0;
				float2 lerpResult709_g126446 = lerp( float2( 0.5,0.5 ) , (Noise_ParamsRT535_g126446).rg , Input_MotionValue629_g126446);
				float3 appendResult712_g126446 = (float3(lerpResult709_g126446 , ( (Noise_ParamsRT535_g126446).b * Input_MotionValue629_g126446 )));
				float4 lerpResult627_g126446 = lerp( half4(0,1,1,0) , TVE_MotionParams , TVE_IsEnabled);
				half2 Global_WindDirection593_g126446 = (lerpResult627_g126446).xy;
				half Input_MotionTilling321_g126446 = ( 4.0 + 0.2 );
				half2 Noise_Coord515_g126446 = ( -(Input_PositionWO419_g126446).xz * Input_MotionTilling321_g126446 * 0.005 );
				float2 temp_output_3_0_g126447 = Noise_Coord515_g126446;
				float2 temp_output_21_0_g126447 = (Global_WindDirection593_g126446*2.0 + -1.0);
				float lerpResult128_g126452 = lerp( _Time.y , ( ( _Time.y * TVE_TimeParams.x ) + TVE_TimeParams.y ) , TVE_TimeParams.w);
				half Input_MotionSpeed62_g126446 = 4.0;
				half Noise_Speed516_g126446 = ( ( lerpResult128_g126452 * Input_MotionSpeed62_g126446 ) * 0.02 );
				float temp_output_15_0_g126447 = Noise_Speed516_g126446;
				float temp_output_23_0_g126447 = frac( temp_output_15_0_g126447 );
				float4 lerpResult39_g126447 = lerp( SAMPLE_TEXTURE2D( _MotionNoiseTex, sampler_Linear_Repeat, ( temp_output_3_0_g126447 + ( temp_output_21_0_g126447 * temp_output_23_0_g126447 ) ) ) , SAMPLE_TEXTURE2D( _MotionNoiseTex, sampler_Linear_Repeat, ( temp_output_3_0_g126447 + ( temp_output_21_0_g126447 * frac( ( temp_output_15_0_g126447 + 0.5 ) ) ) ) ) , ( abs( ( temp_output_23_0_g126447 - 0.5 ) ) / 0.5 ));
				half4 Noise_Params685_g126446 = lerpResult39_g126447;
				half Input_MotionNoise552_g126446 = 0.0;
				half Global_WindIntensity576_g126446 = (lerpResult627_g126446).z;
				half Motion_Intensity720_g126446 = ( Global_WindIntensity576_g126446 * Input_MotionValue629_g126446 );
				float lerpResult701_g126446 = lerp( 1.0 , Input_MotionNoise552_g126446 , Motion_Intensity720_g126446);
				float2 lerpResult646_g126446 = lerp( Global_WindDirection593_g126446 , (Noise_Params685_g126446).rg , lerpResult701_g126446);
				float2 lerpResult724_g126446 = lerp( float2( 0.5,0.5 ) , lerpResult646_g126446 , Motion_Intensity720_g126446);
				float3 appendResult708_g126446 = (float3(lerpResult724_g126446 , ( (Noise_Params685_g126446).a * Motion_Intensity720_g126446 )));
				#if defined( TVE_MOTION_WIND_OFF )
				float3 staticSwitch673_g126446 = half3(0.5,0.5,0);
				#elif defined( TVE_MOTION_WIND_OPTIMIZED )
				float3 staticSwitch673_g126446 = appendResult712_g126446;
				#elif defined( TVE_MOTION_WIND_ADVANCED )
				float3 staticSwitch673_g126446 = appendResult708_g126446;
				#else
				float3 staticSwitch673_g126446 = half3(0.5,0.5,0);
				#endif
				half3 Motion_Local694_g126446 = staticSwitch673_g126446;
				float Debug_Layer885_g126363 = TVE_DEBUG_Layer;
				float temp_output_136_0_g126453 = Debug_Layer885_g126363;
				float temp_output_19_0_g126455 = TVE_WindLayers[(int)temp_output_136_0_g126453];
				half3 Input_Position180_g126456 = Input_PositionWO419_g126446;
				float2 temp_output_75_0_g126456 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126456).xz ) );
				float temp_output_82_0_g126456 = temp_output_136_0_g126453;
				float2 temp_output_119_0_g126456 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126456).xz ) );
				float temp_output_7_0_g126461 = 1.0;
				float temp_output_10_0_g126461 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126461 );
				float4 lerpResult131_g126456 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126456,temp_output_82_0_g126456), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126456,temp_output_82_0_g126456), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126456 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126461 ) / temp_output_10_0_g126461 ) ));
				float4 temp_output_17_0_g126455 = lerpResult131_g126456;
				float4 temp_output_3_0_g126455 = TVE_WindParams;
				float4 ifLocalVar18_g126455 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126455 >= 0.5 )
				ifLocalVar18_g126455 = temp_output_17_0_g126455;
				else
				ifLocalVar18_g126455 = temp_output_3_0_g126455;
				float4 lerpResult22_g126455 = lerp( temp_output_3_0_g126455 , temp_output_17_0_g126455 , temp_output_19_0_g126455);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126455 = lerpResult22_g126455;
				#else
				float4 staticSwitch24_g126455 = ifLocalVar18_g126455;
				#endif
				float4 temp_output_610_0_g126446 = staticSwitch24_g126455;
				float2 lerpResult699_g126446 = lerp( float2( 0.5,0.5 ) , (temp_output_610_0_g126446).rg , Input_MotionValue629_g126446);
				float3 appendResult643_g126446 = (float3(lerpResult699_g126446 , ( (temp_output_610_0_g126446).b * Input_MotionValue629_g126446 )));
				float3 lerpResult623_g126446 = lerp( Motion_Local694_g126446 , appendResult643_g126446 , (temp_output_610_0_g126446).a);
				#if defined( TVE_MOTION_WIND_OFF )
				float3 staticSwitch693_g126446 = appendResult643_g126446;
				#elif defined( TVE_MOTION_WIND_OPTIMIZED )
				float3 staticSwitch693_g126446 = lerpResult623_g126446;
				#elif defined( TVE_MOTION_WIND_ADVANCED )
				float3 staticSwitch693_g126446 = lerpResult623_g126446;
				#else
				float3 staticSwitch693_g126446 = appendResult643_g126446;
				#endif
				#ifdef TVE_MOTION_WIND_ELEMENT
				float3 staticSwitch612_g126446 = staticSwitch693_g126446;
				#else
				float3 staticSwitch612_g126446 = Motion_Local694_g126446;
				#endif
				float ifLocalVar40_g126997 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126997 = (staticSwitch612_g126446).z;
				float temp_output_82_0_g126816 = Debug_Layer885_g126363;
				float temp_output_19_0_g126818 = TVE_CoatLayers[(int)temp_output_82_0_g126816];
				half3 Input_Position180_g126819 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126819 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126819).xz ) );
				float temp_output_82_0_g126819 = temp_output_82_0_g126816;
				float2 temp_output_119_0_g126819 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126819).xz ) );
				float temp_output_7_0_g126824 = 1.0;
				float temp_output_10_0_g126824 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126824 );
				float4 lerpResult131_g126819 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_CoatBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126819,temp_output_82_0_g126819), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_CoatNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126819,temp_output_82_0_g126819), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126819 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126824 ) / temp_output_10_0_g126824 ) ));
				float4 temp_output_17_0_g126818 = lerpResult131_g126819;
				float4 temp_output_3_0_g126818 = TVE_CoatParams;
				float4 ifLocalVar18_g126818 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126818 >= 0.5 )
				ifLocalVar18_g126818 = temp_output_17_0_g126818;
				else
				ifLocalVar18_g126818 = temp_output_3_0_g126818;
				float4 lerpResult22_g126818 = lerp( temp_output_3_0_g126818 , temp_output_17_0_g126818 , temp_output_19_0_g126818);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126818 = lerpResult22_g126818;
				#else
				float4 staticSwitch24_g126818 = ifLocalVar18_g126818;
				#endif
				float ifLocalVar40_g126834 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126834 = (staticSwitch24_g126818).g;
				float temp_output_82_0_g126825 = Debug_Layer885_g126363;
				float temp_output_19_0_g126827 = TVE_CoatLayers[(int)temp_output_82_0_g126825];
				half3 Input_Position180_g126828 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126828 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126828).xz ) );
				float temp_output_82_0_g126828 = temp_output_82_0_g126825;
				float2 temp_output_119_0_g126828 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126828).xz ) );
				float temp_output_7_0_g126833 = 1.0;
				float temp_output_10_0_g126833 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126833 );
				float4 lerpResult131_g126828 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_CoatBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126828,temp_output_82_0_g126828), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_CoatNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126828,temp_output_82_0_g126828), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126828 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126833 ) / temp_output_10_0_g126833 ) ));
				float4 temp_output_17_0_g126827 = lerpResult131_g126828;
				float4 temp_output_3_0_g126827 = TVE_CoatParams;
				float4 ifLocalVar18_g126827 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126827 >= 0.5 )
				ifLocalVar18_g126827 = temp_output_17_0_g126827;
				else
				ifLocalVar18_g126827 = temp_output_3_0_g126827;
				float4 lerpResult22_g126827 = lerp( temp_output_3_0_g126827 , temp_output_17_0_g126827 , temp_output_19_0_g126827);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126827 = lerpResult22_g126827;
				#else
				float4 staticSwitch24_g126827 = ifLocalVar18_g126827;
				#endif
				float ifLocalVar40_g126835 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126835 = (staticSwitch24_g126827).b;
				float temp_output_82_0_g126845 = Debug_Layer885_g126363;
				float temp_output_19_0_g126847 = TVE_PaintLayers[(int)temp_output_82_0_g126845];
				half3 Input_Position180_g126848 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126848 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126848).xz ) );
				float temp_output_82_0_g126848 = temp_output_82_0_g126845;
				float2 temp_output_119_0_g126848 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126848).xz ) );
				float temp_output_7_0_g126853 = 1.0;
				float temp_output_10_0_g126853 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126853 );
				float4 lerpResult131_g126848 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PaintBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126848,temp_output_82_0_g126848), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PaintNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126848,temp_output_82_0_g126848), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126848 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126853 ) / temp_output_10_0_g126853 ) ));
				float4 temp_output_17_0_g126847 = lerpResult131_g126848;
				float4 temp_output_3_0_g126847 = TVE_PaintParams;
				float4 ifLocalVar18_g126847 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126847 >= 0.5 )
				ifLocalVar18_g126847 = temp_output_17_0_g126847;
				else
				ifLocalVar18_g126847 = temp_output_3_0_g126847;
				float4 lerpResult22_g126847 = lerp( temp_output_3_0_g126847 , temp_output_17_0_g126847 , temp_output_19_0_g126847);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126847 = lerpResult22_g126847;
				#else
				float4 staticSwitch24_g126847 = ifLocalVar18_g126847;
				#endif
				float3 ifLocalVar40_g126854 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126854 = (staticSwitch24_g126847).rgb;
				float temp_output_82_0_g126836 = Debug_Layer885_g126363;
				float temp_output_19_0_g126838 = TVE_PaintLayers[(int)temp_output_82_0_g126836];
				half3 Input_Position180_g126839 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126839 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126839).xz ) );
				float temp_output_82_0_g126839 = temp_output_82_0_g126836;
				float2 temp_output_119_0_g126839 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126839).xz ) );
				float temp_output_7_0_g126844 = 1.0;
				float temp_output_10_0_g126844 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126844 );
				float4 lerpResult131_g126839 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PaintBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126839,temp_output_82_0_g126839), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PaintNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126839,temp_output_82_0_g126839), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126839 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126844 ) / temp_output_10_0_g126844 ) ));
				float4 temp_output_17_0_g126838 = lerpResult131_g126839;
				float4 temp_output_3_0_g126838 = TVE_PaintParams;
				float4 ifLocalVar18_g126838 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126838 >= 0.5 )
				ifLocalVar18_g126838 = temp_output_17_0_g126838;
				else
				ifLocalVar18_g126838 = temp_output_3_0_g126838;
				float4 lerpResult22_g126838 = lerp( temp_output_3_0_g126838 , temp_output_17_0_g126838 , temp_output_19_0_g126838);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126838 = lerpResult22_g126838;
				#else
				float4 staticSwitch24_g126838 = ifLocalVar18_g126838;
				#endif
				float ifLocalVar40_g126855 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126855 = saturate( (staticSwitch24_g126838).a );
				float temp_output_82_0_g126856 = Debug_Layer885_g126363;
				float temp_output_19_0_g126858 = TVE_GlowLayers[(int)temp_output_82_0_g126856];
				half3 Input_Position180_g126859 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126859 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126859).xz ) );
				float temp_output_82_0_g126859 = temp_output_82_0_g126856;
				float2 temp_output_119_0_g126859 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126859).xz ) );
				float temp_output_7_0_g126864 = 1.0;
				float temp_output_10_0_g126864 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126864 );
				float4 lerpResult131_g126859 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_GlowBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126859,temp_output_82_0_g126859), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_GlowNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126859,temp_output_82_0_g126859), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126859 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126864 ) / temp_output_10_0_g126864 ) ));
				float4 temp_output_17_0_g126858 = lerpResult131_g126859;
				float4 temp_output_3_0_g126858 = TVE_GlowParams;
				float4 ifLocalVar18_g126858 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126858 >= 0.5 )
				ifLocalVar18_g126858 = temp_output_17_0_g126858;
				else
				ifLocalVar18_g126858 = temp_output_3_0_g126858;
				float4 lerpResult22_g126858 = lerp( temp_output_3_0_g126858 , temp_output_17_0_g126858 , temp_output_19_0_g126858);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126858 = lerpResult22_g126858;
				#else
				float4 staticSwitch24_g126858 = ifLocalVar18_g126858;
				#endif
				float3 ifLocalVar40_g126928 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126928 = (staticSwitch24_g126858).rgb;
				float temp_output_82_0_g126919 = Debug_Layer885_g126363;
				float temp_output_19_0_g126921 = TVE_GlowLayers[(int)temp_output_82_0_g126919];
				half3 Input_Position180_g126922 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126922 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126922).xz ) );
				float temp_output_82_0_g126922 = temp_output_82_0_g126919;
				float2 temp_output_119_0_g126922 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126922).xz ) );
				float temp_output_7_0_g126927 = 1.0;
				float temp_output_10_0_g126927 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126927 );
				float4 lerpResult131_g126922 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_GlowBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126922,temp_output_82_0_g126922), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_GlowNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126922,temp_output_82_0_g126922), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126922 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126927 ) / temp_output_10_0_g126927 ) ));
				float4 temp_output_17_0_g126921 = lerpResult131_g126922;
				float4 temp_output_3_0_g126921 = TVE_GlowParams;
				float4 ifLocalVar18_g126921 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126921 >= 0.5 )
				ifLocalVar18_g126921 = temp_output_17_0_g126921;
				else
				ifLocalVar18_g126921 = temp_output_3_0_g126921;
				float4 lerpResult22_g126921 = lerp( temp_output_3_0_g126921 , temp_output_17_0_g126921 , temp_output_19_0_g126921);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126921 = lerpResult22_g126921;
				#else
				float4 staticSwitch24_g126921 = ifLocalVar18_g126921;
				#endif
				float ifLocalVar40_g126929 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126929 = saturate( (staticSwitch24_g126921).a );
				float temp_output_132_0_g126892 = Debug_Layer885_g126363;
				float temp_output_19_0_g126894 = TVE_AtmoLayers[(int)temp_output_132_0_g126892];
				half3 Input_Position180_g126895 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126895 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126895).xz ) );
				float temp_output_82_0_g126895 = temp_output_132_0_g126892;
				float2 temp_output_119_0_g126895 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126895).xz ) );
				float temp_output_7_0_g126900 = 1.0;
				float temp_output_10_0_g126900 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126900 );
				float4 lerpResult131_g126895 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126895,temp_output_82_0_g126895), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126895,temp_output_82_0_g126895), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126895 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126900 ) / temp_output_10_0_g126900 ) ));
				float4 temp_output_17_0_g126894 = lerpResult131_g126895;
				float4 temp_output_3_0_g126894 = TVE_AtmoParams;
				float4 ifLocalVar18_g126894 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126894 >= 0.5 )
				ifLocalVar18_g126894 = temp_output_17_0_g126894;
				else
				ifLocalVar18_g126894 = temp_output_3_0_g126894;
				float4 lerpResult22_g126894 = lerp( temp_output_3_0_g126894 , temp_output_17_0_g126894 , temp_output_19_0_g126894);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126894 = lerpResult22_g126894;
				#else
				float4 staticSwitch24_g126894 = ifLocalVar18_g126894;
				#endif
				float ifLocalVar40_g126930 = 0;
				if( Debug_Index464_g126363 == 7.0 )
				ifLocalVar40_g126930 = (staticSwitch24_g126894).r;
				float temp_output_132_0_g126865 = Debug_Layer885_g126363;
				float temp_output_19_0_g126867 = TVE_AtmoLayers[(int)temp_output_132_0_g126865];
				half3 Input_Position180_g126868 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126868 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126868).xz ) );
				float temp_output_82_0_g126868 = temp_output_132_0_g126865;
				float2 temp_output_119_0_g126868 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126868).xz ) );
				float temp_output_7_0_g126873 = 1.0;
				float temp_output_10_0_g126873 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126873 );
				float4 lerpResult131_g126868 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126868,temp_output_82_0_g126868), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126868,temp_output_82_0_g126868), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126868 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126873 ) / temp_output_10_0_g126873 ) ));
				float4 temp_output_17_0_g126867 = lerpResult131_g126868;
				float4 temp_output_3_0_g126867 = TVE_AtmoParams;
				float4 ifLocalVar18_g126867 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126867 >= 0.5 )
				ifLocalVar18_g126867 = temp_output_17_0_g126867;
				else
				ifLocalVar18_g126867 = temp_output_3_0_g126867;
				float4 lerpResult22_g126867 = lerp( temp_output_3_0_g126867 , temp_output_17_0_g126867 , temp_output_19_0_g126867);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126867 = lerpResult22_g126867;
				#else
				float4 staticSwitch24_g126867 = ifLocalVar18_g126867;
				#endif
				float ifLocalVar40_g126931 = 0;
				if( Debug_Index464_g126363 == 8.0 )
				ifLocalVar40_g126931 = (staticSwitch24_g126867).g;
				float temp_output_132_0_g126874 = Debug_Layer885_g126363;
				float temp_output_19_0_g126876 = TVE_AtmoLayers[(int)temp_output_132_0_g126874];
				half3 Input_Position180_g126877 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126877 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126877).xz ) );
				float temp_output_82_0_g126877 = temp_output_132_0_g126874;
				float2 temp_output_119_0_g126877 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126877).xz ) );
				float temp_output_7_0_g126882 = 1.0;
				float temp_output_10_0_g126882 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126882 );
				float4 lerpResult131_g126877 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126877,temp_output_82_0_g126877), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126877,temp_output_82_0_g126877), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126877 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126882 ) / temp_output_10_0_g126882 ) ));
				float4 temp_output_17_0_g126876 = lerpResult131_g126877;
				float4 temp_output_3_0_g126876 = TVE_AtmoParams;
				float4 ifLocalVar18_g126876 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126876 >= 0.5 )
				ifLocalVar18_g126876 = temp_output_17_0_g126876;
				else
				ifLocalVar18_g126876 = temp_output_3_0_g126876;
				float4 lerpResult22_g126876 = lerp( temp_output_3_0_g126876 , temp_output_17_0_g126876 , temp_output_19_0_g126876);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126876 = lerpResult22_g126876;
				#else
				float4 staticSwitch24_g126876 = ifLocalVar18_g126876;
				#endif
				float ifLocalVar40_g126932 = 0;
				if( Debug_Index464_g126363 == 9.0 )
				ifLocalVar40_g126932 = (staticSwitch24_g126876).b;
				float temp_output_132_0_g126883 = Debug_Layer885_g126363;
				float temp_output_19_0_g126885 = TVE_AtmoLayers[(int)temp_output_132_0_g126883];
				half3 Input_Position180_g126886 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126886 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126886).xz ) );
				float temp_output_82_0_g126886 = temp_output_132_0_g126883;
				float2 temp_output_119_0_g126886 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126886).xz ) );
				float temp_output_7_0_g126891 = 1.0;
				float temp_output_10_0_g126891 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126891 );
				float4 lerpResult131_g126886 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126886,temp_output_82_0_g126886), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126886,temp_output_82_0_g126886), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126886 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126891 ) / temp_output_10_0_g126891 ) ));
				float4 temp_output_17_0_g126885 = lerpResult131_g126886;
				float4 temp_output_3_0_g126885 = TVE_AtmoParams;
				float4 ifLocalVar18_g126885 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126885 >= 0.5 )
				ifLocalVar18_g126885 = temp_output_17_0_g126885;
				else
				ifLocalVar18_g126885 = temp_output_3_0_g126885;
				float4 lerpResult22_g126885 = lerp( temp_output_3_0_g126885 , temp_output_17_0_g126885 , temp_output_19_0_g126885);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126885 = lerpResult22_g126885;
				#else
				float4 staticSwitch24_g126885 = ifLocalVar18_g126885;
				#endif
				float ifLocalVar40_g126933 = 0;
				if( Debug_Index464_g126363 == 10.0 )
				ifLocalVar40_g126933 = saturate( (staticSwitch24_g126885).a );
				float temp_output_130_0_g126910 = Debug_Layer885_g126363;
				float temp_output_19_0_g126912 = TVE_FormLayers[(int)temp_output_130_0_g126910];
				half3 Input_Position180_g126913 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126913 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126913).xz ) );
				float temp_output_82_0_g126913 = temp_output_130_0_g126910;
				float2 temp_output_119_0_g126913 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126913).xz ) );
				float temp_output_7_0_g126918 = 1.0;
				float temp_output_10_0_g126918 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126918 );
				float4 lerpResult131_g126913 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126913,temp_output_82_0_g126913), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126913,temp_output_82_0_g126913), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126913 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126918 ) / temp_output_10_0_g126918 ) ));
				float4 temp_output_17_0_g126912 = lerpResult131_g126913;
				float4 temp_output_3_0_g126912 = TVE_FormParams;
				float4 ifLocalVar18_g126912 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126912 >= 0.5 )
				ifLocalVar18_g126912 = temp_output_17_0_g126912;
				else
				ifLocalVar18_g126912 = temp_output_3_0_g126912;
				float4 lerpResult22_g126912 = lerp( temp_output_3_0_g126912 , temp_output_17_0_g126912 , temp_output_19_0_g126912);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126912 = lerpResult22_g126912;
				#else
				float4 staticSwitch24_g126912 = ifLocalVar18_g126912;
				#endif
				float3 appendResult1013_g126363 = (float3((staticSwitch24_g126912).rg , 0.0));
				float3 ifLocalVar40_g126934 = 0;
				if( Debug_Index464_g126363 == 11.0 )
				ifLocalVar40_g126934 = appendResult1013_g126363;
				float temp_output_130_0_g126901 = Debug_Layer885_g126363;
				float temp_output_19_0_g126903 = TVE_FormLayers[(int)temp_output_130_0_g126901];
				half3 Input_Position180_g126904 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126904 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126904).xz ) );
				float temp_output_82_0_g126904 = temp_output_130_0_g126901;
				float2 temp_output_119_0_g126904 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126904).xz ) );
				float temp_output_7_0_g126909 = 1.0;
				float temp_output_10_0_g126909 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126909 );
				float4 lerpResult131_g126904 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126904,temp_output_82_0_g126904), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126904,temp_output_82_0_g126904), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126904 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126909 ) / temp_output_10_0_g126909 ) ));
				float4 temp_output_17_0_g126903 = lerpResult131_g126904;
				float4 temp_output_3_0_g126903 = TVE_FormParams;
				float4 ifLocalVar18_g126903 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126903 >= 0.5 )
				ifLocalVar18_g126903 = temp_output_17_0_g126903;
				else
				ifLocalVar18_g126903 = temp_output_3_0_g126903;
				float4 lerpResult22_g126903 = lerp( temp_output_3_0_g126903 , temp_output_17_0_g126903 , temp_output_19_0_g126903);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126903 = lerpResult22_g126903;
				#else
				float4 staticSwitch24_g126903 = ifLocalVar18_g126903;
				#endif
				float ifLocalVar40_g126935 = 0;
				if( Debug_Index464_g126363 == 12.0 )
				ifLocalVar40_g126935 = saturate( (staticSwitch24_g126903).b );
				float temp_output_130_0_g126958 = Debug_Layer885_g126363;
				float temp_output_19_0_g126960 = TVE_FormLayers[(int)temp_output_130_0_g126958];
				half3 Input_Position180_g126961 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126961 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126961).xz ) );
				float temp_output_82_0_g126961 = temp_output_130_0_g126958;
				float2 temp_output_119_0_g126961 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126961).xz ) );
				float temp_output_7_0_g126966 = 1.0;
				float temp_output_10_0_g126966 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126966 );
				float4 lerpResult131_g126961 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126961,temp_output_82_0_g126961), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126961,temp_output_82_0_g126961), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126961 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126966 ) / temp_output_10_0_g126966 ) ));
				float4 temp_output_17_0_g126960 = lerpResult131_g126961;
				float4 temp_output_3_0_g126960 = TVE_FormParams;
				float4 ifLocalVar18_g126960 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126960 >= 0.5 )
				ifLocalVar18_g126960 = temp_output_17_0_g126960;
				else
				ifLocalVar18_g126960 = temp_output_3_0_g126960;
				float4 lerpResult22_g126960 = lerp( temp_output_3_0_g126960 , temp_output_17_0_g126960 , temp_output_19_0_g126960);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126960 = lerpResult22_g126960;
				#else
				float4 staticSwitch24_g126960 = ifLocalVar18_g126960;
				#endif
				float ifLocalVar40_g126967 = 0;
				if( Debug_Index464_g126363 == 13.0 )
				ifLocalVar40_g126967 = saturate( (staticSwitch24_g126960).a );
				float temp_output_136_0_g126936 = Debug_Layer885_g126363;
				float temp_output_19_0_g126938 = TVE_WindLayers[(int)temp_output_136_0_g126936];
				half3 Input_Position180_g126939 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126939 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126939).xz ) );
				float temp_output_82_0_g126939 = temp_output_136_0_g126936;
				float2 temp_output_119_0_g126939 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126939).xz ) );
				float temp_output_7_0_g126944 = 1.0;
				float temp_output_10_0_g126944 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126944 );
				float4 lerpResult131_g126939 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126939,temp_output_82_0_g126939), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126939,temp_output_82_0_g126939), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126939 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126944 ) / temp_output_10_0_g126944 ) ));
				float4 temp_output_17_0_g126938 = lerpResult131_g126939;
				float4 temp_output_3_0_g126938 = TVE_WindParams;
				float4 ifLocalVar18_g126938 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126938 >= 0.5 )
				ifLocalVar18_g126938 = temp_output_17_0_g126938;
				else
				ifLocalVar18_g126938 = temp_output_3_0_g126938;
				float4 lerpResult22_g126938 = lerp( temp_output_3_0_g126938 , temp_output_17_0_g126938 , temp_output_19_0_g126938);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126938 = lerpResult22_g126938;
				#else
				float4 staticSwitch24_g126938 = ifLocalVar18_g126938;
				#endif
				float3 appendResult1012_g126363 = (float3((staticSwitch24_g126938).rg , 0.0));
				float3 ifLocalVar40_g126954 = 0;
				if( Debug_Index464_g126363 == 14.0 )
				ifLocalVar40_g126954 = appendResult1012_g126363;
				float temp_output_136_0_g126945 = Debug_Layer885_g126363;
				float temp_output_19_0_g126947 = TVE_WindLayers[(int)temp_output_136_0_g126945];
				half3 Input_Position180_g126948 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126948 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126948).xz ) );
				float temp_output_82_0_g126948 = temp_output_136_0_g126945;
				float2 temp_output_119_0_g126948 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126948).xz ) );
				float temp_output_7_0_g126953 = 1.0;
				float temp_output_10_0_g126953 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126953 );
				float4 lerpResult131_g126948 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126948,temp_output_82_0_g126948), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126948,temp_output_82_0_g126948), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126948 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126953 ) / temp_output_10_0_g126953 ) ));
				float4 temp_output_17_0_g126947 = lerpResult131_g126948;
				float4 temp_output_3_0_g126947 = TVE_WindParams;
				float4 ifLocalVar18_g126947 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126947 >= 0.5 )
				ifLocalVar18_g126947 = temp_output_17_0_g126947;
				else
				ifLocalVar18_g126947 = temp_output_3_0_g126947;
				float4 lerpResult22_g126947 = lerp( temp_output_3_0_g126947 , temp_output_17_0_g126947 , temp_output_19_0_g126947);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126947 = lerpResult22_g126947;
				#else
				float4 staticSwitch24_g126947 = ifLocalVar18_g126947;
				#endif
				float ifLocalVar40_g126955 = 0;
				if( Debug_Index464_g126363 == 15.0 )
				ifLocalVar40_g126955 = (staticSwitch24_g126947).b;
				float temp_output_136_0_g126970 = Debug_Layer885_g126363;
				float temp_output_19_0_g126971 = TVE_PushLayers[(int)temp_output_136_0_g126970];
				half3 Input_Position180_g126972 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126972 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126972).xz ) );
				float temp_output_82_0_g126972 = temp_output_136_0_g126970;
				float2 temp_output_119_0_g126972 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126972).xz ) );
				float temp_output_7_0_g126977 = 1.0;
				float temp_output_10_0_g126977 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126977 );
				float4 lerpResult131_g126972 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126972,temp_output_82_0_g126972), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126972,temp_output_82_0_g126972), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126972 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126977 ) / temp_output_10_0_g126977 ) ));
				float4 temp_output_17_0_g126971 = lerpResult131_g126972;
				float4 temp_output_3_0_g126971 = TVE_PushParams;
				float4 ifLocalVar18_g126971 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126971 >= 0.5 )
				ifLocalVar18_g126971 = temp_output_17_0_g126971;
				else
				ifLocalVar18_g126971 = temp_output_3_0_g126971;
				float4 lerpResult22_g126971 = lerp( temp_output_3_0_g126971 , temp_output_17_0_g126971 , temp_output_19_0_g126971);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126971 = lerpResult22_g126971;
				#else
				float4 staticSwitch24_g126971 = ifLocalVar18_g126971;
				#endif
				float3 appendResult1780_g126363 = (float3((staticSwitch24_g126971).rg , 0.0));
				float3 ifLocalVar40_g126956 = 0;
				if( Debug_Index464_g126363 == 16.0 )
				ifLocalVar40_g126956 = appendResult1780_g126363;
				float temp_output_136_0_g126979 = Debug_Layer885_g126363;
				float temp_output_19_0_g126980 = TVE_PushLayers[(int)temp_output_136_0_g126979];
				half3 Input_Position180_g126981 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126981 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126981).xz ) );
				float temp_output_82_0_g126981 = temp_output_136_0_g126979;
				float2 temp_output_119_0_g126981 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126981).xz ) );
				float temp_output_7_0_g126986 = 1.0;
				float temp_output_10_0_g126986 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126986 );
				float4 lerpResult131_g126981 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126981,temp_output_82_0_g126981), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126981,temp_output_82_0_g126981), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126981 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126986 ) / temp_output_10_0_g126986 ) ));
				float4 temp_output_17_0_g126980 = lerpResult131_g126981;
				float4 temp_output_3_0_g126980 = TVE_PushParams;
				float4 ifLocalVar18_g126980 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126980 >= 0.5 )
				ifLocalVar18_g126980 = temp_output_17_0_g126980;
				else
				ifLocalVar18_g126980 = temp_output_3_0_g126980;
				float4 lerpResult22_g126980 = lerp( temp_output_3_0_g126980 , temp_output_17_0_g126980 , temp_output_19_0_g126980);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126980 = lerpResult22_g126980;
				#else
				float4 staticSwitch24_g126980 = ifLocalVar18_g126980;
				#endif
				float ifLocalVar40_g126957 = 0;
				if( Debug_Index464_g126363 == 17.0 )
				ifLocalVar40_g126957 = (staticSwitch24_g126980).b;
				float temp_output_136_0_g126988 = Debug_Layer885_g126363;
				float temp_output_19_0_g126989 = TVE_PushLayers[(int)temp_output_136_0_g126988];
				half3 Input_Position180_g126990 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126990 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126990).xz ) );
				float temp_output_82_0_g126990 = temp_output_136_0_g126988;
				float2 temp_output_119_0_g126990 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126990).xz ) );
				float temp_output_7_0_g126995 = 1.0;
				float temp_output_10_0_g126995 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126995 );
				float4 lerpResult131_g126990 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126990,temp_output_82_0_g126990), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126990,temp_output_82_0_g126990), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126990 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126995 ) / temp_output_10_0_g126995 ) ));
				float4 temp_output_17_0_g126989 = lerpResult131_g126990;
				float4 temp_output_3_0_g126989 = TVE_PushParams;
				float4 ifLocalVar18_g126989 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126989 >= 0.5 )
				ifLocalVar18_g126989 = temp_output_17_0_g126989;
				else
				ifLocalVar18_g126989 = temp_output_3_0_g126989;
				float4 lerpResult22_g126989 = lerp( temp_output_3_0_g126989 , temp_output_17_0_g126989 , temp_output_19_0_g126989);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126989 = lerpResult22_g126989;
				#else
				float4 staticSwitch24_g126989 = ifLocalVar18_g126989;
				#endif
				float ifLocalVar40_g126968 = 0;
				if( Debug_Index464_g126363 == 18.0 )
				ifLocalVar40_g126968 = saturate( (staticSwitch24_g126989).a );
				float temp_output_7_0_g126969 = Debug_Min721_g126363;
				float3 temp_cast_61 = (temp_output_7_0_g126969).xxx;
				float temp_output_10_0_g126969 = ( Debug_Max723_g126363 - temp_output_7_0_g126969 );
				float4 appendResult1659_g126363 = (float4(saturate( ( ( ( ifLocalVar40_g126997 + ( ifLocalVar40_g126834 + ifLocalVar40_g126835 ) + ( ifLocalVar40_g126854 + ifLocalVar40_g126855 ) + ( ifLocalVar40_g126928 + ifLocalVar40_g126929 ) + ( ifLocalVar40_g126930 + ifLocalVar40_g126931 + ifLocalVar40_g126932 + ifLocalVar40_g126933 ) + ( ifLocalVar40_g126934 + ifLocalVar40_g126935 + ifLocalVar40_g126967 ) + ( ifLocalVar40_g126954 + ifLocalVar40_g126955 + ifLocalVar40_g126956 + ifLocalVar40_g126957 + ifLocalVar40_g126968 ) ) - temp_cast_61 ) / ( temp_output_10_0_g126969 + 0.0001 ) ) ) , 1.0));
				float4 Output_Globals888_g126363 = appendResult1659_g126363;
				float4 ifLocalVar40_g126435 = 0;
				if( Debug_Type367_g126363 == 9.0 )
				ifLocalVar40_g126435 = Output_Globals888_g126363;
				float3 vertexToFrag328_g126363 = IN.ase_texcoord11.xyz;
				float4 color1016_g126363 = IsGammaSpace() ? float4(0.5831653,0.6037736,0.2135992,0) : float4(0.2992498,0.3229691,0.03750122,0);
				float4 color1017_g126363 = IsGammaSpace() ? float4(0.8117647,0.3488252,0.2627451,0) : float4(0.6239604,0.0997834,0.05612849,0);
				float4 switchResult1015_g126363 = (((ase_vface>0)?(color1016_g126363):(color1017_g126363)));
				float3 ifLocalVar40_g126367 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126367 = (switchResult1015_g126363).rgb;
				float temp_output_7_0_g126425 = Debug_Min721_g126363;
				float3 temp_cast_62 = (temp_output_7_0_g126425).xxx;
				float temp_output_10_0_g126425 = ( Debug_Max723_g126363 - temp_output_7_0_g126425 );
				float4 appendResult1658_g126363 = (float4(saturate( ( ( ( vertexToFrag328_g126363 + ifLocalVar40_g126367 ) - temp_cast_62 ) / ( temp_output_10_0_g126425 + 0.0001 ) ) ) , 1.0));
				float4 Output_Mesh316_g126363 = appendResult1658_g126363;
				float4 ifLocalVar40_g126437 = 0;
				if( Debug_Type367_g126363 == 11.0 )
				ifLocalVar40_g126437 = Output_Mesh316_g126363;
				float _IsTVEShader647_g126363 = _IsTVEShader;
				float Debug_Filter322_g126363 = TVE_DEBUG_Filter;
				float lerpResult1524_g126363 = lerp( 1.0 , _IsTVEShader647_g126363 , Debug_Filter322_g126363);
				float4 lerpResult1517_g126363 = lerp( Shading_Inactive1492_g126363 , ( ( ifLocalVar40_g126426 + ifLocalVar40_g126428 + ifLocalVar40_g126429 + ifLocalVar40_g126430 + ifLocalVar40_g126431 ) + ( ifLocalVar40_g126432 + ifLocalVar40_g126433 + ifLocalVar40_g126434 ) + ( ifLocalVar40_g126435 + ifLocalVar40_g126437 ) ) , lerpResult1524_g126363);
				float dotResult1472_g126363 = dot( WorldNormal , worldViewDir );
				float temp_output_1526_0_g126363 = ( 1.0 - saturate( dotResult1472_g126363 ) );
				float Shading_Fresnel1469_g126363 = (( 1.0 - ( temp_output_1526_0_g126363 * temp_output_1526_0_g126363 ) )*0.3 + 0.7);
				float Debug_Shading1653_g126363 = TVE_DEBUG_Shading;
				float lerpResult1655_g126363 = lerp( 1.0 , Shading_Fresnel1469_g126363 , Debug_Shading1653_g126363);
				float Debug_Clip623_g126363 = TVE_DEBUG_Clip;
				float lerpResult622_g126363 = lerp( 1.0 , SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, uv_MainAlbedoTex ).a , ( Debug_Clip623_g126363 * _RenderClip ));
				clip( lerpResult622_g126363 - _MainAlphaClipValue);
				clip( ( 1.0 - saturate( ( _IsElementShader + _IsHelperShader ) ) ) - 1.0);
				
				o.Albedo = fixed3( 0.5, 0.5, 0.5 );
				o.Normal = fixed3( 0, 0, 1 );
				o.Emission = ( lerpResult1517_g126363 * lerpResult1655_g126363 ).rgb;
				#if defined(_SPECULAR_SETUP)
					o.Specular = fixed3( 0, 0, 0 );
				#else
					o.Metallic = 0;
				#endif
				o.Smoothness = 0;
				o.Occlusion = 1;
				o.Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;

				#ifdef _ALPHATEST_ON
					clip( o.Alpha - AlphaClipThreshold );
				#endif

				#ifdef _DEPTHOFFSET_ON
					outputDepth = IN.pos.z;
				#endif

				#ifndef USING_DIRECTIONAL_LIGHT
					fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				#else
					fixed3 lightDir = _WorldSpaceLightPos0.xyz;
				#endif

				fixed4 c = 0;
				float3 worldN;
				worldN.x = dot(IN.tSpace0.xyz, o.Normal);
				worldN.y = dot(IN.tSpace1.xyz, o.Normal);
				worldN.z = dot(IN.tSpace2.xyz, o.Normal);
				worldN = normalize(worldN);
				o.Normal = worldN;

				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
				gi.indirect.diffuse = 0;
				gi.indirect.specular = 0;
				gi.light.color = _LightColor0.rgb;
				gi.light.dir = lightDir;

				UnityGIInput giInput;
				UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
				giInput.light = gi.light;
				giInput.worldPos = worldPos;
				giInput.worldViewDir = worldViewDir;
				giInput.atten = atten;
				#if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
					giInput.lightmapUV = IN.lmap;
				#else
					giInput.lightmapUV = 0.0;
				#endif
				#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
					giInput.ambient = IN.sh;
				#else
					giInput.ambient.rgb = 0.0;
				#endif
				giInput.probeHDR[0] = unity_SpecCube0_HDR;
				giInput.probeHDR[1] = unity_SpecCube1_HDR;
				#if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
					giInput.boxMin[0] = unity_SpecCube0_BoxMin;
				#endif
				#ifdef UNITY_SPECCUBE_BOX_PROJECTION
					giInput.boxMax[0] = unity_SpecCube0_BoxMax;
					giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
					giInput.boxMax[1] = unity_SpecCube1_BoxMax;
					giInput.boxMin[1] = unity_SpecCube1_BoxMin;
					giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
				#endif

				#if defined(_SPECULAR_SETUP)
					LightingStandardSpecular_GI(o, giInput, gi);
				#else
					LightingStandard_GI( o, giInput, gi );
				#endif

				#ifdef ASE_BAKEDGI
					gi.indirect.diffuse = BakedGI;
				#endif

				#if UNITY_SHOULD_SAMPLE_SH && !defined(LIGHTMAP_ON) && defined(ASE_NO_AMBIENT)
					gi.indirect.diffuse = 0;
				#endif

				#if defined(_SPECULAR_SETUP)
					c += LightingStandardSpecular (o, worldViewDir, gi);
				#else
					c += LightingStandard( o, worldViewDir, gi );
				#endif

				#ifdef ASE_TRANSMISSION
				{
					float shadow = _TransmissionShadow;
					#ifdef DIRECTIONAL
						float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
					#else
						float3 lightAtten = gi.light.color;
					#endif
					half3 transmission = max(0 , -dot(o.Normal, gi.light.dir)) * lightAtten * Transmission;
					c.rgb += o.Albedo * transmission;
				}
				#endif

				#ifdef ASE_TRANSLUCENCY
				{
					float shadow = _TransShadow;
					float normal = _TransNormal;
					float scattering = _TransScattering;
					float direct = _TransDirect;
					float ambient = _TransAmbient;
					float strength = _TransStrength;

					#ifdef DIRECTIONAL
						float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
					#else
						float3 lightAtten = gi.light.color;
					#endif
					half3 lightDir = gi.light.dir + o.Normal * normal;
					half transVdotL = pow( saturate( dot( worldViewDir, -lightDir ) ), scattering );
					half3 translucency = lightAtten * (transVdotL * direct + gi.indirect.diffuse * ambient) * Translucency;
					c.rgb += o.Albedo * translucency * strength;
				}
				#endif

				//#ifdef ASE_REFRACTION
				//	float4 projScreenPos = ScreenPos / ScreenPos.w;
				//	float3 refractionOffset = ( RefractionIndex - 1.0 ) * mul( UNITY_MATRIX_V, WorldNormal ).xyz * ( 1.0 - dot( WorldNormal, WorldViewDirection ) );
				//	projScreenPos.xy += refractionOffset.xy;
				//	float3 refraction = UNITY_SAMPLE_SCREENSPACE_TEXTURE( _GrabTexture, projScreenPos ) * RefractionColor;
				//	color.rgb = lerp( refraction, color.rgb, color.a );
				//	color.a = 1;
				//#endif

				c.rgb += o.Emission;

				#ifdef ASE_FOG
					UNITY_APPLY_FOG(IN.fogCoord, c);
				#endif
				return c;
			}
			ENDCG
		}

		
		Pass
		{
			
			Name "Deferred"
			Tags { "LightMode"="Deferred" }

			AlphaToMask Off

			CGPROGRAM
			#define ASE_NO_AMBIENT 1
			#define ASE_USING_SAMPLING_MACROS 1

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#pragma multi_compile_prepassfinal
			#ifndef UNITY_PASS_DEFERRED
				#define UNITY_PASS_DEFERRED
			#endif
			#include "HLSLSupport.cginc"
			#if !defined( UNITY_INSTANCED_LOD_FADE )
				#define UNITY_INSTANCED_LOD_FADE
			#endif
			#if !defined( UNITY_INSTANCED_SH )
				#define UNITY_INSTANCED_SH
			#endif
			#if !defined( UNITY_INSTANCED_LIGHTMAPSTS )
				#define UNITY_INSTANCED_LIGHTMAPSTS
			#endif
			#include "UnityShaderVariables.cginc"
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"

			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_VERT_TANGENT
			#define ASE_NEEDS_FRAG_WORLD_NORMAL
			#define ASE_NEEDS_FRAG_WORLD_VIEW_DIR
			#pragma shader_feature_local_vertex TVE_MOTION_WIND_ELEMENT
			#pragma shader_feature_local_vertex TVE_MOTION_WIND_OFF TVE_MOTION_WIND_OPTIMIZED TVE_MOTION_WIND_ADVANCED
			#pragma shader_feature_local TVE_LEGACY
			#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
			#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
			#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
			#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
			#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
			#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
			#else//ASE Sampling Macros
			#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
			#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
			#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
			#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
			#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplertex,coord,lod) tex2DArraylod(tex, float4(coord,lod))
			#endif//ASE Sampling Macros
			

			struct appdata {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				#if UNITY_VERSION >= 201810
					UNITY_POSITION(pos);
				#else
					float4 pos : SV_POSITION;
				#endif
				float4 lmap : TEXCOORD2;
				#ifndef LIGHTMAP_ON
					#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
						half3 sh : TEXCOORD3;
					#endif
				#else
					#ifdef DIRLIGHTMAP_OFF
						float4 lmapFadePos : TEXCOORD4;
					#endif
				#endif
				float4 tSpace0 : TEXCOORD5;
				float4 tSpace1 : TEXCOORD6;
				float4 tSpace2 : TEXCOORD7;
				float4 ase_texcoord8 : TEXCOORD8;
				float4 ase_texcoord9 : TEXCOORD9;
				float4 ase_texcoord10 : TEXCOORD10;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#ifdef LIGHTMAP_ON
			float4 unity_LightmapFade;
			#endif
			fixed4 unity_Ambient;
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			uniform half _Banner;
			uniform half _Message;
			uniform float _IsSimpleShader;
			uniform float _IsVertexShader;
			uniform half _IsTVEShader;
			uniform half TVE_DEBUG_Type;
			uniform float _IsCoreShader;
			uniform float _IsBlanketShader;
			uniform float _IsImpostorShader;
			uniform float _IsPolygonalShader;
			uniform float _IsLiteShader;
			uniform float _IsStandardShader;
			uniform float _IsSubsurfaceShader;
			uniform float _IsCustomShader;
			uniform float _IsIdentifier;
			uniform half TVE_DEBUG_Index;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_MainAlbedoTex);
			uniform half4 _main_coord_value;
			SamplerState sampler_MainAlbedoTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_MainNormalTex);
			SamplerState sampler_MainNormalTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_MainMaskTex);
			SamplerState sampler_MainMaskTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondAlbedoTex);
			uniform half _DetailCoordMode;
			uniform half4 _SecondUVs;
			SamplerState sampler_SecondAlbedoTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondNormalTex);
			SamplerState sampler_SecondNormalTex;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondMaskTex);
			SamplerState sampler_SecondMaskTex;
			uniform float _DetailMode;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveTex);
			uniform half4 _EmissiveUVs;
			SamplerState sampler_EmissiveTex;
			uniform float4 _EmissiveColor;
			uniform float _EmissiveCat;
			uniform half TVE_DEBUG_Min;
			uniform half TVE_DEBUG_Max;
			float4 _MainAlbedoTex_TexelSize;
			float4 _MainNormalTex_TexelSize;
			float4 _MainMaskTex_TexelSize;
			float4 _SecondAlbedoTex_TexelSize;
			float4 _SecondMaskTex_TexelSize;
			float4 _EmissiveTex_TexelSize;
			uniform float4 _MainAlbedoTex_ST;
			UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_DEBUG_MipTex);
			SamplerState samplerTVE_DEBUG_MipTex;
			uniform float4 _MainNormalTex_ST;
			uniform float4 _MainMaskTex_ST;
			uniform float4 _SecondAlbedoTex_ST;
			uniform float4 _SecondMaskTex_ST;
			uniform float4 _EmissiveTex_ST;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_Sampler6891741_g126363);
			SamplerState sampler_Linear_Repeat;
			uniform half4 TVE_MotionParams;
			uniform half TVE_IsEnabled;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_MotionNoiseTex);
			uniform half4 TVE_TimeParams;
			uniform half TVE_DEBUG_Layer;
			uniform float TVE_WindLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_WindBaseTex);
			uniform half4 TVE_RenderBaseCoords;
			SamplerState sampler_Linear_Clamp;
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_WindNearTex);
			uniform half4 TVE_RenderNearCoords;
			uniform float4 TVE_RenderNearPositionR;
			uniform half TVE_RenderNearFadeValue;
			uniform half4 TVE_WindParams;
			uniform float TVE_CoatLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_CoatBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_CoatNearTex);
			uniform half4 TVE_CoatParams;
			uniform float TVE_PaintLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_PaintBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_PaintNearTex);
			uniform half4 TVE_PaintParams;
			uniform float TVE_GlowLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_GlowBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_GlowNearTex);
			uniform half4 TVE_GlowParams;
			uniform float TVE_AtmoLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_AtmoBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_AtmoNearTex);
			uniform half4 TVE_AtmoParams;
			uniform float TVE_FormLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_FormBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_FormNearTex);
			uniform half4 TVE_FormParams;
			uniform float TVE_PushLayers[10];
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_PushBaseTex);
			UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(TVE_PushNearTex);
			uniform half4 TVE_PushParams;
			uniform half4 _object_phase_mode;
			uniform half4 _motion_base_vert_mode;
			uniform half _ObjectHeightValue;
			uniform half4 _motion_base_proc_mode;
			uniform half _ObjectRadiusValue;
			uniform half _motion_base_mask_mode;
			uniform half4 _MotionBaseMaskRemap;
			uniform half _MotionBaseMaskMode;
			uniform half4 _motion_small_vert_mode;
			uniform half4 _motion_small_proc_mode;
			uniform half _motion_small_mask_mode;
			uniform half4 _MotionSmallMaskRemap;
			uniform half _MotionSmallMaskMode;
			uniform half4 _motion_tiny_vert_mode;
			uniform half4 _motion_tiny_proc_mode;
			uniform half _motion_tiny_mask_mode;
			uniform half4 _MotionTinyMaskRemap;
			uniform half _MotionTinyMaskMode;
			uniform half TVE_DEBUG_Filter;
			uniform half TVE_DEBUG_Shading;
			uniform half TVE_DEBUG_Clip;
			uniform float _RenderClip;
			uniform float _MainAlphaClipValue;
			uniform float _IsElementShader;
			uniform float _IsHelperShader;


			float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}
			
			float2 DecodeFloatToVector2( float enc )
			{
				float2 result ;
				result.y = enc % 2048;
				result.x = floor(enc / 2048);
				return result / (2048 - 1);
			}
			

			v2f VertexFunction (appdata v  ) {
				UNITY_SETUP_INSTANCE_ID(v);
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o);
				UNITY_TRANSFER_INSTANCE_ID(v,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float Debug_Index464_g126363 = TVE_DEBUG_Index;
				float3 ifLocalVar40_g126364 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126364 = saturate( v.vertex.xyz );
				float3 ifLocalVar40_g126371 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126371 = v.normal;
				float3 ifLocalVar40_g126377 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126377 = v.tangent.xyz;
				float ifLocalVar40_g126369 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126369 = saturate( v.tangent.w );
				float ifLocalVar40_g126413 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126413 = v.ase_color.r;
				float ifLocalVar40_g126414 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126414 = v.ase_color.g;
				float ifLocalVar40_g126415 = 0;
				if( Debug_Index464_g126363 == 7.0 )
				ifLocalVar40_g126415 = v.ase_color.b;
				float ifLocalVar40_g126416 = 0;
				if( Debug_Index464_g126363 == 8.0 )
				ifLocalVar40_g126416 = v.ase_color.a;
				float3 appendResult1147_g126363 = (float3(v.ase_texcoord.x , v.ase_texcoord.y , 0.0));
				float3 ifLocalVar40_g126417 = 0;
				if( Debug_Index464_g126363 == 9.0 )
				ifLocalVar40_g126417 = appendResult1147_g126363;
				float3 appendResult1148_g126363 = (float3(v.texcoord1.xyzw.x , v.texcoord1.xyzw.y , 0.0));
				float3 ifLocalVar40_g126418 = 0;
				if( Debug_Index464_g126363 == 10.0 )
				ifLocalVar40_g126418 = appendResult1148_g126363;
				float3 appendResult1149_g126363 = (float3(v.texcoord2.xyzw.x , v.texcoord2.xyzw.y , 0.0));
				float3 ifLocalVar40_g126440 = 0;
				if( Debug_Index464_g126363 == 11.0 )
				ifLocalVar40_g126440 = appendResult1149_g126363;
				float4 break33_g126438 = _object_phase_mode;
				float temp_output_30_0_g126438 = ( v.ase_color.r * break33_g126438.x );
				float temp_output_29_0_g126438 = ( v.ase_color.g * break33_g126438.y );
				float temp_output_31_0_g126438 = ( v.ase_color.b * break33_g126438.z );
				float temp_output_28_0_g126438 = ( temp_output_30_0_g126438 + temp_output_29_0_g126438 + temp_output_31_0_g126438 + ( v.ase_color.a * break33_g126438.w ) );
				half Motion_PhaseMask1725_g126363 = temp_output_28_0_g126438;
				float3 hsvTorgb260_g126363 = HSVToRGB( float3(Motion_PhaseMask1725_g126363,1.0,1.0) );
				float3 gammaToLinear266_g126363 = GammaToLinearSpace( hsvTorgb260_g126363 );
				float3 ifLocalVar40_g126441 = 0;
				if( Debug_Index464_g126363 == 12.0 )
				ifLocalVar40_g126441 = gammaToLinear266_g126363;
				float4 break1821_g126363 = v.ase_color;
				float4 break33_g127001 = _motion_base_vert_mode;
				float temp_output_30_0_g127001 = ( break1821_g126363.r * break33_g127001.x );
				float temp_output_29_0_g127001 = ( break1821_g126363.g * break33_g127001.y );
				float temp_output_31_0_g127001 = ( break1821_g126363.b * break33_g127001.z );
				float temp_output_28_0_g127001 = ( temp_output_30_0_g127001 + temp_output_29_0_g127001 + temp_output_31_0_g127001 + ( break1821_g126363.a * break33_g127001.w ) );
				float temp_output_1824_0_g126363 = temp_output_28_0_g127001;
				half Bounds_Height1700_g126363 = _ObjectHeightValue;
				half Final_HeightMask1815_g126363 = saturate( ( v.vertex.xyz.y / Bounds_Height1700_g126363 ) );
				float4 break33_g127005 = _motion_base_proc_mode;
				float temp_output_30_0_g127005 = ( Final_HeightMask1815_g126363 * break33_g127005.x );
				half Bounds_Radius1702_g126363 = _ObjectRadiusValue;
				half Final_SphereMask1816_g126363 = saturate( ( length( v.vertex.xyz ) / ( Bounds_Height1700_g126363 * Bounds_Radius1702_g126363 ) ) );
				float temp_output_29_0_g127005 = ( Final_SphereMask1816_g126363 * break33_g127005.y );
				float temp_output_1834_0_g126363 = ( temp_output_30_0_g127005 + temp_output_29_0_g127005 );
				float lerpResult1887_g126363 = lerp( temp_output_1824_0_g126363 , temp_output_1834_0_g126363 , _motion_base_mask_mode);
				float clampResult17_g126998 = clamp( lerpResult1887_g126363 , 0.0001 , 0.9999 );
				float temp_output_7_0_g126999 = _MotionBaseMaskRemap.x;
				float temp_output_10_0_g126999 = ( _MotionBaseMaskRemap.y - temp_output_7_0_g126999 );
				float temp_output_6_0_g127000 = saturate( ( ( clampResult17_g126998 - temp_output_7_0_g126999 ) / ( temp_output_10_0_g126999 + 0.0001 ) ) );
				#ifdef TVE_REGISTER
				float staticSwitch14_g127000 = ( temp_output_6_0_g127000 + ( _MotionBaseMaskMode * 0.0 ) );
				#else
				float staticSwitch14_g127000 = temp_output_6_0_g127000;
				#endif
				half Motion_BaseMask1675_g126363 = staticSwitch14_g127000;
				float ifLocalVar40_g126442 = 0;
				if( Debug_Index464_g126363 == 13.0 )
				ifLocalVar40_g126442 = Motion_BaseMask1675_g126363;
				float4 break1855_g126363 = v.ase_color;
				float4 break33_g127002 = _motion_small_vert_mode;
				float temp_output_30_0_g127002 = ( break1855_g126363.r * break33_g127002.x );
				float temp_output_29_0_g127002 = ( break1855_g126363.g * break33_g127002.y );
				float temp_output_31_0_g127002 = ( break1855_g126363.b * break33_g127002.z );
				float temp_output_28_0_g127002 = ( temp_output_30_0_g127002 + temp_output_29_0_g127002 + temp_output_31_0_g127002 + ( break1855_g126363.a * break33_g127002.w ) );
				float temp_output_1845_0_g126363 = temp_output_28_0_g127002;
				float4 break33_g127006 = _motion_small_proc_mode;
				float temp_output_30_0_g127006 = ( Final_HeightMask1815_g126363 * break33_g127006.x );
				float temp_output_29_0_g127006 = ( Final_SphereMask1816_g126363 * break33_g127006.y );
				float temp_output_1849_0_g126363 = ( temp_output_30_0_g127006 + temp_output_29_0_g127006 );
				float lerpResult1889_g126363 = lerp( temp_output_1845_0_g126363 , temp_output_1849_0_g126363 , _motion_small_mask_mode);
				float enc1882_g126363 = v.ase_texcoord.z;
				float2 localDecodeFloatToVector21882_g126363 = DecodeFloatToVector2( enc1882_g126363 );
				float2 break1878_g126363 = localDecodeFloatToVector21882_g126363;
				half Small_Mask_Legacy1879_g126363 = break1878_g126363.x;
				#ifdef TVE_LEGACY
				float staticSwitch1883_g126363 = Small_Mask_Legacy1879_g126363;
				#else
				float staticSwitch1883_g126363 = lerpResult1889_g126363;
				#endif
				float clampResult17_g127008 = clamp( staticSwitch1883_g126363 , 0.0001 , 0.9999 );
				float temp_output_7_0_g127009 = _MotionSmallMaskRemap.x;
				float temp_output_10_0_g127009 = ( _MotionSmallMaskRemap.y - temp_output_7_0_g127009 );
				float temp_output_6_0_g127010 = saturate( ( ( clampResult17_g127008 - temp_output_7_0_g127009 ) / ( temp_output_10_0_g127009 + 0.0001 ) ) );
				#ifdef TVE_REGISTER
				float staticSwitch14_g127010 = ( temp_output_6_0_g127010 + ( _MotionSmallMaskMode * 0.0 ) );
				#else
				float staticSwitch14_g127010 = temp_output_6_0_g127010;
				#endif
				half Motion_SmallMask1693_g126363 = staticSwitch14_g127010;
				float ifLocalVar40_g126443 = 0;
				if( Debug_Index464_g126363 == 14.0 )
				ifLocalVar40_g126443 = Motion_SmallMask1693_g126363;
				float4 break1867_g126363 = v.ase_color;
				float4 break33_g127003 = _motion_tiny_vert_mode;
				float temp_output_30_0_g127003 = ( break1867_g126363.r * break33_g127003.x );
				float temp_output_29_0_g127003 = ( break1867_g126363.g * break33_g127003.y );
				float temp_output_31_0_g127003 = ( break1867_g126363.b * break33_g127003.z );
				float temp_output_28_0_g127003 = ( temp_output_30_0_g127003 + temp_output_29_0_g127003 + temp_output_31_0_g127003 + ( break1867_g126363.a * break33_g127003.w ) );
				float temp_output_1860_0_g126363 = temp_output_28_0_g127003;
				float4 break33_g127007 = _motion_tiny_proc_mode;
				float temp_output_30_0_g127007 = ( Final_HeightMask1815_g126363 * break33_g127007.x );
				float temp_output_29_0_g127007 = ( Final_SphereMask1816_g126363 * break33_g127007.y );
				float temp_output_1864_0_g126363 = ( temp_output_30_0_g127007 + temp_output_29_0_g127007 );
				float lerpResult1891_g126363 = lerp( temp_output_1860_0_g126363 , temp_output_1864_0_g126363 , _motion_tiny_mask_mode);
				half Tiny_Mask_Legacy1880_g126363 = break1878_g126363.y;
				#ifdef TVE_LEGACY
				float staticSwitch1886_g126363 = Tiny_Mask_Legacy1880_g126363;
				#else
				float staticSwitch1886_g126363 = lerpResult1891_g126363;
				#endif
				float clampResult17_g127011 = clamp( staticSwitch1886_g126363 , 0.0001 , 0.9999 );
				float temp_output_7_0_g127012 = _MotionTinyMaskRemap.x;
				float temp_output_10_0_g127012 = ( _MotionTinyMaskRemap.y - temp_output_7_0_g127012 );
				float temp_output_6_0_g127013 = saturate( ( ( clampResult17_g127011 - temp_output_7_0_g127012 ) / ( temp_output_10_0_g127012 + 0.0001 ) ) );
				#ifdef TVE_REGISTER
				float staticSwitch14_g127013 = ( temp_output_6_0_g127013 + ( _MotionTinyMaskMode * 0.0 ) );
				#else
				float staticSwitch14_g127013 = temp_output_6_0_g127013;
				#endif
				half Motion_TinyMask1717_g126363 = staticSwitch14_g127013;
				float ifLocalVar40_g126444 = 0;
				if( Debug_Index464_g126363 == 15.0 )
				ifLocalVar40_g126444 = Motion_TinyMask1717_g126363;
				float3 appendResult60_g126439 = (float3(v.ase_texcoord3.x , 0.0 , v.ase_texcoord3.y));
				float3 ifLocalVar40_g126445 = 0;
				if( Debug_Index464_g126363 == 16.0 )
				ifLocalVar40_g126445 = appendResult60_g126439;
				float3 vertexToFrag328_g126363 = ( ( ifLocalVar40_g126364 + ifLocalVar40_g126371 + ifLocalVar40_g126377 + ifLocalVar40_g126369 ) + ( ifLocalVar40_g126413 + ifLocalVar40_g126414 + ifLocalVar40_g126415 + ifLocalVar40_g126416 ) + ( ifLocalVar40_g126417 + ifLocalVar40_g126418 + ifLocalVar40_g126440 ) + ( ifLocalVar40_g126441 + ifLocalVar40_g126442 + ifLocalVar40_g126443 + ifLocalVar40_g126444 + ifLocalVar40_g126445 ) );
				o.ase_texcoord10.xyz = vertexToFrag328_g126363;
				
				o.ase_texcoord8 = v.ase_texcoord;
				o.ase_texcoord9 = v.texcoord1.xyzw;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord10.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.vertex.w = 1;
				v.normal = v.normal;
				v.tangent = v.tangent;

				o.pos = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
				o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

				#ifdef DYNAMICLIGHTMAP_ON
					o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				#else
					o.lmap.zw = 0;
				#endif
				#ifdef LIGHTMAP_ON
					o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
					#ifdef DIRLIGHTMAP_OFF
						o.lmapFadePos.xyz = (mul(unity_ObjectToWorld, v.vertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w;
						o.lmapFadePos.w = (-UnityObjectToViewPos(v.vertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w);
					#endif
				#else
					o.lmap.xy = 0;
					#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
						o.sh = 0;
						o.sh = ShadeSHPerVertex (worldNormal, o.sh);
					#endif
				#endif
				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( appdata v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.tangent = v.tangent;
				o.normal = v.normal;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				o.ase_texcoord3 = v.ase_texcoord3;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				appdata o = (appdata) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
				o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				o.ase_texcoord3 = patch[0].ase_texcoord3 * bary.x + patch[1].ase_texcoord3 * bary.y + patch[2].ase_texcoord3 * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			v2f vert ( appdata v )
			{
				return VertexFunction( v );
			}
			#endif

			void frag (v2f IN , bool ase_vface : SV_IsFrontFace
				, out half4 outGBuffer0 : SV_Target0
				, out half4 outGBuffer1 : SV_Target1
				, out half4 outGBuffer2 : SV_Target2
				, out half4 outEmission : SV_Target3
				#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
				, out half4 outShadowMask : SV_Target4
				#endif
				#ifdef _DEPTHOFFSET_ON
				, out float outputDepth : SV_Depth
				#endif
			)
			{
				UNITY_SETUP_INSTANCE_ID(IN);

				#ifdef LOD_FADE_CROSSFADE
					UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
				#endif

				#if defined(_SPECULAR_SETUP)
					SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
				#else
					SurfaceOutputStandard o = (SurfaceOutputStandard)0;
				#endif
				float3 WorldTangent = float3(IN.tSpace0.x,IN.tSpace1.x,IN.tSpace2.x);
				float3 WorldBiTangent = float3(IN.tSpace0.y,IN.tSpace1.y,IN.tSpace2.y);
				float3 WorldNormal = float3(IN.tSpace0.z,IN.tSpace1.z,IN.tSpace2.z);
				float3 worldPos = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				half atten = 1;

				float4 color690_g126363 = IsGammaSpace() ? float4(0.1,0.1,0.1,0) : float4(0.01002283,0.01002283,0.01002283,0);
				float4 Shading_Inactive1492_g126363 = color690_g126363;
				float Debug_Type367_g126363 = TVE_DEBUG_Type;
				float4 color646_g126363 = IsGammaSpace() ? float4(0.9245283,0.7969696,0.4142933,1) : float4(0.8368256,0.5987038,0.1431069,1);
				float4 Output_Converted717_g126363 = color646_g126363;
				float4 ifLocalVar40_g126426 = 0;
				if( Debug_Type367_g126363 == 0.0 )
				ifLocalVar40_g126426 = Output_Converted717_g126363;
				float4 color1529_g126363 = IsGammaSpace() ? float4(0.9254902,0.7960784,0.4156863,1) : float4(0.8387991,0.5972018,0.1441285,1);
				float _IsCoreShader1551_g126363 = _IsCoreShader;
				float4 color1539_g126363 = IsGammaSpace() ? float4(0.6196079,0.7686275,0.1490196,0) : float4(0.3419145,0.5520116,0.01938236,0);
				float _IsBlanketShader1554_g126363 = _IsBlanketShader;
				float4 color1542_g126363 = IsGammaSpace() ? float4(0.9716981,0.3162602,0.4816265,0) : float4(0.9368213,0.08154967,0.1974273,0);
				float _IsImpostorShader1110_g126363 = _IsImpostorShader;
				float4 color1544_g126363 = IsGammaSpace() ? float4(0.3254902,0.6117647,0.8117647,0) : float4(0.08650047,0.3324516,0.6239604,0);
				float _IsPolygonalShader1112_g126363 = _IsPolygonalShader;
				float4 color1649_g126363 = IsGammaSpace() ? float4(0.6,0.6,0.6,0) : float4(0.3185468,0.3185468,0.3185468,0);
				float _IsLiteShader1648_g126363 = _IsLiteShader;
				float4 Output_Scope1531_g126363 = ( ( color1529_g126363 * _IsCoreShader1551_g126363 ) + ( color1539_g126363 * _IsBlanketShader1554_g126363 ) + ( color1542_g126363 * _IsImpostorShader1110_g126363 ) + ( color1544_g126363 * _IsPolygonalShader1112_g126363 ) + ( color1649_g126363 * _IsLiteShader1648_g126363 ) );
				float4 ifLocalVar40_g126428 = 0;
				if( Debug_Type367_g126363 == 2.0 )
				ifLocalVar40_g126428 = Output_Scope1531_g126363;
				float4 color529_g126363 = IsGammaSpace() ? float4(0.62,0.77,0.15,0) : float4(0.3423916,0.5542217,0.01960665,0);
				float _IsVertexShader1158_g126363 = _IsVertexShader;
				float4 color544_g126363 = IsGammaSpace() ? float4(0.3252937,0.6122813,0.8113208,0) : float4(0.08639329,0.3330702,0.6231937,0);
				float _IsSimpleShader359_g126363 = _IsSimpleShader;
				float4 color521_g126363 = IsGammaSpace() ? float4(0.6566009,0.3404236,0.8490566,0) : float4(0.3886527,0.09487338,0.6903409,0);
				float _IsStandardShader344_g126363 = _IsStandardShader;
				float4 color1121_g126363 = IsGammaSpace() ? float4(0.9716981,0.88463,0.1787558,0) : float4(0.9368213,0.7573396,0.02686729,0);
				float _IsSubsurfaceShader548_g126363 = _IsSubsurfaceShader;
				float4 Output_Lighting525_g126363 = ( ( color529_g126363 * _IsVertexShader1158_g126363 ) + ( color544_g126363 * _IsSimpleShader359_g126363 ) + ( color521_g126363 * _IsStandardShader344_g126363 ) + ( color1121_g126363 * _IsSubsurfaceShader548_g126363 ) );
				float4 ifLocalVar40_g126429 = 0;
				if( Debug_Type367_g126363 == 3.0 )
				ifLocalVar40_g126429 = Output_Lighting525_g126363;
				float4 color1559_g126363 = IsGammaSpace() ? float4(0.9245283,0.7969696,0.4142933,1) : float4(0.8368256,0.5987038,0.1431069,1);
				float4 color1563_g126363 = IsGammaSpace() ? float4(0.3053578,0.8867924,0.5362216,0) : float4(0.0759199,0.7615293,0.2491121,0);
				float _IsCustomShader1570_g126363 = _IsCustomShader;
				float4 lerpResult1561_g126363 = lerp( color1559_g126363 , color1563_g126363 , _IsCustomShader1570_g126363);
				float4 Output_Custom1560_g126363 = lerpResult1561_g126363;
				float4 ifLocalVar40_g126430 = 0;
				if( Debug_Type367_g126363 == 4.0 )
				ifLocalVar40_g126430 = Output_Custom1560_g126363;
				float3 hsvTorgb1452_g126363 = HSVToRGB( float3(( _IsIdentifier / 1000.0 ),1.0,1.0) );
				float3 gammaToLinear1453_g126363 = GammaToLinearSpace( hsvTorgb1452_g126363 );
				float4 appendResult1657_g126363 = (float4(gammaToLinear1453_g126363 , 1.0));
				float4 Output_Sharing1355_g126363 = appendResult1657_g126363;
				float4 ifLocalVar40_g126431 = 0;
				if( Debug_Type367_g126363 == 5.0 )
				ifLocalVar40_g126431 = Output_Sharing1355_g126363;
				float Debug_Index464_g126363 = TVE_DEBUG_Index;
				half2 Main_UVs1219_g126363 = ( ( IN.ase_texcoord8.xy * (_main_coord_value).xy ) + (_main_coord_value).zw );
				float4 tex2DNode586_g126363 = SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, Main_UVs1219_g126363 );
				float3 appendResult637_g126363 = (float3(tex2DNode586_g126363.r , tex2DNode586_g126363.g , tex2DNode586_g126363.b));
				float3 ifLocalVar40_g126370 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126370 = appendResult637_g126363;
				float ifLocalVar40_g126374 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126374 = SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, Main_UVs1219_g126363 ).a;
				float4 tex2DNode604_g126363 = SAMPLE_TEXTURE2D( _MainNormalTex, sampler_MainNormalTex, Main_UVs1219_g126363 );
				float3 appendResult876_g126363 = (float3(tex2DNode604_g126363.a , tex2DNode604_g126363.g , 1.0));
				float3 gammaToLinear878_g126363 = GammaToLinearSpace( appendResult876_g126363 );
				float3 ifLocalVar40_g126378 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126378 = gammaToLinear878_g126363;
				float ifLocalVar40_g126366 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126366 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, Main_UVs1219_g126363 ).r;
				float ifLocalVar40_g126384 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126384 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, Main_UVs1219_g126363 ).g;
				float ifLocalVar40_g126375 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126375 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, Main_UVs1219_g126363 ).b;
				float ifLocalVar40_g126365 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126365 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, Main_UVs1219_g126363 ).a;
				float2 appendResult1251_g126363 = (float2(IN.ase_texcoord9.z , IN.ase_texcoord9.w));
				float2 Mesh_DetailCoord1254_g126363 = appendResult1251_g126363;
				float2 lerpResult1231_g126363 = lerp( IN.ase_texcoord8.xy , Mesh_DetailCoord1254_g126363 , _DetailCoordMode);
				half2 Second_UVs1234_g126363 = ( ( lerpResult1231_g126363 * (_SecondUVs).xy ) + (_SecondUVs).zw );
				float4 tex2DNode854_g126363 = SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondAlbedoTex, Second_UVs1234_g126363 );
				float3 appendResult839_g126363 = (float3(tex2DNode854_g126363.r , tex2DNode854_g126363.g , tex2DNode854_g126363.b));
				float3 ifLocalVar40_g126368 = 0;
				if( Debug_Index464_g126363 == 7.0 )
				ifLocalVar40_g126368 = appendResult839_g126363;
				float ifLocalVar40_g126376 = 0;
				if( Debug_Index464_g126363 == 8.0 )
				ifLocalVar40_g126376 = SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondAlbedoTex, Second_UVs1234_g126363 ).a;
				float4 tex2DNode841_g126363 = SAMPLE_TEXTURE2D( _SecondNormalTex, sampler_SecondNormalTex, Second_UVs1234_g126363 );
				float3 appendResult880_g126363 = (float3(tex2DNode841_g126363.a , tex2DNode841_g126363.g , 1.0));
				float3 gammaToLinear879_g126363 = GammaToLinearSpace( appendResult880_g126363 );
				float3 ifLocalVar40_g126383 = 0;
				if( Debug_Index464_g126363 == 8.0 )
				ifLocalVar40_g126383 = gammaToLinear879_g126363;
				float ifLocalVar40_g126379 = 0;
				if( Debug_Index464_g126363 == 10.0 )
				ifLocalVar40_g126379 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs1234_g126363 ).r;
				float ifLocalVar40_g126373 = 0;
				if( Debug_Index464_g126363 == 11.0 )
				ifLocalVar40_g126373 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs1234_g126363 ).g;
				float ifLocalVar40_g126381 = 0;
				if( Debug_Index464_g126363 == 12.0 )
				ifLocalVar40_g126381 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs1234_g126363 ).b;
				float ifLocalVar40_g126382 = 0;
				if( Debug_Index464_g126363 == 13.0 )
				ifLocalVar40_g126382 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs1234_g126363 ).a;
				half2 Emissive_UVs1245_g126363 = ( ( IN.ase_texcoord8.xy * (_EmissiveUVs).xy ) + (_EmissiveUVs).zw );
				float4 tex2DNode858_g126363 = SAMPLE_TEXTURE2D( _EmissiveTex, sampler_EmissiveTex, Emissive_UVs1245_g126363 );
				float ifLocalVar40_g126372 = 0;
				if( Debug_Index464_g126363 == 14.0 )
				ifLocalVar40_g126372 = tex2DNode858_g126363.r;
				float Debug_Min721_g126363 = TVE_DEBUG_Min;
				float temp_output_7_0_g126380 = Debug_Min721_g126363;
				float4 temp_cast_3 = (temp_output_7_0_g126380).xxxx;
				float Debug_Max723_g126363 = TVE_DEBUG_Max;
				float temp_output_10_0_g126380 = ( Debug_Max723_g126363 - temp_output_7_0_g126380 );
				float4 Output_Maps561_g126363 = saturate( ( ( ( float4( ( ( ifLocalVar40_g126370 + ifLocalVar40_g126374 + ifLocalVar40_g126378 ) + ( ifLocalVar40_g126366 + ifLocalVar40_g126384 + ifLocalVar40_g126375 + ifLocalVar40_g126365 ) ) , 0.0 ) + float4( ( ( ( ifLocalVar40_g126368 + ifLocalVar40_g126376 + ifLocalVar40_g126383 ) + ( ifLocalVar40_g126379 + ifLocalVar40_g126373 + ifLocalVar40_g126381 + ifLocalVar40_g126382 ) ) * _DetailMode ) , 0.0 ) + ( ( ifLocalVar40_g126372 * _EmissiveColor ) * _EmissiveCat ) ) - temp_cast_3 ) / ( temp_output_10_0_g126380 + 0.0001 ) ) );
				float4 ifLocalVar40_g126432 = 0;
				if( Debug_Type367_g126363 == 6.0 )
				ifLocalVar40_g126432 = Output_Maps561_g126363;
				float Resolution44_g126401 = max( _MainAlbedoTex_TexelSize.z , _MainAlbedoTex_TexelSize.w );
				float4 color62_g126401 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126401 = 0;
				if( Resolution44_g126401 <= 256.0 )
				ifLocalVar61_g126401 = color62_g126401;
				float4 color55_g126401 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126401 = 0;
				if( Resolution44_g126401 == 512.0 )
				ifLocalVar56_g126401 = color55_g126401;
				float4 color42_g126401 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126401 = 0;
				if( Resolution44_g126401 == 1024.0 )
				ifLocalVar40_g126401 = color42_g126401;
				float4 color48_g126401 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126401 = 0;
				if( Resolution44_g126401 == 2048.0 )
				ifLocalVar47_g126401 = color48_g126401;
				float4 color51_g126401 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126401 = 0;
				if( Resolution44_g126401 >= 4096.0 )
				ifLocalVar52_g126401 = color51_g126401;
				float4 ifLocalVar40_g126387 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126387 = ( ifLocalVar61_g126401 + ifLocalVar56_g126401 + ifLocalVar40_g126401 + ifLocalVar47_g126401 + ifLocalVar52_g126401 );
				float Resolution44_g126400 = max( _MainNormalTex_TexelSize.z , _MainNormalTex_TexelSize.w );
				float4 color62_g126400 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126400 = 0;
				if( Resolution44_g126400 <= 256.0 )
				ifLocalVar61_g126400 = color62_g126400;
				float4 color55_g126400 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126400 = 0;
				if( Resolution44_g126400 == 512.0 )
				ifLocalVar56_g126400 = color55_g126400;
				float4 color42_g126400 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126400 = 0;
				if( Resolution44_g126400 == 1024.0 )
				ifLocalVar40_g126400 = color42_g126400;
				float4 color48_g126400 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126400 = 0;
				if( Resolution44_g126400 == 2048.0 )
				ifLocalVar47_g126400 = color48_g126400;
				float4 color51_g126400 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126400 = 0;
				if( Resolution44_g126400 >= 4096.0 )
				ifLocalVar52_g126400 = color51_g126400;
				float4 ifLocalVar40_g126385 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126385 = ( ifLocalVar61_g126400 + ifLocalVar56_g126400 + ifLocalVar40_g126400 + ifLocalVar47_g126400 + ifLocalVar52_g126400 );
				float Resolution44_g126399 = max( _MainMaskTex_TexelSize.z , _MainMaskTex_TexelSize.w );
				float4 color62_g126399 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126399 = 0;
				if( Resolution44_g126399 <= 256.0 )
				ifLocalVar61_g126399 = color62_g126399;
				float4 color55_g126399 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126399 = 0;
				if( Resolution44_g126399 == 512.0 )
				ifLocalVar56_g126399 = color55_g126399;
				float4 color42_g126399 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126399 = 0;
				if( Resolution44_g126399 == 1024.0 )
				ifLocalVar40_g126399 = color42_g126399;
				float4 color48_g126399 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126399 = 0;
				if( Resolution44_g126399 == 2048.0 )
				ifLocalVar47_g126399 = color48_g126399;
				float4 color51_g126399 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126399 = 0;
				if( Resolution44_g126399 >= 4096.0 )
				ifLocalVar52_g126399 = color51_g126399;
				float4 ifLocalVar40_g126386 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126386 = ( ifLocalVar61_g126399 + ifLocalVar56_g126399 + ifLocalVar40_g126399 + ifLocalVar47_g126399 + ifLocalVar52_g126399 );
				float Resolution44_g126406 = max( _SecondAlbedoTex_TexelSize.z , _SecondAlbedoTex_TexelSize.w );
				float4 color62_g126406 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126406 = 0;
				if( Resolution44_g126406 <= 256.0 )
				ifLocalVar61_g126406 = color62_g126406;
				float4 color55_g126406 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126406 = 0;
				if( Resolution44_g126406 == 512.0 )
				ifLocalVar56_g126406 = color55_g126406;
				float4 color42_g126406 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126406 = 0;
				if( Resolution44_g126406 == 1024.0 )
				ifLocalVar40_g126406 = color42_g126406;
				float4 color48_g126406 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126406 = 0;
				if( Resolution44_g126406 == 2048.0 )
				ifLocalVar47_g126406 = color48_g126406;
				float4 color51_g126406 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126406 = 0;
				if( Resolution44_g126406 >= 4096.0 )
				ifLocalVar52_g126406 = color51_g126406;
				float4 ifLocalVar40_g126393 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126393 = ( ifLocalVar61_g126406 + ifLocalVar56_g126406 + ifLocalVar40_g126406 + ifLocalVar47_g126406 + ifLocalVar52_g126406 );
				float Resolution44_g126405 = max( _SecondMaskTex_TexelSize.z , _SecondMaskTex_TexelSize.w );
				float4 color62_g126405 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126405 = 0;
				if( Resolution44_g126405 <= 256.0 )
				ifLocalVar61_g126405 = color62_g126405;
				float4 color55_g126405 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126405 = 0;
				if( Resolution44_g126405 == 512.0 )
				ifLocalVar56_g126405 = color55_g126405;
				float4 color42_g126405 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126405 = 0;
				if( Resolution44_g126405 == 1024.0 )
				ifLocalVar40_g126405 = color42_g126405;
				float4 color48_g126405 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126405 = 0;
				if( Resolution44_g126405 == 2048.0 )
				ifLocalVar47_g126405 = color48_g126405;
				float4 color51_g126405 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126405 = 0;
				if( Resolution44_g126405 >= 4096.0 )
				ifLocalVar52_g126405 = color51_g126405;
				float4 ifLocalVar40_g126391 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126391 = ( ifLocalVar61_g126405 + ifLocalVar56_g126405 + ifLocalVar40_g126405 + ifLocalVar47_g126405 + ifLocalVar52_g126405 );
				float Resolution44_g126407 = max( _SecondAlbedoTex_TexelSize.z , _SecondAlbedoTex_TexelSize.w );
				float4 color62_g126407 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126407 = 0;
				if( Resolution44_g126407 <= 256.0 )
				ifLocalVar61_g126407 = color62_g126407;
				float4 color55_g126407 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126407 = 0;
				if( Resolution44_g126407 == 512.0 )
				ifLocalVar56_g126407 = color55_g126407;
				float4 color42_g126407 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126407 = 0;
				if( Resolution44_g126407 == 1024.0 )
				ifLocalVar40_g126407 = color42_g126407;
				float4 color48_g126407 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126407 = 0;
				if( Resolution44_g126407 == 2048.0 )
				ifLocalVar47_g126407 = color48_g126407;
				float4 color51_g126407 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126407 = 0;
				if( Resolution44_g126407 >= 4096.0 )
				ifLocalVar52_g126407 = color51_g126407;
				float4 ifLocalVar40_g126392 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126392 = ( ifLocalVar61_g126407 + ifLocalVar56_g126407 + ifLocalVar40_g126407 + ifLocalVar47_g126407 + ifLocalVar52_g126407 );
				float Resolution44_g126404 = max( _EmissiveTex_TexelSize.z , _EmissiveTex_TexelSize.w );
				float4 color62_g126404 = IsGammaSpace() ? float4(0.484069,0.862666,0.9245283,0) : float4(0.1995908,0.7155456,0.8368256,0);
				float4 ifLocalVar61_g126404 = 0;
				if( Resolution44_g126404 <= 256.0 )
				ifLocalVar61_g126404 = color62_g126404;
				float4 color55_g126404 = IsGammaSpace() ? float4(0.1933962,0.7383016,1,0) : float4(0.03108436,0.5044825,1,0);
				float4 ifLocalVar56_g126404 = 0;
				if( Resolution44_g126404 == 512.0 )
				ifLocalVar56_g126404 = color55_g126404;
				float4 color42_g126404 = IsGammaSpace() ? float4(0.4431373,0.7921569,0.1764706,0) : float4(0.1651322,0.5906189,0.02624122,0);
				float4 ifLocalVar40_g126404 = 0;
				if( Resolution44_g126404 == 1024.0 )
				ifLocalVar40_g126404 = color42_g126404;
				float4 color48_g126404 = IsGammaSpace() ? float4(1,0.6889491,0.07075471,0) : float4(1,0.4324122,0.006068094,0);
				float4 ifLocalVar47_g126404 = 0;
				if( Resolution44_g126404 == 2048.0 )
				ifLocalVar47_g126404 = color48_g126404;
				float4 color51_g126404 = IsGammaSpace() ? float4(1,0.2066492,0.0990566,0) : float4(1,0.03521443,0.009877041,0);
				float4 ifLocalVar52_g126404 = 0;
				if( Resolution44_g126404 >= 4096.0 )
				ifLocalVar52_g126404 = color51_g126404;
				float4 ifLocalVar40_g126394 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126394 = ( ifLocalVar61_g126404 + ifLocalVar56_g126404 + ifLocalVar40_g126404 + ifLocalVar47_g126404 + ifLocalVar52_g126404 );
				float4 Output_Resolution737_g126363 = ( ( ifLocalVar40_g126387 + ifLocalVar40_g126385 + ifLocalVar40_g126386 ) + ( ifLocalVar40_g126393 + ifLocalVar40_g126391 + ifLocalVar40_g126392 ) + ifLocalVar40_g126394 );
				float4 ifLocalVar40_g126433 = 0;
				if( Debug_Type367_g126363 == 7.0 )
				ifLocalVar40_g126433 = Output_Resolution737_g126363;
				float2 uv_MainAlbedoTex = IN.ase_texcoord8.xy * _MainAlbedoTex_ST.xy + _MainAlbedoTex_ST.zw;
				float2 UVs72_g126412 = Main_UVs1219_g126363;
				float Resolution44_g126412 = max( _MainAlbedoTex_TexelSize.z , _MainAlbedoTex_TexelSize.w );
				float4 tex2DNode77_g126412 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126412 * ( Resolution44_g126412 / 8.0 ) ) );
				float4 lerpResult78_g126412 = lerp( SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, uv_MainAlbedoTex ) , tex2DNode77_g126412 , tex2DNode77_g126412.a);
				float4 ifLocalVar40_g126390 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126390 = lerpResult78_g126412;
				float2 uv_MainNormalTex = IN.ase_texcoord8.xy * _MainNormalTex_ST.xy + _MainNormalTex_ST.zw;
				float2 UVs72_g126403 = Main_UVs1219_g126363;
				float Resolution44_g126403 = max( _MainNormalTex_TexelSize.z , _MainNormalTex_TexelSize.w );
				float4 tex2DNode77_g126403 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126403 * ( Resolution44_g126403 / 8.0 ) ) );
				float4 lerpResult78_g126403 = lerp( SAMPLE_TEXTURE2D( _MainNormalTex, sampler_MainNormalTex, uv_MainNormalTex ) , tex2DNode77_g126403 , tex2DNode77_g126403.a);
				float4 ifLocalVar40_g126388 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126388 = lerpResult78_g126403;
				float2 uv_MainMaskTex = IN.ase_texcoord8.xy * _MainMaskTex_ST.xy + _MainMaskTex_ST.zw;
				float2 UVs72_g126402 = Main_UVs1219_g126363;
				float Resolution44_g126402 = max( _MainMaskTex_TexelSize.z , _MainMaskTex_TexelSize.w );
				float4 tex2DNode77_g126402 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126402 * ( Resolution44_g126402 / 8.0 ) ) );
				float4 lerpResult78_g126402 = lerp( SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainMaskTex, uv_MainMaskTex ) , tex2DNode77_g126402 , tex2DNode77_g126402.a);
				float4 ifLocalVar40_g126389 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126389 = lerpResult78_g126402;
				float2 uv_SecondAlbedoTex = IN.ase_texcoord8.xy * _SecondAlbedoTex_ST.xy + _SecondAlbedoTex_ST.zw;
				float2 UVs72_g126410 = Second_UVs1234_g126363;
				float Resolution44_g126410 = max( _SecondAlbedoTex_TexelSize.z , _SecondAlbedoTex_TexelSize.w );
				float4 tex2DNode77_g126410 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126410 * ( Resolution44_g126410 / 8.0 ) ) );
				float4 lerpResult78_g126410 = lerp( SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondAlbedoTex, uv_SecondAlbedoTex ) , tex2DNode77_g126410 , tex2DNode77_g126410.a);
				float4 ifLocalVar40_g126397 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126397 = lerpResult78_g126410;
				float2 uv_SecondMaskTex = IN.ase_texcoord8.xy * _SecondMaskTex_ST.xy + _SecondMaskTex_ST.zw;
				float2 UVs72_g126409 = Second_UVs1234_g126363;
				float Resolution44_g126409 = max( _SecondMaskTex_TexelSize.z , _SecondMaskTex_TexelSize.w );
				float4 tex2DNode77_g126409 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126409 * ( Resolution44_g126409 / 8.0 ) ) );
				float4 lerpResult78_g126409 = lerp( SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, uv_SecondMaskTex ) , tex2DNode77_g126409 , tex2DNode77_g126409.a);
				float4 ifLocalVar40_g126395 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126395 = lerpResult78_g126409;
				float2 UVs72_g126411 = Second_UVs1234_g126363;
				float Resolution44_g126411 = max( _SecondAlbedoTex_TexelSize.z , _SecondAlbedoTex_TexelSize.w );
				float4 tex2DNode77_g126411 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126411 * ( Resolution44_g126411 / 8.0 ) ) );
				float4 lerpResult78_g126411 = lerp( SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondAlbedoTex, uv_SecondAlbedoTex ) , tex2DNode77_g126411 , tex2DNode77_g126411.a);
				float4 ifLocalVar40_g126396 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126396 = lerpResult78_g126411;
				float2 uv_EmissiveTex = IN.ase_texcoord8.xy * _EmissiveTex_ST.xy + _EmissiveTex_ST.zw;
				float2 UVs72_g126408 = Emissive_UVs1245_g126363;
				float Resolution44_g126408 = max( _EmissiveTex_TexelSize.z , _EmissiveTex_TexelSize.w );
				float4 tex2DNode77_g126408 = SAMPLE_TEXTURE2D( TVE_DEBUG_MipTex, samplerTVE_DEBUG_MipTex, ( UVs72_g126408 * ( Resolution44_g126408 / 8.0 ) ) );
				float4 lerpResult78_g126408 = lerp( SAMPLE_TEXTURE2D( _EmissiveTex, sampler_EmissiveTex, uv_EmissiveTex ) , tex2DNode77_g126408 , tex2DNode77_g126408.a);
				float4 ifLocalVar40_g126398 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126398 = lerpResult78_g126408;
				float4 Output_MipLevel1284_g126363 = ( ( ifLocalVar40_g126390 + ifLocalVar40_g126388 + ifLocalVar40_g126389 ) + ( ifLocalVar40_g126397 + ifLocalVar40_g126395 + ifLocalVar40_g126396 ) + ifLocalVar40_g126398 );
				float4 ifLocalVar40_g126434 = 0;
				if( Debug_Type367_g126363 == 8.0 )
				ifLocalVar40_g126434 = Output_MipLevel1284_g126363;
				float3 WorldPosition893_g126363 = worldPos;
				half3 Input_PositionWO419_g126446 = WorldPosition893_g126363;
				half Input_MotionTillingRT670_g126446 = ( 4.0 + 0.2 );
				half2 Noise_CoordRT668_g126446 = ( -(Input_PositionWO419_g126446).xz * Input_MotionTillingRT670_g126446 * 0.005 );
				half4 Noise_ParamsRT535_g126446 = SAMPLE_TEXTURE2D( _Sampler6891741_g126363, sampler_Linear_Repeat, Noise_CoordRT668_g126446 );
				half Input_MotionValue629_g126446 = 1.0;
				float2 lerpResult709_g126446 = lerp( float2( 0.5,0.5 ) , (Noise_ParamsRT535_g126446).rg , Input_MotionValue629_g126446);
				float3 appendResult712_g126446 = (float3(lerpResult709_g126446 , ( (Noise_ParamsRT535_g126446).b * Input_MotionValue629_g126446 )));
				float4 lerpResult627_g126446 = lerp( half4(0,1,1,0) , TVE_MotionParams , TVE_IsEnabled);
				half2 Global_WindDirection593_g126446 = (lerpResult627_g126446).xy;
				half Input_MotionTilling321_g126446 = ( 4.0 + 0.2 );
				half2 Noise_Coord515_g126446 = ( -(Input_PositionWO419_g126446).xz * Input_MotionTilling321_g126446 * 0.005 );
				float2 temp_output_3_0_g126447 = Noise_Coord515_g126446;
				float2 temp_output_21_0_g126447 = (Global_WindDirection593_g126446*2.0 + -1.0);
				float lerpResult128_g126452 = lerp( _Time.y , ( ( _Time.y * TVE_TimeParams.x ) + TVE_TimeParams.y ) , TVE_TimeParams.w);
				half Input_MotionSpeed62_g126446 = 4.0;
				half Noise_Speed516_g126446 = ( ( lerpResult128_g126452 * Input_MotionSpeed62_g126446 ) * 0.02 );
				float temp_output_15_0_g126447 = Noise_Speed516_g126446;
				float temp_output_23_0_g126447 = frac( temp_output_15_0_g126447 );
				float4 lerpResult39_g126447 = lerp( SAMPLE_TEXTURE2D( _MotionNoiseTex, sampler_Linear_Repeat, ( temp_output_3_0_g126447 + ( temp_output_21_0_g126447 * temp_output_23_0_g126447 ) ) ) , SAMPLE_TEXTURE2D( _MotionNoiseTex, sampler_Linear_Repeat, ( temp_output_3_0_g126447 + ( temp_output_21_0_g126447 * frac( ( temp_output_15_0_g126447 + 0.5 ) ) ) ) ) , ( abs( ( temp_output_23_0_g126447 - 0.5 ) ) / 0.5 ));
				half4 Noise_Params685_g126446 = lerpResult39_g126447;
				half Input_MotionNoise552_g126446 = 0.0;
				half Global_WindIntensity576_g126446 = (lerpResult627_g126446).z;
				half Motion_Intensity720_g126446 = ( Global_WindIntensity576_g126446 * Input_MotionValue629_g126446 );
				float lerpResult701_g126446 = lerp( 1.0 , Input_MotionNoise552_g126446 , Motion_Intensity720_g126446);
				float2 lerpResult646_g126446 = lerp( Global_WindDirection593_g126446 , (Noise_Params685_g126446).rg , lerpResult701_g126446);
				float2 lerpResult724_g126446 = lerp( float2( 0.5,0.5 ) , lerpResult646_g126446 , Motion_Intensity720_g126446);
				float3 appendResult708_g126446 = (float3(lerpResult724_g126446 , ( (Noise_Params685_g126446).a * Motion_Intensity720_g126446 )));
				#if defined( TVE_MOTION_WIND_OFF )
				float3 staticSwitch673_g126446 = half3(0.5,0.5,0);
				#elif defined( TVE_MOTION_WIND_OPTIMIZED )
				float3 staticSwitch673_g126446 = appendResult712_g126446;
				#elif defined( TVE_MOTION_WIND_ADVANCED )
				float3 staticSwitch673_g126446 = appendResult708_g126446;
				#else
				float3 staticSwitch673_g126446 = half3(0.5,0.5,0);
				#endif
				half3 Motion_Local694_g126446 = staticSwitch673_g126446;
				float Debug_Layer885_g126363 = TVE_DEBUG_Layer;
				float temp_output_136_0_g126453 = Debug_Layer885_g126363;
				float temp_output_19_0_g126455 = TVE_WindLayers[(int)temp_output_136_0_g126453];
				half3 Input_Position180_g126456 = Input_PositionWO419_g126446;
				float2 temp_output_75_0_g126456 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126456).xz ) );
				float temp_output_82_0_g126456 = temp_output_136_0_g126453;
				float2 temp_output_119_0_g126456 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126456).xz ) );
				float temp_output_7_0_g126461 = 1.0;
				float temp_output_10_0_g126461 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126461 );
				float4 lerpResult131_g126456 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126456,temp_output_82_0_g126456), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126456,temp_output_82_0_g126456), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126456 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126461 ) / temp_output_10_0_g126461 ) ));
				float4 temp_output_17_0_g126455 = lerpResult131_g126456;
				float4 temp_output_3_0_g126455 = TVE_WindParams;
				float4 ifLocalVar18_g126455 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126455 >= 0.5 )
				ifLocalVar18_g126455 = temp_output_17_0_g126455;
				else
				ifLocalVar18_g126455 = temp_output_3_0_g126455;
				float4 lerpResult22_g126455 = lerp( temp_output_3_0_g126455 , temp_output_17_0_g126455 , temp_output_19_0_g126455);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126455 = lerpResult22_g126455;
				#else
				float4 staticSwitch24_g126455 = ifLocalVar18_g126455;
				#endif
				float4 temp_output_610_0_g126446 = staticSwitch24_g126455;
				float2 lerpResult699_g126446 = lerp( float2( 0.5,0.5 ) , (temp_output_610_0_g126446).rg , Input_MotionValue629_g126446);
				float3 appendResult643_g126446 = (float3(lerpResult699_g126446 , ( (temp_output_610_0_g126446).b * Input_MotionValue629_g126446 )));
				float3 lerpResult623_g126446 = lerp( Motion_Local694_g126446 , appendResult643_g126446 , (temp_output_610_0_g126446).a);
				#if defined( TVE_MOTION_WIND_OFF )
				float3 staticSwitch693_g126446 = appendResult643_g126446;
				#elif defined( TVE_MOTION_WIND_OPTIMIZED )
				float3 staticSwitch693_g126446 = lerpResult623_g126446;
				#elif defined( TVE_MOTION_WIND_ADVANCED )
				float3 staticSwitch693_g126446 = lerpResult623_g126446;
				#else
				float3 staticSwitch693_g126446 = appendResult643_g126446;
				#endif
				#ifdef TVE_MOTION_WIND_ELEMENT
				float3 staticSwitch612_g126446 = staticSwitch693_g126446;
				#else
				float3 staticSwitch612_g126446 = Motion_Local694_g126446;
				#endif
				float ifLocalVar40_g126997 = 0;
				if( Debug_Index464_g126363 == 0.0 )
				ifLocalVar40_g126997 = (staticSwitch612_g126446).z;
				float temp_output_82_0_g126816 = Debug_Layer885_g126363;
				float temp_output_19_0_g126818 = TVE_CoatLayers[(int)temp_output_82_0_g126816];
				half3 Input_Position180_g126819 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126819 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126819).xz ) );
				float temp_output_82_0_g126819 = temp_output_82_0_g126816;
				float2 temp_output_119_0_g126819 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126819).xz ) );
				float temp_output_7_0_g126824 = 1.0;
				float temp_output_10_0_g126824 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126824 );
				float4 lerpResult131_g126819 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_CoatBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126819,temp_output_82_0_g126819), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_CoatNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126819,temp_output_82_0_g126819), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126819 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126824 ) / temp_output_10_0_g126824 ) ));
				float4 temp_output_17_0_g126818 = lerpResult131_g126819;
				float4 temp_output_3_0_g126818 = TVE_CoatParams;
				float4 ifLocalVar18_g126818 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126818 >= 0.5 )
				ifLocalVar18_g126818 = temp_output_17_0_g126818;
				else
				ifLocalVar18_g126818 = temp_output_3_0_g126818;
				float4 lerpResult22_g126818 = lerp( temp_output_3_0_g126818 , temp_output_17_0_g126818 , temp_output_19_0_g126818);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126818 = lerpResult22_g126818;
				#else
				float4 staticSwitch24_g126818 = ifLocalVar18_g126818;
				#endif
				float ifLocalVar40_g126834 = 0;
				if( Debug_Index464_g126363 == 1.0 )
				ifLocalVar40_g126834 = (staticSwitch24_g126818).g;
				float temp_output_82_0_g126825 = Debug_Layer885_g126363;
				float temp_output_19_0_g126827 = TVE_CoatLayers[(int)temp_output_82_0_g126825];
				half3 Input_Position180_g126828 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126828 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126828).xz ) );
				float temp_output_82_0_g126828 = temp_output_82_0_g126825;
				float2 temp_output_119_0_g126828 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126828).xz ) );
				float temp_output_7_0_g126833 = 1.0;
				float temp_output_10_0_g126833 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126833 );
				float4 lerpResult131_g126828 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_CoatBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126828,temp_output_82_0_g126828), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_CoatNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126828,temp_output_82_0_g126828), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126828 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126833 ) / temp_output_10_0_g126833 ) ));
				float4 temp_output_17_0_g126827 = lerpResult131_g126828;
				float4 temp_output_3_0_g126827 = TVE_CoatParams;
				float4 ifLocalVar18_g126827 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126827 >= 0.5 )
				ifLocalVar18_g126827 = temp_output_17_0_g126827;
				else
				ifLocalVar18_g126827 = temp_output_3_0_g126827;
				float4 lerpResult22_g126827 = lerp( temp_output_3_0_g126827 , temp_output_17_0_g126827 , temp_output_19_0_g126827);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126827 = lerpResult22_g126827;
				#else
				float4 staticSwitch24_g126827 = ifLocalVar18_g126827;
				#endif
				float ifLocalVar40_g126835 = 0;
				if( Debug_Index464_g126363 == 2.0 )
				ifLocalVar40_g126835 = (staticSwitch24_g126827).b;
				float temp_output_82_0_g126845 = Debug_Layer885_g126363;
				float temp_output_19_0_g126847 = TVE_PaintLayers[(int)temp_output_82_0_g126845];
				half3 Input_Position180_g126848 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126848 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126848).xz ) );
				float temp_output_82_0_g126848 = temp_output_82_0_g126845;
				float2 temp_output_119_0_g126848 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126848).xz ) );
				float temp_output_7_0_g126853 = 1.0;
				float temp_output_10_0_g126853 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126853 );
				float4 lerpResult131_g126848 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PaintBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126848,temp_output_82_0_g126848), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PaintNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126848,temp_output_82_0_g126848), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126848 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126853 ) / temp_output_10_0_g126853 ) ));
				float4 temp_output_17_0_g126847 = lerpResult131_g126848;
				float4 temp_output_3_0_g126847 = TVE_PaintParams;
				float4 ifLocalVar18_g126847 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126847 >= 0.5 )
				ifLocalVar18_g126847 = temp_output_17_0_g126847;
				else
				ifLocalVar18_g126847 = temp_output_3_0_g126847;
				float4 lerpResult22_g126847 = lerp( temp_output_3_0_g126847 , temp_output_17_0_g126847 , temp_output_19_0_g126847);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126847 = lerpResult22_g126847;
				#else
				float4 staticSwitch24_g126847 = ifLocalVar18_g126847;
				#endif
				float3 ifLocalVar40_g126854 = 0;
				if( Debug_Index464_g126363 == 3.0 )
				ifLocalVar40_g126854 = (staticSwitch24_g126847).rgb;
				float temp_output_82_0_g126836 = Debug_Layer885_g126363;
				float temp_output_19_0_g126838 = TVE_PaintLayers[(int)temp_output_82_0_g126836];
				half3 Input_Position180_g126839 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126839 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126839).xz ) );
				float temp_output_82_0_g126839 = temp_output_82_0_g126836;
				float2 temp_output_119_0_g126839 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126839).xz ) );
				float temp_output_7_0_g126844 = 1.0;
				float temp_output_10_0_g126844 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126844 );
				float4 lerpResult131_g126839 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PaintBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126839,temp_output_82_0_g126839), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PaintNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126839,temp_output_82_0_g126839), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126839 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126844 ) / temp_output_10_0_g126844 ) ));
				float4 temp_output_17_0_g126838 = lerpResult131_g126839;
				float4 temp_output_3_0_g126838 = TVE_PaintParams;
				float4 ifLocalVar18_g126838 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126838 >= 0.5 )
				ifLocalVar18_g126838 = temp_output_17_0_g126838;
				else
				ifLocalVar18_g126838 = temp_output_3_0_g126838;
				float4 lerpResult22_g126838 = lerp( temp_output_3_0_g126838 , temp_output_17_0_g126838 , temp_output_19_0_g126838);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126838 = lerpResult22_g126838;
				#else
				float4 staticSwitch24_g126838 = ifLocalVar18_g126838;
				#endif
				float ifLocalVar40_g126855 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126855 = saturate( (staticSwitch24_g126838).a );
				float temp_output_82_0_g126856 = Debug_Layer885_g126363;
				float temp_output_19_0_g126858 = TVE_GlowLayers[(int)temp_output_82_0_g126856];
				half3 Input_Position180_g126859 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126859 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126859).xz ) );
				float temp_output_82_0_g126859 = temp_output_82_0_g126856;
				float2 temp_output_119_0_g126859 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126859).xz ) );
				float temp_output_7_0_g126864 = 1.0;
				float temp_output_10_0_g126864 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126864 );
				float4 lerpResult131_g126859 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_GlowBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126859,temp_output_82_0_g126859), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_GlowNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126859,temp_output_82_0_g126859), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126859 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126864 ) / temp_output_10_0_g126864 ) ));
				float4 temp_output_17_0_g126858 = lerpResult131_g126859;
				float4 temp_output_3_0_g126858 = TVE_GlowParams;
				float4 ifLocalVar18_g126858 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126858 >= 0.5 )
				ifLocalVar18_g126858 = temp_output_17_0_g126858;
				else
				ifLocalVar18_g126858 = temp_output_3_0_g126858;
				float4 lerpResult22_g126858 = lerp( temp_output_3_0_g126858 , temp_output_17_0_g126858 , temp_output_19_0_g126858);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126858 = lerpResult22_g126858;
				#else
				float4 staticSwitch24_g126858 = ifLocalVar18_g126858;
				#endif
				float3 ifLocalVar40_g126928 = 0;
				if( Debug_Index464_g126363 == 5.0 )
				ifLocalVar40_g126928 = (staticSwitch24_g126858).rgb;
				float temp_output_82_0_g126919 = Debug_Layer885_g126363;
				float temp_output_19_0_g126921 = TVE_GlowLayers[(int)temp_output_82_0_g126919];
				half3 Input_Position180_g126922 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126922 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126922).xz ) );
				float temp_output_82_0_g126922 = temp_output_82_0_g126919;
				float2 temp_output_119_0_g126922 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126922).xz ) );
				float temp_output_7_0_g126927 = 1.0;
				float temp_output_10_0_g126927 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126927 );
				float4 lerpResult131_g126922 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_GlowBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126922,temp_output_82_0_g126922), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_GlowNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126922,temp_output_82_0_g126922), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126922 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126927 ) / temp_output_10_0_g126927 ) ));
				float4 temp_output_17_0_g126921 = lerpResult131_g126922;
				float4 temp_output_3_0_g126921 = TVE_GlowParams;
				float4 ifLocalVar18_g126921 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126921 >= 0.5 )
				ifLocalVar18_g126921 = temp_output_17_0_g126921;
				else
				ifLocalVar18_g126921 = temp_output_3_0_g126921;
				float4 lerpResult22_g126921 = lerp( temp_output_3_0_g126921 , temp_output_17_0_g126921 , temp_output_19_0_g126921);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126921 = lerpResult22_g126921;
				#else
				float4 staticSwitch24_g126921 = ifLocalVar18_g126921;
				#endif
				float ifLocalVar40_g126929 = 0;
				if( Debug_Index464_g126363 == 6.0 )
				ifLocalVar40_g126929 = saturate( (staticSwitch24_g126921).a );
				float temp_output_132_0_g126892 = Debug_Layer885_g126363;
				float temp_output_19_0_g126894 = TVE_AtmoLayers[(int)temp_output_132_0_g126892];
				half3 Input_Position180_g126895 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126895 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126895).xz ) );
				float temp_output_82_0_g126895 = temp_output_132_0_g126892;
				float2 temp_output_119_0_g126895 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126895).xz ) );
				float temp_output_7_0_g126900 = 1.0;
				float temp_output_10_0_g126900 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126900 );
				float4 lerpResult131_g126895 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126895,temp_output_82_0_g126895), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126895,temp_output_82_0_g126895), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126895 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126900 ) / temp_output_10_0_g126900 ) ));
				float4 temp_output_17_0_g126894 = lerpResult131_g126895;
				float4 temp_output_3_0_g126894 = TVE_AtmoParams;
				float4 ifLocalVar18_g126894 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126894 >= 0.5 )
				ifLocalVar18_g126894 = temp_output_17_0_g126894;
				else
				ifLocalVar18_g126894 = temp_output_3_0_g126894;
				float4 lerpResult22_g126894 = lerp( temp_output_3_0_g126894 , temp_output_17_0_g126894 , temp_output_19_0_g126894);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126894 = lerpResult22_g126894;
				#else
				float4 staticSwitch24_g126894 = ifLocalVar18_g126894;
				#endif
				float ifLocalVar40_g126930 = 0;
				if( Debug_Index464_g126363 == 7.0 )
				ifLocalVar40_g126930 = (staticSwitch24_g126894).r;
				float temp_output_132_0_g126865 = Debug_Layer885_g126363;
				float temp_output_19_0_g126867 = TVE_AtmoLayers[(int)temp_output_132_0_g126865];
				half3 Input_Position180_g126868 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126868 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126868).xz ) );
				float temp_output_82_0_g126868 = temp_output_132_0_g126865;
				float2 temp_output_119_0_g126868 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126868).xz ) );
				float temp_output_7_0_g126873 = 1.0;
				float temp_output_10_0_g126873 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126873 );
				float4 lerpResult131_g126868 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126868,temp_output_82_0_g126868), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126868,temp_output_82_0_g126868), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126868 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126873 ) / temp_output_10_0_g126873 ) ));
				float4 temp_output_17_0_g126867 = lerpResult131_g126868;
				float4 temp_output_3_0_g126867 = TVE_AtmoParams;
				float4 ifLocalVar18_g126867 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126867 >= 0.5 )
				ifLocalVar18_g126867 = temp_output_17_0_g126867;
				else
				ifLocalVar18_g126867 = temp_output_3_0_g126867;
				float4 lerpResult22_g126867 = lerp( temp_output_3_0_g126867 , temp_output_17_0_g126867 , temp_output_19_0_g126867);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126867 = lerpResult22_g126867;
				#else
				float4 staticSwitch24_g126867 = ifLocalVar18_g126867;
				#endif
				float ifLocalVar40_g126931 = 0;
				if( Debug_Index464_g126363 == 8.0 )
				ifLocalVar40_g126931 = (staticSwitch24_g126867).g;
				float temp_output_132_0_g126874 = Debug_Layer885_g126363;
				float temp_output_19_0_g126876 = TVE_AtmoLayers[(int)temp_output_132_0_g126874];
				half3 Input_Position180_g126877 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126877 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126877).xz ) );
				float temp_output_82_0_g126877 = temp_output_132_0_g126874;
				float2 temp_output_119_0_g126877 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126877).xz ) );
				float temp_output_7_0_g126882 = 1.0;
				float temp_output_10_0_g126882 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126882 );
				float4 lerpResult131_g126877 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126877,temp_output_82_0_g126877), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126877,temp_output_82_0_g126877), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126877 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126882 ) / temp_output_10_0_g126882 ) ));
				float4 temp_output_17_0_g126876 = lerpResult131_g126877;
				float4 temp_output_3_0_g126876 = TVE_AtmoParams;
				float4 ifLocalVar18_g126876 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126876 >= 0.5 )
				ifLocalVar18_g126876 = temp_output_17_0_g126876;
				else
				ifLocalVar18_g126876 = temp_output_3_0_g126876;
				float4 lerpResult22_g126876 = lerp( temp_output_3_0_g126876 , temp_output_17_0_g126876 , temp_output_19_0_g126876);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126876 = lerpResult22_g126876;
				#else
				float4 staticSwitch24_g126876 = ifLocalVar18_g126876;
				#endif
				float ifLocalVar40_g126932 = 0;
				if( Debug_Index464_g126363 == 9.0 )
				ifLocalVar40_g126932 = (staticSwitch24_g126876).b;
				float temp_output_132_0_g126883 = Debug_Layer885_g126363;
				float temp_output_19_0_g126885 = TVE_AtmoLayers[(int)temp_output_132_0_g126883];
				half3 Input_Position180_g126886 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126886 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126886).xz ) );
				float temp_output_82_0_g126886 = temp_output_132_0_g126883;
				float2 temp_output_119_0_g126886 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126886).xz ) );
				float temp_output_7_0_g126891 = 1.0;
				float temp_output_10_0_g126891 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126891 );
				float4 lerpResult131_g126886 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126886,temp_output_82_0_g126886), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_AtmoNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126886,temp_output_82_0_g126886), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126886 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126891 ) / temp_output_10_0_g126891 ) ));
				float4 temp_output_17_0_g126885 = lerpResult131_g126886;
				float4 temp_output_3_0_g126885 = TVE_AtmoParams;
				float4 ifLocalVar18_g126885 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126885 >= 0.5 )
				ifLocalVar18_g126885 = temp_output_17_0_g126885;
				else
				ifLocalVar18_g126885 = temp_output_3_0_g126885;
				float4 lerpResult22_g126885 = lerp( temp_output_3_0_g126885 , temp_output_17_0_g126885 , temp_output_19_0_g126885);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126885 = lerpResult22_g126885;
				#else
				float4 staticSwitch24_g126885 = ifLocalVar18_g126885;
				#endif
				float ifLocalVar40_g126933 = 0;
				if( Debug_Index464_g126363 == 10.0 )
				ifLocalVar40_g126933 = saturate( (staticSwitch24_g126885).a );
				float temp_output_130_0_g126910 = Debug_Layer885_g126363;
				float temp_output_19_0_g126912 = TVE_FormLayers[(int)temp_output_130_0_g126910];
				half3 Input_Position180_g126913 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126913 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126913).xz ) );
				float temp_output_82_0_g126913 = temp_output_130_0_g126910;
				float2 temp_output_119_0_g126913 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126913).xz ) );
				float temp_output_7_0_g126918 = 1.0;
				float temp_output_10_0_g126918 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126918 );
				float4 lerpResult131_g126913 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126913,temp_output_82_0_g126913), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126913,temp_output_82_0_g126913), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126913 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126918 ) / temp_output_10_0_g126918 ) ));
				float4 temp_output_17_0_g126912 = lerpResult131_g126913;
				float4 temp_output_3_0_g126912 = TVE_FormParams;
				float4 ifLocalVar18_g126912 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126912 >= 0.5 )
				ifLocalVar18_g126912 = temp_output_17_0_g126912;
				else
				ifLocalVar18_g126912 = temp_output_3_0_g126912;
				float4 lerpResult22_g126912 = lerp( temp_output_3_0_g126912 , temp_output_17_0_g126912 , temp_output_19_0_g126912);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126912 = lerpResult22_g126912;
				#else
				float4 staticSwitch24_g126912 = ifLocalVar18_g126912;
				#endif
				float3 appendResult1013_g126363 = (float3((staticSwitch24_g126912).rg , 0.0));
				float3 ifLocalVar40_g126934 = 0;
				if( Debug_Index464_g126363 == 11.0 )
				ifLocalVar40_g126934 = appendResult1013_g126363;
				float temp_output_130_0_g126901 = Debug_Layer885_g126363;
				float temp_output_19_0_g126903 = TVE_FormLayers[(int)temp_output_130_0_g126901];
				half3 Input_Position180_g126904 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126904 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126904).xz ) );
				float temp_output_82_0_g126904 = temp_output_130_0_g126901;
				float2 temp_output_119_0_g126904 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126904).xz ) );
				float temp_output_7_0_g126909 = 1.0;
				float temp_output_10_0_g126909 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126909 );
				float4 lerpResult131_g126904 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126904,temp_output_82_0_g126904), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126904,temp_output_82_0_g126904), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126904 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126909 ) / temp_output_10_0_g126909 ) ));
				float4 temp_output_17_0_g126903 = lerpResult131_g126904;
				float4 temp_output_3_0_g126903 = TVE_FormParams;
				float4 ifLocalVar18_g126903 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126903 >= 0.5 )
				ifLocalVar18_g126903 = temp_output_17_0_g126903;
				else
				ifLocalVar18_g126903 = temp_output_3_0_g126903;
				float4 lerpResult22_g126903 = lerp( temp_output_3_0_g126903 , temp_output_17_0_g126903 , temp_output_19_0_g126903);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126903 = lerpResult22_g126903;
				#else
				float4 staticSwitch24_g126903 = ifLocalVar18_g126903;
				#endif
				float ifLocalVar40_g126935 = 0;
				if( Debug_Index464_g126363 == 12.0 )
				ifLocalVar40_g126935 = saturate( (staticSwitch24_g126903).b );
				float temp_output_130_0_g126958 = Debug_Layer885_g126363;
				float temp_output_19_0_g126960 = TVE_FormLayers[(int)temp_output_130_0_g126958];
				half3 Input_Position180_g126961 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126961 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126961).xz ) );
				float temp_output_82_0_g126961 = temp_output_130_0_g126958;
				float2 temp_output_119_0_g126961 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126961).xz ) );
				float temp_output_7_0_g126966 = 1.0;
				float temp_output_10_0_g126966 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126966 );
				float4 lerpResult131_g126961 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126961,temp_output_82_0_g126961), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_FormNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126961,temp_output_82_0_g126961), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126961 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126966 ) / temp_output_10_0_g126966 ) ));
				float4 temp_output_17_0_g126960 = lerpResult131_g126961;
				float4 temp_output_3_0_g126960 = TVE_FormParams;
				float4 ifLocalVar18_g126960 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126960 >= 0.5 )
				ifLocalVar18_g126960 = temp_output_17_0_g126960;
				else
				ifLocalVar18_g126960 = temp_output_3_0_g126960;
				float4 lerpResult22_g126960 = lerp( temp_output_3_0_g126960 , temp_output_17_0_g126960 , temp_output_19_0_g126960);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126960 = lerpResult22_g126960;
				#else
				float4 staticSwitch24_g126960 = ifLocalVar18_g126960;
				#endif
				float ifLocalVar40_g126967 = 0;
				if( Debug_Index464_g126363 == 13.0 )
				ifLocalVar40_g126967 = saturate( (staticSwitch24_g126960).a );
				float temp_output_136_0_g126936 = Debug_Layer885_g126363;
				float temp_output_19_0_g126938 = TVE_WindLayers[(int)temp_output_136_0_g126936];
				half3 Input_Position180_g126939 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126939 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126939).xz ) );
				float temp_output_82_0_g126939 = temp_output_136_0_g126936;
				float2 temp_output_119_0_g126939 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126939).xz ) );
				float temp_output_7_0_g126944 = 1.0;
				float temp_output_10_0_g126944 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126944 );
				float4 lerpResult131_g126939 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126939,temp_output_82_0_g126939), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126939,temp_output_82_0_g126939), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126939 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126944 ) / temp_output_10_0_g126944 ) ));
				float4 temp_output_17_0_g126938 = lerpResult131_g126939;
				float4 temp_output_3_0_g126938 = TVE_WindParams;
				float4 ifLocalVar18_g126938 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126938 >= 0.5 )
				ifLocalVar18_g126938 = temp_output_17_0_g126938;
				else
				ifLocalVar18_g126938 = temp_output_3_0_g126938;
				float4 lerpResult22_g126938 = lerp( temp_output_3_0_g126938 , temp_output_17_0_g126938 , temp_output_19_0_g126938);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126938 = lerpResult22_g126938;
				#else
				float4 staticSwitch24_g126938 = ifLocalVar18_g126938;
				#endif
				float3 appendResult1012_g126363 = (float3((staticSwitch24_g126938).rg , 0.0));
				float3 ifLocalVar40_g126954 = 0;
				if( Debug_Index464_g126363 == 14.0 )
				ifLocalVar40_g126954 = appendResult1012_g126363;
				float temp_output_136_0_g126945 = Debug_Layer885_g126363;
				float temp_output_19_0_g126947 = TVE_WindLayers[(int)temp_output_136_0_g126945];
				half3 Input_Position180_g126948 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126948 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126948).xz ) );
				float temp_output_82_0_g126948 = temp_output_136_0_g126945;
				float2 temp_output_119_0_g126948 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126948).xz ) );
				float temp_output_7_0_g126953 = 1.0;
				float temp_output_10_0_g126953 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126953 );
				float4 lerpResult131_g126948 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126948,temp_output_82_0_g126948), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_WindNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126948,temp_output_82_0_g126948), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126948 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126953 ) / temp_output_10_0_g126953 ) ));
				float4 temp_output_17_0_g126947 = lerpResult131_g126948;
				float4 temp_output_3_0_g126947 = TVE_WindParams;
				float4 ifLocalVar18_g126947 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126947 >= 0.5 )
				ifLocalVar18_g126947 = temp_output_17_0_g126947;
				else
				ifLocalVar18_g126947 = temp_output_3_0_g126947;
				float4 lerpResult22_g126947 = lerp( temp_output_3_0_g126947 , temp_output_17_0_g126947 , temp_output_19_0_g126947);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126947 = lerpResult22_g126947;
				#else
				float4 staticSwitch24_g126947 = ifLocalVar18_g126947;
				#endif
				float ifLocalVar40_g126955 = 0;
				if( Debug_Index464_g126363 == 15.0 )
				ifLocalVar40_g126955 = (staticSwitch24_g126947).b;
				float temp_output_136_0_g126970 = Debug_Layer885_g126363;
				float temp_output_19_0_g126971 = TVE_PushLayers[(int)temp_output_136_0_g126970];
				half3 Input_Position180_g126972 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126972 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126972).xz ) );
				float temp_output_82_0_g126972 = temp_output_136_0_g126970;
				float2 temp_output_119_0_g126972 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126972).xz ) );
				float temp_output_7_0_g126977 = 1.0;
				float temp_output_10_0_g126977 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126977 );
				float4 lerpResult131_g126972 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126972,temp_output_82_0_g126972), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126972,temp_output_82_0_g126972), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126972 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126977 ) / temp_output_10_0_g126977 ) ));
				float4 temp_output_17_0_g126971 = lerpResult131_g126972;
				float4 temp_output_3_0_g126971 = TVE_PushParams;
				float4 ifLocalVar18_g126971 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126971 >= 0.5 )
				ifLocalVar18_g126971 = temp_output_17_0_g126971;
				else
				ifLocalVar18_g126971 = temp_output_3_0_g126971;
				float4 lerpResult22_g126971 = lerp( temp_output_3_0_g126971 , temp_output_17_0_g126971 , temp_output_19_0_g126971);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126971 = lerpResult22_g126971;
				#else
				float4 staticSwitch24_g126971 = ifLocalVar18_g126971;
				#endif
				float3 appendResult1780_g126363 = (float3((staticSwitch24_g126971).rg , 0.0));
				float3 ifLocalVar40_g126956 = 0;
				if( Debug_Index464_g126363 == 16.0 )
				ifLocalVar40_g126956 = appendResult1780_g126363;
				float temp_output_136_0_g126979 = Debug_Layer885_g126363;
				float temp_output_19_0_g126980 = TVE_PushLayers[(int)temp_output_136_0_g126979];
				half3 Input_Position180_g126981 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126981 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126981).xz ) );
				float temp_output_82_0_g126981 = temp_output_136_0_g126979;
				float2 temp_output_119_0_g126981 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126981).xz ) );
				float temp_output_7_0_g126986 = 1.0;
				float temp_output_10_0_g126986 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126986 );
				float4 lerpResult131_g126981 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126981,temp_output_82_0_g126981), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126981,temp_output_82_0_g126981), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126981 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126986 ) / temp_output_10_0_g126986 ) ));
				float4 temp_output_17_0_g126980 = lerpResult131_g126981;
				float4 temp_output_3_0_g126980 = TVE_PushParams;
				float4 ifLocalVar18_g126980 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126980 >= 0.5 )
				ifLocalVar18_g126980 = temp_output_17_0_g126980;
				else
				ifLocalVar18_g126980 = temp_output_3_0_g126980;
				float4 lerpResult22_g126980 = lerp( temp_output_3_0_g126980 , temp_output_17_0_g126980 , temp_output_19_0_g126980);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126980 = lerpResult22_g126980;
				#else
				float4 staticSwitch24_g126980 = ifLocalVar18_g126980;
				#endif
				float ifLocalVar40_g126957 = 0;
				if( Debug_Index464_g126363 == 17.0 )
				ifLocalVar40_g126957 = (staticSwitch24_g126980).b;
				float temp_output_136_0_g126988 = Debug_Layer885_g126363;
				float temp_output_19_0_g126989 = TVE_PushLayers[(int)temp_output_136_0_g126988];
				half3 Input_Position180_g126990 = WorldPosition893_g126363;
				float2 temp_output_75_0_g126990 = ( (TVE_RenderBaseCoords).zw + ( (TVE_RenderBaseCoords).xy * (Input_Position180_g126990).xz ) );
				float temp_output_82_0_g126990 = temp_output_136_0_g126988;
				float2 temp_output_119_0_g126990 = ( (TVE_RenderNearCoords).zw + ( (TVE_RenderNearCoords).xy * (Input_Position180_g126990).xz ) );
				float temp_output_7_0_g126995 = 1.0;
				float temp_output_10_0_g126995 = ( TVE_RenderNearFadeValue - temp_output_7_0_g126995 );
				float4 lerpResult131_g126990 = lerp( SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushBaseTex, sampler_Linear_Clamp, float3(temp_output_75_0_g126990,temp_output_82_0_g126990), 0.0 ) , SAMPLE_TEXTURE2D_ARRAY_LOD( TVE_PushNearTex, sampler_Linear_Repeat, float3(temp_output_119_0_g126990,temp_output_82_0_g126990), 0.0 ) , saturate( ( ( saturate( ( distance( Input_Position180_g126990 , (TVE_RenderNearPositionR).xyz ) / (TVE_RenderNearPositionR).w ) ) - temp_output_7_0_g126995 ) / temp_output_10_0_g126995 ) ));
				float4 temp_output_17_0_g126989 = lerpResult131_g126990;
				float4 temp_output_3_0_g126989 = TVE_PushParams;
				float4 ifLocalVar18_g126989 = 0;
				UNITY_BRANCH 
				if( temp_output_19_0_g126989 >= 0.5 )
				ifLocalVar18_g126989 = temp_output_17_0_g126989;
				else
				ifLocalVar18_g126989 = temp_output_3_0_g126989;
				float4 lerpResult22_g126989 = lerp( temp_output_3_0_g126989 , temp_output_17_0_g126989 , temp_output_19_0_g126989);
				#ifdef SHADER_API_MOBILE
				float4 staticSwitch24_g126989 = lerpResult22_g126989;
				#else
				float4 staticSwitch24_g126989 = ifLocalVar18_g126989;
				#endif
				float ifLocalVar40_g126968 = 0;
				if( Debug_Index464_g126363 == 18.0 )
				ifLocalVar40_g126968 = saturate( (staticSwitch24_g126989).a );
				float temp_output_7_0_g126969 = Debug_Min721_g126363;
				float3 temp_cast_61 = (temp_output_7_0_g126969).xxx;
				float temp_output_10_0_g126969 = ( Debug_Max723_g126363 - temp_output_7_0_g126969 );
				float4 appendResult1659_g126363 = (float4(saturate( ( ( ( ifLocalVar40_g126997 + ( ifLocalVar40_g126834 + ifLocalVar40_g126835 ) + ( ifLocalVar40_g126854 + ifLocalVar40_g126855 ) + ( ifLocalVar40_g126928 + ifLocalVar40_g126929 ) + ( ifLocalVar40_g126930 + ifLocalVar40_g126931 + ifLocalVar40_g126932 + ifLocalVar40_g126933 ) + ( ifLocalVar40_g126934 + ifLocalVar40_g126935 + ifLocalVar40_g126967 ) + ( ifLocalVar40_g126954 + ifLocalVar40_g126955 + ifLocalVar40_g126956 + ifLocalVar40_g126957 + ifLocalVar40_g126968 ) ) - temp_cast_61 ) / ( temp_output_10_0_g126969 + 0.0001 ) ) ) , 1.0));
				float4 Output_Globals888_g126363 = appendResult1659_g126363;
				float4 ifLocalVar40_g126435 = 0;
				if( Debug_Type367_g126363 == 9.0 )
				ifLocalVar40_g126435 = Output_Globals888_g126363;
				float3 vertexToFrag328_g126363 = IN.ase_texcoord10.xyz;
				float4 color1016_g126363 = IsGammaSpace() ? float4(0.5831653,0.6037736,0.2135992,0) : float4(0.2992498,0.3229691,0.03750122,0);
				float4 color1017_g126363 = IsGammaSpace() ? float4(0.8117647,0.3488252,0.2627451,0) : float4(0.6239604,0.0997834,0.05612849,0);
				float4 switchResult1015_g126363 = (((ase_vface>0)?(color1016_g126363):(color1017_g126363)));
				float3 ifLocalVar40_g126367 = 0;
				if( Debug_Index464_g126363 == 4.0 )
				ifLocalVar40_g126367 = (switchResult1015_g126363).rgb;
				float temp_output_7_0_g126425 = Debug_Min721_g126363;
				float3 temp_cast_62 = (temp_output_7_0_g126425).xxx;
				float temp_output_10_0_g126425 = ( Debug_Max723_g126363 - temp_output_7_0_g126425 );
				float4 appendResult1658_g126363 = (float4(saturate( ( ( ( vertexToFrag328_g126363 + ifLocalVar40_g126367 ) - temp_cast_62 ) / ( temp_output_10_0_g126425 + 0.0001 ) ) ) , 1.0));
				float4 Output_Mesh316_g126363 = appendResult1658_g126363;
				float4 ifLocalVar40_g126437 = 0;
				if( Debug_Type367_g126363 == 11.0 )
				ifLocalVar40_g126437 = Output_Mesh316_g126363;
				float _IsTVEShader647_g126363 = _IsTVEShader;
				float Debug_Filter322_g126363 = TVE_DEBUG_Filter;
				float lerpResult1524_g126363 = lerp( 1.0 , _IsTVEShader647_g126363 , Debug_Filter322_g126363);
				float4 lerpResult1517_g126363 = lerp( Shading_Inactive1492_g126363 , ( ( ifLocalVar40_g126426 + ifLocalVar40_g126428 + ifLocalVar40_g126429 + ifLocalVar40_g126430 + ifLocalVar40_g126431 ) + ( ifLocalVar40_g126432 + ifLocalVar40_g126433 + ifLocalVar40_g126434 ) + ( ifLocalVar40_g126435 + ifLocalVar40_g126437 ) ) , lerpResult1524_g126363);
				float dotResult1472_g126363 = dot( WorldNormal , worldViewDir );
				float temp_output_1526_0_g126363 = ( 1.0 - saturate( dotResult1472_g126363 ) );
				float Shading_Fresnel1469_g126363 = (( 1.0 - ( temp_output_1526_0_g126363 * temp_output_1526_0_g126363 ) )*0.3 + 0.7);
				float Debug_Shading1653_g126363 = TVE_DEBUG_Shading;
				float lerpResult1655_g126363 = lerp( 1.0 , Shading_Fresnel1469_g126363 , Debug_Shading1653_g126363);
				float Debug_Clip623_g126363 = TVE_DEBUG_Clip;
				float lerpResult622_g126363 = lerp( 1.0 , SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, uv_MainAlbedoTex ).a , ( Debug_Clip623_g126363 * _RenderClip ));
				clip( lerpResult622_g126363 - _MainAlphaClipValue);
				clip( ( 1.0 - saturate( ( _IsElementShader + _IsHelperShader ) ) ) - 1.0);
				
				o.Albedo = fixed3( 0.5, 0.5, 0.5 );
				o.Normal = fixed3( 0, 0, 1 );
				o.Emission = ( lerpResult1517_g126363 * lerpResult1655_g126363 ).rgb;
				#if defined(_SPECULAR_SETUP)
					o.Specular = fixed3( 0, 0, 0 );
				#else
					o.Metallic = 0;
				#endif
				o.Smoothness = 0;
				o.Occlusion = 1;
				o.Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float3 BakedGI = 0;

				#ifdef _ALPHATEST_ON
					clip( o.Alpha - AlphaClipThreshold );
				#endif

				#ifdef _DEPTHOFFSET_ON
					outputDepth = IN.pos.z;
				#endif

				#ifndef USING_DIRECTIONAL_LIGHT
					fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				#else
					fixed3 lightDir = _WorldSpaceLightPos0.xyz;
				#endif

				float3 worldN;
				worldN.x = dot(IN.tSpace0.xyz, o.Normal);
				worldN.y = dot(IN.tSpace1.xyz, o.Normal);
				worldN.z = dot(IN.tSpace2.xyz, o.Normal);
				worldN = normalize(worldN);
				o.Normal = worldN;

				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
				gi.indirect.diffuse = 0;
				gi.indirect.specular = 0;
				gi.light.color = 0;
				gi.light.dir = half3(0,1,0);

				UnityGIInput giInput;
				UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
				giInput.light = gi.light;
				giInput.worldPos = worldPos;
				giInput.worldViewDir = worldViewDir;
				giInput.atten = atten;
				#if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
					giInput.lightmapUV = IN.lmap;
				#else
					giInput.lightmapUV = 0.0;
				#endif
				#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
					giInput.ambient = IN.sh;
				#else
					giInput.ambient.rgb = 0.0;
				#endif
				giInput.probeHDR[0] = unity_SpecCube0_HDR;
				giInput.probeHDR[1] = unity_SpecCube1_HDR;
				#if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
					giInput.boxMin[0] = unity_SpecCube0_BoxMin;
				#endif
				#ifdef UNITY_SPECCUBE_BOX_PROJECTION
					giInput.boxMax[0] = unity_SpecCube0_BoxMax;
					giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
					giInput.boxMax[1] = unity_SpecCube1_BoxMax;
					giInput.boxMin[1] = unity_SpecCube1_BoxMin;
					giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
				#endif

				#if defined(_SPECULAR_SETUP)
					LightingStandardSpecular_GI( o, giInput, gi );
				#else
					LightingStandard_GI( o, giInput, gi );
				#endif

				#ifdef ASE_BAKEDGI
					gi.indirect.diffuse = BakedGI;
				#endif

				#if UNITY_SHOULD_SAMPLE_SH && !defined(LIGHTMAP_ON) && defined(ASE_NO_AMBIENT)
					gi.indirect.diffuse = 0;
				#endif

				#if defined(_SPECULAR_SETUP)
					outEmission = LightingStandardSpecular_Deferred( o, worldViewDir, gi, outGBuffer0, outGBuffer1, outGBuffer2 );
				#else
					outEmission = LightingStandard_Deferred( o, worldViewDir, gi, outGBuffer0, outGBuffer1, outGBuffer2 );
				#endif

				#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
					outShadowMask = UnityGetRawBakedOcclusions (IN.lmap.xy, float3(0, 0, 0));
				#endif
				#ifndef UNITY_HDR_ON
					outEmission.rgb = exp2(-outEmission.rgb);
				#endif
			}
			ENDCG
		}

	
	}
	
	
	Dependency "LightMode"="ForwardBase"

	Fallback Off
}
/*ASEBEGIN
Version=19603
Node;AmplifyShaderEditor.RangedFloatNode;2155;-1792,-5248;Half;False;Global;TVE_DEBUG_Layer;TVE_DEBUG_Layer;4;0;Create;True;0;5;Vertex Colors;100;Texture Coords;200;Vertex Postion;300;Vertex Normals;301;Vertex Tangents;302;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2013;-1792,-5312;Half;False;Global;TVE_DEBUG_Index;TVE_DEBUG_Index;4;0;Create;True;0;5;Vertex Colors;100;Texture Coords;200;Vertex Postion;300;Vertex Normals;301;Vertex Tangents;302;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1908;-1792,-5376;Half;False;Global;TVE_DEBUG_Type;TVE_DEBUG_Type;4;0;Create;True;0;5;Vertex Colors;100;Texture Coords;200;Vertex Postion;300;Vertex Normals;301;Vertex Tangents;302;0;True;2;Space(10);StyledEnum (Vertex Position _Vertex Normals _VertexTangents _Vertex Sign _Vertex Red (Variation) _Vertex Green (Occlusion) _Vertex Blue (Blend) _Vertex Alpha (Height) _Motion Bending _Motion Rolling _Motion Flutter);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2069;-1792,-4992;Half;False;Global;TVE_DEBUG_Min;TVE_DEBUG_Min;4;0;Create;True;0;5;Vertex Colors;100;Texture Coords;200;Vertex Postion;300;Vertex Normals;301;Vertex Tangents;302;0;True;2;Space(10);StyledEnum (Vertex Position _Vertex Normals _VertexTangents _Vertex Sign _Vertex Red (Variation) _Vertex Green (Occlusion) _Vertex Blue (Blend) _Vertex Alpha (Height) _Motion Bending _Motion Rolling _Motion Flutter);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2032;-1792,-5056;Half;False;Global;TVE_DEBUG_Clip;TVE_DEBUG_Clip;4;0;Create;True;0;5;Vertex Colors;100;Texture Coords;200;Vertex Postion;300;Vertex Normals;301;Vertex Tangents;302;0;True;2;Space(10);StyledEnum (Vertex Position _Vertex Normals _VertexTangents _Vertex Sign _Vertex Red (Variation) _Vertex Green (Occlusion) _Vertex Blue (Blend) _Vertex Alpha (Height) _Motion Bending _Motion Rolling _Motion Flutter);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2070;-1792,-4928;Half;False;Global;TVE_DEBUG_Max;TVE_DEBUG_Max;4;0;Create;True;0;5;Vertex Colors;100;Texture Coords;200;Vertex Postion;300;Vertex Normals;301;Vertex Tangents;302;0;True;2;Space(10);StyledEnum (Vertex Position _Vertex Normals _VertexTangents _Vertex Sign _Vertex Red (Variation) _Vertex Green (Occlusion) _Vertex Blue (Blend) _Vertex Alpha (Height) _Motion Bending _Motion Rolling _Motion Flutter);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1953;-1792,-5184;Half;False;Global;TVE_DEBUG_Filter;TVE_DEBUG_Filter;4;0;Create;True;0;5;Vertex Colors;100;Texture Coords;200;Vertex Postion;300;Vertex Normals;301;Vertex Tangents;302;0;True;2;Space(10);StyledEnum (Vertex Position _Vertex Normals _VertexTangents _Vertex Sign _Vertex Red (Variation) _Vertex Green (Occlusion) _Vertex Blue (Blend) _Vertex Alpha (Height) _Motion Bending _Motion Rolling _Motion Flutter);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2329;-1792,-5120;Half;False;Global;TVE_DEBUG_Shading;TVE_DEBUG_Shading;4;0;Create;True;0;5;Vertex Colors;100;Texture Coords;200;Vertex Postion;300;Vertex Normals;301;Vertex Tangents;302;0;True;2;Space(10);StyledEnum (Vertex Position _Vertex Normals _VertexTangents _Vertex Sign _Vertex Red (Variation) _Vertex Green (Occlusion) _Vertex Blue (Blend) _Vertex Alpha (Height) _Motion Bending _Motion Rolling _Motion Flutter);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1774;-880,2944;Inherit;False;True;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;1803;-1344,2944;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0.3;False;4;FLOAT;0.7;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1878;-1792,-5632;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Debug);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1772;-1088,3072;Float;False;Constant;_Float3;Float 3;31;0;Create;True;0;0;0;False;0;False;24;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1931;-1408,-5632;Half;False;Property;_DebugCategory;[ Debug Category ];145;0;Create;True;0;0;0;False;1;StyledCategory(Debug Settings, 5, 10);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;1843;-1632,2944;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1771;-1088,2944;Inherit;False;-1;;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;1800;-1472,2944;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1804;-1792,2944;Inherit;False;Constant;_Float1;Float 1;0;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1881;-1600,-5632;Half;False;Property;_Message;Message;146;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use this shader to debug the original mesh or the converted mesh attributes., 0,0);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;2203;-896,-5632;Inherit;False;Base Compile;-1;;73162;e67c8238031dbf04ab79a5d4d63d1b4f;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;2351;-1408,-5376;Inherit;False;Tool Debug;1;;126363;d48cde928c5068141abea1713047719b;1,1236,0;8;336;FLOAT;0;False;465;FLOAT;0;False;884;FLOAT;0;False;337;FLOAT;0;False;1652;FLOAT;0;False;624;FLOAT;0;False;720;FLOAT;0;False;722;FLOAT;0;False;1;COLOR;338
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2109;-896,-5376;Float;False;True;-1;2;;0;4;Hidden/BOXOPHOBIC/The Visual Engine/Helpers/Debug;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ForwardBase;0;1;ForwardBase;18;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;True;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=True=DisableBatching;True;7;False;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;False;0;;1;LightMode=ForwardBase;0;Standard;40;Workflow,InvertActionOnDeselection;1;0;Surface;0;0;  Blend;0;0;  Refraction Model;0;0;  Dither Shadows;1;0;Two Sided;0;638071577106831206;Deferred Pass;1;0;Transmission;0;0;  Transmission Shadow;0.5,False,;0;Translucency;0;0;  Translucency Strength;1,False,;0;  Normal Distortion;0.5,False,;0;  Scattering;2,False,;0;  Direct;0.9,False,;0;  Ambient;0.1,False,;0;  Shadow;0.5,False,;0;Cast Shadows;0;0;  Use Shadow Threshold;0;0;Receive Shadows;0;0;GPU Instancing;0;638141543866713469;LOD CrossFade;0;0;Built-in Fog;0;0;Ambient Light;0;0;Meta Pass;0;0;Add Pass;0;0;Override Baked GI;0;0;Extra Pre Pass;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Fwd Specular Highlights Toggle;0;0;Fwd Reflections Toggle;0;0;Disable Batching;1;0;Vertex Position,InvertActionOnDeselection;1;0;0;6;False;True;False;True;False;False;False;;True;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2112;-896,-5376;Float;False;False;-1;2;ASEMaterialInspector;0;4;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;Meta;0;4;Meta;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;True;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;False;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2113;-896,-5376;Float;False;False;-1;2;ASEMaterialInspector;0;4;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ShadowCaster;0;5;ShadowCaster;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;True;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;True;1;=;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2110;-896,-5376;Float;False;False;-1;2;ASEMaterialInspector;0;4;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ForwardAdd;0;2;ForwardAdd;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;True;4;1;False;;1;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;True;1;LightMode=ForwardAdd;False;False;0;True;1;LightMode=ForwardAdd;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2108;-896,-5376;Float;False;False;-1;2;ASEMaterialInspector;0;4;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ExtraPrePass;0;0;ExtraPrePass;6;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;True;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=ForwardBase;False;False;0;-1;59;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;=;LightMode=ForwardBase;=;=;=;=;=;=;=;=;=;=;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2111;-896,-5376;Float;False;False;-1;2;ASEMaterialInspector;0;4;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;Deferred;0;3;Deferred;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;True;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Deferred;True;2;False;0;False;0;0;Standard;0;False;0
WireConnection;1774;0;1771;0
WireConnection;1774;1;1772;0
WireConnection;1774;3;1803;0
WireConnection;1803;0;1800;0
WireConnection;1843;0;1804;0
WireConnection;1800;0;1843;0
WireConnection;2351;336;1908;0
WireConnection;2351;465;2013;0
WireConnection;2351;884;2155;0
WireConnection;2351;337;1953;0
WireConnection;2351;1652;2329;0
WireConnection;2351;624;2032;0
WireConnection;2351;720;2069;0
WireConnection;2351;722;2070;0
WireConnection;2109;2;2351;338
ASEEND*/
//CHKSM=BF6B7FF85E03D3A63111D8B80EF412AB056B75C8