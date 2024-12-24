// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Visual Engine/Elements/Paint Maps"
{
	Properties
	{
		[HideInInspector]_IsVersion("_IsVersion", Float) = 2040
		[HideInInspector]_IsElementShader("_IsElementShader", Float) = 1
		[StyledCategory(Render Settings, true, 0, 10)]_RenderCategory("[ Render Category ]", Float) = 1
		_ElementIntensity("Render Intensity", Range( 0 , 1)) = 1
		[StyledMessage(Info, When using a higher Layer number the Global Volume will create more render textures to render the elements. Try using fewer layers when possible., _ElementLayerMessage, 1, 10, 10)]_ElementLayerMessage("Render Layer Message", Float) = 0
		[StyledMessage(Warning, When using all layers the Global Volume will create one render texture for each layer to render the elements. Try using fewer layers when possible., _ElementLayerWarning, 1, 10, 10)]_ElementLayerWarning("Render Layer Warning", Float) = 0
		[StyledMask(Paint Layers, Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 7 Layer_8 8, 0, 0)]_ElementLayerMask("Render Layer", Float) = 1
		[StyledSpace(10)]_EndRender("[ End Render ]", Float) = 1
		[StyledCategory(Element Settings, true, 0, 10)]_CategoryElement("[ Category Element ]", Float) = 1
		[NoScaleOffset][StyledTextureSingleLine]_MainTex1("Winter Map", 2D) = "white" {}
		[NoScaleOffset][StyledTextureSingleLine]_MainTex2("Spring Map", 2D) = "white" {}
		[NoScaleOffset][StyledTextureSingleLine]_MainTex3("Summer Map", 2D) = "white" {}
		[NoScaleOffset][StyledTextureSingleLine]_MainTex4("Autumn Map", 2D) = "white" {}
		[Space(10)][StyledRemapSlider]_SeasonRemap("Seasons Curve", Vector) = (0,1,0,0)
		[StyledSpace(10)]_EndElement("[ End Element ]", Float) = 1
		[StyledCategory(Fading Settings, true, 0, 10)]_CategoryFade("[ Category Fade ]", Float) = 1
		[Enum(Off,0,On,1)]_ElementVolumeFadeMode("Bounds Fade", Float) = 0
		_ElementVolumeFadeValue("Bounds Fade Blend", Range( 0 , 1)) = 0.75
		[StyledSpace(10)]_EndFade("[ End Fade ]", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

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
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _IsVersion;
			uniform half _IsElementShader;
			uniform half _RenderCategory;
			uniform half _ElementLayerMessage;
			uniform half _ElementLayerWarning;
			uniform half _ElementLayerMask;
			uniform half _CategoryElement;
			uniform half _CategoryFade;
			uniform half _EndElement;
			uniform half _EndFade;
			uniform half _EndRender;
			uniform half4 TVE_SeasonOption;
			uniform sampler2D _MainTex1;
			uniform sampler2D _MainTex2;
			uniform half TVE_SeasonLerp;
			uniform half4 _SeasonRemap;
			uniform sampler2D _MainTex3;
			uniform sampler2D _MainTex4;
			uniform float _ElementIntensity;
			uniform half4 TVE_RenderBasePositionR;
			uniform float _ElementVolumeFadeValue;
			uniform float _ElementVolumeFadeMode;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
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
				half TVE_SeasonOptions_X227 = TVE_SeasonOption.x;
				float2 uv_MainTex1248 = i.ase_texcoord1.xy;
				half3 Value_Winter251 = tex2D( _MainTex1, uv_MainTex1248 ).rgb;
				float2 uv_MainTex2253 = i.ase_texcoord1.xy;
				half3 Value_Spring249 = tex2D( _MainTex2, uv_MainTex2253 ).rgb;
				float temp_output_7_0_g22367 = _SeasonRemap.x;
				float temp_output_10_0_g22367 = ( _SeasonRemap.y - temp_output_7_0_g22367 );
				half TVE_SeasonLerp218 = saturate( saturate( ( ( TVE_SeasonLerp - temp_output_7_0_g22367 ) / temp_output_10_0_g22367 ) ) );
				float3 lerpResult261 = lerp( Value_Winter251 , Value_Spring249 , TVE_SeasonLerp218);
				half TVE_SeasonOptions_Y219 = TVE_SeasonOption.y;
				float2 uv_MainTex3254 = i.ase_texcoord1.xy;
				half3 Value_Summer250 = tex2D( _MainTex3, uv_MainTex3254 ).rgb;
				float3 lerpResult268 = lerp( Value_Spring249 , Value_Summer250 , TVE_SeasonLerp218);
				half TVE_SeasonOptions_Z220 = TVE_SeasonOption.z;
				float2 uv_MainTex4255 = i.ase_texcoord1.xy;
				half3 Value_Autumn252 = tex2D( _MainTex4, uv_MainTex4255 ).rgb;
				float3 lerpResult269 = lerp( Value_Summer250 , Value_Autumn252 , TVE_SeasonLerp218);
				half TVE_SeasonOptions_W221 = TVE_SeasonOption.w;
				float3 lerpResult267 = lerp( Value_Autumn252 , Value_Winter251 , TVE_SeasonLerp218);
				half3 Value_Seasons277 = ( ( TVE_SeasonOptions_X227 * lerpResult261 ) + ( TVE_SeasonOptions_Y219 * lerpResult268 ) + ( TVE_SeasonOptions_Z220 * lerpResult269 ) + ( TVE_SeasonOptions_W221 * lerpResult267 ) );
				float temp_output_7_0_g22370 = 1.0;
				float temp_output_10_0_g22370 = ( _ElementVolumeFadeValue - temp_output_7_0_g22370 );
				float lerpResult18_g22368 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g22370 ) / temp_output_10_0_g22370 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha182 = ( _ElementIntensity * lerpResult18_g22368 );
				float4 appendResult169 = (float4(Value_Seasons277 , Element_Alpha182));
				
				
				finalColor = appendResult169;
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
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _IsVersion;
			uniform half _IsElementShader;
			uniform half _RenderCategory;
			uniform half _ElementLayerMessage;
			uniform half _ElementLayerWarning;
			uniform half _ElementLayerMask;
			uniform half _CategoryElement;
			uniform half _CategoryFade;
			uniform half _EndElement;
			uniform half _EndFade;
			uniform half _EndRender;
			uniform half4 TVE_SeasonOption;
			uniform sampler2D _MainTex1;
			uniform sampler2D _MainTex2;
			uniform half TVE_SeasonLerp;
			uniform half4 _SeasonRemap;
			uniform sampler2D _MainTex3;
			uniform sampler2D _MainTex4;
			uniform float _ElementIntensity;
			uniform half4 TVE_RenderBasePositionR;
			uniform float _ElementVolumeFadeValue;
			uniform float _ElementVolumeFadeMode;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
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
				half TVE_SeasonOptions_X227 = TVE_SeasonOption.x;
				float2 uv_MainTex1248 = i.ase_texcoord1.xy;
				half3 Value_Winter251 = tex2D( _MainTex1, uv_MainTex1248 ).rgb;
				float2 uv_MainTex2253 = i.ase_texcoord1.xy;
				half3 Value_Spring249 = tex2D( _MainTex2, uv_MainTex2253 ).rgb;
				float temp_output_7_0_g22367 = _SeasonRemap.x;
				float temp_output_10_0_g22367 = ( _SeasonRemap.y - temp_output_7_0_g22367 );
				half TVE_SeasonLerp218 = saturate( saturate( ( ( TVE_SeasonLerp - temp_output_7_0_g22367 ) / temp_output_10_0_g22367 ) ) );
				float3 lerpResult261 = lerp( Value_Winter251 , Value_Spring249 , TVE_SeasonLerp218);
				half TVE_SeasonOptions_Y219 = TVE_SeasonOption.y;
				float2 uv_MainTex3254 = i.ase_texcoord1.xy;
				half3 Value_Summer250 = tex2D( _MainTex3, uv_MainTex3254 ).rgb;
				float3 lerpResult268 = lerp( Value_Spring249 , Value_Summer250 , TVE_SeasonLerp218);
				half TVE_SeasonOptions_Z220 = TVE_SeasonOption.z;
				float2 uv_MainTex4255 = i.ase_texcoord1.xy;
				half3 Value_Autumn252 = tex2D( _MainTex4, uv_MainTex4255 ).rgb;
				float3 lerpResult269 = lerp( Value_Summer250 , Value_Autumn252 , TVE_SeasonLerp218);
				half TVE_SeasonOptions_W221 = TVE_SeasonOption.w;
				float3 lerpResult267 = lerp( Value_Autumn252 , Value_Winter251 , TVE_SeasonLerp218);
				half3 Value_Seasons277 = ( ( TVE_SeasonOptions_X227 * lerpResult261 ) + ( TVE_SeasonOptions_Y219 * lerpResult268 ) + ( TVE_SeasonOptions_Z220 * lerpResult269 ) + ( TVE_SeasonOptions_W221 * lerpResult267 ) );
				half3 Input_Color94_g22374 = Value_Seasons277;
				half3 Element_Valid47_g22374 = Input_Color94_g22374;
				float temp_output_7_0_g22370 = 1.0;
				float temp_output_10_0_g22370 = ( _ElementVolumeFadeValue - temp_output_7_0_g22370 );
				float lerpResult18_g22368 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g22370 ) / temp_output_10_0_g22370 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha182 = ( _ElementIntensity * lerpResult18_g22368 );
				half Input_Alpha48_g22374 = Element_Alpha182;
				float4 appendResult58_g22374 = (float4(Element_Valid47_g22374 , Input_Alpha48_g22374));
				
				
				finalColor = appendResult58_g22374;
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
Node;AmplifyShaderEditor.RangedFloatNode;225;-1920,-448;Half;False;Global;TVE_SeasonLerp;TVE_SeasonLerp;14;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;226;-1920,-384;Half;False;Property;_SeasonRemap;Seasons Curve;16;0;Create;False;0;0;0;False;2;Space(10);StyledRemapSlider;False;0,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;222;-1600,-448;Inherit;False;Math Remap;-1;;22367;5eda8a2bb94ebef4ab0e43d19291cd8b;1,14,1;3;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;223;-1408,-448;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;248;-3840,1664;Inherit;True;Property;_MainTex1;Winter Map;12;1;[NoScaleOffset];Create;False;0;0;0;False;1;StyledTextureSingleLine;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode;253;-3840,1920;Inherit;True;Property;_MainTex2;Spring Map;13;1;[NoScaleOffset];Create;False;0;0;0;False;1;StyledTextureSingleLine;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode;255;-3840,2432;Inherit;True;Property;_MainTex4;Autumn Map;15;1;[NoScaleOffset];Create;False;0;0;0;False;1;StyledTextureSingleLine;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode;254;-3840,2176;Inherit;True;Property;_MainTex3;Summer Map;14;1;[NoScaleOffset];Create;False;0;0;0;False;1;StyledTextureSingleLine;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode;218;-1216,-448;Half;False;TVE_SeasonLerp;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;251;-3456,1664;Half;False;Value_Winter;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector4Node;224;-1920,-768;Half;False;Global;TVE_SeasonOption;TVE_SeasonOption;14;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,1,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;249;-3456,1920;Half;False;Value_Spring;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;250;-3456,2176;Half;False;Value_Summer;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;252;-3456,2432;Half;False;Value_Autumn;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;257;-1920,1920;Inherit;False;249;Value_Spring;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;276;-1920,2560;Inherit;False;218;TVE_SeasonLerp;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;256;-1920,1664;Inherit;False;251;Value_Winter;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;219;-1248,-672;Half;False;TVE_SeasonOptions_Y;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;220;-1248,-608;Half;False;TVE_SeasonOptions_Z;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;221;-1248,-544;Half;False;TVE_SeasonOptions_W;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;227;-1248,-736;Half;False;TVE_SeasonOptions_X;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;258;-1920,2176;Inherit;False;250;Value_Summer;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;259;-1920,2432;Inherit;False;252;Value_Autumn;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;261;-1536,1792;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;260;-1536,1664;Inherit;False;227;TVE_SeasonOptions_X;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;262;-1536,1920;Inherit;False;219;TVE_SeasonOptions_Y;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;267;-1536,2560;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;268;-1536,2048;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;269;-1536,2304;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;270;-1536,2176;Inherit;False;220;TVE_SeasonOptions_Z;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;271;-1536,2432;Inherit;False;221;TVE_SeasonOptions_W;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;195;-1920,1184;Inherit;False;Element Fade Volume;19;;22368;4935729172cdadd45b9b14c3fa9c1db4;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;202;-1920,1120;Inherit;False;Element Paint;1;;22371;5810d2854679b554b88f8bb18ff3bfa0;0;0;1;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;263;-1280,1920;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;264;-1280,1664;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;272;-1280,2432;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;273;-1280,2176;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;170;-1664,1120;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;265;-896,1664;Inherit;False;4;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;182;-1216,1120;Half;False;Element_Alpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;277;-576,1664;Half;False;Value_Seasons;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;278;256,2048;Inherit;False;277;Value_Seasons;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;184;256,2112;Inherit;False;182;Element_Alpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;167;-1168,-1408;Inherit;False;Element Compile;-1;;22373;5302407fc6d65554791e558e67d59358;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;163;-1664,-1408;Half;False;Property;_CategoryElement;[ Category Element ];11;0;Create;True;0;0;0;True;1;StyledCategory(Element Settings, true, 0, 10);False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;171;-1456,-1408;Half;False;Property;_CategoryFade;[ Category Fade ];18;0;Create;True;0;0;0;True;1;StyledCategory(Fading Settings, true, 0, 10);False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;196;-1664,-1344;Half;False;Property;_EndElement;[ End Element ];17;0;Create;True;0;0;0;True;1;StyledSpace(10);False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;197;-1456,-1344;Half;False;Property;_EndFade;[ End Fade ];22;0;Create;True;0;0;0;True;1;StyledSpace(10);False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;200;-1664,-1280;Half;False;Property;_EndRender;[ End Render ];10;0;Create;True;0;0;0;True;1;StyledSpace(10);False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;-1920,-1408;Half;False;Property;_ElementMessage;Element Message;0;0;Create;True;0;0;0;False;1;StyledMessage(Info, Use this element when your scene is just too static., 0, 15);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;176;256,1664;Inherit;False;277;Value_Seasons;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;169;640,1664;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;183;256,1728;Inherit;False;182;Element_Alpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;180;640,2048;Inherit;False;Element Visuals;-1;;22374;78cf0f00cfd72824e84597f2db54a76e;1,64,0;3;59;FLOAT3;0,0,0;False;117;FLOAT;0;False;77;COLOR;1,1,1,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;177;1088,1664;Float;False;True;-1;2;TVEShaderGUIElement;100;16;BOXOPHOBIC/The Visual Engine/Elements/Paint Maps;f4f273c3bb6262d4396be458405e60f9;True;VolumePass;0;0;VolumePass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;DisableBatching=True=DisableBatching;False;False;0;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;True;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;2;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;178;1088,2048;Float;False;False;-1;2;ASEMaterialInspector;100;16;New Amplify Shader;f4f273c3bb6262d4396be458405e60f9;True;VisualPass;0;1;VisualPass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;False;False;0;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;0;False;0
WireConnection;222;6;225;0
WireConnection;222;7;226;1
WireConnection;222;8;226;2
WireConnection;223;0;222;0
WireConnection;218;0;223;0
WireConnection;251;0;248;5
WireConnection;249;0;253;5
WireConnection;250;0;254;5
WireConnection;252;0;255;5
WireConnection;219;0;224;2
WireConnection;220;0;224;3
WireConnection;221;0;224;4
WireConnection;227;0;224;1
WireConnection;261;0;256;0
WireConnection;261;1;257;0
WireConnection;261;2;276;0
WireConnection;267;0;259;0
WireConnection;267;1;256;0
WireConnection;267;2;276;0
WireConnection;268;0;257;0
WireConnection;268;1;258;0
WireConnection;268;2;276;0
WireConnection;269;0;258;0
WireConnection;269;1;259;0
WireConnection;269;2;276;0
WireConnection;263;0;262;0
WireConnection;263;1;268;0
WireConnection;264;0;260;0
WireConnection;264;1;261;0
WireConnection;272;0;271;0
WireConnection;272;1;267;0
WireConnection;273;0;270;0
WireConnection;273;1;269;0
WireConnection;170;0;202;4
WireConnection;170;1;195;0
WireConnection;265;0;264;0
WireConnection;265;1;263;0
WireConnection;265;2;273;0
WireConnection;265;3;272;0
WireConnection;182;0;170;0
WireConnection;277;0;265;0
WireConnection;169;0;176;0
WireConnection;169;3;183;0
WireConnection;180;59;278;0
WireConnection;180;117;184;0
WireConnection;177;0;169;0
WireConnection;178;0;180;0
ASEEND*/
//CHKSM=E033C7FC9477472FF598A2849B5363581E273BF6