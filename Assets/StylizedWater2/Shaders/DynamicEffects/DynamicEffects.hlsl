#if !defined(SHADERGRAPH_PREVIEW)
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#endif

#include "../Libraries/URP.hlsl"
#include "../Libraries/Common.hlsl"
#include "Common.hlsl"

TEXTURE2D(_WaterDynamicEffectsBuffer);
float4 _WaterDynamicEffectsBuffer_TexelSize;
TEXTURE2D(_WaterDynamicEffectsNormals);

float4 _WaterDynamicEffectsCoords;
//XY: Bounds min position
//Z: Bounds scale
//W: Bool, rendering pass active
float4 _WaterDynamicEffectsParams;
//X: Enable normals
//Y: Enable displacement
//Z: Render range
//W: End fade percentage (1/value)

#define NORMALS_AVAILABLE _WaterDynamicEffectsParams.x > 0
#define GLOBAL_DISPLACEMENT_STRENGTH _WaterDynamicEffectsParams.y

#define DE_DISPLACEMENT_CHANNEL 0
#define DE_FOAM_CHANNEL 1
#define DE_NORMALS_CHANNEL 2
#define DE_ALPHA_CHANNEL 3

float BoundsEdgeMask(float2 position)
{
	const float blendDistance = (_WaterDynamicEffectsParams.z * 0.5) * _WaterDynamicEffectsParams.w;
	
	const float extents = (_WaterDynamicEffectsCoords.z * 0.5);
	
	position = position - extents;
	
	const float2 boundsMin = _WaterDynamicEffectsCoords.xy - extents;
	const float2 boundsMax = _WaterDynamicEffectsCoords.xy + extents;
	
	float2 weightDir = min(position - boundsMin, boundsMax - position) / blendDistance;
	
	return saturate(min(weightDir.x, weightDir.y));
}

//Shader Graph
void BoundsEdgeMask_float(float3 positionWS, out float mask)
{
	#if !defined(SHADERGRAPH_PREVIEW)
	mask = BoundsEdgeMask(positionWS.xz);
	#else
	mask = 1.0;
#endif
}

float2 DynamicEffectsSampleCoords(float3 positionWS)
{
	return (positionWS.xz - _WaterDynamicEffectsCoords.xy) / (_WaterDynamicEffectsCoords.z);
}

//Account for the SampleDynamicEffectsData being called in a vertex or tessellation shader
#if defined(SHADER_STAGE_VERTEX) || defined(SHADER_STAGE_DOMAIN) || defined(SHADER_STAGE_HULL)
#define SAMPLE_FUNC(texName, sampler, uv) SAMPLE_TEXTURE2D_LOD(texName, sampler, uv, 0)
#else
#define SAMPLE_FUNC(texName, sampler, uv) SAMPLE_TEXTURE2D(texName, sampler, uv)
#endif

float4 SampleDynamicEffectsData(float3 positionWS)
{
	float4 data = 0;
	
	if(_WaterDynamicEffectsCoords.w > 0)
	{
		data = SAMPLE_FUNC(_WaterDynamicEffectsBuffer, sampler_LinearClamp, DynamicEffectsSampleCoords(positionWS));

		if(_WaterDynamicEffectsHighPrecision == false)
		{
			data[DE_DISPLACEMENT_CHANNEL] = UnpackChannel(data[DE_DISPLACEMENT_CHANNEL]);
			//data[DE_FOAM_CHANNEL] = UnpackChannel(data[DE_FOAM_CHANNEL]);
			//data[DE_NORMALS_CHANNEL] = UnpackChannel(data[DE_NORMALS_CHANNEL]);
		}
		
		data[DE_DISPLACEMENT_CHANNEL] *= GLOBAL_DISPLACEMENT_STRENGTH;
		
		const half mask = BoundsEdgeMask(positionWS.xz);
		data *= mask;
		
		data[DE_ALPHA_CHANNEL] = mask;
	}
	
	return data;
}

//Shorthand for only sampling displacement
float SampleDynamicEffectsDisplacement(float3 positionWS)
{
	return SampleDynamicEffectsData(positionWS).r;
}

//Shader Graph
void SampleDynamicEffectsData_float(float3 positionWS, out float4 data)
{
	#if !defined(SHADERGRAPH_PREVIEW)
	data = SampleDynamicEffectsData(positionWS);
	#else
	data = 0;
	#endif
}

float4 SampleDynamicEffectsNormals(float3 positionWS)
{
	float4 normals = float4(0, 1, 0.0, 0.0);
	
	if(_WaterDynamicEffectsCoords.w > 0 && NORMALS_AVAILABLE)
	{
		normals = SAMPLE_TEXTURE2D(_WaterDynamicEffectsNormals, sampler_LinearClamp, DynamicEffectsSampleCoords(positionWS));
		
		normals.xz = normals.xy * 2.0 - 1.0;
		normals.y = max(1.0e-16, sqrt(1.0 - saturate(dot(normals.xz, normals.xz))));
	}
	
	return normals;
}

#include "../Libraries/Foam.hlsl"
TEXTURE2D(_FoamTexDynamic);

float SampleDynamicFoam(float2 uv, float tiling, float subTiling, float2 time, float speed, float subSpeed)
{
	return SampleFoamTexture(TEXTURE2D_ARGS(_FoamTexDynamic, sampler_FoamTex), uv, tiling, subTiling, time, speed, subSpeed, 0.0, 0.0, 0.0, false);
}