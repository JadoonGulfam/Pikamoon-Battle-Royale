// Upgrade NOTE: upgraded instancing buffer 'BOXOPHOBICTheVisualEngineElementsPushInteractionSimple' to new syntax.

// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Visual Engine/Elements/Push Interaction (Simple)"
{
	Properties
	{
		[StyledMessage(Info, Use the Interaction elements to add touch bending to the vegetation objects. Element Texture RG is used a World XZ bending. Element Texture A and Particle Color A are used as alpha masks., 0, 15)]_ElementMessage("Element Message", Float) = 0
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
		[Space(10)]_SpeedTresholdValue("Particle Speed Treshold", Float) = 10
		[Space(10)][StyledToggle]_ElementInvertMode("Use Inverted Direction", Float) = 0
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
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			AlphaToMask Off
			Cull Back
			ColorMask [_render_colormask]
			ZWrite Off
			ZTest LEqual
			Offset 0 , 0
			
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
			uniform half _ElementMessage;
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
			uniform sampler2D _MainTex;
			uniform float _ElementInvertMode;
			uniform float _ElementIntensity;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform float _ElementVolumeFadeValue;
			uniform float _ElementVolumeFadeMode;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsPushInteractionSimple)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsPushInteractionSimple
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsPushInteractionSimple)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_color = v.color;
				
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
				half4 MainTex_RGBA587_g23955 = tex2D( _MainTex, ( 1.0 - i.ase_texcoord1.xy ) );
				float3 temp_cast_0 = (0.0001).xxx;
				float3 temp_cast_1 = (0.9999).xxx;
				float3 clampResult17_g23979 = clamp( (MainTex_RGBA587_g23955).rgb , temp_cast_0 , temp_cast_1 );
				float temp_output_7_0_g23965 = _MainTexColorRemap.x;
				float3 temp_cast_2 = (temp_output_7_0_g23965).xxx;
				float temp_output_10_0_g23965 = ( _MainTexColorRemap.y - temp_output_7_0_g23965 );
				float3 temp_output_1765_0_g23955 = saturate( ( ( clampResult17_g23979 - temp_cast_2 ) / temp_output_10_0_g23965 ) );
				half Element_Remap_R73_g23955 = (temp_output_1765_0_g23955).x;
				half Element_Remap_G265_g23955 = (temp_output_1765_0_g23955).y;
				float3 appendResult274_g23955 = (float3((Element_Remap_R73_g23955*2.0 + -1.0) , 0.0 , (Element_Remap_G265_g23955*2.0 + -1.0)));
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float3 break281_g23955 = ( mul( unity_ObjectToWorld, float4( appendResult274_g23955 , 0.0 ) ).xyz / ase_objectScale );
				float2 appendResult1403_g23955 = (float2(break281_g23955.x , break281_g23955.z));
				half2 Direction_Texture284_g23955 = appendResult1403_g23955;
				half Element_InvertMode489_g23955 = _ElementInvertMode;
				float2 lerpResult1471_g23955 = lerp( Direction_Texture284_g23955 , -Direction_Texture284_g23955 , Element_InvertMode489_g23955);
				float3 appendResult292_g23955 = (float3(saturate( (lerpResult1471_g23955*0.5 + 0.5) ) , 0.0));
				half3 Final_MotionInteraction_RGB303_g23955 = appendResult292_g23955;
				float clampResult17_g23980 = clamp( (MainTex_RGBA587_g23955).a , 0.0001 , 0.9999 );
				float temp_output_7_0_g23964 = _MainTexAlphaRemap.x;
				float temp_output_10_0_g23964 = ( _MainTexAlphaRemap.y - temp_output_7_0_g23964 );
				half Element_Remap_A74_g23955 = saturate( ( ( clampResult17_g23980 - temp_output_7_0_g23964 ) / ( temp_output_10_0_g23964 + 0.0001 ) ) );
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half Element_Params_A1737_g23955 = _ElementParams_Instance.w;
				half Element_Falloff1883_g23955 = 1.0;
				float temp_output_7_0_g23984 = 1.0;
				float temp_output_10_0_g23984 = ( _ElementVolumeFadeValue - temp_output_7_0_g23984 );
				float lerpResult18_g23982 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g23984 ) / temp_output_10_0_g23984 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g23955 = ( _ElementIntensity * Element_Remap_A74_g23955 * Element_Params_A1737_g23955 * i.ase_color.a * Element_Falloff1883_g23955 * lerpResult18_g23982 );
				half Final_MotionInteraction_A305_g23955 = Element_Alpha56_g23955;
				float4 appendResult479_g23955 = (float4(Final_MotionInteraction_RGB303_g23955 , Final_MotionInteraction_A305_g23955));
				
				
				finalColor = appendResult479_g23955;
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
			uniform half _ElementMessage;
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
			uniform sampler2D _MainTex;
			uniform float _ElementInvertMode;
			uniform float _ElementIntensity;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform float _ElementVolumeFadeValue;
			uniform float _ElementVolumeFadeMode;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsPushInteractionSimple)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsPushInteractionSimple
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsPushInteractionSimple)

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_color = v.color;
				
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
				half4 MainTex_RGBA587_g23955 = tex2D( _MainTex, ( 1.0 - i.ase_texcoord1.xy ) );
				float3 temp_cast_0 = (0.0001).xxx;
				float3 temp_cast_1 = (0.9999).xxx;
				float3 clampResult17_g23979 = clamp( (MainTex_RGBA587_g23955).rgb , temp_cast_0 , temp_cast_1 );
				float temp_output_7_0_g23965 = _MainTexColorRemap.x;
				float3 temp_cast_2 = (temp_output_7_0_g23965).xxx;
				float temp_output_10_0_g23965 = ( _MainTexColorRemap.y - temp_output_7_0_g23965 );
				float3 temp_output_1765_0_g23955 = saturate( ( ( clampResult17_g23979 - temp_cast_2 ) / temp_output_10_0_g23965 ) );
				half Element_Remap_R73_g23955 = (temp_output_1765_0_g23955).x;
				half Element_Remap_G265_g23955 = (temp_output_1765_0_g23955).y;
				float3 appendResult274_g23955 = (float3((Element_Remap_R73_g23955*2.0 + -1.0) , 0.0 , (Element_Remap_G265_g23955*2.0 + -1.0)));
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float3 break281_g23955 = ( mul( unity_ObjectToWorld, float4( appendResult274_g23955 , 0.0 ) ).xyz / ase_objectScale );
				float2 appendResult1403_g23955 = (float2(break281_g23955.x , break281_g23955.z));
				half2 Direction_Texture284_g23955 = appendResult1403_g23955;
				half Element_InvertMode489_g23955 = _ElementInvertMode;
				float2 lerpResult1471_g23955 = lerp( Direction_Texture284_g23955 , -Direction_Texture284_g23955 , Element_InvertMode489_g23955);
				float3 appendResult292_g23955 = (float3(saturate( (lerpResult1471_g23955*0.5 + 0.5) ) , 0.0));
				half3 Final_MotionInteraction_RGB303_g23955 = appendResult292_g23955;
				half3 Input_Color94_g23990 = Final_MotionInteraction_RGB303_g23955;
				half3 Element_Valid47_g23990 = ( Input_Color94_g23990 * Input_Color94_g23990 );
				float clampResult17_g23980 = clamp( (MainTex_RGBA587_g23955).a , 0.0001 , 0.9999 );
				float temp_output_7_0_g23964 = _MainTexAlphaRemap.x;
				float temp_output_10_0_g23964 = ( _MainTexAlphaRemap.y - temp_output_7_0_g23964 );
				half Element_Remap_A74_g23955 = saturate( ( ( clampResult17_g23980 - temp_output_7_0_g23964 ) / ( temp_output_10_0_g23964 + 0.0001 ) ) );
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half Element_Params_A1737_g23955 = _ElementParams_Instance.w;
				half Element_Falloff1883_g23955 = 1.0;
				float temp_output_7_0_g23984 = 1.0;
				float temp_output_10_0_g23984 = ( _ElementVolumeFadeValue - temp_output_7_0_g23984 );
				float lerpResult18_g23982 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g23984 ) / temp_output_10_0_g23984 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g23955 = ( _ElementIntensity * Element_Remap_A74_g23955 * Element_Params_A1737_g23955 * i.ase_color.a * Element_Falloff1883_g23955 * lerpResult18_g23982 );
				half Final_MotionInteraction_A305_g23955 = Element_Alpha56_g23955;
				half Input_Alpha48_g23990 = Final_MotionInteraction_A305_g23955;
				float4 appendResult58_g23990 = (float4(Element_Valid47_g23990 , Input_Alpha48_g23990));
				
				
				finalColor = appendResult58_g23990;
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
Node;AmplifyShaderEditor.FunctionNode;428;-640,-896;Inherit;False;Element Push;1;;23663;daf80be7c77d54d4bb4554e32ce03c7a;0;0;1;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;380;-64,-1296;Inherit;False;Element Compile;-1;;23665;5302407fc6d65554791e558e67d59358;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;197;-640,-1152;Half;False;Property;_render_colormask;_render_colormask;71;1;[HideInInspector];Create;True;0;0;0;True;0;False;15;15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-640,-1280;Half;False;Property;_ElementMessage;Element Message;0;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use the Interaction elements to add touch bending to the vegetation objects. Element Texture RG is used a World XZ bending. Element Texture A and Particle Color A are used as alpha masks., 0, 15);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;431;-384,-896;Inherit;False;Element Shader;10;;23955;0e972c73cae2ee54ea51acc9738801d0;12,477,2,1778,2,478,0,1824,0,1814,3,145,3,1784,1,1935,1,1933,1,481,1,1904,0,1907,0;1;1974;FLOAT;0;False;2;FLOAT4;0;FLOAT4;1779
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;382;-64,-896;Float;False;True;-1;2;TVEShaderGUIElement;100;16;BOXOPHOBIC/The Visual Engine/Elements/Push Interaction (Simple);f4f273c3bb6262d4396be458405e60f9;True;VolumePass;0;0;VolumePass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;DisableBatching=True=DisableBatching;False;False;0;True;True;2;5;False;;10;False;;2;5;False;;10;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;True;True;True;True;True;True;0;True;_render_colormask;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;3;False;;True;True;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;2;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;383;-64,-768;Float;False;False;-1;2;ASEMaterialInspector;100;16;New Amplify Shader;f4f273c3bb6262d4396be458405e60f9;True;VisualPass;0;1;VisualPass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;False;False;0;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;True;True;True;True;True;True;0;False;_render_colormask;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;0;False;0
WireConnection;431;1974;428;4
WireConnection;382;0;431;0
WireConnection;383;0;431;1779
ASEEND*/
//CHKSM=8D39C932F1F5300BF8B4DEA4D9EC829CB3673C5F