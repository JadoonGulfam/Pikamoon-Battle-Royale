// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Visual Engine/Effects/CustomRT Motion"
{
    Properties
    {
		[StyledBanner(CustomRT Motion)]_BANNER("[ BANNER ]", Float) = 0
		[StyledMessage(Info, Use the Noise value to randomize the wind direction and the Min and Max Speed values to change the noise speed based on the wind intensity. , 0, 10)]_MotionInfo("MotionInfo", Float) = 0
		[NoScaleOffset][StyledTextureSingleLine]_MotionNoiseTex("Motion Texture", 2D) = "white" {}
		[StyledSpace(10)]_Space("Space", Float) = 0
		_MotionNoiseValue("Motion Wind Noise", Range( 0 , 1)) = 0.5
		_MotionSpeedMinValue("Motion Wind Speed Min", Range( 0 , 50)) = 4
		_MotionSpeedMaxValue("Motion Wind Speed Max", Range( 0 , 50)) = 8

    }

	SubShader
	{
		LOD 0

		
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
        Pass
        {
			Name "Custom RT Update"
            CGPROGRAM
            #define ASE_USING_SAMPLING_MACROS 1

            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex ASECustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0
			#include "UnityShaderVariables.cginc"
			#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
			#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
			#else//ASE Sampling Macros
			#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
			#endif//ASE Sampling Macros
			


			struct ase_appdata_customrendertexture
			{
				uint vertexID : SV_VertexID;
				
			};

			struct ase_v2f_customrendertexture
			{
				float4 vertex           : SV_POSITION;
				float3 localTexcoord    : TEXCOORD0;    // Texcoord local to the update zone (== globalTexcoord if no partial update zone is specified)
				float3 globalTexcoord   : TEXCOORD1;    // Texcoord relative to the complete custom texture
				uint primitiveID        : TEXCOORD2;    // Index of the update zone (correspond to the index in the updateZones of the Custom Texture)
				float3 direction        : TEXCOORD3;    // For cube textures, direction of the pixel being rendered in the cubemap
				
			};

			uniform float _BANNER;
			uniform float _Space;
			uniform float _MotionInfo;
			uniform half4 TVE_MotionParams;
			uniform half TVE_IsEnabled;
			UNITY_DECLARE_TEX2D_NOSAMPLER(_MotionNoiseTex);
			uniform half4 TVE_TimeParams;
			uniform half _MotionSpeedMinValue;
			SamplerState sampler_Linear_Repeat;
			uniform half _MotionSpeedMaxValue;
			uniform half _MotionNoiseValue;


			ase_v2f_customrendertexture ASECustomRenderTextureVertexShader(ase_appdata_customrendertexture IN  )
			{
				ase_v2f_customrendertexture OUT;
				
			#if UNITY_UV_STARTS_AT_TOP
				const float2 vertexPositions[6] =
				{
					{ -1.0f,  1.0f },
					{ -1.0f, -1.0f },
					{  1.0f, -1.0f },
					{  1.0f,  1.0f },
					{ -1.0f,  1.0f },
					{  1.0f, -1.0f }
				};

				const float2 texCoords[6] =
				{
					{ 0.0f, 0.0f },
					{ 0.0f, 1.0f },
					{ 1.0f, 1.0f },
					{ 1.0f, 0.0f },
					{ 0.0f, 0.0f },
					{ 1.0f, 1.0f }
				};
			#else
				const float2 vertexPositions[6] =
				{
					{  1.0f,  1.0f },
					{ -1.0f, -1.0f },
					{ -1.0f,  1.0f },
					{ -1.0f, -1.0f },
					{  1.0f,  1.0f },
					{  1.0f, -1.0f }
				};

				const float2 texCoords[6] =
				{
					{ 1.0f, 1.0f },
					{ 0.0f, 0.0f },
					{ 0.0f, 1.0f },
					{ 0.0f, 0.0f },
					{ 1.0f, 1.0f },
					{ 1.0f, 0.0f }
				};
			#endif

				uint primitiveID = IN.vertexID / 6;
				uint vertexID = IN.vertexID % 6;
				float3 updateZoneCenter = CustomRenderTextureCenters[primitiveID].xyz;
				float3 updateZoneSize = CustomRenderTextureSizesAndRotations[primitiveID].xyz;
				float rotation = CustomRenderTextureSizesAndRotations[primitiveID].w * UNITY_PI / 180.0f;

			#if !UNITY_UV_STARTS_AT_TOP
				rotation = -rotation;
			#endif

				// Normalize rect if needed
				if (CustomRenderTextureUpdateSpace > 0.0) // Pixel space
				{
					// Normalize xy because we need it in clip space.
					updateZoneCenter.xy /= _CustomRenderTextureInfo.xy;
					updateZoneSize.xy /= _CustomRenderTextureInfo.xy;
				}
				else // normalized space
				{
					// Un-normalize depth because we need actual slice index for culling
					updateZoneCenter.z *= _CustomRenderTextureInfo.z;
					updateZoneSize.z *= _CustomRenderTextureInfo.z;
				}

				// Compute rotation

				// Compute quad vertex position
				float2 clipSpaceCenter = updateZoneCenter.xy * 2.0 - 1.0;
				float2 pos = vertexPositions[vertexID] * updateZoneSize.xy;
				pos = CustomRenderTextureRotate2D(pos, rotation);
				pos.x += clipSpaceCenter.x;
			#if UNITY_UV_STARTS_AT_TOP
				pos.y += clipSpaceCenter.y;
			#else
				pos.y -= clipSpaceCenter.y;
			#endif

				// For 3D texture, cull quads outside of the update zone
				// This is neeeded in additional to the preliminary minSlice/maxSlice done on the CPU because update zones can be disjointed.
				// ie: slices [1..5] and [10..15] for two differents zones so we need to cull out slices 0 and [6..9]
				if (CustomRenderTextureIs3D > 0.0)
				{
					int minSlice = (int)(updateZoneCenter.z - updateZoneSize.z * 0.5);
					int maxSlice = minSlice + (int)updateZoneSize.z;
					if (_CustomRenderTexture3DSlice < minSlice || _CustomRenderTexture3DSlice >= maxSlice)
					{
						pos.xy = float2(1000.0, 1000.0); // Vertex outside of ncs
					}
				}

				OUT.vertex = float4(pos, 0.0, 1.0);
				OUT.primitiveID = asuint(CustomRenderTexturePrimitiveIDs[primitiveID]);
				OUT.localTexcoord = float3(texCoords[vertexID], CustomRenderTexture3DTexcoordW);
				OUT.globalTexcoord = float3(pos.xy * 0.5 + 0.5, CustomRenderTexture3DTexcoordW);
			#if UNITY_UV_STARTS_AT_TOP
				OUT.globalTexcoord.y = 1.0 - OUT.globalTexcoord.y;
			#endif
				OUT.direction = CustomRenderTextureComputeCubeDirection(OUT.globalTexcoord.xy);

				return OUT;
			}

            float4 frag(ase_v2f_customrendertexture IN ) : COLOR
            {
				float4 finalColor;
				float4 lerpResult136_g77191 = lerp( half4(0,1,1,0) , TVE_MotionParams , TVE_IsEnabled);
				half2 Global_WindDirection141_g77191 = (lerpResult136_g77191).xy;
				half2 Noise_Coord418_g77191 = IN.localTexcoord.xy;
				float2 temp_output_3_0_g77197 = Noise_Coord418_g77191;
				float2 temp_output_21_0_g77197 = (Global_WindDirection141_g77191*2.0 + -1.0);
				float lerpResult128_g77195 = lerp( _Time.y , ( ( _Time.y * TVE_TimeParams.x ) + TVE_TimeParams.y ) , TVE_TimeParams.w);
				half Noise_SpeedA144_g77191 = ( lerpResult128_g77195 * 0.02 * _MotionSpeedMinValue );
				float temp_output_15_0_g77197 = Noise_SpeedA144_g77191;
				float temp_output_23_0_g77197 = frac( temp_output_15_0_g77197 );
				float4 lerpResult39_g77197 = lerp( SAMPLE_TEXTURE2D( _MotionNoiseTex, sampler_Linear_Repeat, ( temp_output_3_0_g77197 + ( temp_output_21_0_g77197 * temp_output_23_0_g77197 ) ) ) , SAMPLE_TEXTURE2D( _MotionNoiseTex, sampler_Linear_Repeat, ( temp_output_3_0_g77197 + ( temp_output_21_0_g77197 * frac( ( temp_output_15_0_g77197 + 0.5 ) ) ) ) ) , ( abs( ( temp_output_23_0_g77197 - 0.5 ) ) / 0.5 ));
				half4 Noise_TexA152_g77191 = lerpResult39_g77197;
				float2 temp_output_3_0_g77200 = Noise_Coord418_g77191;
				float2 temp_output_21_0_g77200 = (Global_WindDirection141_g77191*2.0 + -1.0);
				float lerpResult128_g77194 = lerp( _Time.y , ( ( _Time.y * TVE_TimeParams.x ) + TVE_TimeParams.y ) , TVE_TimeParams.w);
				half Noise_SpeedB244_g77191 = ( lerpResult128_g77194 * 0.02 * _MotionSpeedMaxValue );
				float temp_output_15_0_g77200 = Noise_SpeedB244_g77191;
				float temp_output_23_0_g77200 = frac( temp_output_15_0_g77200 );
				float4 lerpResult39_g77200 = lerp( SAMPLE_TEXTURE2D( _MotionNoiseTex, sampler_Linear_Repeat, ( temp_output_3_0_g77200 + ( temp_output_21_0_g77200 * temp_output_23_0_g77200 ) ) ) , SAMPLE_TEXTURE2D( _MotionNoiseTex, sampler_Linear_Repeat, ( temp_output_3_0_g77200 + ( temp_output_21_0_g77200 * frac( ( temp_output_15_0_g77200 + 0.5 ) ) ) ) ) , ( abs( ( temp_output_23_0_g77200 - 0.5 ) ) / 0.5 ));
				half4 Noise_TexB245_g77191 = lerpResult39_g77200;
				half Global_WindIntensity153_g77191 = (lerpResult136_g77191).z;
				float2 lerpResult248_g77191 = lerp( (Noise_TexA152_g77191).rg , (Noise_TexB245_g77191).rg , Global_WindIntensity153_g77191);
				float lerpResult262_g77191 = lerp( 1.0 , _MotionNoiseValue , Global_WindIntensity153_g77191);
				float2 lerpResult166_g77191 = lerp( Global_WindDirection141_g77191 , lerpResult248_g77191 , lerpResult262_g77191);
				float2 lerpResult172_g77191 = lerp( float2( 0.5,0.5 ) , lerpResult166_g77191 , Global_WindIntensity153_g77191);
				half2 Wind_Direction192_g77191 = lerpResult172_g77191;
				float lerpResult320_g77191 = lerp( (Noise_TexA152_g77191).a , (Noise_TexB245_g77191).a , Global_WindIntensity153_g77191);
				half Wind_Noise322_g77191 = ( lerpResult320_g77191 * Global_WindIntensity153_g77191 );
				float4 appendResult188_g77191 = (float4(Wind_Direction192_g77191 , Wind_Noise322_g77191 , 0.0));
				
                finalColor = appendResult188_g77191;
				return finalColor;
            }
            ENDCG
		}
    }
	
	CustomEditor "TVEShaderGUIHelper"
	Fallback Off
}
/*ASEBEGIN
Version=19603
Node;AmplifyShaderEditor.FunctionNode;284;2432,2560;Inherit;False;Base Compile;-1;;76890;e67c8238031dbf04ab79a5d4d63d1b4f;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;330;2048,2560;Inherit;False;Property;_BANNER;[ BANNER ];0;0;Create;True;0;0;0;True;1;StyledBanner(CustomRT Motion);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;418;2048,2688;Inherit;False;Compute Global Wind;1;;77191;f4994c96c48c75e4ab5e1dd842e8376b;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;2432,2688;Float;False;True;-1;2;TVEShaderGUIHelper;0;2;BOXOPHOBIC/The Visual Engine/Effects/CustomRT Motion;32120270d1b3a8746af2aca8bc749736;True;Custom RT Update;0;0;Custom RT Update;1;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;0;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;True;0
WireConnection;0;0;418;0
ASEEND*/
//CHKSM=4FB21F9E33C5EF6EACFBD2A390C30397DAC65572