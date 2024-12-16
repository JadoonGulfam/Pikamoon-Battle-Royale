#include "Common.hlsl"

sampler2D _BaseMap;
sampler2D _MaskMap;
			
UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
	UNITY_DEFINE_INSTANCED_PROP(float4, _BaseMap_ST)
	UNITY_DEFINE_INSTANCED_PROP(float4, _MaskMap_ST)
	UNITY_DEFINE_INSTANCED_PROP(float2, _AnimationSpeed)
	UNITY_DEFINE_INSTANCED_PROP(float2, _MaskAnimationSpeed)
	UNITY_DEFINE_INSTANCED_PROP(float, _HeightScale)
	UNITY_DEFINE_INSTANCED_PROP(float, _FoamStrength)
	UNITY_DEFINE_INSTANCED_PROP(float, _NormalStrength)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

#define _BaseMap_ST UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseMap_ST)
#define _MaskMap_ST UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _MaskMap_ST)
#define HeightScale UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _HeightScale)
#define FoamStrength UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _FoamStrength)
#define NormalStrength UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _NormalStrength)
#define AnimationSpeed UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _AnimationSpeed)
#define MaskAnimationSpeed UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _MaskAnimationSpeed)

struct EffectOutput
{
	float displacement;
	float foamAmount;
	float normalGradient;
	float alpha;
};

float4 OutputEffect(float height, float foam, float normalHeight, float alpha)
{
	if(_WaterDynamicEffectsHighPrecision == false)
	{
		OutputChannel(height);
		//OutputChannel(foam);
		OutputChannel(normalHeight);
	}
	
	return float4(height, foam, normalHeight, alpha);

}
#define OUTPUT_EFFECT(struct) return OutputEffect(struct.displacement, struct.foamAmount, struct.normalGradient, struct.alpha)