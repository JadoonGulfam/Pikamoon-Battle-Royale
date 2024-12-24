// Upgrade NOTE: upgraded instancing buffer 'BOXOPHOBICTheVisualEngineElementsGlowEmissive' to new syntax.

// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Visual Engine/Elements/Glow Emissive"
{
	Properties
	{
		[StyledMessage(Info, Use the Emissive elements to control the emissive effect on vegetation or static props. Element Texture R and Particle Color R are used as value multipliers. Element Texture A and Particle Color A are used as alpha masks., 0, 15)]_ElementMessage("Element Message", Float) = 0
		[HideInInspector]_IsVersion("_IsVersion", Float) = 2040
		[HideInInspector]_IsElementShader("_IsElementShader", Float) = 1
		[StyledCategory(Render Settings, true, 0, 10)]_RenderCategory("[ Render Category ]", Float) = 1
		_ElementIntensity("Render Intensity", Range( 0 , 1)) = 1
		[StyledMessage(Info, When using a higher Layer number the Global Volume will create more render textures to render the elements. Try using fewer layers when possible., _ElementLayerMessage, 1, 10, 10)]_ElementLayerMessage("Render Layer Message", Float) = 0
		[StyledMessage(Warning, When using all layers the Global Volume will create one render texture for each layer to render the elements. Try using fewer layers when possible., _ElementLayerWarning, 1, 10, 10)]_ElementLayerWarning("Render Layer Warning", Float) = 0
		[StyledMask(Glow Layers, Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 7 Layer_8 8, 0, 0)]_ElementLayerMask("Render Layer", Float) = 1
		[HideInInspector]_ElementParams("Render Params", Vector) = (1,1,1,1)
		[Enum(Constant,0,Seasons,1)]_ElementMode("Render Mode", Float) = 0
		[StyledSpace(10)]_RenderEnd("[ Render End ]", Float) = 0
		[StyledCategory(Element Settings, true, 0, 10)]_ElementCategory("[ Element Category ]", Float) = 1
		[NoScaleOffset][StyledTextureSingleLine]_MainTex("Element Texture", 2D) = "white" {}
		[StyledSpace(10)]_SpaceTexture("#SpaceTexture", Float) = 0
		[Enum(Main UV,0,Planar,1)]_ElementUVsMode("Element Sampling", Float) = 0
		[StyledVector(9)]_MainUVs("Element UV Value", Vector) = (1,1,0,0)
		[StyledRemapSlider]_MainTexColorRemap("Element Value", Vector) = (0,1,0,0)
		[StyledRemapSlider]_MainTexAlphaRemap("Element Alpha", Vector) = (0,1,0,0)
		[StyledRemapSlider]_MainTexFalloffRemap("Element Falloff", Vector) = (0,0,0,0)
		[HDR][Gamma][Space(10)]_MainColor("Element Value", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HDR][Gamma][Space(10)]_AdditionalColor1("Winter Value", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HDR][Gamma]_AdditionalColor2("Spring Value", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HDR][Gamma]_AdditionalColor3("Summer Value", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HDR][Gamma]_AdditionalColor4("Autumn Value", Color) = (0.5019608,0.5019608,0.5019608,1)
		[Space(10)][StyledRemapSlider]_SeasonRemap("Seasons Curve", Vector) = (0,1,0,0)
		[Space(10)]_SpeedTresholdValue("Particle Speed Treshold", Float) = 10
		[StyledSpace(10)]_ElementEnd("[ Element End ]", Float) = 0
		[StyledCategory(Fading Settings, true, 0, 10)]_FadeCategory("[ Fade Category ]", Float) = 1
		[Enum(Off,0,On,1)]_ElementVolumeFadeMode("Bounds Fade", Float) = 0
		_ElementVolumeFadeValue("Bounds Fade Blend", Range( 0 , 1)) = 0.75
		[HDR][Enum(Off,0,On,1)][Space(10)]_ElementRaycastMode("Raycast Fade", Float) = 0
		[StyledLayers()]_RaycastLayerMask("Raycast Layer", Float) = 1
		_RaycastDistanceMaxValue("Raycast Limit", Float) = 2
		[StyledSpace(10)]_FadeEnd("[ Fade End ]", Float) = 0

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "DisableBatching"="True" }
	LOD 100

		
		Pass
		{
			
			Name "VolumePass"
			
			CGINCLUDE
			#pragma target 3.0
			ENDCG
			Blend SrcAlpha OneMinusSrcAlpha
			AlphaToMask Off
			Cull Back
			ColorMask RGB
			ZWrite Off
			ZTest LEqual
			
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#define ASE_NEEDS_FRAG_COLOR
			#define ASE_NEEDS_FRAG_WORLD_POSITION


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _IsVersion;
			uniform half _IsElementShader;
			uniform half _RenderCategory;
			uniform half _ElementLayerMessage;
			uniform half _ElementLayerWarning;
			uniform half _ElementLayerMask;
			uniform half4 _MainTexColorRemap;
			uniform half _RaycastLayerMask;
			uniform half _RaycastDistanceMaxValue;
			uniform half _ElementRaycastMode;
			uniform half _FadeEnd;
			uniform half _SpaceTexture;
			uniform half _FadeCategory;
			uniform half _ElementCategory;
			uniform half _ElementEnd;
			uniform half _RenderEnd;
			uniform float _SpeedTresholdValue;
			uniform half _ElementMessage;
			uniform half4 _MainColor;
			uniform half4 TVE_SeasonOption;
			uniform half4 _AdditionalColor1;
			uniform half4 _AdditionalColor2;
			uniform half TVE_SeasonLerp;
			uniform half4 _SeasonRemap;
			uniform half4 _AdditionalColor3;
			uniform half4 _AdditionalColor4;
			uniform float _ElementMode;
			uniform sampler2D _MainTex;
			uniform float _ElementUVsMode;
			uniform half4 _MainUVs;
			uniform float _ElementIntensity;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 _MainTexFalloffRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform float _ElementVolumeFadeValue;
			uniform float _ElementVolumeFadeMode;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsGlowEmissive)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsGlowEmissive
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsGlowEmissive)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				half TVE_SeasonOptions_X50_g23638 = TVE_SeasonOption.x;
				half4 Color_Winter_RGBA58_g23638 = _AdditionalColor1;
				half4 Color_Spring_RGBA59_g23638 = _AdditionalColor2;
				float temp_output_7_0_g23659 = _SeasonRemap.x;
				float temp_output_10_0_g23659 = ( _SeasonRemap.y - temp_output_7_0_g23659 );
				half TVE_SeasonLerp54_g23638 = saturate( saturate( ( ( TVE_SeasonLerp - temp_output_7_0_g23659 ) / temp_output_10_0_g23659 ) ) );
				float4 lerpResult13_g23638 = lerp( Color_Winter_RGBA58_g23638 , Color_Spring_RGBA59_g23638 , TVE_SeasonLerp54_g23638);
				half TVE_SeasonOptions_Y51_g23638 = TVE_SeasonOption.y;
				half4 Color_Summer_RGBA60_g23638 = _AdditionalColor3;
				float4 lerpResult14_g23638 = lerp( Color_Spring_RGBA59_g23638 , Color_Summer_RGBA60_g23638 , TVE_SeasonLerp54_g23638);
				half TVE_SeasonOptions_Z52_g23638 = TVE_SeasonOption.z;
				half4 Color_Autumn_RGBA61_g23638 = _AdditionalColor4;
				float4 lerpResult15_g23638 = lerp( Color_Summer_RGBA60_g23638 , Color_Autumn_RGBA61_g23638 , TVE_SeasonLerp54_g23638);
				half TVE_SeasonOptions_W53_g23638 = TVE_SeasonOption.w;
				float4 lerpResult12_g23638 = lerp( Color_Autumn_RGBA61_g23638 , Color_Winter_RGBA58_g23638 , TVE_SeasonLerp54_g23638);
				float4 vertexToFrag11_g23639 = ( ( TVE_SeasonOptions_X50_g23638 * lerpResult13_g23638 ) + ( TVE_SeasonOptions_Y51_g23638 * lerpResult14_g23638 ) + ( TVE_SeasonOptions_Z52_g23638 * lerpResult15_g23638 ) + ( TVE_SeasonOptions_W53_g23638 * lerpResult12_g23638 ) );
				o.ase_texcoord1 = vertexToFrag11_g23639;
				float3 ase_worldPos = mul(unity_ObjectToWorld, float4( (v.vertex).xyz, 1 )).xyz;
				float2 appendResult1900_g23638 = (float2(ase_worldPos.x , ase_worldPos.z));
				float2 lerpResult1899_g23638 = lerp( ( 1.0 - v.ase_texcoord.xy ) , appendResult1900_g23638 , _ElementUVsMode);
				float2 vertexToFrag11_g23670 = ( ( lerpResult1899_g23638 * (_MainUVs).xy ) + (_MainUVs).zw );
				o.ase_texcoord2.xy = vertexToFrag11_g23670;
				
				o.ase_color = v.color;
				o.ase_texcoord2.zw = v.ase_texcoord.xy;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				half4 Color_Main_RGBA49_g23638 = _MainColor;
				float4 vertexToFrag11_g23639 = i.ase_texcoord1;
				half4 Color_Seasons1715_g23638 = vertexToFrag11_g23639;
				half Element_Mode55_g23638 = _ElementMode;
				float4 lerpResult30_g23638 = lerp( Color_Main_RGBA49_g23638 , Color_Seasons1715_g23638 , Element_Mode55_g23638);
				float2 vertexToFrag11_g23670 = i.ase_texcoord2.xy;
				half4 MainTex_RGBA587_g23638 = tex2D( _MainTex, vertexToFrag11_g23670 );
				float3 temp_cast_0 = (0.0001).xxx;
				float3 temp_cast_1 = (0.9999).xxx;
				float3 clampResult17_g23664 = clamp( (MainTex_RGBA587_g23638).rgb , temp_cast_0 , temp_cast_1 );
				float temp_output_7_0_g23648 = _MainTexColorRemap.x;
				float3 temp_cast_2 = (temp_output_7_0_g23648).xxx;
				float temp_output_10_0_g23648 = ( _MainTexColorRemap.y - temp_output_7_0_g23648 );
				float3 temp_output_1765_0_g23638 = saturate( ( ( clampResult17_g23664 - temp_cast_2 ) / temp_output_10_0_g23648 ) );
				half3 Element_Remap_RGB1555_g23638 = temp_output_1765_0_g23638;
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half3 Element_Params_RGB1735_g23638 = (_ElementParams_Instance).xyz;
				half3 Element_Color1756_g23638 = ( Element_Remap_RGB1555_g23638 * Element_Params_RGB1735_g23638 * (i.ase_color).rgb );
				half3 Final_Colors_RGB142_g23638 = ( (lerpResult30_g23638).rgb * Element_Color1756_g23638 );
				float clampResult17_g23665 = clamp( (MainTex_RGBA587_g23638).a , 0.0001 , 0.9999 );
				float temp_output_7_0_g23647 = _MainTexAlphaRemap.x;
				float temp_output_10_0_g23647 = ( _MainTexAlphaRemap.y - temp_output_7_0_g23647 );
				half Element_Remap_A74_g23638 = saturate( ( ( clampResult17_g23665 - temp_output_7_0_g23647 ) / ( temp_output_10_0_g23647 + 0.0001 ) ) );
				half Element_Params_A1737_g23638 = _ElementParams_Instance.w;
				float clampResult17_g23645 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord2.zw*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				float temp_output_7_0_g23646 = _MainTexFalloffRemap.x;
				float temp_output_10_0_g23646 = ( _MainTexFalloffRemap.y - temp_output_7_0_g23646 );
				half Element_Falloff1883_g23638 = saturate( ( ( clampResult17_g23645 - temp_output_7_0_g23646 ) / ( temp_output_10_0_g23646 + 0.0001 ) ) );
				float temp_output_7_0_g23669 = 1.0;
				float temp_output_10_0_g23669 = ( _ElementVolumeFadeValue - temp_output_7_0_g23669 );
				float lerpResult18_g23667 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g23669 ) / temp_output_10_0_g23669 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g23638 = ( _ElementIntensity * Element_Remap_A74_g23638 * Element_Params_A1737_g23638 * i.ase_color.a * Element_Falloff1883_g23638 * lerpResult18_g23667 );
				half Final_Colors_A144_g23638 = ( (lerpResult30_g23638).a * Element_Alpha56_g23638 );
				float4 appendResult470_g23638 = (float4(Final_Colors_RGB142_g23638 , Final_Colors_A144_g23638));
				
				
				finalColor = appendResult470_g23638;
				return finalColor;
			}
			ENDCG
		}
		
		
		Pass
		{
			
			Name "VisualPass"
			
			CGINCLUDE
			#pragma target 3.0
			ENDCG
			Blend SrcAlpha OneMinusSrcAlpha
			AlphaToMask Off
			Cull Back
			ColorMask RGBA
			ZWrite Off
			ZTest LEqual
			
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#define ASE_NEEDS_FRAG_COLOR
			#define ASE_NEEDS_FRAG_WORLD_POSITION


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _IsVersion;
			uniform half _IsElementShader;
			uniform half _RenderCategory;
			uniform half _ElementLayerMessage;
			uniform half _ElementLayerWarning;
			uniform half _ElementLayerMask;
			uniform half4 _MainTexColorRemap;
			uniform half _RaycastLayerMask;
			uniform half _RaycastDistanceMaxValue;
			uniform half _ElementRaycastMode;
			uniform half _FadeEnd;
			uniform half _SpaceTexture;
			uniform half _FadeCategory;
			uniform half _ElementCategory;
			uniform half _ElementEnd;
			uniform half _RenderEnd;
			uniform float _SpeedTresholdValue;
			uniform half _ElementMessage;
			uniform half4 _MainColor;
			uniform half4 TVE_SeasonOption;
			uniform half4 _AdditionalColor1;
			uniform half4 _AdditionalColor2;
			uniform half TVE_SeasonLerp;
			uniform half4 _SeasonRemap;
			uniform half4 _AdditionalColor3;
			uniform half4 _AdditionalColor4;
			uniform float _ElementMode;
			uniform sampler2D _MainTex;
			uniform float _ElementUVsMode;
			uniform half4 _MainUVs;
			uniform float _ElementIntensity;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 _MainTexFalloffRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform float _ElementVolumeFadeValue;
			uniform float _ElementVolumeFadeMode;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsGlowEmissive)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsGlowEmissive
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsGlowEmissive)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				half TVE_SeasonOptions_X50_g23638 = TVE_SeasonOption.x;
				half4 Color_Winter_RGBA58_g23638 = _AdditionalColor1;
				half4 Color_Spring_RGBA59_g23638 = _AdditionalColor2;
				float temp_output_7_0_g23659 = _SeasonRemap.x;
				float temp_output_10_0_g23659 = ( _SeasonRemap.y - temp_output_7_0_g23659 );
				half TVE_SeasonLerp54_g23638 = saturate( saturate( ( ( TVE_SeasonLerp - temp_output_7_0_g23659 ) / temp_output_10_0_g23659 ) ) );
				float4 lerpResult13_g23638 = lerp( Color_Winter_RGBA58_g23638 , Color_Spring_RGBA59_g23638 , TVE_SeasonLerp54_g23638);
				half TVE_SeasonOptions_Y51_g23638 = TVE_SeasonOption.y;
				half4 Color_Summer_RGBA60_g23638 = _AdditionalColor3;
				float4 lerpResult14_g23638 = lerp( Color_Spring_RGBA59_g23638 , Color_Summer_RGBA60_g23638 , TVE_SeasonLerp54_g23638);
				half TVE_SeasonOptions_Z52_g23638 = TVE_SeasonOption.z;
				half4 Color_Autumn_RGBA61_g23638 = _AdditionalColor4;
				float4 lerpResult15_g23638 = lerp( Color_Summer_RGBA60_g23638 , Color_Autumn_RGBA61_g23638 , TVE_SeasonLerp54_g23638);
				half TVE_SeasonOptions_W53_g23638 = TVE_SeasonOption.w;
				float4 lerpResult12_g23638 = lerp( Color_Autumn_RGBA61_g23638 , Color_Winter_RGBA58_g23638 , TVE_SeasonLerp54_g23638);
				float4 vertexToFrag11_g23639 = ( ( TVE_SeasonOptions_X50_g23638 * lerpResult13_g23638 ) + ( TVE_SeasonOptions_Y51_g23638 * lerpResult14_g23638 ) + ( TVE_SeasonOptions_Z52_g23638 * lerpResult15_g23638 ) + ( TVE_SeasonOptions_W53_g23638 * lerpResult12_g23638 ) );
				o.ase_texcoord1 = vertexToFrag11_g23639;
				float3 ase_worldPos = mul(unity_ObjectToWorld, float4( (v.vertex).xyz, 1 )).xyz;
				float2 appendResult1900_g23638 = (float2(ase_worldPos.x , ase_worldPos.z));
				float2 lerpResult1899_g23638 = lerp( ( 1.0 - v.ase_texcoord.xy ) , appendResult1900_g23638 , _ElementUVsMode);
				float2 vertexToFrag11_g23670 = ( ( lerpResult1899_g23638 * (_MainUVs).xy ) + (_MainUVs).zw );
				o.ase_texcoord2.xy = vertexToFrag11_g23670;
				
				o.ase_color = v.color;
				o.ase_texcoord2.zw = v.ase_texcoord.xy;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				half4 Color_Main_RGBA49_g23638 = _MainColor;
				float4 vertexToFrag11_g23639 = i.ase_texcoord1;
				half4 Color_Seasons1715_g23638 = vertexToFrag11_g23639;
				half Element_Mode55_g23638 = _ElementMode;
				float4 lerpResult30_g23638 = lerp( Color_Main_RGBA49_g23638 , Color_Seasons1715_g23638 , Element_Mode55_g23638);
				float2 vertexToFrag11_g23670 = i.ase_texcoord2.xy;
				half4 MainTex_RGBA587_g23638 = tex2D( _MainTex, vertexToFrag11_g23670 );
				float3 temp_cast_0 = (0.0001).xxx;
				float3 temp_cast_1 = (0.9999).xxx;
				float3 clampResult17_g23664 = clamp( (MainTex_RGBA587_g23638).rgb , temp_cast_0 , temp_cast_1 );
				float temp_output_7_0_g23648 = _MainTexColorRemap.x;
				float3 temp_cast_2 = (temp_output_7_0_g23648).xxx;
				float temp_output_10_0_g23648 = ( _MainTexColorRemap.y - temp_output_7_0_g23648 );
				float3 temp_output_1765_0_g23638 = saturate( ( ( clampResult17_g23664 - temp_cast_2 ) / temp_output_10_0_g23648 ) );
				half3 Element_Remap_RGB1555_g23638 = temp_output_1765_0_g23638;
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half3 Element_Params_RGB1735_g23638 = (_ElementParams_Instance).xyz;
				half3 Element_Color1756_g23638 = ( Element_Remap_RGB1555_g23638 * Element_Params_RGB1735_g23638 * (i.ase_color).rgb );
				half3 Final_Colors_RGB142_g23638 = ( (lerpResult30_g23638).rgb * Element_Color1756_g23638 );
				half3 Input_Color94_g23649 = Final_Colors_RGB142_g23638;
				half3 Element_Valid47_g23649 = Input_Color94_g23649;
				float clampResult17_g23665 = clamp( (MainTex_RGBA587_g23638).a , 0.0001 , 0.9999 );
				float temp_output_7_0_g23647 = _MainTexAlphaRemap.x;
				float temp_output_10_0_g23647 = ( _MainTexAlphaRemap.y - temp_output_7_0_g23647 );
				half Element_Remap_A74_g23638 = saturate( ( ( clampResult17_g23665 - temp_output_7_0_g23647 ) / ( temp_output_10_0_g23647 + 0.0001 ) ) );
				half Element_Params_A1737_g23638 = _ElementParams_Instance.w;
				float clampResult17_g23645 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord2.zw*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				float temp_output_7_0_g23646 = _MainTexFalloffRemap.x;
				float temp_output_10_0_g23646 = ( _MainTexFalloffRemap.y - temp_output_7_0_g23646 );
				half Element_Falloff1883_g23638 = saturate( ( ( clampResult17_g23645 - temp_output_7_0_g23646 ) / ( temp_output_10_0_g23646 + 0.0001 ) ) );
				float temp_output_7_0_g23669 = 1.0;
				float temp_output_10_0_g23669 = ( _ElementVolumeFadeValue - temp_output_7_0_g23669 );
				float lerpResult18_g23667 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g23669 ) / temp_output_10_0_g23669 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g23638 = ( _ElementIntensity * Element_Remap_A74_g23638 * Element_Params_A1737_g23638 * i.ase_color.a * Element_Falloff1883_g23638 * lerpResult18_g23667 );
				half Final_Colors_A144_g23638 = ( (lerpResult30_g23638).a * Element_Alpha56_g23638 );
				half Input_Alpha48_g23649 = Final_Colors_A144_g23638;
				float4 appendResult58_g23649 = (float4(Element_Valid47_g23649 , Input_Alpha48_g23649));
				
				
				finalColor = appendResult58_g23649;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "TVEShaderGUIElement"
	
	Fallback Off
}
/*ASEBEGIN
Version=19603
Node;AmplifyShaderEditor.FunctionNode;144;-640,-1152;Inherit;False;Element Glow;1;;23635;3d1b37ca7ef3f9c4f9ae756ac35720e3;0;0;1;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;131;-64,-1408;Inherit;False;Element Compile;-1;;23637;5302407fc6d65554791e558e67d59358;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;142;-384,-1152;Inherit;False;Element Shader;10;;23638;0e972c73cae2ee54ea51acc9738801d0;12,477,0,1778,0,478,0,1824,0,1814,0,145,0,1784,0,1935,1,1933,1,481,0,1904,1,1907,1;1;1974;FLOAT;1;False;2;FLOAT4;0;FLOAT4;1779
Node;AmplifyShaderEditor.RangedFloatNode;111;-640,-1408;Half;False;Property;_ElementMessage;Element Message;0;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use the Emissive elements to control the emissive effect on vegetation or static props. Element Texture R and Particle Color R are used as value multipliers. Element Texture A and Particle Color A are used as alpha masks., 0, 15);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;132;-64,-1152;Float;False;True;-1;2;TVEShaderGUIElement;100;16;BOXOPHOBIC/The Visual Engine/Elements/Glow Emissive;f4f273c3bb6262d4396be458405e60f9;True;VolumePass;0;0;VolumePass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;DisableBatching=True=DisableBatching;False;False;0;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;True;True;True;True;True;False;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;2;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;133;-64,-1024;Float;False;False;-1;2;ASEMaterialInspector;100;16;New Amplify Shader;f4f273c3bb6262d4396be458405e60f9;True;VisualPass;0;1;VisualPass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;False;False;0;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;0;False;0
WireConnection;142;1974;144;4
WireConnection;132;0;142;0
WireConnection;133;0;142;1779
ASEEND*/
//CHKSM=5EC3C32A74762125D5658833A06BCE582455A114