// Upgrade NOTE: upgraded instancing buffer 'BOXOPHOBICTheVisualEngineElementsPushInteraction' to new syntax.

// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Visual Engine/Elements/Push Interaction"
{
	Properties
	{
		[StyledMessage(Info, The Element Texture mode is setting the direction based on the Element Texture__ where RG is used as World XZ direction. Element Texture A and Particle Color A are used as alpha masks., _MotionDirectionMode, 1, 0, 15)]_ElementDirectionTextureMessage("Element Direction Message", Float) = 0
		[StyledMessage(Info, The Element Forward mode is setting the direction in the element transform forward axis. Element Texture A and Particle Color A are used as alpha masks., _MotionDirectionMode, 0, 0, 15)]_ElementDirectionForwardMessage("Element Direction Message", Float) = 0
		[StyledMessage(Info, The Particle Translate mode is setting the direction based on the particle gameobject transform movement direction. Use the Speed Treshold to control how fast the particle movement is transformend into interaction. Element Texture A and Particle Color A are used as alpha masks., _MotionDirectionMode, 2, 0, 15)]_ElementDirectionTranslateMessage("Element Direction Message", Float) = 0
		[StyledMessage(Info, The Particle Velocity mode is setting the direction based on the particles motion direction. This option requires the particle to have custom vertex streams for Velocity and Speed set after the UV stream under the particle Renderer module. Element Texture A and Particle Color A are used as alpha masks., _MotionDirectionMode, 3, 0, 15)]_ElementDirectionVelocityMessage("Element Direction Message", Float) = 0
		[HideInInspector]_IsVersion("_IsVersion", Float) = 2040
		[HideInInspector]_IsElementShader("_IsElementShader", Float) = 1
		[StyledCategory(Render Settings, true, 0, 10)]_RenderCategory("[ Render Category ]", Float) = 1
		_ElementIntensity("Render Intensity", Range( 0 , 1)) = 1
		[StyledMessage(Info, When using a higher Layer number the Global Volume will create more render textures to render the elements. Try using fewer layers when possible., _ElementLayerMessage, 1, 10, 10)]_ElementLayerMessage("Render Layer Message", Float) = 0
		[StyledMessage(Warning, When using all layers the Global Volume will create one render texture for each layer to render the elements. Try using fewer layers when possible., _ElementLayerWarning, 1, 10, 10)]_ElementLayerWarning("Render Layer Warning", Float) = 0
		[StyledMask(Push Layers, Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 7 Layer_8 8, 0, 0)]_ElementLayerMask("Render Layer", Float) = 1
		[HideInInspector]_ElementParams("Render Params", Vector) = (1,1,1,1)
		[StyledSpace(10)]_RenderEnd("[ Render End ]", Float) = 0
		[StyledCategory(Element Settings, true, 0, 10)]_ElementCategory("[ Element Category ]", Float) = 1
		[NoScaleOffset][StyledTextureSingleLine]_MainTex("Element Texture", 2D) = "white" {}
		[StyledSpace(10)]_SpaceTexture("#SpaceTexture", Float) = 0
		[StyledRemapSlider]_MainTexColorRemap("Element Value", Vector) = (0,1,0,0)
		[StyledRemapSlider]_MainTexAlphaRemap("Element Alpha", Vector) = (0,1,0,0)
		[StyledRemapSlider]_MainTexFalloffRemap("Element Falloff", Vector) = (0,0,0,0)
		[Space(10)][StyledTextureSingleLine]_MotionTex("Motion Texture", 2D) = "linearGrey" {}
		[HideInInspector]_MotionPhaseValue("Motion Phase", Float) = 0
		[Space(10)]_MotionIntensityValue("Motion Wind Intensity", Range( 0 , 1)) = 1
		[Enum(Element Forward,0,Element Texture,1,Particle Translate,2,Particle Velocity,3)]_MotionDirectionMode("Motion Wind Direction", Float) = 1
		_MotionNoiseValue("Motion Wind Noise", Range( 0 , 1)) = 0
		_MotionTillingValue("Motion Wind Tilling", Range( 0 , 100)) = 5
		_MotionSpeedValue("Motion Wind Speed", Range( 0 , 50)) = 5
		[Space(10)]_SpeedTresholdValue("Particle Speed Treshold", Float) = 10
		[Space(10)][StyledToggle]_ElementInvertMode("Use Inverted Direction", Float) = 0
		[StyledSpace(10)]_ElementEnd("[ Element End ]", Float) = 0
		[StyledCategory(Fading Settings, true, 0, 10)]_FadeCategory("[ Fade Category ]", Float) = 1
		[Enum(Off,0,On,1)]_ElementVolumeFadeMode("Bounds Fade", Float) = 0
		_ElementVolumeFadeValue("Bounds Fade Blend", Range( 0 , 1)) = 0.75
		[HDR][Enum(Off,0,On,1)][Space(10)]_ElementRaycastMode("Raycast Fade", Float) = 0
		[StyledLayers()]_RaycastLayerMask("Raycast Layer", Float) = 1
		_RaycastDistanceMaxValue("Raycast Limit", Float) = 2
		[HideInInspector]_motion_direction_mode("_motion_direction_mode", Vector) = (0,0,0,0)
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
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
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
			uniform half _ElementDirectionForwardMessage;
			uniform half _ElementDirectionTextureMessage;
			uniform half _ElementDirectionTranslateMessage;
			uniform half _ElementDirectionVelocityMessage;
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
			uniform half4 _motion_direction_mode;
			uniform sampler2D _MainTex;
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
			uniform float _ElementVolumeFadeValue;
			uniform float _ElementVolumeFadeMode;
			uniform half _MotionDirectionMode;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsPushInteraction)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsPushInteraction
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsPushInteraction)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1 = v.ase_texcoord;
				o.ase_color = v.color;
				o.ase_texcoord2 = v.ase_texcoord1;
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
				half2 Direction_Transform1406_g77708 = (( mul( unity_ObjectToWorld, float4( float3(0,0,1) , 0.0 ) ).xyz / ase_objectScale )).xz;
				half4 MainTex_RGBA587_g77708 = tex2D( _MainTex, ( 1.0 - i.ase_texcoord1.xy ) );
				float3 temp_cast_2 = (0.0001).xxx;
				float3 temp_cast_3 = (0.9999).xxx;
				float3 clampResult17_g77733 = clamp( (MainTex_RGBA587_g77708).rgb , temp_cast_2 , temp_cast_3 );
				float temp_output_7_0_g77718 = _MainTexColorRemap.x;
				float3 temp_cast_4 = (temp_output_7_0_g77718).xxx;
				float temp_output_10_0_g77718 = ( _MainTexColorRemap.y - temp_output_7_0_g77718 );
				float3 temp_output_1765_0_g77708 = saturate( ( ( clampResult17_g77733 - temp_cast_4 ) / temp_output_10_0_g77718 ) );
				half Element_Remap_R73_g77708 = (temp_output_1765_0_g77708).x;
				half Element_Remap_G265_g77708 = (temp_output_1765_0_g77708).y;
				float3 appendResult274_g77708 = (float3((Element_Remap_R73_g77708*2.0 + -1.0) , 0.0 , (Element_Remap_G265_g77708*2.0 + -1.0)));
				float3 break281_g77708 = ( mul( unity_ObjectToWorld, float4( appendResult274_g77708 , 0.0 ) ).xyz / ase_objectScale );
				float2 appendResult1403_g77708 = (float2(break281_g77708.x , break281_g77708.z));
				half2 Direction_Texture284_g77708 = appendResult1403_g77708;
				float2 appendResult1404_g77708 = (float2(i.ase_color.r , i.ase_color.g));
				half2 Direction_VertexColor1150_g77708 = (appendResult1404_g77708*2.0 + -1.0);
				float2 appendResult1382_g77708 = (float2(i.ase_texcoord1.z , i.ase_texcoord2.x));
				half2 Direction_Velocity1394_g77708 = ( appendResult1382_g77708 / i.ase_texcoord2.y );
				float2 temp_output_1452_0_g77708 = ( ( _motion_direction_mode.x * Direction_Transform1406_g77708 ) + ( _motion_direction_mode.y * Direction_Texture284_g77708 ) + ( _motion_direction_mode.z * Direction_VertexColor1150_g77708 ) + ( _motion_direction_mode.w * Direction_Velocity1394_g77708 ) );
				half Element_InvertMode489_g77708 = _ElementInvertMode;
				float2 lerpResult1468_g77708 = lerp( temp_output_1452_0_g77708 , -temp_output_1452_0_g77708 , Element_InvertMode489_g77708);
				half2 Direction_Advanced1454_g77708 = lerpResult1468_g77708;
				half2 Motion_Coords2098_g77708 = -(( WorldPosition - TVE_WorldOrigin )).xz;
				half2 Motion_Tilling1409_g77708 = ( Motion_Coords2098_g77708 * 0.005 * _MotionTillingValue );
				float2 temp_output_3_0_g77730 = Motion_Tilling1409_g77708;
				float2 temp_output_21_0_g77730 = Direction_Advanced1454_g77708;
				float mulTime113_g77745 = _Time.y * 0.015;
				float lerpResult128_g77745 = lerp( mulTime113_g77745 , ( ( mulTime113_g77745 * TVE_TimeParams.x ) + TVE_TimeParams.y ) , TVE_TimeParams.w);
				float temp_output_1969_0_g77708 = (lerpResult128_g77745*_MotionSpeedValue + _MotionPhaseValue);
				float temp_output_15_0_g77730 = temp_output_1969_0_g77708;
				float temp_output_23_0_g77730 = frac( temp_output_15_0_g77730 );
				float4 lerpResult39_g77730 = lerp( tex2D( _MotionTex, ( temp_output_3_0_g77730 + ( temp_output_21_0_g77730 * temp_output_23_0_g77730 ) ) ) , tex2D( _MotionTex, ( temp_output_3_0_g77730 + ( temp_output_21_0_g77730 * frac( ( temp_output_15_0_g77730 + 0.5 ) ) ) ) ) , ( abs( ( temp_output_23_0_g77730 - 0.5 ) ) / 0.5 ));
				float4 temp_output_1423_0_g77708 = lerpResult39_g77730;
				half2 Motion_FlowRG1427_g77708 = (temp_output_1423_0_g77708).rg;
				half Motion_Noise2056_g77708 = _MotionNoiseValue;
				float2 lerpResult2141_g77708 = lerp( (Direction_Advanced1454_g77708*0.5 + 0.5) , Motion_FlowRG1427_g77708 , Motion_Noise2056_g77708);
				half Motion_Intensity2000_g77708 = _MotionIntensityValue;
				float2 lerpResult2139_g77708 = lerp( float2( 0.5,0.5 ) , lerpResult2141_g77708 , Motion_Intensity2000_g77708);
				half Motion_FlowA2144_g77708 = (temp_output_1423_0_g77708).a;
				float3 appendResult1436_g77708 = (float3(saturate( lerpResult2139_g77708 ) , ( Motion_FlowA2144_g77708 * Motion_Intensity2000_g77708 * Motion_Noise2056_g77708 )));
				half3 Final_MotionAdvanced_RGB1438_g77708 = appendResult1436_g77708;
				float clampResult17_g77734 = clamp( (MainTex_RGBA587_g77708).a , 0.0001 , 0.9999 );
				float temp_output_7_0_g77717 = _MainTexAlphaRemap.x;
				float temp_output_10_0_g77717 = ( _MainTexAlphaRemap.y - temp_output_7_0_g77717 );
				half Element_Remap_A74_g77708 = saturate( ( ( clampResult17_g77734 - temp_output_7_0_g77717 ) / ( temp_output_10_0_g77717 + 0.0001 ) ) );
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half Element_Params_A1737_g77708 = _ElementParams_Instance.w;
				float clampResult17_g77715 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord1.xy*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				float temp_output_7_0_g77716 = _MainTexFalloffRemap.x;
				float temp_output_10_0_g77716 = ( _MainTexFalloffRemap.y - temp_output_7_0_g77716 );
				half Element_Falloff1883_g77708 = saturate( ( ( clampResult17_g77715 - temp_output_7_0_g77716 ) / ( temp_output_10_0_g77716 + 0.0001 ) ) );
				float temp_output_7_0_g77738 = 1.0;
				float temp_output_10_0_g77738 = ( _ElementVolumeFadeValue - temp_output_7_0_g77738 );
				float lerpResult18_g77736 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g77738 ) / temp_output_10_0_g77738 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g77708 = ( _ElementIntensity * Element_Remap_A74_g77708 * Element_Params_A1737_g77708 * i.ase_color.a * Element_Falloff1883_g77708 * lerpResult18_g77736 );
				half Final_MotionAdvanced_A1439_g77708 = Element_Alpha56_g77708;
				float4 appendResult1463_g77708 = (float4(Final_MotionAdvanced_RGB1438_g77708 , Final_MotionAdvanced_A1439_g77708));
				float4 temp_output_6_0_g77740 = appendResult1463_g77708;
				half Motion_Direction1013_g77708 = _MotionDirectionMode;
				#ifdef TVE_REGISTER
				float4 staticSwitch14_g77740 = ( temp_output_6_0_g77740 + ( Motion_Direction1013_g77708 * 0.0 ) );
				#else
				float4 staticSwitch14_g77740 = temp_output_6_0_g77740;
				#endif
				
				
				finalColor = staticSwitch14_g77740;
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
			uniform half _ElementDirectionForwardMessage;
			uniform half _ElementDirectionTextureMessage;
			uniform half _ElementDirectionTranslateMessage;
			uniform half _ElementDirectionVelocityMessage;
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
			uniform half4 _motion_direction_mode;
			uniform sampler2D _MainTex;
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
			uniform float _ElementVolumeFadeValue;
			uniform float _ElementVolumeFadeMode;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsPushInteraction)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsPushInteraction
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsPushInteraction)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1 = v.ase_texcoord;
				o.ase_color = v.color;
				o.ase_texcoord2 = v.ase_texcoord1;
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
				half2 Direction_Transform1406_g77708 = (( mul( unity_ObjectToWorld, float4( float3(0,0,1) , 0.0 ) ).xyz / ase_objectScale )).xz;
				half4 MainTex_RGBA587_g77708 = tex2D( _MainTex, ( 1.0 - i.ase_texcoord1.xy ) );
				float3 temp_cast_2 = (0.0001).xxx;
				float3 temp_cast_3 = (0.9999).xxx;
				float3 clampResult17_g77733 = clamp( (MainTex_RGBA587_g77708).rgb , temp_cast_2 , temp_cast_3 );
				float temp_output_7_0_g77718 = _MainTexColorRemap.x;
				float3 temp_cast_4 = (temp_output_7_0_g77718).xxx;
				float temp_output_10_0_g77718 = ( _MainTexColorRemap.y - temp_output_7_0_g77718 );
				float3 temp_output_1765_0_g77708 = saturate( ( ( clampResult17_g77733 - temp_cast_4 ) / temp_output_10_0_g77718 ) );
				half Element_Remap_R73_g77708 = (temp_output_1765_0_g77708).x;
				half Element_Remap_G265_g77708 = (temp_output_1765_0_g77708).y;
				float3 appendResult274_g77708 = (float3((Element_Remap_R73_g77708*2.0 + -1.0) , 0.0 , (Element_Remap_G265_g77708*2.0 + -1.0)));
				float3 break281_g77708 = ( mul( unity_ObjectToWorld, float4( appendResult274_g77708 , 0.0 ) ).xyz / ase_objectScale );
				float2 appendResult1403_g77708 = (float2(break281_g77708.x , break281_g77708.z));
				half2 Direction_Texture284_g77708 = appendResult1403_g77708;
				float2 appendResult1404_g77708 = (float2(i.ase_color.r , i.ase_color.g));
				half2 Direction_VertexColor1150_g77708 = (appendResult1404_g77708*2.0 + -1.0);
				float2 appendResult1382_g77708 = (float2(i.ase_texcoord1.z , i.ase_texcoord2.x));
				half2 Direction_Velocity1394_g77708 = ( appendResult1382_g77708 / i.ase_texcoord2.y );
				float2 temp_output_1452_0_g77708 = ( ( _motion_direction_mode.x * Direction_Transform1406_g77708 ) + ( _motion_direction_mode.y * Direction_Texture284_g77708 ) + ( _motion_direction_mode.z * Direction_VertexColor1150_g77708 ) + ( _motion_direction_mode.w * Direction_Velocity1394_g77708 ) );
				half Element_InvertMode489_g77708 = _ElementInvertMode;
				float2 lerpResult1468_g77708 = lerp( temp_output_1452_0_g77708 , -temp_output_1452_0_g77708 , Element_InvertMode489_g77708);
				half2 Direction_Advanced1454_g77708 = lerpResult1468_g77708;
				half2 Motion_Coords2098_g77708 = -(( WorldPosition - TVE_WorldOrigin )).xz;
				half2 Motion_Tilling1409_g77708 = ( Motion_Coords2098_g77708 * 0.005 * _MotionTillingValue );
				float2 temp_output_3_0_g77730 = Motion_Tilling1409_g77708;
				float2 temp_output_21_0_g77730 = Direction_Advanced1454_g77708;
				float mulTime113_g77745 = _Time.y * 0.015;
				float lerpResult128_g77745 = lerp( mulTime113_g77745 , ( ( mulTime113_g77745 * TVE_TimeParams.x ) + TVE_TimeParams.y ) , TVE_TimeParams.w);
				float temp_output_1969_0_g77708 = (lerpResult128_g77745*_MotionSpeedValue + _MotionPhaseValue);
				float temp_output_15_0_g77730 = temp_output_1969_0_g77708;
				float temp_output_23_0_g77730 = frac( temp_output_15_0_g77730 );
				float4 lerpResult39_g77730 = lerp( tex2D( _MotionTex, ( temp_output_3_0_g77730 + ( temp_output_21_0_g77730 * temp_output_23_0_g77730 ) ) ) , tex2D( _MotionTex, ( temp_output_3_0_g77730 + ( temp_output_21_0_g77730 * frac( ( temp_output_15_0_g77730 + 0.5 ) ) ) ) ) , ( abs( ( temp_output_23_0_g77730 - 0.5 ) ) / 0.5 ));
				float4 temp_output_1423_0_g77708 = lerpResult39_g77730;
				half2 Motion_FlowRG1427_g77708 = (temp_output_1423_0_g77708).rg;
				half Motion_Noise2056_g77708 = _MotionNoiseValue;
				float2 lerpResult2141_g77708 = lerp( (Direction_Advanced1454_g77708*0.5 + 0.5) , Motion_FlowRG1427_g77708 , Motion_Noise2056_g77708);
				half Motion_Intensity2000_g77708 = _MotionIntensityValue;
				float2 lerpResult2139_g77708 = lerp( float2( 0.5,0.5 ) , lerpResult2141_g77708 , Motion_Intensity2000_g77708);
				half Motion_FlowA2144_g77708 = (temp_output_1423_0_g77708).a;
				float3 appendResult1436_g77708 = (float3(saturate( lerpResult2139_g77708 ) , ( Motion_FlowA2144_g77708 * Motion_Intensity2000_g77708 * Motion_Noise2056_g77708 )));
				half3 Final_MotionAdvanced_RGB1438_g77708 = appendResult1436_g77708;
				half3 Input_Color94_g77743 = Final_MotionAdvanced_RGB1438_g77708;
				half3 Element_Valid47_g77743 = ( Input_Color94_g77743 * Input_Color94_g77743 );
				float clampResult17_g77734 = clamp( (MainTex_RGBA587_g77708).a , 0.0001 , 0.9999 );
				float temp_output_7_0_g77717 = _MainTexAlphaRemap.x;
				float temp_output_10_0_g77717 = ( _MainTexAlphaRemap.y - temp_output_7_0_g77717 );
				half Element_Remap_A74_g77708 = saturate( ( ( clampResult17_g77734 - temp_output_7_0_g77717 ) / ( temp_output_10_0_g77717 + 0.0001 ) ) );
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half Element_Params_A1737_g77708 = _ElementParams_Instance.w;
				float clampResult17_g77715 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord1.xy*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				float temp_output_7_0_g77716 = _MainTexFalloffRemap.x;
				float temp_output_10_0_g77716 = ( _MainTexFalloffRemap.y - temp_output_7_0_g77716 );
				half Element_Falloff1883_g77708 = saturate( ( ( clampResult17_g77715 - temp_output_7_0_g77716 ) / ( temp_output_10_0_g77716 + 0.0001 ) ) );
				float temp_output_7_0_g77738 = 1.0;
				float temp_output_10_0_g77738 = ( _ElementVolumeFadeValue - temp_output_7_0_g77738 );
				float lerpResult18_g77736 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g77738 ) / temp_output_10_0_g77738 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g77708 = ( _ElementIntensity * Element_Remap_A74_g77708 * Element_Params_A1737_g77708 * i.ase_color.a * Element_Falloff1883_g77708 * lerpResult18_g77736 );
				half Final_MotionAdvanced_A1439_g77708 = Element_Alpha56_g77708;
				half Input_Alpha48_g77743 = Final_MotionAdvanced_A1439_g77708;
				float4 appendResult58_g77743 = (float4(Element_Valid47_g77743 , Input_Alpha48_g77743));
				
				
				finalColor = appendResult58_g77743;
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
Node;AmplifyShaderEditor.FunctionNode;570;-640,-768;Inherit;False;Element Push;4;;30822;daf80be7c77d54d4bb4554e32ce03c7a;0;0;1;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;416;-64,-1280;Inherit;False;Element Compile;-1;;30824;5302407fc6d65554791e558e67d59358;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;197;-640,-960;Half;False;Property;_render_colormask;_render_colormask;74;1;[HideInInspector];Create;True;0;0;0;True;0;False;15;15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;440;-640,-1280;Half;False;Property;_ElementDirectionForwardMessage;Element Direction Message;1;0;Create;False;0;0;0;True;1;StyledMessage(Info, The Element Forward mode is setting the direction in the element transform forward axis. Element Texture A and Particle Color A are used as alpha masks., _MotionDirectionMode, 0, 0, 15);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;441;-640,-1216;Half;False;Property;_ElementDirectionTextureMessage;Element Direction Message;0;0;Create;False;0;0;0;True;1;StyledMessage(Info, The Element Texture mode is setting the direction based on the Element Texture__ where RG is used as World XZ direction. Element Texture A and Particle Color A are used as alpha masks., _MotionDirectionMode, 1, 0, 15);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;442;-640,-1152;Half;False;Property;_ElementDirectionTranslateMessage;Element Direction Message;2;0;Create;False;0;0;0;True;1;StyledMessage(Info, The Particle Translate mode is setting the direction based on the particle gameobject transform movement direction. Use the Speed Treshold to control how fast the particle movement is transformend into interaction. Element Texture A and Particle Color A are used as alpha masks., _MotionDirectionMode, 2, 0, 15);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;439;-640,-1088;Half;False;Property;_ElementDirectionVelocityMessage;Element Direction Message;3;0;Create;False;0;0;0;True;1;StyledMessage(Info, The Particle Velocity mode is setting the direction based on the particles motion direction. This option requires the particle to have custom vertex streams for Velocity and Speed set after the UV stream under the particle Renderer module. Element Texture A and Particle Color A are used as alpha masks., _MotionDirectionMode, 3, 0, 15);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;633;-384,-768;Inherit;False;Element Shader;13;;77708;0e972c73cae2ee54ea51acc9738801d0;12,477,2,1778,2,478,0,1824,0,1814,3,145,3,1784,2,1935,1,1933,1,481,2,1904,0,1907,1;1;1974;FLOAT;0;False;2;FLOAT4;0;FLOAT4;1779
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;417;-64,-768;Float;False;True;-1;2;TVEShaderGUIElement;100;17;BOXOPHOBIC/The Visual Engine/Elements/Push Interaction;f4f273c3bb6262d4396be458405e60f9;True;VolumePass;0;0;VolumePass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;DisableBatching=True=DisableBatching;False;False;0;True;True;2;5;False;;10;False;;2;5;False;;10;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;True;True;True;True;True;True;0;False;_render_colormask;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;2;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;418;-64,-640;Float;False;False;-1;2;ASEMaterialInspector;100;17;New Amplify Shader;f4f273c3bb6262d4396be458405e60f9;True;VisualPass;0;1;VisualPass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;False;False;0;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;0;False;0
WireConnection;633;1974;570;4
WireConnection;417;0;633;0
WireConnection;418;0;633;1779
ASEEND*/
//CHKSM=FC4AAA5ED739EBC6EF4256916A614DB881B7FDD3