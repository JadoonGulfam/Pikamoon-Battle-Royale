Shader "Hidden/KriptoFX/KWS/VolumetricLighting"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" { }
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.5

			#pragma multi_compile _ USE_CAUSTIC USE_ADDITIONAL_CAUSTIC
			#pragma multi_compile _ SUPPORT_LOCAL_LIGHTS

			#define LIGHT_EVALUATION_NO_CONTACT_SHADOWS // To define before LightEvaluation.hlsl
			//#define LIGHT_EVALUATION_NO_HEIGHT_FOG

			/*        #ifndef LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
			            #define USE_BIG_TILE_LIGHTLIST
			#endif*/


			#define MAX_VOLUMETRIC_LIGHT_ITERATIONS 8
			#define PREFER_HALF 0
			#define SHADOW_USE_DEPTH_BIAS   0 // Too expensive, not particularly effective
			#define SHADOW_LOW          // Different options are too expensive.
			#define AREA_SHADOW_LOW
			#define SHADOW_AUTO_FLIP_NORMAL 0 // No normal information, so no need to flip
			#define SHADOW_VIEW_BIAS        1 // Prevents light leaking through thin geometry. Not as good as normal bias at grazing angles, but cheaper and independent from the geometry.
			
			
			#define USE_CLUSTERED_LIGHTLIST
			//#define USE_FPTL_LIGHTLIST
			#define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
			

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/VolumeRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl"


			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Builtin/BuiltinData.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"

			// #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/VolumetricLighting/VolumetricLighting.cs.hlsl"
			// #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/VolumetricLighting/VBuffer.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Sky/PhysicallyBasedSky/PhysicallyBasedSkyCommon.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightEvaluation.hlsl"

			#include "../PlatformSpecific/Includes/KWS_HelpersIncludes.cginc"
			#include "../Common/CommandPass/KWS_VolumetricLight_Common.cginc"

			//#if PRE_FILTER_LIGHT_LIST
			//
			//#if MAX_NR_BIG_TILE_LIGHTS_PLUS_ONE > 48
			//#define MAX_SUPPORTED_LIGHTS 48
			//#else
			//#define MAX_SUPPORTED_LIGHTS MAX_NR_BIG_TILE_LIGHTS_PLUS_ONE
			//#endif
			//
			//int gs_localLightList[GROUP_SIZE_1D * GROUP_SIZE_1D][MAX_SUPPORTED_LIGHTS];
			//
			//#endif

			inline void IntegrateAdditionalLight(RaymarchData raymarchData, inout float3 scattering, inout float transmittance, float atten, float3 lightPos, float3 step, inout float3 currentPos)
			{
				float3 posToLight = normalize(currentPos - lightPos.xyz);
				
				#if defined(USE_ADDITIONAL_CAUSTIC)
					if (lightPos.y > raymarchData.waterHeight)
					{
						atten += atten * RaymarchCaustic(raymarchData, currentPos, posToLight);
					}
				#endif
				
				IntegrateLightSlice(scattering, transmittance, atten, raymarchData);
				currentPos += step;
			}

			void RayMarchDirLight(RaymarchData raymarchData, inout RaymarchResult result)
			{
				result.DirLightScattering = 0;
				result.DirLightSurfaceShadow = 1;
				result.DirLightSceneShadow = 1;

				float exposure = GetExposure();
				float3 finalScattering = 0;
				
				float3 reflectedStep = reflect(raymarchData.rayDir, float3(0, -1, 0)) * (raymarchData.rayLength / KWS_RayMarchSteps);

				for (uint lightIdx = 0; lightIdx < _DirectionalLightCount; ++lightIdx)
				{
					DirectionalLightData light = _DirectionalLightDatas[lightIdx];
					float3 L = -light.forward;
					float sunAngleAttenuation = GetVolumeLightSunAngleAttenuation(L);

					float transmittance = 1;
					float3 currentPos = raymarchData.currentPos;

					LightLoopContext context;
					context.shadowContext = InitShadowContext();
					PositionInputs posInput;
					posInput.positionWS = GetCameraRelativePositionWS(currentPos);
					
					float4 lightColor = EvaluateLight_Directional(context, posInput, light);
					lightColor.a *= light.volumetricLightDimmer;
					lightColor.rgb = saturate(lightColor.rgb * exposure * lightColor.a); // Composite
					
					finalScattering = GetAmbientColor(GetExposure()) * 0.5;
					finalScattering *= GetVolumeLightInDepthTransmitance(raymarchData.waterHeight, currentPos.y, raymarchData.transparent, raymarchData.waterID);
					finalScattering *= sunAngleAttenuation;

					float3 step = raymarchData.step;
					
					float3 color; float attenuation;
					if (light.volumetricLightDimmer > 0)
					{
						for (uint j = 0; j < MAX_VOLUMETRIC_LIGHT_ITERATIONS; ++j)
						{
							if (j >= KWS_RayMarchSteps) break;
							if (length(currentPos - raymarchData.rayStart) > raymarchData.rayLengthToSceneZ) break;
							if (length(currentPos - raymarchData.rayStart) > raymarchData.rayLengthToWaterZ) step = reflectedStep;

							posInput.positionWS = GetCameraRelativePositionWS(currentPos);
							
							float atten = 1;
							if (_DirectionalShadowIndex >= 0 && _DirectionalShadowIndex == lightIdx && (light.volumetricLightDimmer > 0) && (light.volumetricShadowDimmer > 0))
							{
								atten = GetDirectionalShadowAttenuation(context.shadowContext, raymarchData.uv, GetCameraRelativePositionWS(currentPos), 0, light.shadowIndex, L);
								atten = lerp(1, atten, light.volumetricShadowDimmer);
							}
							//lightColor.rgb *= ComputeShadowColor(atten, light.shadowTint, light.penumbraTint);
							
							#if defined(USE_CAUSTIC) || defined(USE_ADDITIONAL_CAUSTIC)
								atten += atten * RaymarchCaustic(raymarchData, currentPos, light.forward);
							#endif
							atten *= sunAngleAttenuation;
							atten *= GetVolumeLightInDepthTransmitance(raymarchData.waterHeight, currentPos.y, raymarchData.transparent, raymarchData.waterID);
							
							IntegrateLightSlice(finalScattering, transmittance, atten, raymarchData);
							currentPos += step;
						}

						
						result.DirLightScattering += finalScattering * lightColor.rgb * raymarchData.tubidityColor;
					}
					
					if (_DirectionalShadowIndex >= 0 && _DirectionalShadowIndex == lightIdx)
					{
						result.DirLightSurfaceShadow = GetDirectionalShadowAttenuation(context.shadowContext, raymarchData.uv, GetCameraRelativePositionWS(raymarchData.rayStart), 0, light.shadowIndex, L);
						#if defined(USE_CAUSTIC) || defined(USE_ADDITIONAL_CAUSTIC)
							result.DirLightSceneShadow = GetDirectionalShadowAttenuation(context.shadowContext, raymarchData.uv, GetCameraRelativePositionWS(raymarchData.rayEnd), 0, light.shadowIndex, L);
						#endif
					}
				}
			}



			void RayMarchAdditionalLights(RaymarchData raymarchData, inout RaymarchResult result)
			{
				result.AdditionalLightsScattering = 0;
				result.AdditionalLightsSceneAttenuation = 0;

				if (LIGHTFEATUREFLAGS_PUNCTUAL)
				{
					uint lightCount, lightStart;
					LightLoopContext context;
					context.shadowContext = InitShadowContext();
					
					float exposure = GetExposure();
					float3 reflectedStep = reflect(raymarchData.rayDir, float3(0, -1, 0)) * (raymarchData.rayLength / KWS_RayMarchSteps);

					#ifndef LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
						PositionInputs posInput;
						posInput.tileCoord = (float2) (raymarchData.uv * _ScreenSize.xy) / GetTileSize();
						posInput.linearDepth = LinearEyeDepth(raymarchData.waterVolumeDepth.x);
						GetCountAndStart(posInput, LIGHTCATEGORY_PUNCTUAL, lightStart, lightCount);
					#else
						lightCount = _PunctualLightCount;
						lightStart = 0;
					#endif

					uint startFirstLane = 0;
					bool fastPath;

					fastPath = IsFastPath(lightStart, startFirstLane);
					if (fastPath)
					{
						lightStart = startFirstLane;
					}

					uint v_lightIdx = lightStart;
					uint v_lightListOffset = 0;
					while(v_lightListOffset < lightCount)
					{
						v_lightIdx = FetchIndex(lightStart, v_lightListOffset);
						uint s_lightIdx = ScalarizeElementIndex(v_lightIdx, fastPath);
						if (s_lightIdx == -1)
							break;

						float3 step = raymarchData.step;
						LightData light = FetchLight(s_lightIdx);
						if (s_lightIdx >= v_lightIdx)
						{
							v_lightListOffset++;
							float3 currentPos = raymarchData.currentPos;
							float3 L;
							float4 distances; // {d, d^2, 1/d, d_proj}
							
							float3 scattering = 0;
							float transmittance = 1;
							
							float4 lightColor = float4(light.color, 1.0);
							lightColor.a *= light.volumetricLightDimmer;
							lightColor.rgb *= lightColor.a * exposure;

							GetPunctualLightVectors(GetCameraRelativePositionWS(raymarchData.rayEnd), light, L, distances);
							float surfaceAtten = PunctualLightAttenuation(distances, light.rangeAttenuationScale, light.rangeAttenuationBias, light.angleScale, light.angleOffset);
							surfaceAtten *= light.volumetricLightDimmer * exposure;
							result.AdditionalLightsSceneAttenuation = max(result.AdditionalLightsSceneAttenuation, saturate(surfaceAtten * 100));
							
							
							UNITY_LOOP
							for (uint i = 0; i < KWS_RayMarchSteps; ++i)
							{
								if (length(currentPos - raymarchData.rayStart) > raymarchData.rayLengthToSceneZ) break;
								if (length(currentPos - raymarchData.rayStart) > raymarchData.rayLengthToWaterZ) step = reflectedStep;
								
								GetPunctualLightVectors(GetCameraRelativePositionWS(currentPos), light, L, distances);

								float atten = PunctualLightAttenuation(distances, light.rangeAttenuationScale, light.rangeAttenuationBias, light.angleScale, light.angleOffset);
								atten *= GetPunctualShadowAttenuation(context.shadowContext, raymarchData.uv, GetCameraRelativePositionWS(currentPos), 0, light.shadowIndex, L, distances.x, light.lightType == GPULIGHTTYPE_POINT, light.lightType != GPULIGHTTYPE_PROJECTOR_BOX);

								IntegrateAdditionalLight(raymarchData, scattering, transmittance, atten, light.positionRWS, step, currentPos);
							}
							result.AdditionalLightsScattering += scattering * lightColor.rgb * raymarchData.tubidityColor;
						}
					}
				}
			}


			
			//void RayMarchAdditionalLights(RaymarchData raymarchData, inout RaymarchResult result)
			//{
			//	result.AdditionalLightsScattering = 0;
			//	result.AdditionalLightsSceneAttenuation = 0;
			//	//float debugLightCount = 0;

			//	if (LIGHTFEATUREFLAGS_PUNCTUAL)
			//	{
			//		uint lightCount, lightStart;
			//		LightLoopContext context;
			//		context.shadowContext = InitShadowContext();
			//		//PositionInputs posInput;
			//		float exposure = GetExposure();
			//		float3 currentPos = raymarchData.currentPos;

			//		UNITY_LOOP
			//		for (uint i = 0; i < KWS_RayMarchSteps; ++i)
			//		{
			//			if (length(currentPos - raymarchData.rayStart) > raymarchData.rayLengthToSceneZ) break;
			

			//			#ifndef LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
			//				PositionInputs posInput;
			//				posInput.tileCoord = (float2) (raymarchData.uv * _ScreenSize.xy) / GetTileSize();
			//				//posInput.linearDepth = LinearEyeDepth(raymarchData.waterVolumeDepth.x);
			//				float3 pos = raymarchData.rayStart + raymarchData.rayDir * raymarchData.rayLength * ((float)i / KWS_RayMarchSteps);
			//				posInput.linearDepth = -TransformWorldToView(GetCameraRelativePositionWS(pos)).z;
			//				GetCountAndStart(posInput, LIGHTCATEGORY_PUNCTUAL, lightStart, lightCount);
			//			#else
			//				lightCount = _PunctualLightCount;
			//				lightStart = 0;
			//			#endif

			
			//			uint v_lightIdx = lightStart;
			//			uint v_lightListOffset = 0;

			//			while(v_lightListOffset < lightCount)
			//			{
			//				v_lightIdx = FetchIndex(lightStart, v_lightListOffset);
			//				uint s_lightIdx = v_lightIdx;
			
			//				if (s_lightIdx == -1)
			//					break;

			//				LightData light = FetchLight(s_lightIdx);
			//				if (s_lightIdx >= v_lightIdx)
			//				{
			//					//debugLightCount++;
			//					v_lightListOffset++;
			
			//					float3 L;
			//					float4 distances; // {d, d^2, 1/d, d_proj}
			
			//					float3 scattering = 0;
			//					float transmittance = 1;
			
			//					float4 lightColor = float4(light.color, 1.0);
			//					lightColor.a *= light.volumetricLightDimmer;
			//					lightColor.rgb *= lightColor.a * exposure;

			//					GetPunctualLightVectors(GetCameraRelativePositionWS(currentPos), light, L, distances);
			//					//GetPunctualLightVectors(GetCameraRelativePositionWS(raymarchData.rayEnd), light, L, distances);
			//					float surfaceAtten = PunctualLightAttenuation(distances, light.rangeAttenuationScale, light.rangeAttenuationBias, light.angleScale, light.angleOffset);
			//					surfaceAtten *= light.volumetricLightDimmer * exposure;
			//					result.AdditionalLightsSceneAttenuation = max(result.AdditionalLightsSceneAttenuation, saturate(surfaceAtten * 100));
			

			//					float atten = PunctualLightAttenuation(distances, light.rangeAttenuationScale, light.rangeAttenuationBias, light.angleScale, light.angleOffset);
			//					atten *= GetPunctualShadowAttenuation(context.shadowContext, raymarchData.uv, GetCameraRelativePositionWS(currentPos), 0, light.shadowIndex, L, distances.x, light.lightType == GPULIGHTTYPE_POINT, light.lightType != GPULIGHTTYPE_PROJECTOR_BOX);

			//					IntegrateAdditionalLight(raymarchData, scattering, transmittance, atten, light.positionRWS, currentPos);
			
			//					result.AdditionalLightsScattering += scattering * lightColor.rgb * raymarchData.tubidityColor;
			//				}
			//			}
			//		}
			//	}
			//}


			void frag(vertexOutput i, out half3 volumeLightColor : SV_Target0, out half3 additionalData : SV_Target1)
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				float waterMask = GetWaterMask(i.uv);
				if (waterMask == 0) discard;

				RaymarchData raymarchData = InitRaymarchData(i, waterMask);
				RaymarchResult raymarchResult = (RaymarchResult)0;

				RayMarchDirLight(raymarchData, raymarchResult);
				RayMarchAdditionalLights(raymarchData, raymarchResult);
				
				volumeLightColor = raymarchResult.DirLightScattering + raymarchResult.AdditionalLightsScattering;
				additionalData = float3(raymarchResult.DirLightSurfaceShadow, raymarchResult.DirLightSceneShadow, raymarchResult.AdditionalLightsSceneAttenuation);

				AddTemporalAccumulation(raymarchData.rayEnd, volumeLightColor);
			}

			ENDHLSL
		}
	}
}