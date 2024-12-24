// Upgrade NOTE: upgraded instancing buffer 'BOXOPHOBICTheVisualEngineElementsPaintNoise' to new syntax.

// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Visual Engine/Elements/Paint Noise"
{
	Properties
	{
		[StyledMessage(Info, Use the Paint Noise elements to add two colors noise tinting to the vegetation assets. Element Texture A is used as alpha mask., 0, 15)]_ElementMessage("Element Message", Float) = 0
		[HideInInspector]_IsVersion("_IsVersion", Float) = 2040
		[HideInInspector]_IsElementShader("_IsElementShader", Float) = 1
		[StyledCategory(Render Settings, true, 0, 10)]_RenderCategory("[ Render Category ]", Float) = 1
		_ElementIntensity("Render Intensity", Range( 0 , 1)) = 1
		[StyledMessage(Info, When using a higher Layer number the Global Volume will create more render textures to render the elements. Try using fewer layers when possible., _ElementLayerMessage, 1, 10, 10)]_ElementLayerMessage("Render Layer Message", Float) = 0
		[StyledMessage(Warning, When using all layers the Global Volume will create one render texture for each layer to render the elements. Try using fewer layers when possible., _ElementLayerWarning, 1, 10, 10)]_ElementLayerWarning("Render Layer Warning", Float) = 0
		[StyledMask(Paint Layers, Default 0 Layer_1 1 Layer_2 2 Layer_3 3 Layer_4 4 Layer_5 5 Layer_6 6 Layer_7 7 Layer_8 8, 0, 0)]_ElementLayerMask("Render Layer", Float) = 1
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
		[Space(10)][StyledRemapSlider]_SeasonRemap("Seasons Curve", Vector) = (0,1,0,0)
		[HDR][Gamma][Space(10)]_NoiseColorOne("Noise ColorA", Color) = (0,1,0,1)
		[HDR][Gamma]_NoiseColorTwo("Noise ColorB", Color) = (1,0,0,1)
		[StyledRemapSlider]_NoiseRemap("Noise Remap", Vector) = (0,1,0,0)
		_NoiseTillingValue("Noise Tilling", Float) = 1
		[Space(10)]_SpeedTresholdValue("Particle Speed Treshold", Float) = 10
		[Space(10)]_InfluenceValue1("Winter Influence", Range( 0 , 1)) = 1
		_InfluenceValue2("Spring Influence", Range( 0 , 1)) = 1
		_InfluenceValue3("Summer Influence", Range( 0 , 1)) = 1
		_InfluenceValue4("Autumn Influence", Range( 0 , 1)) = 1
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
			uniform half _render_colormask;
			uniform half _ElementMessage;
			uniform half4 _NoiseColorOne;
			uniform half4 _NoiseColorTwo;
			uniform half _NoiseTillingValue;
			uniform half4 _NoiseRemap;
			uniform half _ElementIntensity;
			uniform sampler2D _MainTex;
			uniform half _ElementUVsMode;
			uniform half4 _MainUVs;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 _MainTexFalloffRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform half _ElementVolumeFadeValue;
			uniform half _ElementVolumeFadeMode;
			uniform half4 TVE_SeasonOption;
			uniform half _InfluenceValue1;
			uniform half _InfluenceValue2;
			uniform half TVE_SeasonLerp;
			uniform half4 _SeasonRemap;
			uniform half _InfluenceValue3;
			uniform half _InfluenceValue4;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsPaintNoise)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsPaintNoise
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsPaintNoise)
			inline float noise_randomValue (float2 uv) { return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453); }
			inline float noise_interpolate (float a, float b, float t) { return (1.0-t)*a + (t*b); }
			inline float valueNoise (float2 uv)
			{
				float2 i = floor(uv);
				float2 f = frac( uv );
				f = f* f * (3.0 - 2.0 * f);
				uv = abs( frac(uv) - 0.5);
				float2 c0 = i + float2( 0.0, 0.0 );
				float2 c1 = i + float2( 1.0, 0.0 );
				float2 c2 = i + float2( 0.0, 1.0 );
				float2 c3 = i + float2( 1.0, 1.0 );
				float r0 = noise_randomValue( c0 );
				float r1 = noise_randomValue( c1 );
				float r2 = noise_randomValue( c2 );
				float r3 = noise_randomValue( c3 );
				float bottomOfGrid = noise_interpolate( r0, r1, f.x );
				float topOfGrid = noise_interpolate( r2, r3, f.x );
				float t = noise_interpolate( bottomOfGrid, topOfGrid, f.y );
				return t;
			}
			
			float SimpleNoise(float2 UV)
			{
				float t = 0.0;
				float freq = pow( 2.0, float( 0 ) );
				float amp = pow( 0.5, float( 3 - 0 ) );
				t += valueNoise( UV/freq )*amp;
				freq = pow(2.0, float(1));
				amp = pow(0.5, float(3-1));
				t += valueNoise( UV/freq )*amp;
				freq = pow(2.0, float(2));
				amp = pow(0.5, float(3-2));
				t += valueNoise( UV/freq )*amp;
				return t;
			}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldPos = mul(unity_ObjectToWorld, float4( (v.vertex).xyz, 1 )).xyz;
				half2 appendResult1900_g24794 = (half2(ase_worldPos.x , ase_worldPos.z));
				half2 lerpResult1899_g24794 = lerp( ( 1.0 - v.ase_texcoord.xy ) , appendResult1900_g24794 , _ElementUVsMode);
				half2 vertexToFrag11_g24826 = ( ( lerpResult1899_g24794 * (_MainUVs).xy ) + (_MainUVs).zw );
				o.ase_texcoord1.zw = vertexToFrag11_g24826;
				half TVE_SeasonOptions_X50_g24794 = TVE_SeasonOption.x;
				half Influence_Winter808_g24794 = _InfluenceValue1;
				half Influence_Spring814_g24794 = _InfluenceValue2;
				half temp_output_7_0_g24815 = _SeasonRemap.x;
				half temp_output_10_0_g24815 = ( _SeasonRemap.y - temp_output_7_0_g24815 );
				half TVE_SeasonLerp54_g24794 = saturate( saturate( ( ( TVE_SeasonLerp - temp_output_7_0_g24815 ) / temp_output_10_0_g24815 ) ) );
				half lerpResult829_g24794 = lerp( Influence_Winter808_g24794 , Influence_Spring814_g24794 , TVE_SeasonLerp54_g24794);
				half TVE_SeasonOptions_Y51_g24794 = TVE_SeasonOption.y;
				half Influence_Summer815_g24794 = _InfluenceValue3;
				half lerpResult833_g24794 = lerp( Influence_Spring814_g24794 , Influence_Summer815_g24794 , TVE_SeasonLerp54_g24794);
				half TVE_SeasonOptions_Z52_g24794 = TVE_SeasonOption.z;
				half Influence_Autumn810_g24794 = _InfluenceValue4;
				half lerpResult816_g24794 = lerp( Influence_Summer815_g24794 , Influence_Autumn810_g24794 , TVE_SeasonLerp54_g24794);
				half TVE_SeasonOptions_W53_g24794 = TVE_SeasonOption.w;
				half lerpResult817_g24794 = lerp( Influence_Autumn810_g24794 , Influence_Winter808_g24794 , TVE_SeasonLerp54_g24794);
				half vertexToFrag11_g24797 = ( ( TVE_SeasonOptions_X50_g24794 * lerpResult829_g24794 ) + ( TVE_SeasonOptions_Y51_g24794 * lerpResult833_g24794 ) + ( TVE_SeasonOptions_Z52_g24794 * lerpResult816_g24794 ) + ( TVE_SeasonOptions_W53_g24794 * lerpResult817_g24794 ) );
				o.ase_texcoord2.x = vertexToFrag11_g24797;
				
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_color = v.color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.yzw = 0;
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
				half Noise_Scale892_g24794 = _NoiseTillingValue;
				half simpleNoise775_g24794 = SimpleNoise( i.ase_texcoord1.xy*Noise_Scale892_g24794 );
				half Noise_Min893_g24794 = _NoiseRemap.x;
				half temp_output_7_0_g24798 = Noise_Min893_g24794;
				half Noise_Max894_g24794 = _NoiseRemap.y;
				half temp_output_10_0_g24798 = ( Noise_Max894_g24794 - temp_output_7_0_g24798 );
				half WorldNoise1483_g24794 = saturate( ( ( simpleNoise775_g24794 - temp_output_7_0_g24798 ) / temp_output_10_0_g24798 ) );
				half4 lerpResult772_g24794 = lerp( _NoiseColorOne , _NoiseColorTwo , WorldNoise1483_g24794);
				half3 Final_ColorsNoise_RGB784_g24794 = (lerpResult772_g24794).rgb;
				half2 vertexToFrag11_g24826 = i.ase_texcoord1.zw;
				half4 MainTex_RGBA587_g24794 = tex2D( _MainTex, vertexToFrag11_g24826 );
				half clampResult17_g24821 = clamp( (MainTex_RGBA587_g24794).a , 0.0001 , 0.9999 );
				half temp_output_7_0_g24803 = _MainTexAlphaRemap.x;
				half temp_output_10_0_g24803 = ( _MainTexAlphaRemap.y - temp_output_7_0_g24803 );
				half Element_Remap_A74_g24794 = saturate( ( ( clampResult17_g24821 - temp_output_7_0_g24803 ) / ( temp_output_10_0_g24803 + 0.0001 ) ) );
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half Element_Params_A1737_g24794 = _ElementParams_Instance.w;
				half clampResult17_g24801 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord1.xy*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				half temp_output_7_0_g24802 = _MainTexFalloffRemap.x;
				half temp_output_10_0_g24802 = ( _MainTexFalloffRemap.y - temp_output_7_0_g24802 );
				half Element_Falloff1883_g24794 = saturate( ( ( clampResult17_g24801 - temp_output_7_0_g24802 ) / ( temp_output_10_0_g24802 + 0.0001 ) ) );
				half temp_output_7_0_g24825 = 1.0;
				half temp_output_10_0_g24825 = ( _ElementVolumeFadeValue - temp_output_7_0_g24825 );
				half lerpResult18_g24823 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g24825 ) / temp_output_10_0_g24825 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g24794 = ( _ElementIntensity * Element_Remap_A74_g24794 * Element_Params_A1737_g24794 * i.ase_color.a * Element_Falloff1883_g24794 * lerpResult18_g24823 );
				half vertexToFrag11_g24797 = i.ase_texcoord2.x;
				half Element_Seasons834_g24794 = vertexToFrag11_g24797;
				half Final_ColorsNoise_A785_g24794 = ( (lerpResult772_g24794).a * Element_Alpha56_g24794 * Element_Seasons834_g24794 );
				half4 appendResult790_g24794 = (half4(Final_ColorsNoise_RGB784_g24794 , Final_ColorsNoise_A785_g24794));
				
				
				finalColor = appendResult790_g24794;
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
			uniform half _render_colormask;
			uniform half _ElementMessage;
			uniform half4 _NoiseColorOne;
			uniform half4 _NoiseColorTwo;
			uniform half _NoiseTillingValue;
			uniform half4 _NoiseRemap;
			uniform half _ElementIntensity;
			uniform sampler2D _MainTex;
			uniform half _ElementUVsMode;
			uniform half4 _MainUVs;
			uniform half4 _MainTexAlphaRemap;
			uniform half4 _MainTexFalloffRemap;
			uniform half4 TVE_RenderBasePositionR;
			uniform half _ElementVolumeFadeValue;
			uniform half _ElementVolumeFadeMode;
			uniform half4 TVE_SeasonOption;
			uniform half _InfluenceValue1;
			uniform half _InfluenceValue2;
			uniform half TVE_SeasonLerp;
			uniform half4 _SeasonRemap;
			uniform half _InfluenceValue3;
			uniform half _InfluenceValue4;
			UNITY_INSTANCING_BUFFER_START(BOXOPHOBICTheVisualEngineElementsPaintNoise)
				UNITY_DEFINE_INSTANCED_PROP(half4, _ElementParams)
