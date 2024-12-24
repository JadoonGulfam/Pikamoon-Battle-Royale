// Upgrade NOTE: upgraded instancing buffer 'HiddenBOXOPHOBICTheVisualEngineElementsMotionWindControl' to new syntax.

// Made with Amplify Shader Editor v1.9.4.4
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/BOXOPHOBIC/The Visual Engine/Elements/Motion Wind Control"
{
	Properties
	{
		[StyledBanner(Motion Control Element)]_Banner("Banner", Float) = 0
		[StyledMessage(Info, Use the Motion Wind Control elements to change the wind power and direction based on the element forward direction. Element Texture A is used as alpha mask. Particle Color A is used as Element Intensity multiplier. , 0,0)]_Message("Message", Float) = 0
		_ElementIntensity("Render Intensity", Range( 0 , 1)) = 1
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
		[Space(10)]_MainValue("Element Value", Range( 0 , 1)) = 1
		[Space(10)]_AdditionalValue1("Winter Value", Range( 0 , 1)) = 1
		_AdditionalValue2("Spring Value", Range( 0 , 1)) = 1
		_AdditionalValue3("Summer Value", Range( 0 , 1)) = 1
		_AdditionalValue4("Autumn Value", Range( 0 , 1)) = 1
		[Space(10)][StyledRemapSlider]_SeasonRemap("Seasons Curve", Vector) = (0,1,0,0)
		[Space(10)]_SpeedTresholdValue("Speed Treshold", Float) = 10
		[StyledSpace(10)]_ElementEnd("[ Element End ]", Float) = 0
		[StyledCategory(Fading Settings, true, 0, 10)]_FadeCategory("[ Fade Category ]", Float) = 1
		[StyledToggle]_ElementVolumeFadeMode("Enable Distance Fade", Float) = 0
		[HDR][StyledToggle]_ElementRaycastMode("Enable Raycast Fade", Float) = 0
		[StyledLayers()]_RaycastLayerMask("Raycast Layer", Float) = 1
		_RaycastDistanceMaxValue("Raycast Distance", Float) = 2
		[HideInInspector]_ElementLayerValue("Legacy Layer Value", Float) = -1
		[HideInInspector]_InvertX("Legacy Invert Mode", Float) = 0
		[HideInInspector]_ElementFadeSupport("Legacy Edge Fading", Float) = 0
		[StyledSpace(10)]_FadeEnd("[ Fade End ]", Float) = 0

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "PreviewType"="Plane" "DisableBatching"="True" }
	LOD 0

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask Off
		Cull Off
		ColorMask RGB
		ZWrite Off
		ZTest LEqual
		
		
		
		Pass
		{
			Name "Unlit"

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
				float4 ase_color : COLOR;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _Banner;
			uniform half _Message;
			uniform float _ElementFadeSupport;
			uniform half _ElementLayerValue;
			uniform float _InvertX;
			uniform half4 _MainTexColorRemap;
			uniform half _RaycastDistanceMaxValue;
			uniform half _RaycastLayerMask;
			uniform half _ElementRaycastMode;
			uniform half _FadeEnd;
			uniform half _SpaceTexture;
			uniform half _FadeCategory;
			uniform half _ElementCategory;
			uniform half _ElementEnd;
			uniform half _RenderEnd;
			uniform float _SpeedTresholdValue;
			uniform half _MainValue;
			uniform half4 TVE_SeasonOption;
			uniform half _AdditionalValue1;
			uniform half _AdditionalValue2;
			uniform half TVE_SeasonLerp;
			uniform half4 _SeasonRemap;
			uniform half _AdditionalValue3;
			uniform half _AdditionalValue4;
			uniform float _ElementMode;
			uniform sampler2D _MainTex;
			uniform float _ElementUVsMode;
			uniform half4 _MainUVs;
			uniform float _ElementIntensity;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 _MainTexFalloffRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform half TVE_RenderBaseFadeValue;
			uniform float _ElementVolumeFadeMode;
			UNITY_INSTANCING_BUFFER_START(HiddenBOXOPHOBICTheVisualEngineElementsMotionWindControl)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr HiddenBOXOPHOBICTheVisualEngineElementsMotionWindControl
			UNITY_INSTANCING_BUFFER_END(HiddenBOXOPHOBICTheVisualEngineElementsMotionWindControl)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				half TVE_SeasonOptions_X50_g53317 = TVE_SeasonOption.x;
				half Value_Winter158_g53317 = _AdditionalValue1;
				half Value_Spring159_g53317 = _AdditionalValue2;
				float temp_output_7_0_g53343 = _SeasonRemap.x;
				float temp_output_10_0_g53343 = ( _SeasonRemap.y - temp_output_7_0_g53343 );
				half TVE_SeasonLerp54_g53317 = saturate( saturate( ( ( TVE_SeasonLerp - temp_output_7_0_g53343 ) / temp_output_10_0_g53343 ) ) );
				float lerpResult168_g53317 = lerp( Value_Winter158_g53317 , Value_Spring159_g53317 , TVE_SeasonLerp54_g53317);
				half TVE_SeasonOptions_Y51_g53317 = TVE_SeasonOption.y;
				half Value_Summer160_g53317 = _AdditionalValue3;
				float lerpResult167_g53317 = lerp( Value_Spring159_g53317 , Value_Summer160_g53317 , TVE_SeasonLerp54_g53317);
				half TVE_SeasonOptions_Z52_g53317 = TVE_SeasonOption.z;
				half Value_Autumn161_g53317 = _AdditionalValue4;
				float lerpResult166_g53317 = lerp( Value_Summer160_g53317 , Value_Autumn161_g53317 , TVE_SeasonLerp54_g53317);
				half TVE_SeasonOptions_W53_g53317 = TVE_SeasonOption.w;
				float lerpResult165_g53317 = lerp( Value_Autumn161_g53317 , Value_Winter158_g53317 , TVE_SeasonLerp54_g53317);
				float vertexToFrag11_g53319 = ( ( ( TVE_SeasonOptions_X50_g53317 * lerpResult168_g53317 ) + ( TVE_SeasonOptions_Y51_g53317 * lerpResult167_g53317 ) ) + ( ( TVE_SeasonOptions_Z52_g53317 * lerpResult166_g53317 ) + ( TVE_SeasonOptions_W53_g53317 * lerpResult165_g53317 ) ) );
				o.ase_texcoord1.x = vertexToFrag11_g53319;
				float3 ase_worldPos = mul(unity_ObjectToWorld, float4( (v.vertex).xyz, 1 )).xyz;
				float2 appendResult1900_g53317 = (float2(ase_worldPos.x , ase_worldPos.z));
				float2 lerpResult1899_g53317 = lerp( ( 1.0 - v.ase_texcoord.xy ) , appendResult1900_g53317 , _ElementUVsMode);
				float2 vertexToFrag11_g53353 = ( ( lerpResult1899_g53317 * (_MainUVs).xy ) + (_MainUVs).zw );
				o.ase_texcoord1.yz = vertexToFrag11_g53353;
				
				o.ase_color = v.color;
				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.w = 0;
				o.ase_texcoord2.zw = 0;
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
				half Value_Main157_g53317 = _MainValue;
				float vertexToFrag11_g53319 = i.ase_texcoord1.x;
				half Value_Seasons1721_g53317 = vertexToFrag11_g53319;
				half Element_Mode55_g53317 = _ElementMode;
				float lerpResult181_g53317 = lerp( Value_Main157_g53317 , Value_Seasons1721_g53317 , Element_Mode55_g53317);
				float2 vertexToFrag11_g53353 = i.ase_texcoord1.yz;
				half4 MainTex_RGBA587_g53317 = tex2D( _MainTex, vertexToFrag11_g53353 );
				float3 temp_cast_0 = (0.0001).xxx;
				float3 temp_cast_1 = (0.9999).xxx;
				float3 clampResult17_g53347 = clamp( (MainTex_RGBA587_g53317).rgb , temp_cast_0 , temp_cast_1 );
				float temp_output_7_0_g53329 = _MainTexColorRemap.x;
				float3 temp_cast_2 = (temp_output_7_0_g53329).xxx;
				float temp_output_10_0_g53329 = ( _MainTexColorRemap.y - temp_output_7_0_g53329 );
				float3 temp_output_1765_0_g53317 = saturate( ( ( clampResult17_g53347 - temp_cast_2 ) / temp_output_10_0_g53329 ) );
				half Element_Remap_R73_g53317 = (temp_output_1765_0_g53317).x;
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half Element_Params_R1738_g53317 = _ElementParams_Instance.x;
				half Element_Value1744_g53317 = ( Element_Remap_R73_g53317 * Element_Params_R1738_g53317 * i.ase_color.r );
				half Final_Extras_RGB213_g53317 = ( lerpResult181_g53317 * Element_Value1744_g53317 );
				float clampResult17_g53348 = clamp( (MainTex_RGBA587_g53317).a , 0.0001 , 0.9999 );
				float temp_output_7_0_g53328 = _MainTexAlphaRemap.x;
				float temp_output_10_0_g53328 = ( _MainTexAlphaRemap.y - temp_output_7_0_g53328 );
				half Element_Remap_A74_g53317 = saturate( ( ( clampResult17_g53348 - temp_output_7_0_g53328 ) / ( temp_output_10_0_g53328 + 0.0001 ) ) );
				half Element_Params_A1737_g53317 = _ElementParams_Instance.w;
				float clampResult17_g53326 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord2.xy*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				float temp_output_7_0_g53327 = _MainTexFalloffRemap.x;
				float temp_output_10_0_g53327 = ( _MainTexFalloffRemap.y - temp_output_7_0_g53327 );
				half Element_Falloff1883_g53317 = saturate( ( ( clampResult17_g53326 - temp_output_7_0_g53327 ) / ( temp_output_10_0_g53327 + 0.0001 ) ) );
				float temp_output_7_0_g53352 = 1.0;
				float temp_output_10_0_g53352 = ( TVE_RenderBaseFadeValue - temp_output_7_0_g53352 );
				float lerpResult18_g53350 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g53352 ) / temp_output_10_0_g53352 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g53317 = ( _ElementIntensity * Element_Remap_A74_g53317 * Element_Params_A1737_g53317 * i.ase_color.a * Element_Falloff1883_g53317 * lerpResult18_g53350 );
				half Final_Extras_A241_g53317 = Element_Alpha56_g53317;
				float4 appendResult1732_g53317 = (float4(0.0 , 0.0 , Final_Extras_RGB213_g53317 , Final_Extras_A241_g53317));
				
				
				finalColor = appendResult1732_g53317;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "OOShaderGUIElement"
	
	Fallback Off
}
/*ASEBEGIN
Version=19404
Node;AmplifyShaderEditor.FunctionNode;117;-640,-1280;Inherit;False;Element Base Motion;-1;;19292;;0;0;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-384,-1280;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Motion Control Element);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-256,-1280;Half;False;Property;_Message;Message;1;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use the Motion Wind Control elements to change the wind power and direction based on the element forward direction. Element Texture A is used as alpha mask. Particle Color A is used as Element Intensity multiplier. , 0,0);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;197;-640,-1024;Inherit;False;Element Shader;2;;53317;0e972c73cae2ee54ea51acc9738801d0;12,477,2,1778,2,478,0,1824,0,1814,3,145,3,1784,0,481,0,1935,1,1933,1,1904,1,1907,1;1;1974;FLOAT;0;False;2;FLOAT4;0;FLOAT4;1779
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;-320,-1024;Float;False;True;-1;2;OOShaderGUIElement;0;5;Hidden/BOXOPHOBIC/The Visual Engine/Elements/Motion Wind Control;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;2;False;;True;True;True;True;True;False;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;2;False;;True;0;False;;True;False;0;False;;0;False;;True;4;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;PreviewType=Plane;DisableBatching=True=DisableBatching;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
WireConnection;0;0;197;0
ASEEND*/
//CHKSM=4CBCAB652E589A385FB579BB6BCD7EAF1A49C64A