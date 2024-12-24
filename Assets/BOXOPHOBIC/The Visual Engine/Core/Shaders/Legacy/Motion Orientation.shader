// Upgrade NOTE: upgraded instancing buffer 'HiddenBOXOPHOBICTheVisualEngineElementsMotionOrientation' to new syntax.

// Made with Amplify Shader Editor v1.9.4.4
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/BOXOPHOBIC/The Visual Engine/Elements/Motion Orientation"
{
	Properties
	{
		[StyledBanner(Motion Orientation Element)]_Banner("Banner", Float) = 0
		[StyledMessage(Info, Use the Motion Orientation elements to change the motion direction based on the Element Texture. Element Texture RG is used a World XZ direction and Texture A is used as alpha mask. Particle Color A is used as Element Intensity multiplier., 0,0)]_Message("Message", Float) = 0
		_ElementIntensity("Render Intensity", Range( 0 , 1)) = 1
		[HideInInspector]_ElementParams("Render Params", Vector) = (1,1,1,1)
		[StyledSpace(10)]_RenderEnd("[ Render End ]", Float) = 0
		[StyledCategory(Element Settings, true, 0, 10)]_ElementCategory("[ Element Category ]", Float) = 1
		[NoScaleOffset][StyledTextureSingleLine]_MainTex("Element Texture", 2D) = "white" {}
		[StyledSpace(10)]_SpaceTexture("#SpaceTexture", Float) = 0
		[Enum(Main UV,0,Planar,1)]_ElementUVsMode("Element Sampling", Float) = 0
		[StyledVector(9)]_MainUVs("Element UV Value", Vector) = (1,1,0,0)
		[StyledRemapSlider]_MainTexColorRemap("Element Value", Vector) = (0,1,0,0)
		[StyledRemapSlider]_MainTexAlphaRemap("Element Alpha", Vector) = (0,1,0,0)
		[StyledRemapSlider]_MainTexFalloffRemap("Element Falloff", Vector) = (0,0,0,0)
		[Space(10)][StyledTextureSingleLine]_MotionTex("Motion Texture", 2D) = "linearGrey" {}
		[Enum(Element Forward,0,Element Texture,1,Particle Translate,2,Particle Velocity,3)][Space(10)]_MotionDirectionMode("Motion Direction", Float) = 1
		_MotionIntensityValue("Motion Intensity", Range( 0 , 1)) = 1
		_MotionNoiseValue("Motion Noise", Range( 0 , 1)) = 0
		_MotionTillingValue("Motion Tilling", Range( 0 , 40)) = 4
		_MotionSpeedValue("Motion Speed", Range( 0 , 40)) = 4
		[HideInInspector]_MotionPhaseValue("Motion Phase", Float) = 0
		[Space(10)]_SpeedTresholdValue("Speed Treshold", Float) = 10
		[Space(10)][StyledToggle]_ElementInvertMode("Use Inverted Element Direction", Float) = 0
		[StyledSpace(10)]_ElementEnd("[ Element End ]", Float) = 0
		[StyledCategory(Fading Settings, true, 0, 10)]_FadeCategory("[ Fade Category ]", Float) = 1
		[StyledToggle]_ElementVolumeFadeMode("Enable Distance Fade", Float) = 0
		[HDR][StyledToggle]_ElementRaycastMode("Enable Raycast Fade", Float) = 0
		[StyledLayers()]_RaycastLayerMask("Raycast Layer", Float) = 1
		_RaycastDistanceMaxValue("Raycast Distance", Float) = 2
		[HideInInspector]_ElementLayerValue("Legacy Layer Value", Float) = -1
		[HideInInspector]_InvertX("Legacy Invert Mode", Float) = 0
		[HideInInspector]_ElementFadeSupport("Legacy Edge Fading", Float) = 0
		[HideInInspector]_motion_direction_mode("_motion_direction_mode", Vector) = (0,0,0,0)
		[StyledSpace(10)]_FadeEnd("[ Fade End ]", Float) = 0
		[HideInInspector]_render_colormask("_render_colormask", Float) = 15

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "PreviewType"="Plane" "DisableBatching"="True" }
	LOD 0

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha, One One
		AlphaToMask Off
		Cull Off
		ColorMask RGBA
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
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_COLOR
			#define ASE_NEEDS_FRAG_WORLD_POSITION


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
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
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _Banner;
			uniform half _Message;
			uniform half _render_colormask;
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
			uniform half4 _motion_direction_mode;
			uniform sampler2D _MainTex;
			uniform float _ElementUVsMode;
			uniform half4 _MainUVs;
			uniform float _ElementInvertMode;
			uniform sampler2D _MotionTex;
			uniform float3 TVE_WorldOrigin;
			uniform half _MotionTillingValue;
			uniform half4 TVE_TimeParams;
			uniform half _MotionSpeedValue;
			uniform half _MotionPhaseValue;
			uniform half _MotionNoiseValue;
			uniform half _MotionIntensityValue;
			uniform float _ElementIntensity;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 _MainTexFalloffRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform half TVE_RenderBaseFadeValue;
			uniform float _ElementVolumeFadeMode;
			uniform float _MotionDirectionMode;
			UNITY_INSTANCING_BUFFER_START(HiddenBOXOPHOBICTheVisualEngineElementsMotionOrientation)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr HiddenBOXOPHOBICTheVisualEngineElementsMotionOrientation
			UNITY_INSTANCING_BUFFER_END(HiddenBOXOPHOBICTheVisualEngineElementsMotionOrientation)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldPos = mul(unity_ObjectToWorld, float4( (v.vertex).xyz, 1 )).xyz;
				float2 appendResult1900_g21482 = (float2(ase_worldPos.x , ase_worldPos.z));
				float2 lerpResult1899_g21482 = lerp( ( 1.0 - v.ase_texcoord.xy ) , appendResult1900_g21482 , _ElementUVsMode);
				float2 vertexToFrag11_g22414 = ( ( lerpResult1899_g21482 * (_MainUVs).xy ) + (_MainUVs).zw );
				o.ase_texcoord1.xy = vertexToFrag11_g22414;
				
				o.ase_color = v.color;
				o.ase_texcoord2 = v.ase_texcoord;
				o.ase_texcoord3 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
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
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				half2 Direction_Transform1406_g21482 = (( mul( unity_ObjectToWorld, float4( float3(0,0,1) , 0.0 ) ).xyz / ase_objectScale )).xz;
				float2 vertexToFrag11_g22414 = i.ase_texcoord1.xy;
				half4 MainTex_RGBA587_g21482 = tex2D( _MainTex, vertexToFrag11_g22414 );
				float3 temp_cast_2 = (0.0001).xxx;
				float3 temp_cast_3 = (0.9999).xxx;
				float3 clampResult17_g22408 = clamp( (MainTex_RGBA587_g21482).rgb , temp_cast_2 , temp_cast_3 );
				float temp_output_7_0_g22183 = _MainTexColorRemap.x;
				float3 temp_cast_4 = (temp_output_7_0_g22183).xxx;
				float temp_output_10_0_g22183 = ( _MainTexColorRemap.y - temp_output_7_0_g22183 );
				float3 temp_output_1765_0_g21482 = saturate( ( ( clampResult17_g22408 - temp_cast_4 ) / temp_output_10_0_g22183 ) );
				half Element_Remap_R73_g21482 = (temp_output_1765_0_g21482).x;
				half Element_Remap_G265_g21482 = (temp_output_1765_0_g21482).y;
				float3 appendResult274_g21482 = (float3((Element_Remap_R73_g21482*2.0 + -1.0) , 0.0 , (Element_Remap_G265_g21482*2.0 + -1.0)));
				float3 break281_g21482 = ( mul( unity_ObjectToWorld, float4( appendResult274_g21482 , 0.0 ) ).xyz / ase_objectScale );
				float2 appendResult1403_g21482 = (float2(break281_g21482.x , break281_g21482.z));
				half2 Direction_Texture284_g21482 = appendResult1403_g21482;
				float2 appendResult1404_g21482 = (float2(i.ase_color.r , i.ase_color.g));
				half2 Direction_VertexColor1150_g21482 = (appendResult1404_g21482*2.0 + -1.0);
				float2 appendResult1382_g21482 = (float2(i.ase_texcoord2.z , i.ase_texcoord3.x));
				half2 Direction_Velocity1394_g21482 = ( appendResult1382_g21482 / i.ase_texcoord3.y );
				float2 temp_output_1452_0_g21482 = ( ( _motion_direction_mode.x * Direction_Transform1406_g21482 ) + ( _motion_direction_mode.y * Direction_Texture284_g21482 ) + ( _motion_direction_mode.z * Direction_VertexColor1150_g21482 ) + ( _motion_direction_mode.w * Direction_Velocity1394_g21482 ) );
				half Element_InvertMode489_g21482 = _ElementInvertMode;
				float2 lerpResult1468_g21482 = lerp( temp_output_1452_0_g21482 , -temp_output_1452_0_g21482 , Element_InvertMode489_g21482);
				half2 Direction_Advanced1454_g21482 = lerpResult1468_g21482;
				half2 Noise_Coords1409_g21482 = ( -(( WorldPosition - TVE_WorldOrigin )).xz * _MotionTillingValue * 0.005 );
				float2 temp_output_3_0_g22386 = Noise_Coords1409_g21482;
				float2 temp_output_21_0_g22386 = Direction_Advanced1454_g21482;
				float lerpResult128_g22385 = lerp( _Time.y , ( ( _Time.y * TVE_TimeParams.x ) + TVE_TimeParams.y ) , TVE_TimeParams.w);
				float temp_output_15_0_g22386 = ( (lerpResult128_g22385*_MotionSpeedValue + _MotionPhaseValue) * 0.02 );
				float temp_output_23_0_g22386 = frac( temp_output_15_0_g22386 );
				float4 lerpResult39_g22386 = lerp( tex2D( _MotionTex, ( temp_output_3_0_g22386 + ( temp_output_21_0_g22386 * temp_output_23_0_g22386 ) ) ) , tex2D( _MotionTex, ( temp_output_3_0_g22386 + ( temp_output_21_0_g22386 * frac( ( temp_output_15_0_g22386 + 0.5 ) ) ) ) ) , ( abs( ( temp_output_23_0_g22386 - 0.5 ) ) / 0.5 ));
				float4 temp_output_1423_0_g21482 = lerpResult39_g22386;
				half2 Motion_FlowNoise1427_g21482 = (temp_output_1423_0_g21482).rg;
				float2 lerpResult1435_g21482 = lerp( (Direction_Advanced1454_g21482*0.5 + 0.5) , Motion_FlowNoise1427_g21482 , _MotionNoiseValue);
				half Motion_FlowIntensity2000_g21482 = _MotionIntensityValue;
				float2 lerpResult1997_g21482 = lerp( float2( 0.5,0.5 ) , lerpResult1435_g21482 , Motion_FlowIntensity2000_g21482);
				half Motion_WindNoise1980_g21482 = (temp_output_1423_0_g21482).b;
				float3 appendResult1436_g21482 = (float3(saturate( lerpResult1997_g21482 ) , Motion_WindNoise1980_g21482));
				half3 Final_MotionAdvanced_RGB1438_g21482 = appendResult1436_g21482;
				float clampResult17_g22409 = clamp( (MainTex_RGBA587_g21482).a , 0.0001 , 0.9999 );
				float temp_output_7_0_g22149 = _MainTexAlphaRemap.x;
				float temp_output_10_0_g22149 = ( _MainTexAlphaRemap.y - temp_output_7_0_g22149 );
				half Element_Remap_A74_g21482 = saturate( ( ( clampResult17_g22409 - temp_output_7_0_g22149 ) / ( temp_output_10_0_g22149 + 0.0001 ) ) );
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half Element_Params_A1737_g21482 = _ElementParams_Instance.w;
				float clampResult17_g21978 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord2.xy*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				float temp_output_7_0_g21979 = _MainTexFalloffRemap.x;
				float temp_output_10_0_g21979 = ( _MainTexFalloffRemap.y - temp_output_7_0_g21979 );
				half Element_Falloff1883_g21482 = saturate( ( ( clampResult17_g21978 - temp_output_7_0_g21979 ) / ( temp_output_10_0_g21979 + 0.0001 ) ) );
				float temp_output_7_0_g22413 = 1.0;
				float temp_output_10_0_g22413 = ( TVE_RenderBaseFadeValue - temp_output_7_0_g22413 );
				float lerpResult18_g22411 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g22413 ) / temp_output_10_0_g22413 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g21482 = ( _ElementIntensity * Element_Remap_A74_g21482 * Element_Params_A1737_g21482 * i.ase_color.a * Element_Falloff1883_g21482 * lerpResult18_g22411 );
				half Final_MotionAdvanced_A1439_g21482 = Element_Alpha56_g21482;
				float4 appendResult1463_g21482 = (float4(Final_MotionAdvanced_RGB1438_g21482 , Final_MotionAdvanced_A1439_g21482));
				float4 temp_output_6_0_g21490 = appendResult1463_g21482;
				half Element_DirectionMode1013_g21482 = _MotionDirectionMode;
				#ifdef TVE_REGISTER
				float4 staticSwitch14_g21490 = ( temp_output_6_0_g21490 + ( Element_DirectionMode1013_g21482 * 0.0 ) );
				#else
				float4 staticSwitch14_g21490 = temp_output_6_0_g21490;
				#endif
				
				
				finalColor = staticSwitch14_g21490;
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
Node;AmplifyShaderEditor.FunctionNode;117;-640,-1280;Inherit;False;Element Base Motion;-1;;19417;;0;0;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-384,-1280;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Motion Orientation Element);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-256,-1280;Half;False;Property;_Message;Message;1;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use the Motion Orientation elements to change the motion direction based on the Element Texture. Element Texture RG is used a World XZ direction and Texture A is used as alpha mask. Particle Color A is used as Element Intensity multiplier., 0,0);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;164;-640,-1152;Half;False;Property;_render_colormask;_render_colormask;64;1;[HideInInspector];Create;True;0;0;0;True;0;False;15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;166;-638,-1024;Inherit;False;Element Shader;2;;21482;0e972c73cae2ee54ea51acc9738801d0;12,477,2,1778,2,478,0,1824,0,1814,3,145,3,1784,2,481,2,1935,1,1933,1,1904,1,1907,1;1;1974;FLOAT;0;False;2;FLOAT4;0;FLOAT4;1779
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;-304,-1024;Float;False;True;-1;2;OOShaderGUIElement;0;5;Hidden/BOXOPHOBIC/The Visual Engine/Elements/Motion Orientation;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;2;5;False;;10;False;;4;1;False;;1;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;2;False;;True;True;True;True;True;True;0;False;_render_colormask;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;2;False;;True;0;False;;True;False;0;False;;0;False;;True;4;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;PreviewType=Plane;DisableBatching=True=DisableBatching;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
WireConnection;0;0;166;0
ASEEND*/
//CHKSM=5D9525BB5BE6F89E05004921DAD5AF5FAED8471F