#define _ElementParams_arr BOXOPHOBICTheVisualEngineElementsPaintNoise
			UNITY_INSTANCING_BUFFER_END(BOXOPHOBICTheVisualEngineElementsPaintNoise)
			inline float noise_randomValue (float2 uv) { return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453); }
			inline float noise_interpolate (float a, float b, float t) { return (1.0-t)*a + (t*b); }
			inline float valueNoise (float2 uv)
			{
				float2 i = floor(uv);
				float2 f = frac( uv );
				f = f* f * (3.0 - 2.0 * f);
				uv = abs( frac(uv) - 0.5);
				float2 c0 = i + float2( 0.0, 0.0 );
				float2 c1 = i + float2( 1.0, 0.0 );
				float2 c2 = i + float2( 0.0, 1.0 );
				float2 c3 = i + float2( 1.0, 1.0 );
				float r0 = noise_randomValue( c0 );
				float r1 = noise_randomValue( c1 );
				float r2 = noise_randomValue( c2 );
				float r3 = noise_randomValue( c3 );
				float bottomOfGrid = noise_interpolate( r0, r1, f.x );
				float topOfGrid = noise_interpolate( r2, r3, f.x );
				float t = noise_interpolate( bottomOfGrid, topOfGrid, f.y );
				return t;
			}
			
			float SimpleNoise(float2 UV)
			{
				float t = 0.0;
				float freq = pow( 2.0, float( 0 ) );
				float amp = pow( 0.5, float( 3 - 0 ) );
				t += valueNoise( UV/freq )*amp;
				freq = pow(2.0, float(1));
				amp = pow(0.5, float(3-1));
				t += valueNoise( UV/freq )*amp;
				freq = pow(2.0, float(2));
				amp = pow(0.5, float(3-2));
				t += valueNoise( UV/freq )*amp;
				return t;
			}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldPos = mul(unity_ObjectToWorld, float4( (v.vertex).xyz, 1 )).xyz;
				half2 appendResult1900_g24794 = (half2(ase_worldPos.x , ase_worldPos.z));
				half2 lerpResult1899_g24794 = lerp( ( 1.0 - v.ase_texcoord.xy ) , appendResult1900_g24794 , _ElementUVsMode);
				half2 vertexToFrag11_g24826 = ( ( lerpResult1899_g24794 * (_MainUVs).xy ) + (_MainUVs).zw );
				o.ase_texcoord1.zw = vertexToFrag11_g24826;
				half TVE_SeasonOptions_X50_g24794 = TVE_SeasonOption.x;
				half Influence_Winter808_g24794 = _InfluenceValue1;
				half Influence_Spring814_g24794 = _InfluenceValue2;
				half temp_output_7_0_g24815 = _SeasonRemap.x;
				half temp_output_10_0_g24815 = ( _SeasonRemap.y - temp_output_7_0_g24815 );
				half TVE_SeasonLerp54_g24794 = saturate( saturate( ( ( TVE_SeasonLerp - temp_output_7_0_g24815 ) / temp_output_10_0_g24815 ) ) );
				half lerpResult829_g24794 = lerp( Influence_Winter808_g24794 , Influence_Spring814_g24794 , TVE_SeasonLerp54_g24794);
				half TVE_SeasonOptions_Y51_g24794 = TVE_SeasonOption.y;
				half Influence_Summer815_g24794 = _InfluenceValue3;
				half lerpResult833_g24794 = lerp( Influence_Spring814_g24794 , Influence_Summer815_g24794 , TVE_SeasonLerp54_g24794);
				half TVE_SeasonOptions_Z52_g24794 = TVE_SeasonOption.z;
				half Influence_Autumn810_g24794 = _InfluenceValue4;
				half lerpResult816_g24794 = lerp( Influence_Summer815_g24794 , Influence_Autumn810_g24794 , TVE_SeasonLerp54_g24794);
				half TVE_SeasonOptions_W53_g24794 = TVE_SeasonOption.w;
				half lerpResult817_g24794 = lerp( Influence_Autumn810_g24794 , Influence_Winter808_g24794 , TVE_SeasonLerp54_g24794);
				half vertexToFrag11_g24797 = ( ( TVE_SeasonOptions_X50_g24794 * lerpResult829_g24794 ) + ( TVE_SeasonOptions_Y51_g24794 * lerpResult833_g24794 ) + ( TVE_SeasonOptions_Z52_g24794 * lerpResult816_g24794 ) + ( TVE_SeasonOptions_W53_g24794 * lerpResult817_g24794 ) );
				o.ase_texcoord2.x = vertexToFrag11_g24797;
				
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_color = v.color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.yzw = 0;
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
				half Noise_Scale892_g24794 = _NoiseTillingValue;
				half simpleNoise775_g24794 = SimpleNoise( i.ase_texcoord1.xy*Noise_Scale892_g24794 );
				half Noise_Min893_g24794 = _NoiseRemap.x;
				half temp_output_7_0_g24798 = Noise_Min893_g24794;
				half Noise_Max894_g24794 = _NoiseRemap.y;
				half temp_output_10_0_g24798 = ( Noise_Max894_g24794 - temp_output_7_0_g24798 );
				half WorldNoise1483_g24794 = saturate( ( ( simpleNoise775_g24794 - temp_output_7_0_g24798 ) / temp_output_10_0_g24798 ) );
				half4 lerpResult772_g24794 = lerp( _NoiseColorOne , _NoiseColorTwo , WorldNoise1483_g24794);
				half3 Final_ColorsNoise_RGB784_g24794 = (lerpResult772_g24794).rgb;
				half3 Input_Color94_g24807 = Final_ColorsNoise_RGB784_g24794;
				half3 Element_Valid47_g24807 = Input_Color94_g24807;
				half2 vertexToFrag11_g24826 = i.ase_texcoord1.zw;
				half4 MainTex_RGBA587_g24794 = tex2D( _MainTex, vertexToFrag11_g24826 );
				half clampResult17_g24821 = clamp( (MainTex_RGBA587_g24794).a , 0.0001 , 0.9999 );
				half temp_output_7_0_g24803 = _MainTexAlphaRemap.x;
				half temp_output_10_0_g24803 = ( _MainTexAlphaRemap.y - temp_output_7_0_g24803 );
				half Element_Remap_A74_g24794 = saturate( ( ( clampResult17_g24821 - temp_output_7_0_g24803 ) / ( temp_output_10_0_g24803 + 0.0001 ) ) );
				half4 _ElementParams_Instance = UNITY_ACCESS_INSTANCED_PROP(_ElementParams_arr, _ElementParams);
				half Element_Params_A1737_g24794 = _ElementParams_Instance.w;
				half clampResult17_g24801 = clamp( saturate( ( 1.0 - distance( (i.ase_texcoord1.xy*2.0 + -1.0) , float2( 0,0 ) ) ) ) , 0.0001 , 0.9999 );
				half temp_output_7_0_g24802 = _MainTexFalloffRemap.x;
				half temp_output_10_0_g24802 = ( _MainTexFalloffRemap.y - temp_output_7_0_g24802 );
				half Element_Falloff1883_g24794 = saturate( ( ( clampResult17_g24801 - temp_output_7_0_g24802 ) / ( temp_output_10_0_g24802 + 0.0001 ) ) );
				half temp_output_7_0_g24825 = 1.0;
				half temp_output_10_0_g24825 = ( _ElementVolumeFadeValue - temp_output_7_0_g24825 );
				half lerpResult18_g24823 = lerp( 1.0 , saturate( ( ( saturate( ( distance( WorldPosition , (TVE_RenderBasePositionR).xyz ) / (TVE_RenderBasePositionR).w ) ) - temp_output_7_0_g24825 ) / temp_output_10_0_g24825 ) ) , _ElementVolumeFadeMode);
				half Element_Alpha56_g24794 = ( _ElementIntensity * Element_Remap_A74_g24794 * Element_Params_A1737_g24794 * i.ase_color.a * Element_Falloff1883_g24794 * lerpResult18_g24823 );
				half vertexToFrag11_g24797 = i.ase_texcoord2.x;
				half Element_Seasons834_g24794 = vertexToFrag11_g24797;
				half Final_ColorsNoise_A785_g24794 = ( (lerpResult772_g24794).a * Element_Alpha56_g24794 * Element_Seasons834_g24794 );
				half Input_Alpha48_g24807 = Final_ColorsNoise_A785_g24794;
				half4 appendResult58_g24807 = (half4(Element_Valid47_g24807 , Input_Alpha48_g24807));
				
				
				finalColor = appendResult58_g24807;
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
Node;AmplifyShaderEditor.FunctionNode;187;-640,-384;Inherit;False;Element Paint;1;;24792;5810d2854679b554b88f8bb18ff3bfa0;0;0;1;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;185;-384,-384;Inherit;False;Element Shader;10;;24794;0e972c73cae2ee54ea51acc9738801d0;12,477,0,1778,0,478,2,1824,2,1814,0,145,0,1784,0,1935,1,1933,1,481,0,1904,1,1907,1;1;1974;FLOAT;1;False;2;FLOAT4;0;FLOAT4;1779
Node;AmplifyShaderEditor.FunctionNode;181;-64,-768;Inherit;False;Element Compile;-1;;25007;5302407fc6d65554791e558e67d59358;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;159;-640,-640;Half;False;Property;_render_colormask;_render_colormask;70;1;[HideInInspector];Create;True;0;0;0;True;0;False;15;15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;100;-640,-768;Half;False;Property;_ElementMessage;Element Message;0;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use the Paint Noise elements to add two colors noise tinting to the vegetation assets. Element Texture A is used as alpha mask., 0, 15);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;182;-64,-384;Half;False;True;-1;2;TVEShaderGUIElement;100;16;BOXOPHOBIC/The Visual Engine/Elements/Paint Noise;f4f273c3bb6262d4396be458405e60f9;True;VolumePass;0;0;VolumePass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;DisableBatching=True=DisableBatching;False;False;0;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;2;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;183;-64,-272;Float;False;False;-1;2;ASEMaterialInspector;100;16;New Amplify Shader;f4f273c3bb6262d4396be458405e60f9;True;VisualPass;0;1;VisualPass;2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;False;False;0;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;0;False;;True;False;0;False;;0;False;;False;True;2;False;0;;0;0;Standard;0;False;0
WireConnection;185;1974;187;4
WireConnection;182;0;185;0
WireConnection;183;0;185;1779
ASEEND*/
//CHKSM=65E0522E3C649C65618EA05699AB5CA29DB4852C