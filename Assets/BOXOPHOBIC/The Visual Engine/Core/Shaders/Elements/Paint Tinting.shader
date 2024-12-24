// Upgrade NOTE: upgraded instancing buffer 'BOXOPHOBICTheVisualEngineElementsPaintTinting' to new syntax.

// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Visual Engine/Elements/Paint Tinting"
{
	Properties
	{
		[StyledMessage(Info, Use the Tinting elements to add color tinting to the vegetation assets. Element Texture RGB and Particle Color RGB are used as value multipliers. Element Texture A and Particle Color A are used as alpha masks., 0, 15)]_ElementMessage("Element Message", Float) = 0
		[HideInInspector]_IsVersion("_IsVersion", Float) = 2040
		[HideInInspector]_IsElementShader("_IsElementShader", Float) = 1
		[StyledCategory(Render Settings, true, 0, 10)]_RenderCategory("[ Render Category ]", Float) = 1
		_ElementIntensity("Render Intensity", Range( 0 , 1)) = 1
		[StyledMessage(Info, When using a higher Layer number the Global Volume will create more render textures to render the elements. Try using fewer layers when possible., _ElementLayerMessage, 1, 10, 10)]_ElementLayerMessage("Render Layer Message", Float) = 0
		[StyledMessage(Warning, When using all layers the Global Volume will create one render texture for each layer to render the elements. Try using fewer layers when possible., _ElementLayerWarning, 1, 10, 10)]_ElementLayerWarning("Render Layer Warning", Float) = 0
		[StyledMask(Paint Layers, Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 7 Layer_8 8, 0, 0)]_ElementLayerMask("Render Layer", Float) = 1
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
		[HideInInspector]_render_colormask("_render_colormask", Float) = 15

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
			Blend SrcAlpha OneMinusSrcAlpha, One One
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
			uniform half _render_colormask;
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
			uniform half _ElementMode;
			uniform sampler2D _MainTex;
			uniform half _ElementUVsMode;
			uniform half4 _MainUVs;
			uniform half _ElementIntensity;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 _MainTexFalloffRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform half _ElementVolumeFadeValue;
			uniform half _ElementVolumeFadeMode;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsPaintTinting)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsPaintTinting
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsPaintTinting)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				half TVE_SeasonOptions_X50_g25572 = TVE_SeasonOption.x;
				half4 Color_Winter_RGBA58_g25572 = _AdditionalColor1;
				half4 Color_Spring_RGBA59_g25572 = _AdditionalColor2;
				half temp_output_7_0_g25593 = _SeasonRemap.x;
				half temp_output_10_0_g25593 = ( _SeasonRemap.y - temp_output_7_0_g25593 );
				half TVE_SeasonLerp54_g25572 = saturate( saturate( ( ( TVE_SeasonLerp - temp_output_7_0_g25593 ) / temp_output_10_0_g25593 ) ) );
				half4 lerpResult13_g25572 = lerp( Color_Winter_RGBA58_g25572 , Color_Spring_RGBA59_g25572 , TVE_SeasonLerp54_g25572);
				half TVE_SeasonOptions_Y51_g25572 = TVE_SeasonOption.y;
				half4 Color_Summer_RGBA60_g25572 = _AdditionalColor3;
				half4 lerpResult14_g25572 = lerp( Color_Spring_RGBA59_g25572 , Color_Summer_RGBA60_g25572 , TVE_SeasonLerp54_g25572);
				half TVE_SeasonOptions_Z52_g25572 = TVE_SeasonOption.z;
				half4 Color_Autumn_RGBA61_g25572 = _AdditionalColor4;
				half4 lerpResult15_g25572 = lerp( Color_Summer_RGBA60_g25572 , Color_Autumn_RGBA61_g25572 , TVE_SeasonLerp54_g25572);
				half TVE_SeasonOptions_W53_g25572 = TVE_SeasonOption.w;
				half4 lerpResult12_g25572 = lerp( Color_Autumn_RGBA61_g25572 , Color_Winter_RGBA58_g25572 , TVE_SeasonLerp54_g25572);
				half4 vertexToFrag11_g25573 = ( ( TVE_SeasonOptions_X50_g25572 * lerpResult13_g25572 ) + ( TVE_SeasonOptions_Y51_g25572 * lerpResult14_g25572 ) + ( TVE_SeasonOptions_Z52_g25572 * lerpResult15_g25572 ) + ( TVE_SeasonOptions_W53_g25572 * lerpResult12_g25572 ) );
				o.ase_texcoord1 = vertexToFrag11_g25573;
				float3 ase_worldPos = mul(unity_ObjectToWorld, float4( (v.vertex).xyz, 1 )).xyz;
				half2 appendResult1900_g25572 = (half2(ase_worldPos.x , ase_worldPos.z));
				half2 lerpResult1899_g25572 = lerp( ( 1.0 - v.ase_texcoord.xy ) , appendResult1900_g25572 , _ElementUVsMode);
				half2 vertexToFrag11_g25604 = ( ( lerpResult1899_g25572 * (_MainUVs).xy ) + (_MainUVs).zw );
				o.ase_texcoord2.xy = vertexToFrag11_g25604;
				
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
				half4 Color_Main_RGBA49_g25572 = _MainColor;
				half4 vertexToFrag11_g25573 = i.ase_texcoord1;
				half4 Color_Seasons1715_g25572 = vertexToFrag11_g25573;
				half Element_Mode55_g25572 = _ElementMode;
				half4 lerpResult30_g25572 = lerp( Color_Main_RGBA49_g25572 , Color_Seasons1715_g25572 , Element_Mode55_g25572);
				half2 vertexToFrag11_g25604 = i.ase_texcoord2.xy;
				half4 MainTex_RGBA587_g25572 = tex2D( _MainTex, vertexToFrag11_g25604 );
				half3 temp_cast_0 = (0.0001).xxx;
				half3 temp_cast_1 = (0.9999).xxx;
				half3 clampResult17_g25598 = clamp( (MainTex_RGBA587_g25572).rgb , temp_cast_0 , temp_cast_1 );
				half temp_output_7_0_g25582 = _MainTexColorRemap.x;
				half3 temp_cast_2 = (temp_output_7_0_g25582).xxx;
				half temp_output_10_0_g25582 = ( _MainTexColorRemap.y - temp_output_7_0_g25582 );
				half3 temp_output_1765_0_g25572 = saturate( ( ( clampResult17_g25598 - temp_cast_2 ) / temp_output_10_0_g25582 ) );
				half3 Element_Remap_RGB1555_g25572 = temp_output_1765_0_g25572;
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half3 Element_Params_RGB1735_g25572 = (_ElementParams_Instance).xyz;
				half3 Element_Color1756_g25572 = ( Element_Remap_RGB1555_g25572 * Element_Params_RGB1735_g25572 * (i.ase_color).rgb );
				half3 Final_Colors_RGB142_g25572 = ( (lerpResult30_g25572).rgb * Element_Color1756_g25572 );
				half clampResult17_g25599 = clamp( (MainTex_RGBA587_g25572).a , 0.0001 , 0.9999 );
				half temp_output_7_0_g25581 = _MainTexAlphaRemap.x;
				half temp_output_10_0_g25581 = ( _MainTexAlphaRemap.y - temp_output_7_0_g25581 );
				half Element_Remap_A74_g25572 = saturate( ( ( clampResult17_g25599 - temp_output_7_0_g25581 ) / ( temp_output_10_0_g25581 + 0.0001 ) ) );
				half Element_Params_A1737_g25572 = _ElementParams_Instance.w;
				half clampResult17_g25579 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord2.zw*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				half temp_output_7_0_g25580 = _MainTexFalloffRemap.x;
				half temp_output_10_0_g25580 = ( _MainTexFalloffRemap.y - temp_output_7_0_g25580 );
				half Element_Falloff1883_g25572 = saturate( ( ( clampResult17_g25579 - temp_output_7_0_g25580 ) / ( temp_output_10_0_g25580 + 0.0001 ) ) );
				half temp_output_7_0_g25603 = 1.0;
				half temp_output_10_0_g25603 = ( _ElementVolumeFadeValue - temp_output_7_0_g25603 );
				half lerpResult18_g25601 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g25603 ) / temp_output_10_0_g25603 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g25572 = ( _ElementIntensity * Element_Remap_A74_g25572 * Element_Params_A1737_g25572 * i.ase_color.a * Element_Falloff1883_g25572 * lerpResult18_g25601 );
				half Final_Colors_A144_g25572 = ( (lerpResult30_g25572).a * Element_Alpha56_g25572 );
				half4 appendResult470_g25572 = (half4(Final_Colors_RGB142_g25572 , Final_Colors_A144_g25572));
				
				
				finalColor = appendResult470_g25572;
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
			uniform half _render_colormask;
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
			uniform half _SpeedTresholdValue;
			uniform half _ElementMessage;
			uniform half4 _MainColor;
			uniform half4 TVE_SeasonOption;
			uniform half4 _AdditionalColor1;
			uniform half4 _AdditionalColor2;
			uniform half TVE_SeasonLerp;
			uniform half4 _SeasonRemap;
			uniform half4 _AdditionalColor3;
			uniform half4 _AdditionalColor4;
			uniform half _ElementMode;
			uniform sampler2D _MainTex;
			uniform half _ElementUVsMode;
			uniform half4 _MainUVs;
			uniform half _ElementIntensity;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 _MainTexFalloffRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform half _ElementVolumeFadeValue;
			uniform half _ElementVolumeFadeMode;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsPaintTinting)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsPaintTinting
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsPaintTinting)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				half TVE_SeasonOptions_X50_g25572 = TVE_SeasonOption.x;
				half4 Color_Winter_RGBA58_g25572 = _AdditionalColor1;
				half4 Color_Spring_RGBA59_g25572 = _AdditionalColor2;
				half temp_output_7_0_g25593 = _SeasonRemap.x;
				half temp_output_10_0_g25593 = ( _SeasonRemap.y - temp_output_7_0_g25593 );
				half TVE_SeasonLerp54_g25572 = saturate( saturate( ( ( TVE_SeasonLerp - temp_output_7_0_g25593 ) / temp_output_10_0_g25593 ) ) );
				half4 lerpResult13_g25572 = lerp( Color_Winter_RGBA58_g25572 , Color_Spring_RGBA59_g25572 , TVE_SeasonLerp54_g25572);
				half TVE_SeasonOptions_Y51_g25572 = TVE_SeasonOption.y;
				half4 Color_Summer_RGBA60_g25572 = _AdditionalColor3;
				half4 lerpResult14_g25572 = lerp( Color_Spring_RGBA59_g25572 , Color_Summer_RGBA60_g25572 , TVE_SeasonLerp54_g25572);
				half TVE_SeasonOptions_Z52_g25572 = TVE_SeasonOption.z;
				half4 Color_Autumn_RGBA61_g25572 = _AdditionalColor4;
				half4 lerpResult15_g25572 = lerp( Color_Summer_RGBA60_g25572 , Color_Autumn_RGBA61_g25572 , TVE_SeasonLerp54_g25572);
				half TVE_SeasonOptions_W53_g25572 = TVE_SeasonOption.w;
				half4 lerpResult12_g25572 = lerp( Color_Autumn_RGBA61_g25572 , Color_Winter_RGBA58_g25572 , TVE_SeasonLerp54_g25572);
				half4 vertexToFrag11_g25573 = ( ( TVE_SeasonOptions_X50_g25572 * lerpResult13_g25572 ) + ( TVE_SeasonOptions_Y51_g25572 * lerpResult14_g25572 ) + ( TVE_SeasonOptions_Z52_g25572 * lerpResult15_g25572 ) + ( TVE_SeasonOptions_W53_g25572 * lerpResult12_g25572 ) );
				o.ase_texcoord1 = vertexToFrag11_g25573;
				float3 ase_worldPos = mul(unity_ObjectToWorld, float4( (v.vertex).xyz, 1 )).xyz;
				half2 appendResult1900_g25572 = (half2(ase_worldPos.x , ase_worldPos.z));
				half2 lerpResult1899_g25572 = lerp( ( 1.0 - v.ase_texcoord.xy ) , appendResult1900_g25572 , _ElementUVsMode);
				half2 vertexToFrag11_g25604 = ( ( lerpResult1899_g25572 * (_MainUVs).xy ) + (_MainUVs).zw );
				o.ase_texcoord2.xy = vertexToFrag11_g25604;
				
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
				half4 Color_Main_RGBA49_g25572 = _MainColor;
				half4 vertexToFrag11_g25573 = i.ase_texcoord1;
				half4 Color_Seasons1715_g25572 = vertexToFrag11_g25573;
				half Element_Mode55_g25572 = _ElementMode;
				half4 lerpResult30_g25572 = lerp( Color_Main_RGBA49_g25572 , Color_Seasons1715_g25572 , Element_Mode55_g25572);
				half2 vertexToFrag11_g25604 = i.ase_texcoord2.xy;
				half4 MainTex_RGBA587_g25572 = tex2D( _MainTex, vertexToFrag11_g25604 );
				half3 temp_cast_0 = (0.0001).xxx;
				half3 temp_cast_1 = (0.9999).xxx;
				half3 clampResult17_g25598 = clamp( (MainTex_RGBA587_g25572).rgb , temp_cast_0 , temp_cast_1 );
				half temp_output_7_0_g25582 = _MainTexColorRemap.x;
				half3 temp_cast_2 = (temp_output_7_0_g25582).xxx;
				half temp_output_10_0_g25582 = ( _MainTexColorRemap.y - temp_output_7_0_g25582 );
				half3 temp_output_1765_0_g25572 = saturate( ( ( clampResult17_g25598 - temp_cast_2 ) / temp_output_10_0_g25582 ) );
				half3 Element_Remap_RGB1555_g25572 = temp_output_1765_0_g25572;
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half3 Element_Params_RGB1735_g25572 = (_ElementParams_Instance).xyz;
				half3 Element_Color1756_g25572 = ( Element_Remap_RGB1555_g25572 * Element_Params_RGB1735_g25572 * (i.ase_color).rgb );
				half3 Final_Colors_RGB142_g25572 = ( (lerpResult30_g25572).rgb * Element_Color1756_g25572 );
				half3 Input_Color94_g25583 = Final_Colors_RGB142_g25572;
				half3 Element_Valid47_g25583 = Input_Color94_g25583;
				half clampResult17_g25599 = clamp( (MainTex_RGBA587_g25572).a , 0.0001 , 0.9999 );
				half temp_output_7_0_g25581 = _MainTexAlphaRemap.x;
				half temp_output_10_0_g25581 = ( _MainTexAlphaRemap.y - temp_output_7_0_g25581 );
				half Element_Remap_A74_g25572 = saturate( ( ( clampResult17_g25599 - temp_output_7_0_g25581 ) / ( temp_output_10_0_g25581 + 0.0001 ) ) );
				half Element_Params_A1737_g25572 = _ElementParams_Instance.w;
				half clampResult17_g25579 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord2.zw*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				half temp_output_7_0_g25580 = _MainTexFalloffRemap.x;
				half temp_output_10_0_g25580 = ( _MainTexFalloffRemap.y - temp_output_7_0_g25580 );
				half Element_Falloff1883_g25572 = saturate( ( ( clampResult17_g25579 - temp_output_7_0_g25580 ) / ( temp_output_10_0_g25580 + 0.0001 ) ) );
				half temp_output_7_0_g25603 = 1.0;
				half temp_output_10_0_g25603 = ( _ElementVolumeFadeValue - temp_output_7_0_g25603 );
				half lerpResult18_g25601 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g25603 ) / temp_output_10_0_g25603 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g25572 = ( _ElementIntensity * Element_Remap_A74_g25572 * Element_Params_A1737_g25572 * i.ase_color.a * Element_Falloff1883_g25572 * lerpResult18_g25601 );
				half Final_Colors_A144_g25572 = ( (lerpResult30_g25572).a * Element_Alpha56_g25572 );
				half Input_Alpha48_g25583 = Final_Colors_A144_g25572;
				half4 appendResult58_g25583 = (half4(Element_Valid47_g25583 , Input_Alpha48_g25583));
				
				
				finalColor = appendResult58_g25583;
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
Node;AmplifyShaderEditor.FunctionNode;402;-640,-384;Inherit;False;Element Paint;1;;25569;5810d2854679b554b88f8bb18ff3bfa0;0;0;1;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;248;-64,-768;Inherit;False;Element Compile;-1;;25571;5302407fc6d65554791e558e67d59358;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;178;-640,-640;Half;False;Property;_render_colormask;_render_colormask;70;1;[HideInInspector];Create;True;0;0;0;True;0;False;15;15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;401;-384,-384;Inherit;False;Element Shader;10;;25572;0e972c73cae2ee54ea51acc9738801d0;12,477,0,1778,0,478,0,1824,0,1814,0,145,0,1784,0,1935,1,1933,1,481,0,1904,1,1907,1;1;1974;FLOAT;0;False;2;FLOAT4;0;FLOAT4;1779
Node;AmplifyShaderEditor.RangedFloatNode;100;-640,-768;Half;False;Property;_ElementMessage;Element Message;0;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use the Tinting elements to add color tinting to the vegetation assets. Element Texture RGB and Particle Color RGB are used as value multipliers. Element Texture A and Particle Color A are used as alpha masks., 0, 15);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;272;-64,-384;Half;False;True;-1;2;TVEShaderGUIElement;100;16;BOXOPHOBIC/The Visual Engine/Elements/Paint Tinting;f4f273c3bb6262d4396be458405e60f9;True;VolumePass;0;0;VolumePass;2;True;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;True;3;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;DisableBatching=True=DisableBatching;False;False;0;True;True;2;5;False;;10;False;;4;1;False;;1;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;2;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;273;-64,-256;Float;False;False;-1;2;ASEMaterialInspector;100;16;New Amplify Shader;f4f273c3bb6262d4396be458405e60f9;True;VisualPass;0;1;VisualPass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;False;False;0;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;0;False;0
WireConnection;401;1974;402;4
WireConnection;272;0;401;0
WireConnection;273;0;401;1779
ASEEND*/
//CHKSM=3E134FE5D309B9E1EFA6F8FB917D638FDADF5167